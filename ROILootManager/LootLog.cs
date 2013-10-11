using System;
using System.Collections.Generic;
using System.Linq;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using log4net;

namespace ROILootManager {
    class LootLog {
        private static ILog logger = LogManager.GetLogger(typeof(LootLog));

        private SpreadsheetEntry spreadsheet;

        private List<EventEntry> events = new List<EventEntry>();
        private List<ArmorTypeEntry> armorTypes = new List<ArmorTypeEntry>();
        private List<String> tiers = new List<String>();

        private string logURI;

        ListFeed logFeed;

        public const string EVENT_EVENT_COL = "event";
        public const string EVENT_SHORT_COL = "shortname";
        public const string EVENT_TIER_COL = "tier";
        public const string CONSTANTS_ARMOR_TYPES_NAME_COL = "armortypes";
        public const string CONSTANTS_TIER_COL = "tiers";

        public bool isLoading = false;

        public LootLog() {
            string log = PropertyManager.getManager().getProperty("lootLogURI");

            if (log == null) {
                log = Constants.ROI_ROF_LOOT_URI;
            }

            reloadLog(log);
        }

        public void reloadLog(string log) {
            PropertyManager.getManager().setProperty("lootLogURI", log);

            spreadsheet = GDriveManager.getSpreadsheet(log);
            logger.Info("Spreadsheet " + spreadsheet.Title.Text + " loaded.");

            loadEvents();
            loadArmorTypes();
            loadLogFeed();

            logURI = log;
        }

        public string getLogUri() {
            return logURI;
        }

        public List<EventEntry> getEvents() {
            return events;
        }

        public List<String> getTiers() {
            return tiers;
        }

        public List<ArmorTypeEntry> getArmorTypes() {
            return armorTypes;
        }

        static bool findEventsWorkSheet(AtomEntry e) {
            return e.Title.Text.Equals("RainOfFearRaids", StringComparison.InvariantCultureIgnoreCase);
        }

        static bool findArmorTypesWorkSheet(AtomEntry e) {
            return e.Title.Text.Equals("Constants", StringComparison.InvariantCultureIgnoreCase);
        }

        static bool findLootWorkSheet(AtomEntry e) {
            return e.Title.Text.Equals("RainOfFearLoot", StringComparison.InvariantCultureIgnoreCase);
        }

        public void loadEvents() {
            events.Clear();

            // Get the first worksheet of the first spreadsheet.
            // TODO: Choose a worksheet more intelligently based on your
            // app's needs.
            WorksheetFeed wsFeed = spreadsheet.Worksheets;

            // Get the Event list worksheet
            WorksheetEntry worksheet = (WorksheetEntry)wsFeed.Entries.ToList().Find(new Predicate<AtomEntry>(findEventsWorkSheet));

            // Define the URL to request the list feed of the worksheet.
            AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            // Fetch the list feed of the worksheet.
            ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());
            listQuery.OrderByColumn = EVENT_SHORT_COL;



            ListFeed listFeed = GDriveManager.getService().Query(listQuery);

            List<BulkLoader> rows = new List<BulkLoader>();

            // Iterate through each row, printing its cell values.
            foreach (ListEntry row in listFeed.Entries) {

                // Print the first column's cell value
                //logger.Debug(row.Title.Text);
                EventEntry evt = new EventEntry();
                // Iterate over the remaining columns, and print each cell value
                foreach (ListEntry.Custom element in row.Elements) {
                    //logger.Debug(element.Value);
                    switch (element.LocalName.ToLower()) {
                        case EVENT_EVENT_COL:
                            evt.eventName = element.Value;
                            break;
                        case EVENT_SHORT_COL:
                            evt.shortName = element.Value;
                            break;

                        case EVENT_TIER_COL:
                            evt.tier = element.Value;
                            break;
                    }
                }

                events.Add(evt);
                rows.Add(evt);
            }

            DBManager.getManager().bulkInsert(rows, "events");

            logger.Info("Events loaded successfully. " + events.Count + " entries.");
        }

        public void loadArmorTypes() {
            armorTypes.Clear();

            // Get the first worksheet of the first spreadsheet.
            // TODO: Choose a worksheet more intelligently based on your
            // app's needs.
            WorksheetFeed wsFeed = spreadsheet.Worksheets;

            // Get the Event list worksheet
            WorksheetEntry worksheet = (WorksheetEntry)wsFeed.Entries.ToList().Find(new Predicate<AtomEntry>(findArmorTypesWorkSheet));
            ;

            // Define the URL to request the list feed of the worksheet.
            AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            // Fetch the list feed of the worksheet.
            ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());
            listQuery.OrderByColumn = CONSTANTS_ARMOR_TYPES_NAME_COL;



            ListFeed listFeed = GDriveManager.getService().Query(listQuery);

