using System;
using System.Collections.Generic;
using System.Linq;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using log4net;
using System.Data.Common;

namespace ROILootManager {
    public class ItemListing {
        private static ILog logger = LogManager.GetLogger(typeof(ItemListing));

        private SpreadsheetEntry spreadsheet;

        private List<ItemEntry> itemArr = new List<ItemEntry>();

        public const string ITEM_NAME_COL = "itemname";
        public const string EVENT_COL = "event";
        public const string SLOT_COL = "slot";
        public const string TIER_COL = "tier";

        public ItemListing() {
            spreadsheet = GDriveManager.getSpreadsheet(Constants.ITEM_LISTING_URI);
            logger.Info("Spreadsheet " + spreadsheet.Title.Text + " loaded.");

            loadItems();
            loadGolbalItems();
        }

        public void loadItems() {
            // Clear the current roster
            itemArr.Clear();

            // Get the first worksheet of the first spreadsheet.
            // TODO: Choose a worksheet more intelligently based on your
            // app's needs.
            WorksheetFeed wsFeed = spreadsheet.Worksheets;
            WorksheetEntry worksheet = (WorksheetEntry)wsFeed.Entries.ToList().Find((AtomEntry e) => e.Title.Text.Equals("ROFItems", StringComparison.InvariantCultureIgnoreCase));

            // Define the URL to request the list feed of the worksheet.
            AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            // Fetch the list feed of the worksheet.
            ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());


            ListFeed listFeed = GDriveManager.getService().Query(listQuery);

            List<BulkLoader> rows = new List<BulkLoader>();

            // Iterate through each row, printing its cell values.
            foreach (ListEntry row in listFeed.Entries) {

                // Print the first column's cell value
                //logger.Debug(row.Title.Text);
                ItemEntry r = new ItemEntry();
                // Iterate over the remaining columns, and print each cell value
                foreach (ListEntry.Custom element in row.Elements) {
                    //logger.Debug(element.Value);
                    switch (element.LocalName.ToLower()) {
                        case ITEM_NAME_COL:
                            r.itemName = element.Value;
                            break;
                        case EVENT_COL:
                            r.eventName = element.Value;
                            break;

                        case SLOT_COL:
                            r.slot = element.Value;
                            break;

                        case "special":
                            r.is_special = element.Value;
                            break;
                    }
                }

                rows.Add(r);
                itemArr.Add(r);
            }

            DBManager.getManager().bulkInsert(rows, "items");

            logger.Info("Items loaded successfully. " + itemArr.Count + " entries.");
        }

        public void loadGolbalItems() {
            WorksheetFeed wsFeed = spreadsheet.Worksheets;
            WorksheetEntry worksheet = (WorksheetEntry)wsFeed.Entries.ToList().Find((AtomEntry e) => e.Title.Text.Equals("ROF Global Drops", StringComparison.InvariantCultureIgnoreCase));

            // Define the URL to request the list feed of the worksheet.
            AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            // Fetch the list feed of the worksheet.
            ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());


            ListFeed listFeed = GDriveManager.getService().Query(listQuery);

            List<BulkLoader> rows = new List<BulkLoader>();

            // Iterate through each row, printing its cell values.
            foreach (ListEntry row in listFeed.Entries) {

                // Print the first column's cell value
                //logger.Debug(row.Title.Text);
                ItemEntry r = new ItemEntry();
                // Iterate over the remaining columns, and print each cell value
                foreach (ListEntry.Custom element in row.Elements) {
                    //logger.Debug(element.Value);
                    switch (element.LocalName.ToLower()) {
                        case ITEM_NAME_COL:
                            r.itemName = element.Value;
                            break;
                        case TIER_COL:
                            r.tier = element.Value;
                            r.isGlobal = true;
                            break;

                        case SLOT_COL:
                            r.slot = element.Value;
                            break;

                        case "special":
                            r.is_special = element.Value;
                            break;
                    }
                }

                itemArr.Add(r);
                rows.Add(r);
            }

