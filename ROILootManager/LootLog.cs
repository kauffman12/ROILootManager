using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Sheets.v4.Data;
using log4net;

namespace ROILootManager
{
  class LootLog
  {
    private static ILog logger = LogManager.GetLogger(typeof(LootLog));

    private List<EventEntry> events = new List<EventEntry>();
    private List<ArmorTypeEntry> armorTypes = new List<ArmorTypeEntry>();
    private List<String> tiers = new List<String>();
    private ValueRange logFeed;

    private string logURI;

    public const string EVENT_EVENT_COL = "event";
    public const string EVENT_SHORT_COL = "short name";
    public const string EVENT_TIER_COL = "tier";
    public const string EVENT_DISPLAY_COL = "display in list";
    public const string CONSTANTS_ARMOR_TYPES_NAME_COL = "armor types";
    public const string CONSTANTS_TIER_COL = "tiers";

    public bool isLoading = false;

    public LootLog()
    {
      string log = PropertyManager.getManager().getProperty("lootLogURI");

      if (log == null)
      {
        log = Constants.LOOT_ID;
      }

      reloadLog(log);
    }

    public void reloadLog(string docId)
    {
      PropertyManager.getManager().setProperty("lootLogURI", docId);
      logURI = docId;

      loadEvents();
      loadArmorTypes();
      loadLogFeed();
    }

    public string getLogUri()
    {
      return logURI;
    }

    public List<EventEntry> getEvents()
    {
      return events;
    }

    public List<String> getTiers()
    {
      return tiers;
    }

    public List<ArmorTypeEntry> getArmorTypes()
    {
      return armorTypes;
    }

    public void loadEvents()
    {
      events.Clear();

      ValueRange response = GDriveManager.readSpreadsheet(logURI, "RainOfFearRaids");
      IDictionary<string, int> headerMap = GDriveManager.getHeaderMap(response.Values);

      // order by 2nd column (Short Name)
      IEnumerable<IList<Object>> sorted = response.Values.Skip(1).OrderBy(f => f.ElementAt(headerMap[EVENT_SHORT_COL]).ToString());
      List<BulkLoader> rows = new List<BulkLoader>();

      // Iterate through each row
      foreach (IList<Object> row in sorted)
      {
        string display = row[headerMap[EVENT_DISPLAY_COL]].ToString().ToLower();
        if (display.Equals("yes"))
        {
          EventEntry evt = new EventEntry();
          evt.eventName = row[headerMap[EVENT_EVENT_COL]].ToString();
          evt.shortName = row[headerMap[EVENT_SHORT_COL]].ToString();
          evt.tier = row[headerMap[EVENT_TIER_COL]].ToString();

          events.Add(evt);
          rows.Add(evt);
        }
      }

      DBManager.getManager().bulkInsert(rows, "events");

      logger.Info("Events loaded successfully. " + events.Count + " entries.");
    }

    public void loadArmorTypes()
    {
      armorTypes.Clear();

      ValueRange response = GDriveManager.readSpreadsheet(logURI, "Constants");
      IDictionary<string, int> headerMap = GDriveManager.getHeaderMap(response.Values);

      IEnumerable<IList<Object>> sorted = response.Values.Skip(1).OrderBy(f => f.ElementAt(headerMap[CONSTANTS_ARMOR_TYPES_NAME_COL]).ToString());
      List<BulkLoader> rows = new List<BulkLoader>();

      // Iterate through each row
      foreach (IList<Object> row in sorted)
      {
        ArmorTypeEntry at = new ArmorTypeEntry();
        at.armorType = GDriveManager.readCell(row, headerMap[CONSTANTS_ARMOR_TYPES_NAME_COL]);

        // for some reason if data isn't present in the 2nd column the row array is set to 1
        // not sure what happens if the first column was missing data
        if (row.Count > headerMap[CONSTANTS_TIER_COL])
        {
          string tier = row[headerMap[CONSTANTS_TIER_COL]].ToString();
          if (!tier.Equals(""))
          {
            tiers.Add(tier);
          }
        }

        armorTypes.Add(at);
      }

      tiers.Reverse();
      logger.Info("Events loaded successfully. " + events.Count + " entries.");
    }

    public void loadLogFeed()
    {
      logFeed = GDriveManager.readSpreadsheet(logURI, "RainOfFearLoot");
    }

    public void loadLootInfo()
    {
      if (!isLoading)
      {
        isLoading = true;
        loadLogFeed();

        IDictionary<string, int> headerMap = GDriveManager.getHeaderMap(logFeed.Values);
        List<BulkLoader> rows = new List<BulkLoader>();

        // Iterate through each row
        foreach (IList<Object> row in logFeed.Values.Skip(1))
        {
          LootEntry loot = new LootEntry();
          loot.date = GDriveManager.readCell(row, headerMap["date"]);
          loot.name = GDriveManager.readCell(row, headerMap["name"]);
          loot.eventName = GDriveManager.readCell(row, headerMap["event"]);
          loot.item = GDriveManager.readCell(row, headerMap["item"]);
          loot.slot = GDriveManager.readCell(row, headerMap["slot"]);
          loot.rot = GDriveManager.readCell(row, headerMap["rot"]);
          loot.altLoot = GDriveManager.readCell(row, headerMap["alt loot"]);

          rows.Add(loot);
        }

        DBManager.getManager().emptyTable("loot");
        DBManager.getManager().bulkInsert(rows, "loot");
        isLoading = false;
      }
    }

    public void insertNewLootEntry(string date, string name, string eventName, string item, string slot, string rot, string altLoot)
    {
      try
      {
        // Create a local representation of the new row.
        IList<Object> values = new List<Object> { date, name, eventName, item, slot, rot, altLoot };
        GDriveManager.appendSpreadsheet(logURI, "RainOfFearLoot", values);

        logger.Info(String.Format("Inserted loot entry. [{0}, {1}, {2}, {3}, {4}, {5}, {6}]", date, name, eventName, item, slot, rot, altLoot));
      }
      catch (Exception e)
      {
        logger.Error(String.Format("Failed to insert loot entry [{0}, {1}, {2}, {3}, {4}, {5}, {6}]", date, name, eventName, item, slot, rot, altLoot), e);
        throw e;
      }
    }
  }

  class EventEntry : BulkLoader
  {
    public string eventName { get; set; }

    public string shortName { get; set; }

    public string tier { get; set; }

    public string[] getColumnArray()
    {
      return new string[] { eventName, shortName, tier };
    }
  }

  class ArmorTypeEntry
  {
    public string armorType { get; set; }
  }

  public class LootEntry : BulkLoader
  {
    public string date { get; set; }

    public string name { get; set; }

    public string eventName { get; set; }

    public string item { get; set; }

    public string slot { get; set; }

    public string rot { get; set; }

    public string altLoot { get; set; }

    public string[] getColumnArray()
    {
      return new string[] { DateTime.Parse(date).ToString("yyyy-MM-dd"), name, eventName, item, slot, rot, altLoot };
    }
  }
}
