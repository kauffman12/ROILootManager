using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using System.Data.Common;
using Google.Apis.Sheets.v4.Data;

namespace ROILootManager
{
  public class ItemListing
  {
    private static ILog logger = LogManager.GetLogger(typeof(ItemListing));

    private List<ItemEntry> itemArr = new List<ItemEntry>();

    public const string ITEM_NAME_COL = "item name";
    public const string EVENT_COL = "event";
    public const string SLOT_COL = "slot";
    public const string TIER_COL = "tier";
    public const string SPECIAL_COL = "special";

    public ItemListing()
    {
      loadItems();
      loadGolbalItems();
    }

    public void loadItems()
    {
      // Clear the current items
      itemArr.Clear();

      ValueRange response = GDriveManager.readSpreadsheet(Constants.ITEMS_ID, "ROFItems");
      IDictionary<string, int> headerMap = GDriveManager.getHeaderMap(response.Values);
      List<BulkLoader> rows = new List<BulkLoader>();

      // Iterate through each row
      foreach (IList<Object> row in response.Values.Skip(1))
      {
        ItemEntry item = new ItemEntry();
        item.itemName = GDriveManager.readCell(row, headerMap[ITEM_NAME_COL]);
        item.eventName = GDriveManager.readCell(row, headerMap[EVENT_COL]);
        item.slot = GDriveManager.readCell(row, headerMap[SLOT_COL]);
        item.is_special = GDriveManager.readCell(row, headerMap[SPECIAL_COL]);

        rows.Add(item);
        itemArr.Add(item);
      }

      DBManager.getManager().bulkInsert(rows, "items");
      logger.Info("Items loaded successfully. " + itemArr.Count + " entries.");
    }

    public void loadGolbalItems()
    {
      ValueRange response = GDriveManager.readSpreadsheet(Constants.ITEMS_ID, "ROF Global Drops");
      IDictionary<string, int> headerMap = GDriveManager.getHeaderMap(response.Values);
      List<BulkLoader> rows = new List<BulkLoader>();

      // Iterate through each row
      foreach (IList<Object> row in response.Values.Skip(1))
      {
        ItemEntry item = new ItemEntry();
        item.itemName = GDriveManager.readCell(row, headerMap[ITEM_NAME_COL]);
        item.tier = GDriveManager.readCell(row, headerMap[TIER_COL]);
        item.slot = GDriveManager.readCell(row, headerMap[SLOT_COL]);
        item.is_special = GDriveManager.readCell(row, headerMap[SPECIAL_COL]);
        item.isGlobal = true;

        rows.Add(item);
        itemArr.Add(item);
      }

      DBManager.getManager().bulkInsert(rows, "items");
      logger.Info("Items loaded successfully. " + itemArr.Count + " entries.");
    }

    public List<ItemEntry> getFilteredList(string eventName, string slot, string tier)
    {
      List<ItemEntry> filtered = new List<ItemEntry>(itemArr);

      return filtered.FindAll((ItemEntry e) =>
      {
        bool eventMatch = false;
        bool slotMatch = false;

        if (e != null)
        {
          if ("".Equals(eventName) || e.eventName.Equals(eventName) || (e.isGlobal == true && e.tier.Equals(tier)))
            eventMatch = true;

          if ("".Equals(slot) || e.slot.Equals(slot))
            slotMatch = true;
        }
        return eventMatch && slotMatch;
      }).Distinct().OrderBy(e => e.itemName).ToList();
    }

    public void addMissingItems()
    {
      try
      {
        string sql = "SELECT DISTINCT slot, item, short_event_name FROM loot AS l WHERE NOT EXISTS(SELECT 1 FROM items WHERE item = l.item AND short_event_name = l.short_event_name AND slot = l.slot) AND NOT EXISTS(SELECT 1 FROM items WHERE is_global = 'Yes' AND item = l.item AND slot = l.slot)";

        DbDataReader rs = DBManager.getManager().executeQuery(sql);

        while (rs.Read())
        {
          string slot = rs[0].ToString().Trim();
          string item = rs[1].ToString().Trim();
          string eventName = rs[2].ToString().Trim();

          logger.Info(String.Format("Inserting loot entry. [{0}, {1}, {2}]", slot, item, eventName));

          // Send the new row to the API for insertion.
          IList<Object> values = new List<Object> { slot, item, eventName };
          GDriveManager.appendSpreadsheet(Constants.ITEMS_ID, "ROFItems", values);
          DBManager.getManager().insertItemEntry(slot, item, eventName, "", "", "");
        }
      }
      catch (Exception e)
      {
        logger.Error("Failed to insert loot entry", e);
        throw e;
      }
    }
  }


  public class ItemEntry : BulkLoader
  {
    public string itemName { get; set; }

    public string slot { get; set; }

    public string eventName { get; set; }

    public bool isGlobal { get; set; }

    public string tier { get; set; }

    public string is_special { get; set; }

    public ItemEntry()
    {
      isGlobal = false;
      tier = "";
      slot = "";
      eventName = "";
      itemName = "";
      is_special = "";
    }

    public string[] getColumnArray()
    {
      return new string[] { slot, itemName, eventName, (isGlobal == true ? "Yes" : ""), tier, is_special };
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;

      if (obj is ItemEntry)
        return this.itemName.Equals(((ItemEntry)obj).itemName);
      else
        return false;
    }

    public override int GetHashCode()
    {
      return itemName.GetHashCode();
    }
  }

  public class ItemEntryComparitor : IEqualityComparer<ItemEntry>
  {
    public bool Equals(ItemEntry e1, ItemEntry e2)
    {
      return e1.itemName.Equals(e2.itemName);
    }

    public int GetHashCode(ItemEntry e)
    {
      return e.itemName.GetHashCode();
    }
  }
}
