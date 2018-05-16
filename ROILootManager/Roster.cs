using System;
using System.Collections.Generic;
using log4net;
using System.IO;
using Newtonsoft.Json;
using Google.Apis.Sheets.v4.Data;
using System.Linq;

namespace ROILootManager
{
  class Roster
  {
    private static ILog logger = LogManager.GetLogger(typeof(Roster));

    List<RosterEntry> rosterArr = new List<RosterEntry>();

    public const string NAME_COL = "name";
    public const string CLASS_COL = "class";
    public const string RANK_COL = "rank";
    public const string ACTIVE_COL = "active";

    public Roster()
    {

      // Clear the current roster
      rosterArr.Clear();

      ValueRange response = GDriveManager.readSpreadsheet(Constants.ROSTER_ID, "Sheet1");
      IDictionary<string, int> headerMap = GDriveManager.getHeaderMap(response.Values);

      IEnumerable<IList<Object>> sorted = response.Values.Skip(1).OrderBy(f => f.ElementAt(headerMap[NAME_COL]).ToString());
      List<BulkLoader> rows = new List<BulkLoader>();

      // Iterate through each row
      foreach (IList<Object> row in sorted)
      {
        RosterEntry r = new RosterEntry();
        r.name = GDriveManager.readCell(row, headerMap[NAME_COL]);
        r.classType = GDriveManager.readCell(row, headerMap[CLASS_COL]);
        r.rank = GDriveManager.readCell(row, headerMap[RANK_COL]);
        r.active = GDriveManager.readCell(row, headerMap[ACTIVE_COL]);

        rows.Add(r);
        rosterArr.Add(r);
      }

      DBManager.getManager().bulkInsert(rows, "roster");

      logger.Info("Roster loaded successfully. " + rosterArr.Count + " entries.");
    }

    public List<RosterEntry> getActiveNames()
    {
      return rosterArr.FindAll(new Predicate<RosterEntry>(RosterEntry.isActive));
    }

    public void loadAttendance()
    {
      using (var webClient = new System.Net.WebClient())
      {
        List<BulkLoader> attend = new List<BulkLoader>();
        string json = webClient.DownloadString(Constants.RAID_ATTENDANCE_URL);
        JsonTextReader reader = new JsonTextReader(new StringReader(json));
        while (reader.Read())
        {
          if (JsonToken.StartObject.Equals(reader.TokenType))
          {
            RosterAttendence r = new RosterAttendence();

            reader.Read();
            reader.Read();
            r.thirty = reader.Value.ToString();

            reader.Read();
            reader.Read();
            r.sixty = reader.Value.ToString();

            reader.Read();
            reader.Read();
            r.ninety = reader.Value.ToString();

            reader.Read();
            reader.Read();
            reader.Read();
            reader.Read();
            r.name = reader.Value.ToString();

            attend.Add(r);

          }
        }

        DBManager.getManager().emptyTable("attendance");
        DBManager.getManager().bulkInsert(attend, "attendance");
      }
    }
  }

  class RosterEntry : BulkLoader
  {
    public string name { get; set; }

    public string classType { get; set; }

    public string rank { get; set; }

    public string active { get; set; }

    public string[] getColumnArray()
    {
      return new string[] { name, classType, rank, active };
    }

    public static bool isActive(RosterEntry e)
    {
      return e.active.Equals("Yes", StringComparison.InvariantCultureIgnoreCase);
    }
  }

  class RosterAttendence : BulkLoader
  {
    public string name { get; set; }

    public string thirty { get; set; }

    public string sixty { get; set; }

    public string ninety { get; set; }

    public string[] getColumnArray()
    {
      return new string[] { name, thirty, sixty, ninety };
    }
  }
}
