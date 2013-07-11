using System;
using System.Collections.Generic;
using System.Linq;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using log4net;
using System.Data.Common;
using System.Linq;

namespace ROILootManager {
    class SpellManager {
        private static ILog logger = LogManager.GetLogger(typeof(SpellManager));

        private SpreadsheetEntry spreadsheet;

        private const string TEMPLATE_WS = "ClassSpells";

        private const string SPELL_LOG_WS = "GuildSpells";

        public SpellManager() {
            spreadsheet = GDriveManager.getSpreadsheet(Constants.SPELL_LISTING_URI);
            logger.Info("Spreadsheet " + spreadsheet.Title.Text + " loaded");

            loadSpellTemplate();
            loadSpellLog();
        }

        public void loadSpellTemplate() {
            ListFeed logFeed = GDriveManager.getListFeed(spreadsheet, TEMPLATE_WS);
            List<BulkLoader> rows = new List<BulkLoader>();

            foreach (ListEntry row in logFeed.Entries) {

                SpellTemplateEntry spell = new SpellTemplateEntry();

                foreach (ListEntry.Custom element in row.Elements) {
                    switch (element.LocalName.ToLower()) {
                        case "class":
                            spell.classType = element.Value;
                            break;

                        case "spells":
                            spell.spells = element.Value;
                            break;

                        case "level":
                            spell.level = element.Value;
                            break;

                        case "priority":
                            spell.priority = element.Value;
                            break;
                    }
                }

                rows.Add(spell);
            }

            DBManager.getManager().emptyTable("spell_template");
            DBManager.getManager().bulkInsert(rows, "spell_template");
        }

        public void loadSpellLog() {
            ListFeed logFeed = GDriveManager.getListFeed(spreadsheet, SPELL_LOG_WS);
            List<BulkLoader> rows = new List<BulkLoader>();

            foreach (ListEntry row in logFeed.Entries) {

                SpellLogEntry spell = new SpellLogEntry();

                foreach (ListEntry.Custom element in row.Elements) {
                    switch (element.LocalName.ToLower()) {
                        case "name":
                            spell.name = element.Value;
                            break;

                        case "class":
                            spell.classType = element.Value;
                            break;

                        case "spells":
                            spell.spells = element.Value;
                            break;

                        case "level":
                            spell.level = element.Value;
                            break;

                        case "priority":
                            spell.priority = element.Value;
                            break;

                        case "hasspell":
                            spell.hasSpell = element.Value;
                            break;
                    }
                }

                rows.Add(spell);
            }

            DBManager.getManager().emptyTable("spell_log");
            DBManager.getManager().bulkInsert(rows, "spell_log");
        }

        public int loadSpellLogFromRoster() {
            int spellsAdded = 0;
            try {
                ListFeed listFeed = GDriveManager.getListFeed(spreadsheet, SPELL_LOG_WS);

                string sql = "SELECT * FROM (SELECT DISTINCT r.name, r.class, s.spells, s.level, s.priority FROM roster AS r, spell_template AS s WHERE r.active = 'Yes' AND r.class = s.class) AS tmp WHERE NOT EXISTS(SELECT 1 FROM spell_log AS sl WHERE sl.name = tmp.name AND sl.spells = tmp.spells AND sl.class = tmp.class)";
              
                DbDataReader rs = DBManager.getManager().executeQuery(sql);

                while (rs.Read()) {
                    string name = rs[0].ToString().Trim();
                    string className = rs[1].ToString().Trim();
                    string spells = rs[2].ToString().Trim();
                    string level = rs[3].ToString().Trim();
                    string priority = rs[4].ToString().Trim();
                    ListEntry row = new ListEntry();

                    row.Elements.Add(new ListEntry.Custom() { LocalName = "name", Value = name });
                    row.Elements.Add(new ListEntry.Custom() { LocalName = "class", Value = className });
                    row.Elements.Add(new ListEntry.Custom() { LocalName = "spells", Value = spells });
                    row.Elements.Add(new ListEntry.Custom() { LocalName = "level", Value = level });
                    row.Elements.Add(new ListEntry.Custom() { LocalName = "priority", Value = priority });
                    row.Elements.Add(new ListEntry.Custom() { LocalName = "hasspell", Value = "" });

                    logger.Info(String.Format("Inserting spell entry. [{0}, {1}, {2}, {3}, {4}]", name, className, spells, level, priority));

                    // Send the new row to the API for insertion.
                    GDriveManager.getService().Insert(listFeed, row);

                    spellsAdded++;
                }

                loadSpellLog();

            } catch (Exception e) {
                logger.Error("Failed to insert spell entry", e);
                throw e;
            }

            return spellsAdded;
        }

        public int updateSpellPriority() {
            int spellsUpdated = 0;

            try {
                string sql = "SELECT DISTINCT st.class, st.spells, st.level, st.priority FROM spell_template AS st, spell_log AS sl WHERE st.spells = sl.spells AND st.class = sl.class AND st.level = sl.level AND st.priority <> sl.priority";

                DbDataReader rs = DBManager.getManager().executeQuery(sql);

                while (rs.Read()) {
                    string className = rs[0].ToString().Trim();
                    string spells = rs[1].ToString().Trim();
                    string level = rs[2].ToString().Trim();
                    string priority = rs[3].ToString().Trim();

                    ListFeed listFeed = GDriveManager.getListFeedQuery(spreadsheet, SPELL_LOG_WS, String.Format("class == \"{0}\" and spells == \"{1}\" and level == \"{2}\"", className, spells, level));

                    foreach (ListEntry row in listFeed.Entries) {
                        foreach (ListEntry.Custom element in row.Elements) {
                            switch (element.LocalName.ToLower()) {
                                case "priority":
                                    element.Value = priority;
                                    break;
                            }
                        }

                        row.Update();
                        spellsUpdated++;
                    }

                    logger.Info(String.Format("Updating spell entry. [{0}, {1}, {2}, {3}]", className, spells, level, priority));

                }

                loadSpellLog();

            } catch (Exception e) {
                logger.Error("Failed to update spell entry", e);
                throw e;
            }


            return spellsUpdated;
        }
    }

    public class SpellTemplateEntry : BulkLoader {
        public string classType { get; set; }

        public string spells { get; set; }

        public string level { get; set; }

        public string priority { get; set; }

        public SpellTemplateEntry() {
            classType = "";
            spells = "";
            level = "";
            priority = "";
        }

        public string[] getColumnArray() {
            return new string[] { classType, spells, level, priority };
        }
    }

    public class SpellLogEntry : BulkLoader {
        public string name { get; set; }
        
        public string classType { get; set; }

        public string spells { get; set; }

        public string level { get; set; }

        public string priority { get; set; }

        public string hasSpell { get; set; }

        public SpellLogEntry() {
            name = "";
            classType = "";
            spells = "";
            level = "";
            priority = "";
            hasSpell = "";
        }

        public string[] getColumnArray() {
            return new string[] { name, classType, spells, level, priority, hasSpell };
        }
    }
}
