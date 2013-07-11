using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text.RegularExpressions;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using log4net;

namespace ROILootManager {
    class Roster {
        private static ILog logger = LogManager.GetLogger(typeof(Roster));

        private SpreadsheetEntry spreadsheet;

        List<RosterEntry> rosterArr = new List<RosterEntry>();

        public const string NAME_COL = "name";
        public const string CLASS_COL = "class";
        public const string RANK_COL = "rank";
        public const string ACTIVE_COL = "active";

        public Roster() {
            spreadsheet = GDriveManager.getSpreadsheet(Constants.ROSTER_URI);
            logger.Info("Spreadsheet " + spreadsheet.Title.Text + " loaded.");

            loadRoster();
        }

        public void loadRoster() {
            // Clear the current roster
            rosterArr.Clear();

            // Get the first worksheet of the first spreadsheet.
            // TODO: Choose a worksheet more intelligently based on your
            // app's needs.
            WorksheetFeed wsFeed = spreadsheet.Worksheets;
            WorksheetEntry worksheet = (WorksheetEntry)wsFeed.Entries[0];

            // Define the URL to request the list feed of the worksheet.
            AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            // Fetch the list feed of the worksheet.
            ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());
            //listQuery.SpreadsheetQuery = ACTIVE_COL + " = 'Yes'";
            listQuery.OrderByColumn = NAME_COL;



            ListFeed listFeed = GDriveManager.getService().Query(listQuery);

            List<BulkLoader> rows = new List<BulkLoader>();

            // Iterate through each row, printing its cell values.
            foreach (ListEntry row in listFeed.Entries) {

                // Print the first column's cell value
                //logger.Debug(row.Title.Text);
                RosterEntry r = new RosterEntry();
                // Iterate over the remaining columns, and print each cell value
                foreach (ListEntry.Custom element in row.Elements) {
                    //logger.Debug(element.Value);
                    switch (element.LocalName.ToLower()) {
                        case NAME_COL:
                            r.name = element.Value;
                            break;
                        case CLASS_COL:
                            r.classType = element.Value;
                            break;

                        case RANK_COL:
                            r.rank = element.Value;
                            break;

                        case ACTIVE_COL:
                            r.active = element.Value;
                            break;
                    }
                }

                rows.Add(r);
                rosterArr.Add(r);
            }

            DBManager.getManager().bulkInsert(rows, "roster");

            logger.Info("Roster loaded successfully. " + rosterArr.Count + " entries.");
        }

        public List<RosterEntry> getActiveNames() {
            return rosterArr.FindAll(new Predicate<RosterEntry>(RosterEntry.isActive));
        }

        public void loadAttendance() {
            DbDataReader rs = DBManager.getManager().executeQuery("SELECT name FROM roster WHERE active = 'Yes'");
            System.Net.WebClient client = new System.Net.WebClient();
            Regex rx = new Regex("<span class=\".+\">([0-9]+)% of raids</span>");
            List<BulkLoader> attend = new List<BulkLoader>();
            while (rs.Read()) {
                RosterAttendence r = new RosterAttendence();
                r.name = rs[0].ToString();

                string content = client.DownloadString(String.Format("http://roiguild.org/dkp/viewmember.php?name={0}", rs[0].ToString()));
                Match m = rx.Match(content);
                int count = 0;
                while (m.Success && count < 3) {
                    switch (count) {
                        case 0:
                            r.thirty = m.Groups[1].Value;
                            break;
                        case 1:
                            r.sixty = m.Groups[1].Value;
                            break;

                        case 2:
                            r.ninety = m.Groups[1].Value;
                            break;
                    }

                    m = m.NextMatch();
                    count++;
                }

                attend.Add(r);
            }

            DBManager.getManager().emptyTable("attendance");
            DBManager.getManager().bulkInsert(attend, "attendance");
        }
    }

    class RosterEntry : BulkLoader {
        public string name { get; set; }

        public string classType { get; set; }

        public string rank { get; set; }

        public string active { get; set; }

        public string[] getColumnArray() {
            return new string[] { name, classType, rank, active };
        }

        public static bool isActive(RosterEntry e) {
            return e.active.Equals("Yes", StringComparison.InvariantCultureIgnoreCase);
        }
    }

    class RosterAttendence : BulkLoader {
        public string name { get; set; }

        public string thirty { get; set; }

        public string sixty { get; set; }

        public string ninety { get; set; }

        public string[] getColumnArray() {
            return new string[] { name, thirty, sixty, ninety };
        }
    }
}
