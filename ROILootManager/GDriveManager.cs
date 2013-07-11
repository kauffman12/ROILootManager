using System;
using System.Windows.Forms;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using log4net;
using System.Linq;


namespace ROILootManager {
    static class GDriveManager {
        private static ILog logger = LogManager.GetLogger(typeof(GDriveManager));

        private static SpreadsheetsService service;
        private static SpreadsheetFeed feed;
        private static bool isInitialized = false;
        private static OAuth2Parameters logonParameters;

        public static void initialize() {
            try {
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

                string accessToken = logonParameters.AccessToken;
                logger.Debug("OAuth Access Token: " + accessToken);

                // Initialize the variables needed to make the request
                GOAuth2RequestFactory requestFactory =
                    new GOAuth2RequestFactory(null, Constants.APPLICATION_NAME, logonParameters);
                service = new SpreadsheetsService(Constants.APPLICATION_NAME);
                service.RequestFactory = requestFactory;



                // Instantiate a SpreadsheetQuery object to retrieve spreadsheets.
                SpreadsheetQuery query = new SpreadsheetQuery();

                // Make a request to the API and get all spreadsheets.
                feed = service.Query(query);


                // Iterate through all of the spreadsheets returned
                foreach (SpreadsheetEntry entry in feed.Entries) {
                    // Print the title of this spreadsheet to the screen
                    logger.Debug(String.Format("Title: {0}, URI: {1}", entry.Title.Text, entry.Id.Uri.ToString()));
                }

                isInitialized = true;
            } catch (Exception e) {
                MessageBox.Show("Could not Authenticate - shutting down.");
                logger.Error("Could not Authenticate", e);
                System.Environment.Exit(-1);
            }
        }

        public static SpreadsheetsService getService() {
            logger.Debug(String.Format("Token expiring in {0}...", logonParameters.TokenExpiry - DateTime.Now));
            if (logonParameters.TokenExpiry > DateTime.Now) {
                OAuthUtil.RefreshAccessToken(logonParameters);
            }
            return service;
        }

        public static SpreadsheetEntry getSpreadsheet(string uri) {
            if (!isInitialized)
                initialize();

            SpreadsheetFeed feed = getService().Query(new SpreadsheetQuery(uri));

            if (feed.Entries.Count > 0) {
                return (SpreadsheetEntry)feed.Entries[0];
            }

            return null;
        }

        public static ListFeed getListFeed(SpreadsheetEntry spreadsheet, string worksheetName) {
            return GDriveManager.getListFeedQuery(spreadsheet, worksheetName, "");
        }

        public static ListFeed getListFeedQuery(SpreadsheetEntry spreadsheet, string worksheetName, string query) {
            WorksheetFeed wsFeed = spreadsheet.Worksheets;

            WorksheetEntry worksheet = (WorksheetEntry)wsFeed.Entries.ToList().Find((AtomEntry e) => e.Title.Text.Equals(worksheetName, StringComparison.InvariantCultureIgnoreCase));

            // Define the URL to request the list feed of the worksheet.
            AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            // Fetch the list feed of the worksheet.
            ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());

            if (query != null && !"".Equals(query)) {
                listQuery.SpreadsheetQuery = query;
            }


            return GDriveManager.getService().Query(listQuery);
        }

    }
}
