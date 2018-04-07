using System;
using System.Windows.Forms;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using log4net;
using System.Linq;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;
using System.Collections;

namespace ROILootManager
{
  static class GDriveManager
  {
    private static ILog logger = LogManager.GetLogger(typeof(GDriveManager));
    private static OAuth2Parameters logonParameters;

    public static void initialize()
    {
      try
      {
        // OAuth2Parameters holds all the parameters related to OAuth 2.0.
        logonParameters = new OAuth2Parameters();

        // Set your OAuth 2.0 Client Id (which you can register at
        // https://code.google.com/apis/console).
        logonParameters.ClientId = Constants.CLIENT_ID;

        // Set your OAuth 2.0 Client Secret, which can be obtained at
        // https://code.google.com/apis/console.
        logonParameters.ClientSecret = Constants.CLIENT_SECRET;

        // Set your Redirect URI, which can be registered at
        // https://code.google.com/apis/console.
        logonParameters.RedirectUri = Constants.REDIRECT_URI;

        // Set the scope for this particular service.
        logonParameters.Scope = Constants.SCOPE;

        // Get the authorization code
        logonParameters.AccessCode = frmAuthCode.getAuthCode(logonParameters);

        OAuthUtil.GetAccessToken(logonParameters);
        logger.Debug("OAuth Access Token: " + logonParameters.AccessToken);
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
      if (logonParameters.TokenExpiry > DateTime.Now)
      {
        OAuthUtil.RefreshAccessToken(logonParameters);
      }

      return logonParameters.AccessToken;
    }
  }
}
