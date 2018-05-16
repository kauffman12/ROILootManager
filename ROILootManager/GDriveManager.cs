using System;
using System.Windows.Forms;
using log4net;
using System.Linq;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ROILootManager
{
  static class GDriveManager
  {
    private static ILog logger = LogManager.GetLogger(typeof(GDriveManager));
    private static Dictionary<string, string> tokens;
    private static DateTime expireTime;

    public static void initialize()
    {
      try
      {
        // init expire time
        expireTime = DateTime.Now;

        // wait for token to be received
        //tokenEndpointDecoded["access_token"];
        //tokenEndpointDecoded["refresh_token"];
        //tokenEndpointDecoded["expires_in"]; 
        tokens = getAccessCodeAsync().GetAwaiter().GetResult();

        // update expire time based on token
        expireTime = expireTime.AddSeconds(Double.Parse(tokens["expires_in"]));
      }
      catch (Exception e)
      {
        MessageBox.Show("Could not Authenticate - shutting down.");
        logger.Error("Could not Authenticate", e);
        System.Environment.Exit(-1);
      }
    }


    public static IDictionary<string, int> getHeaderMap(IList<IList<Object>> values)
    {
      IList<Object> headers = values.Take(1).ElementAt(0);
      IDictionary<string, int> headerMap = new Dictionary<string, int>();

      for (int i = 0; i < headers.Count; i++)
      {
        headerMap.Add(headers[i].ToString().ToLower(), i);
      }

      return headerMap;
    }

    public static void appendSpreadsheet(string docId, string sheetName, IList<Object> values)
    {
      ValueRange valueRange = new ValueRange();
      valueRange.MajorDimension = "ROWS";
      valueRange.Values = new List<IList<Object>> { values };

      SheetsService sheetsSvc = new SheetsService();
      SpreadsheetsResource.ValuesResource.AppendRequest request =
        sheetsSvc.Spreadsheets.Values.Append(valueRange, docId, sheetName);

      request.OauthToken = getAccessToken();
      request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
      request.Execute();
    }

    public static ValueRange readSpreadsheet(string docId, string sheetName)
    {
      SheetsService sheetsSvc = new SheetsService();
      SpreadsheetsResource.ValuesResource.GetRequest request =
              sheetsSvc.Spreadsheets.Values.Get(docId, sheetName);
      request.OauthToken = getAccessToken();
      return request.Execute();
    }

    public static string readCell(IList<Object> row, int cell)
    {
      string result = "";
      if (row.Count > cell)
      {
        result = row[cell].ToString().Trim();
      }

      return result;
    }

    private static string getAccessToken()
    {
      if (DateTime.Now > expireTime)
      {
        expireTime = DateTime.Now;

        Dictionary<string, string> result = RefreshTokens().GetAwaiter().GetResult();
        tokens["access_token"] = result["access_token"];
        tokens["expires_in"] = result["expires_in"];

        expireTime = expireTime.AddSeconds(Double.Parse(tokens["expires_in"]));
      }

      return tokens["access_token"];
    }

    private static async Task<Dictionary<string, string>> getAccessCodeAsync()
    {
      // Generates state and PKCE values.
      string state = randomDataBase64url(32);
      string code_verifier = randomDataBase64url(32);
      string code_challenge = base64urlencodeNoPadding(sha256(code_verifier));
      const string code_challenge_method = "S256";

      // Creates a redirect URI using an available port on the loopback address.
      string redirectURI = string.Format("http://{0}:{1}/", IPAddress.Loopback, 4567);

      // Creates the OAuth 2.0 authorization request.
      string authorizationRequest = string.Format("{0}?response_type=code&scope={1}&redirect_uri={2}&client_id={3}&state={4}&code_challenge={5}&code_challenge_method={6}",
          Constants.AUTH_ENDPOINT,
          System.Uri.EscapeDataString(Constants.SCOPE),
          System.Uri.EscapeDataString(redirectURI),
          Constants.CLIENT_ID,
          state,
          code_challenge,
          code_challenge_method);

      // Opens request in the browser.
      System.Diagnostics.Process.Start(authorizationRequest);

      // Creates an HttpListener to listen for requests on that redirect URI.
      var http = new HttpListener();
      http.Prefixes.Add(redirectURI);
      http.Start();

      // Waits for the OAuth authorization response.
      var context = await http.GetContextAsync();

      // Sends an HTTP response to the browser.
      var response = context.Response;
      string responseString = string.Format("<html><head></head><body>Authentication Successful. You may close the browser.</body></html>");
      var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
      response.ContentLength64 = buffer.Length;
      var responseOutput = response.OutputStream;
      Task responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
      {
        responseOutput.Close();
        http.Stop();
        Console.WriteLine("HTTP server stopped.");
      });

      // Checks for errors.
      if (context.Request.QueryString.Get("error") != null)
      {
        throw new Exception(context.Request.QueryString.Get("error"));
      }
      if (context.Request.QueryString.Get("code") == null
          || context.Request.QueryString.Get("state") == null)
      {
        throw new Exception("Malformed authorization response. " + context.Request.QueryString);
      }

      // extracts the code
      var code = context.Request.QueryString.Get("code");
      var incoming_state = context.Request.QueryString.Get("state");

      // Compares the receieved state to the expected value, to ensure that
      // this app made the request which resulted in authorization.
      if (incoming_state != state)
      {
        throw new Exception(String.Format("Received request with invalid state ({0})", incoming_state));
      }

      return PerformCodeExchange(code, code_verifier, redirectURI).Result;
    }

    private static async Task<Dictionary<string, string>> PerformCodeExchange(string code, string code_verifier, string redirectURI)
    {
      Dictionary<string, string> result = null;

      // builds the  request
      string tokenRequestBody = string.Format("code={0}&redirect_uri={1}&client_id={2}&code_verifier={3}&client_secret={4}&scope=&grant_type=authorization_code",
          code,
          System.Uri.EscapeDataString(redirectURI),
          Constants.CLIENT_ID,
          code_verifier,
          Constants.CLIENT_SECRET
          );

      // sends the request
      HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(Constants.TOKEN_REQUEST_URI);
      tokenRequest.Method = "POST";
      tokenRequest.ContentType = "application/x-www-form-urlencoded";
      tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
      byte[] _byteVersion = System.Text.Encoding.ASCII.GetBytes(tokenRequestBody);
      tokenRequest.ContentLength = _byteVersion.Length;
      Stream stream = tokenRequest.GetRequestStream();
      await stream.WriteAsync(_byteVersion, 0, _byteVersion.Length);
      stream.Close();

      // gets the response
      WebResponse tokenResponse = await tokenRequest.GetResponseAsync();
      using (System.IO.StreamReader reader = new System.IO.StreamReader(tokenResponse.GetResponseStream()))
      {
        // reads response body
        string responseText = await reader.ReadToEndAsync();

        // converts to dictionary
        result = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText);
      }

      return result;
    }

    private static async Task<Dictionary<string, string>> RefreshTokens()
    {
      Dictionary<string, string> result = null;

      // builds the  request
      string refreshRequestBody = string.Format("client_id={0}&client_secret={1}&refresh_token={2}&grant_type=refresh_token",
          Constants.CLIENT_ID,
          Constants.CLIENT_SECRET,
          tokens["refresh_token"]
      );

      // sends the request
      HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(Constants.TOKEN_REQUEST_URI);
      tokenRequest.Method = "POST";
      tokenRequest.ContentType = "application/x-www-form-urlencoded";
      tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
      byte[] _byteVersion = System.Text.Encoding.ASCII.GetBytes(refreshRequestBody);
      tokenRequest.ContentLength = _byteVersion.Length;
      Stream stream = tokenRequest.GetRequestStream();
      await stream.WriteAsync(_byteVersion, 0, _byteVersion.Length);
      stream.Close();

      // gets the response
      WebResponse tokenResponse = await tokenRequest.GetResponseAsync();
      using (System.IO.StreamReader reader = new System.IO.StreamReader(tokenResponse.GetResponseStream()))
      {
        // reads response body
        string responseText = await reader.ReadToEndAsync();

        // converts to dictionary
        result = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText);
      }

      return result;
    }

    // Not currently using because of permissions
    // Hopefully they can just run netsh http add urlacl url="http://+:4567/" user=everyone
    private static int GetRandomUnusedPort()
    {
      var listener = new TcpListener(IPAddress.Loopback, 0);
      listener.Start();
      var port = ((IPEndPoint)listener.LocalEndpoint).Port;
      listener.Stop();
      return port;
    }

    private static string randomDataBase64url(uint length)
    {
      RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
      byte[] bytes = new byte[length];
      rng.GetBytes(bytes);
      return base64urlencodeNoPadding(bytes);
    }

    private static byte[] sha256(string inputStirng)
    {
      byte[] bytes = System.Text.Encoding.ASCII.GetBytes(inputStirng);
      SHA256Managed sha256 = new SHA256Managed();
      return sha256.ComputeHash(bytes);
    }

    private static string base64urlencodeNoPadding(byte[] buffer)
    {
      string base64 = Convert.ToBase64String(buffer);

      // Converts base64 to base64url.
      base64 = base64.Replace("+", "-");
      base64 = base64.Replace("/", "_");
      // Strips padding.
      base64 = base64.Replace("=", "");

      return base64;
    }
  }
}