            // Iterate through each row, printing its cell values.
            foreach (ListEntry row in listFeed.Entries) {

                // Print the first column's cell value
                //logger.Debug(row.Title.Text);
                ArmorTypeEntry at = new ArmorTypeEntry();
                // Iterate over the remaining columns, and print each cell value
                foreach (ListEntry.Custom element in row.Elements) {
                    //logger.Debug(element.Value);
                    if (element.Value.Length > 0) {
                        switch (element.LocalName.ToLower()) {
                            case CONSTANTS_ARMOR_TYPES_NAME_COL:
                                at.armorType = element.Value;
                                break;
                            case CONSTANTS_TIER_COL:
                                tiers.Add(element.Value);
                                break;
                        }
                    }
                }

                armorTypes.Add(at);
            }

            tiers.Reverse();

            logger.Info("Events loaded successfully. " + events.Count + " entries.");
        }

        public void loadLogFeed() {
            // Get the Loot Log List worksheet
            WorksheetEntry worksheet = (WorksheetEntry)spreadsheet.Worksheets.Entries.ToList().Find(new Predicate<AtomEntry>(findLootWorkSheet));
            ;

            // Define the URL to request the list feed of the worksheet.
            AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            // Fetch the list feed of the worksheet.
            ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());

            logFeed = GDriveManager.getService().Query(listQuery);
        }

        public void loadLootInfo() {
            if (!isLoading) {
                isLoading = true;
                loadLogFeed();
                List<BulkLoader> rows = new List<BulkLoader>();

                // Add the loot log to the database
                foreach (ListEntry row in logFeed.Entries) {

                    LootEntry loot = new LootEntry();

                    // Iterate over the remaining columns
                    foreach (ListEntry.Custom element in row.Elements) {
                        switch (element.LocalName.ToLower()) {
                            case "date":
                                loot.date = element.Value;
                                break;

                            case "name":
                                loot.name = element.Value;
                                break;

                            case "event":
                                loot.eventName = element.Value;
                                break;

                            case "item":
                                loot.item = element.Value;
                                break;

                            case "slot":
                                loot.slot = element.Value;
                                break;

                            case "rot":
                                loot.rot = element.Value;
                                break;

                            case "altloot":
                                loot.altLoot = element.Value;
                                break;
                        }
                    }

                    rows.Add(loot);
                }

                DBManager.getManager().emptyTable("loot");
                DBManager.getManager().bulkInsert(rows, "loot");
                isLoading = false;
            }
        }

        public void insertNewLootEntry(string date, string name, string eventName, string item, string slot, string rot, string altLoot) {
            try {
                // Create a local representation of the new row.
                ListEntry row = new ListEntry();
                row.Elements.Add(new ListEntry.Custom() { LocalName = "date", Value = date });
                row.Elements.Add(new ListEntry.Custom() { LocalName = "name", Value = name });
                row.Elements.Add(new ListEntry.Custom() { LocalName = "event", Value = eventName });
                row.Elements.Add(new ListEntry.Custom() { LocalName = "item", Value = item });
                row.Elements.Add(new ListEntry.Custom() { LocalName = "slot", Value = slot });
                row.Elements.Add(new ListEntry.Custom() { LocalName = "rot", Value = rot });
                row.Elements.Add(new ListEntry.Custom() { LocalName = "altloot", Value = altLoot });

                // Send the new row to the API for insertion.
                GDriveManager.getService().Insert(logFeed, row);

                logger.Info(String.Format("Inserted loot entry. [{0}, {1}, {2}, {3}, {4}, {5}, {6}]", date, name, eventName, item, slot, rot, altLoot));
            } catch (Exception e) {
                logger.Error(String.Format("Failed to insert loot entry [{0}, {1}, {2}, {3}, {4}, {5}, {6}]", date, name, eventName, item, slot, rot, altLoot), e);
                throw e;
            }
        }

        public void removeEmptyLootRows() {
            int count = 0;
            foreach (ListEntry row in logFeed.Entries) {
                bool hasValue = false;
                foreach (ListEntry.Custom col in row.Elements) {
                    if (!col.Value.Equals(""))
                        hasValue = true;
                }

                if (!hasValue) {
                    // Delete the row using the API.
                    //row.Delete();
                    count++;
                }
            }
            logger.Info(String.Format("Cleaned {0} empty rows.", count));
        }

    }

    class EventEntry : BulkLoader {
        public string eventName { get; set; }

        public string shortName { get; set; }

        public string tier { get; set; }

        public string[] getColumnArray() {
            return new string[] { eventName, shortName, tier };
        }
    }

    class ArmorTypeEntry {
        public string armorType { get; set; }
    }

    public class LootEntry : BulkLoader {
        public string date { get; set; }

        public string name { get; set; }

        public string eventName { get; set; }

        public string item { get; set; }

        public string slot { get; set; }

        public string rot { get; set; }

        public string altLoot { get; set; }

        public string[] getColumnArray() {
            return new string[] { DateTime.Parse(date).ToString("yyyy-MM-dd"), name, eventName, item, slot, rot, altLoot };
        }
    }
}
