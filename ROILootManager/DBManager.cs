using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using log4net;

namespace ROILootManager {
    class DBManager {
        private static ILog logger = LogManager.GetLogger(typeof(DBManager));

        public static DBManager singleton;

        private const string DB_CONNECTION_STRING = "Data Source=lootDB.db3;Versio=3;";

        private SQLiteConnection conn;

        public DBManager() {
            conn = new SQLiteConnection(DB_CONNECTION_STRING);
            conn.Open();
            initializeTables();
        }

        public static DBManager getManager() {
            if (singleton == null)
                singleton = new DBManager();

            return singleton;
        }

        public void dropTable(string table) {
            string sql = String.Format("DROP TABLE IF EXISTS {0};", table);
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public void emptyTable(string table) {
            string sql = String.Format("DELETE FROM {0};", table);
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public static string safeParam(string param) {
            if (param == null)
                return "";
            else
                return param.Replace("'", "''");
        }

        public bool tableExists(string table) {
            DbDataReader rs = executeQuery(String.Format("SELECT name FROM sqlite_master WHERE type='table' AND name='{0}';", table));
            bool result = rs.HasRows;
            rs.Close();
            return result;
        }

        public void initializeTables() {
            DbTransaction dbTrans = conn.BeginTransaction();
            dropTable("roster");
            dropTable("loot");
            dropTable("items");
            dropTable("events");
            dropTable("spell_template");
            dropTable("spell_log");

            string sql = "CREATE TABLE roster (name VARCHAR(50), class VARCHAR(20), rank VARCHAR(20), active VARCHAR(3));";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE loot (loot_date DATE, name VARCHAR(20), short_event_name VARCHAR(50), item VARCHAR(150), slot VARCHAR(20), rot VARCHAR(3), alt_loot VARCHAR(3));";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE events (event VARCHAR(100), short_event_name VARCHAR(50), tier VARCHAR(5));";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE items (slot VARCHAR(20), item VARCHAR(150), short_event_name VARCHAR(50), is_global VARCHAR(3), tier VARCHAR(5), is_special VARCHAR(3));";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE spell_template (class VARCHAR(20), spells VARCHAR(500), level VARCHAR(3), priority VARCHAR(5));";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE spell_log (name VARCHAR(50), class VARCHAR(20), spells VARCHAR(500), level VARCHAR(3), priority VARCHAR(5), has_spell VARCHAR(3));";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            if (!tableExists("attendance")) {
                sql = "CREATE TABLE attendance (name VARCHAR(20), thirty VARCHAR(3), sixty VARCHAR(3), ninety VARCHAR(3));";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
            }

            if (!tableExists("current_version")) {
                sql = "CREATE TABLE current_version (version VARCHAR(10));";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
            }

            sql = "CREATE TEMP VIEW tmp_spell_log AS SELECT r.name, r.class, s.spells, s.level, s.priority FROM roster AS r, spell_template AS s WHERE r.active = 'Yes' AND r.class = s.class;";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();


            dbTrans.Commit();

            doUpgradeActions();
        }

        public void doUpgradeActions() {
            DbTransaction dbTrans = conn.BeginTransaction();
            // Query here for the current DB version


            // Do things necessary to upgrade to a new DB version

            // Update the current version
            emptyTable("current_version");
            string sql = String.Format("INSERT INTO current_version (version) VALUES ({0})", Constants.PROGRAM_VERSION);
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            dbTrans.Commit();
        }

        public int insertRosterEntry(string name, string memberClass, string rank, string active) {
            DbTransaction dbTrans = conn.BeginTransaction();
            name = safeParam(name);
            memberClass = safeParam(memberClass);
            rank = safeParam(rank);
            active = safeParam(active);

            string sql = String.Format("INSERT INTO roster (name, class, rank, active) VALUES ('{0}', '{1}', '{2}', '{3}');", name, memberClass, rank, active);
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            int result = command.ExecuteNonQuery();
            dbTrans.Commit();
            return result;
        }

        public int insertLootEntry(string date, string name, string shortEventName, string item, string slot, string rot, string altLoot) {
            DbTransaction dbTrans = conn.BeginTransaction();
            date = safeParam(date);
            name = safeParam(name);
            shortEventName = safeParam(shortEventName);
            item = safeParam(item);
            slot = safeParam(slot);
            rot = safeParam(rot);
            altLoot = safeParam(altLoot);

            string sql = String.Format("INSERT INTO loot (loot_date, name, short_event_name, item, slot, rot, alt_loot) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');",
                date, name, shortEventName, item, slot, rot, altLoot);
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            int result = command.ExecuteNonQuery();
            dbTrans.Commit();
            return result;
        }

        public void bulkInsert(List<BulkLoader> rows, string table) {
            DateTime start = DateTime.Now;
            int colCount = 0;
            using (DbTransaction dbTrans = conn.BeginTransaction()) {
                using (DbCommand cmd = conn.CreateCommand()) {
                    switch (table) {
                        case "loot":
                            cmd.CommandText = "INSERT INTO loot (loot_date, name, short_event_name, item, slot, rot, alt_loot) VALUES(?, ?, ?, ?, ?, ? ,?)";
                            colCount = 7;
                            break;

                        case "items":
                            cmd.CommandText = "INSERT INTO items (slot, item, short_event_name, is_global, tier, is_special) VALUES (?, ?, ?, ?, ?, ?);";
                            colCount = 6;
                            break;

                        case "roster":
                            cmd.CommandText = "INSERT INTO roster (name, class, rank, active) VALUES (?, ?, ?, ?);";
                            colCount = 4;
                            break;

                        case "events":
                            cmd.CommandText = "INSERT INTO events (event, short_event_name, tier) VALUES (?, ?, ?);";
                            colCount = 3;
                            break;

                        case "attendance":
                            cmd.CommandText = "INSERT INTO attendance(name, thirty, sixty, ninety) VALUES (?, ?, ?, ?);";
                            colCount = 4;
                            break;

                        case "spell_template":
                            cmd.CommandText = "INSERT INTO spell_template(class, spells, level, priority) VALUES(?, ?, ?, ?);";
                            colCount = 4;
                            break;

                        case "spell_log":
                            cmd.CommandText = "INSERT INTO spell_log(name, class, spells, level, priority, has_spell) VALUES(?, ?, ?, ?, ?, ?);";
                            colCount = 6;
                            break;

                        default:
                            return;

                    }

                    List<DbParameter> fields = new List<DbParameter>();

                    for (int i = 0; i < colCount; i++) {
                        fields.Add(cmd.CreateParameter());
                    }

                    cmd.Parameters.AddRange(fields.ToArray());

                    foreach (BulkLoader l in rows) {
                        string[] columns = l.getColumnArray();
                        for (int i = 0; i < colCount; i++) {
                            fields[i].Value = columns[i].Trim();
                        }

                        cmd.ExecuteNonQuery();
                    }
                }

                dbTrans.Commit();
            }

            logger.Debug(String.Format("Bulk insert on {0} completed in {1}...", table, DateTime.Now - start));
        }

        public void bulkUpdate(List<BulkUpdater> rows, string sql) {
            if (rows.Count == 0)
                return;

            int colCount = rows[0].getColumnArray().Length;

            DateTime start = DateTime.Now;
            using (DbTransaction dbTrans = conn.BeginTransaction()) {
                using (DbCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = sql;

                    List<DbParameter> fields = new List<DbParameter>();

                    for (int i = 0; i < colCount; i++) {
                        fields.Add(cmd.CreateParameter());
                    }

                    cmd.Parameters.AddRange(fields.ToArray());

                    foreach (BulkUpdater l in rows) {
                        string[] columns = l.getColumnArray();
                        for (int i = 0; i < colCount; i++) {
                            fields[i].Value = columns[i];
                        }

                        cmd.ExecuteNonQuery();
                    }
                }

                dbTrans.Commit();
            }

            logger.Debug(String.Format("Bulk update ({0}) completed in {1}...", sql, DateTime.Now - start));
        }

        public int insertEventEntry(string eventName, string shortEventName, string tier) {
            DbTransaction dbTrans = conn.BeginTransaction();
            eventName = safeParam(eventName);
            shortEventName = safeParam(shortEventName);
            tier = safeParam(tier);

            string sql = String.Format("INSERT INTO events (event, short_event_name, tier) VALUES ('{0}', '{1}', '{2}');", eventName, shortEventName, tier);
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            int result = command.ExecuteNonQuery();
            dbTrans.Commit();
            return result;
        }

        public int insertItemEntry(string slot, string item, string shortEventName, string isGlobal, string tier, string is_special) {
            DbTransaction dbTrans = conn.BeginTransaction();
            slot = safeParam(slot);
            item = safeParam(item);
            shortEventName = safeParam(shortEventName);
            isGlobal = safeParam(isGlobal);
            tier = safeParam(tier);
            is_special = safeParam(is_special);

            string sql = String.Format("INSERT INTO items (slot, item, short_event_name, is_global, tier) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", slot, item, shortEventName, isGlobal, tier, is_special);
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            int result = command.ExecuteNonQuery();
            dbTrans.Commit();
            return result;
        }

        public DbDataReader executeQuery(string sql) {
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            return command.ExecuteReader();
        }
    }

    public interface BulkLoader {
        string[] getColumnArray();
    }

    public interface BulkUpdater {
        string[] getColumnArray();

        string[] getFilterArray();
    }
}