            DBManager.getManager().bulkInsert(rows, "items");

            logger.Info("Items loaded successfully. " + itemArr.Count + " entries.");
        }

        public List<ItemEntry> getFilteredList(string eventName, string slot, string tier) {
            List<ItemEntry> filtered = new List<ItemEntry>(itemArr);

            return filtered.FindAll((ItemEntry e) => {
                bool eventMatch = false;
                bool slotMatch = false;

                if (e != null) {
                    if ("".Equals(eventName) || e.eventName.Equals(eventName) || (e.isGlobal == true && e.tier.Equals(tier)))
                        eventMatch = true;

                    if ("".Equals(slot) || e.slot.Equals(slot))
                        slotMatch = true;
                }
                return eventMatch && slotMatch;
            }).Distinct().OrderBy(e => e.itemName).ToList();
        }

        public void addMissingItems() {
            try {
                WorksheetFeed wsFeed = spreadsheet.Worksheets;
                WorksheetEntry worksheet = (WorksheetEntry)wsFeed.Entries.ToList().Find((AtomEntry e) => e.Title.Text.Equals("ROFItems", StringComparison.InvariantCultureIgnoreCase));

                // Define the URL to request the list feed of the worksheet.
                AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

                // Fetch the list feed of the worksheet.
                ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());

                ListFeed listFeed = GDriveManager.getService().Query(listQuery);

                string sql = "SELECT DISTINCT slot, item, short_event_name FROM loot AS l WHERE NOT EXISTS(SELECT 1 FROM items WHERE item = l.item AND short_event_name = l.short_event_name AND slot = l.slot) AND NOT EXISTS(SELECT 1 FROM items WHERE is_global = 'Yes' AND item = l.item AND slot = l.slot)";

                DbDataReader rs = DBManager.getManager().executeQuery(sql);

                while (rs.Read()) {
                    string slot = rs[0].ToString().Trim();
                    string item = rs[1].ToString().Trim();
                    string eventName = rs[2].ToString().Trim();
                    ListEntry row = new ListEntry();

                    row.Elements.Add(new ListEntry.Custom() { LocalName = "slot", Value = slot });
                    row.Elements.Add(new ListEntry.Custom() { LocalName = "itemname", Value = item });
                    row.Elements.Add(new ListEntry.Custom() { LocalName = "event", Value = eventName });

                    logger.Info(String.Format("Inserting loot entry. [{0}, {1}, {2}]", slot, item, eventName));

                    // Send the new row to the API for insertion.
                    GDriveManager.getService().Insert(listFeed, row);

                    DBManager.getManager().insertItemEntry(slot, item, eventName, "", "", "");
                }


            } catch (Exception e) {
                logger.Error("Failed to insert loot entry", e);
                throw e;
            }
        }
    }


    public class ItemEntry : BulkLoader {
        public string itemName { get; set; }

        public string slot { get; set; }

        public string eventName { get; set; }

        public bool isGlobal { get; set; }

        public string tier { get; set; }

        public string is_special { get; set; }

        public ItemEntry() {
            isGlobal = false;
            tier = "";
            slot = "";
            eventName = "";
            itemName = "";
            is_special = "";
        }

        public string[] getColumnArray() {
            return new string[] { slot, itemName, eventName, (isGlobal == true ? "Yes" : ""), tier, is_special };
        }

        public override bool Equals(object obj) {
            if (obj == null)
                return false;

            if (obj is ItemEntry)
                return this.itemName.Equals(((ItemEntry)obj).itemName);
            else
                return false;
        }

        public override int GetHashCode() {
            return itemName.GetHashCode();
        }
    }

    public class ItemEntryComparitor : IEqualityComparer<ItemEntry> {
        public bool Equals(ItemEntry e1, ItemEntry e2) {
            return e1.itemName.Equals(e2.itemName);
        }

        public int GetHashCode(ItemEntry e) {
            return e.itemName.GetHashCode();
        }
    }
}
