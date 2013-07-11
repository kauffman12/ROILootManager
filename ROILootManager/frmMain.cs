using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using log4net;
using System.Threading;

namespace ROILootManager {
    public partial class frmMain : Form {
        private static ILog logger = LogManager.GetLogger(typeof(frmMain));

        private Roster roster;
        private LootLog lootLog;
        private LogReader logReader;
        private ItemListing itemListing;
        private bool summaryRosterChanging = false;
        private frmLootLog lootLogForm = new frmLootLog();
        private Thread logRefresher;

        public frmMain() {
            InitializeComponent();
            Refresh();
            logger.Info("Main form initialized.");

            try {
                roster = new Roster();
                lootLog = new LootLog();
                logReader = new LogReader();
                logReader.logEvent += new LogReaderEvent(logReaderEvent);
                itemListing = new ItemListing();

                string savedFile = PropertyManager.getManager().getProperty("EQlogFile");
                if (savedFile != null) {
                    if (logReader.setLogFile(savedFile)) {
                        logReader.start();
                        grpChatLogs.Text = String.Format("Chat Logs ({0})", savedFile);
                    }

                }

                logRefresher = new Thread(updateLootLog);
                logRefresher.Start();

                this.Text = "ROI Loot Manager - v" + Constants.PROGRAM_VERSION;

            } catch (Exception e) {
                string message = "Could not get one of the worksheets used. A severe error occured or you may not have access. The program will close.";
                logger.Error(message, e);
                MessageBox.Show(message);
                System.Environment.Exit(-1);
            }

            cmbName.DataSource = roster.getActiveNames();
            cmbName.DisplayMember = Roster.NAME_COL;
            cmbName.ValueMember = Roster.NAME_COL;
            cmbName.SelectedIndex = -1;

            cmbEvent.DataSource = lootLog.getEvents();
            cmbEvent.DisplayMember = "shortName";
            cmbEvent.ValueMember = "shortName";
            cmbEvent.SelectedIndex = -1;

            cmbSlot.DataSource = lootLog.getArmorTypes();
            cmbSlot.DisplayMember = "armorType";
            cmbSlot.ValueMember = "armorType";
            cmbSlot.SelectedIndex = -1;

            dteRaidDate.Value = DateTime.Today;

            updateEqLogControls();

            loadRosterNames();
            //btnLootSummaryAll_Click(null, null);

            dgvLootSummary.SortCompare += new DataGridViewSortCompareEventHandler(lootSummarySorter);
            dgvVisibleSummary.SortCompare += new DataGridViewSortCompareEventHandler(lootSummarySorter);
            dgvNonVisibleSummary.SortCompare += new DataGridViewSortCompareEventHandler(lootSummarySorter);
            dgvWeaponSummary.SortCompare += new DataGridViewSortCompareEventHandler(lootSummarySorter);
            lvRosterNames.ItemChecked += new ItemCheckedEventHandler(lvRosterNames_ItemChecked);

        }

        private void updateLootLog() {
            while (true) {
                lootLog.loadLootInfo();
                //getLootSummary();
                Thread.Sleep(1800000);
                //Thread.Sleep(20000);
            }
        }

        private void updateEqLogControls() {
            switch (lootLog.getLogUri()) {
                case Constants.ROI_ROF_LOOT_URI:
                    miRainOfFear.Checked = true;
                    miTestLogFile.Checked = false;
                    gbAddNewLoot.Text = "Add New Loot (ROF)";
                    break;

                case Constants.ROI_LOOT_TEST_URI:
                    miTestLogFile.Checked = true;
                    miRainOfFear.Checked = false;
                    gbAddNewLoot.Text = "Add New Loot (Test Sheet)";
                    break;
            }
        }

        private void lootSummarySorter(object sender, DataGridViewSortCompareEventArgs e) {
            string col1 = e.CellValue1.ToString();
            string col2 = e.CellValue2.ToString();
            switch (e.Column.Name) {
                case "clmLootSummaryLastLootDate":
                case "clmVisibleSummaryLastLoot":
                case "clmNonVisibleSummaryLastLootDate":
                case "clmWeaponSummaryLastLoot":
                case "clmLootLogDate":
                    col1 = (col1 == null || "".Equals(col1)) ? "1/1/1900" : col1;
                    col2 = (col2 == null || "".Equals(col2)) ? "1/1/1900" : col2;
                    e.SortResult = System.DateTime.Compare(DateTime.Parse(col1), DateTime.Parse(col2));
                    break;

                case "clmLootSummaryTotal":
                case "clmLootSummaryVisibles":
                case "clmLootSummaryNonVisibles":
                case "clmLootSummaryWeapons":
                case "clmLootSummaryRots":
                case "clmLootSummarySpecial":
                case "clmVisibleSummaryTotal":
                case "clmVisibleSummaryWrist":
                case "clmVisibleSummaryLegs":
                case "clmVisibleSummaryHead":
                case "clmVisibleSummaryHands":
                case "clmVisibleSummaryFeet":
                case "clmVisibleSummaryChest":
                case "clmVisibleSummaryArms":
                case "clmNonVisibleSummaryTotal":
                case "clmNonVisibleSummaryWaist":
                case "clmNonVisibleSummaryShoulders":
                case "clmNonVisibleSummaryShield":
                case "clmNonVisibleSummaryRing":
                case "clmNonVisibleSummaryRange":
                case "clmNonVisibleSummaryNeck":
                case "clmNonVisibleSummaryFace":
                case "clmNonVisibleSummaryEar":
                case "clmNonVisibleSummaryCharm":
                case "clmNonVisibleSummaryBack":
                case "clmWeaponSummaryTotal":
                case "clmWeaponSummaryhth":
                case "clmWeaponSummary2hs":
                case "clmWeaponSummary2hp":
                case "clmWeaponSummary2hb":
                case "clmWeaponSummary1hs":
                case "clmWeaponSummary1hp":
                case "clmWeaponSummary1hb":
                    e.SortResult = System.Int32.Parse(col1).CompareTo(System.Int32.Parse(col2));
                    break;

                case "clmLootSummaryAttendance":
                    if (col1.Contains("/"))
                        col1 = col1.Substring(0, col1.IndexOf("/")).Trim().PadLeft(4, ' ');
                    else
                        col1 = "".PadLeft(4, ' ');

                    if (col2.Contains("/"))
                        col2 = col2.Substring(0, col2.IndexOf("/")).Trim().PadLeft(4, ' ');
                    else
                        col2 = "".PadLeft(4, ' ');

                    e.SortResult = System.String.Compare(col1, col2);
                    break;

                default:
                    e.SortResult = System.String.Compare(col1, col2);
                    break;

            }

            e.Handled = true;
        }

        private void loadRosterNames() {
            summaryRosterChanging = true;
            DbDataReader rs = DBManager.getManager().executeQuery("SELECT name FROM roster WHERE active = 'Yes' ORDER BY name");

            lvRosterNames.Items.Clear();

            while (rs.Read()) {
                string name = rs[0].ToString();
                ListViewItem item = new ListViewItem(name);
                item.Text = name;
                item.Name = name;
                lvRosterNames.Items.Add(item);
            }

            rs.Close();
            summaryRosterChanging = false;
        }

        private void getLootSummary() {
            dgvLootSummary.Rows.Clear();
            dgvVisibleSummary.Rows.Clear();
            dgvNonVisibleSummary.Rows.Clear();
            dgvWeaponSummary.Rows.Clear();

            if (lvRosterNames.CheckedItems.Count > 0) {
                StringBuilder selectedNames = new StringBuilder();


                foreach (ListViewItem o in lvRosterNames.CheckedItems) {
                    selectedNames.Append(String.Format("'{0}', ", DBManager.safeParam(o.Text)));
                }




                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT r.name,");
                sql.AppendLine("       (SELECT thirty || ' / ' || sixty || ' / ' || ninety FROM attendance WHERE name = r.name) AS attendance,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes') AS loot_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Arms', 'Chest', 'Feet', 'Hands', 'Head', 'Legs', 'Wrist')) AS visible_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Back', 'Charm', 'Ear', 'Face', 'Neck', 'Range', 'Ring', 'Shield', 'Shoulders', 'Waist')) AS non_visible_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('1HB', '1HP', '1HS', '2HB', '2HP', '2HS', 'HTH')) AS weapon_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot = 'Yes') AS rot_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot AS l WHERE name = r.name AND rot <> 'Yes' AND l.short_event_name = 'NTOV: Vulak''Aerr' AND NOT EXISTS(SELECT 1 FROM items AS i WHERE i.item = l.item AND is_global = 'Yes')) AS vulak_toal,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot AS l, items AS i WHERE name = r.name AND l.rot <> 'Yes' AND UPPER(l.item) = UPPER(i.item) AND i.is_special = 'Yes') AS special_total,");
                sql.AppendLine("       strftime('%m/%d/%Y', (SELECT MAX(loot_date) FROM loot WHERE name = r.name AND rot <> 'Yes')) AS last_loot_date");
                sql.AppendLine("FROM   roster AS r");
                sql.AppendLine("WHERE  r.active = 'Yes'");
                sql.AppendLine("AND    r.name IN (" + selectedNames.ToString().Trim().Trim(',') + ")");
                sql.AppendLine("ORDER BY r.name");

                DbDataReader rs = DBManager.getManager().executeQuery(sql.ToString());

                while (rs.Read()) {
                    dgvLootSummary.Rows.Add(new string[] { rs[0].ToString(), rs[1].ToString(), rs[2].ToString(), rs[3].ToString(), rs[4].ToString(), rs[5].ToString(), rs[6].ToString(), rs[7].ToString(), rs[8].ToString(), rs[9].ToString() });
                }

                rs.Close();


                sql = new StringBuilder();
                sql.AppendLine("SELECT r.name,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Arms')) AS arms_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Chest')) AS chest_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Feet')) AS feet_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Hands')) AS hands_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Head')) AS head_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Legs')) AS legs_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Wrist')) AS wrist_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Arms', 'Chest', 'Feet', 'Hands', 'Head', 'Legs', 'Wrist')) AS visible_total,");
                sql.AppendLine("       strftime('%m/%d/%Y', (SELECT MAX(loot_date) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Arms', 'Chest', 'Feet', 'Hands', 'Head', 'Legs', 'Wrist'))) AS last_visible_loot_date");
                sql.AppendLine("FROM   roster AS r");
                sql.AppendLine("WHERE  r.active = 'Yes'");
                sql.AppendLine("AND    r.name IN (" + selectedNames.ToString().Trim().Trim(',') + ")");
                sql.AppendLine("ORDER BY r.name");

                rs = DBManager.getManager().executeQuery(sql.ToString());

                while (rs.Read()) {
                    dgvVisibleSummary.Rows.Add(new string[] { rs[0].ToString(), rs[1].ToString(), rs[2].ToString(), rs[3].ToString(), rs[4].ToString(), rs[5].ToString(), rs[6].ToString(), rs[7].ToString(), rs[8].ToString(), rs[9].ToString() });
                }

                rs.Close();


                sql = new StringBuilder();
                sql.AppendLine("SELECT r.name,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Back')) AS back_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Charm')) AS charm_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Ear')) AS ear_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Face')) AS face_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Neck')) AS neck_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Range')) AS range_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Ring')) AS ring_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Shield')) AS shield_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Shoulders')) AS shoulders_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Waist')) AS waist_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Back', 'Charm', 'Ear', 'Face', 'Neck', 'Range', 'Ring', 'Shield', 'Shoulders', 'Waist')) AS non_visible_total,");
                sql.AppendLine("       strftime('%m/%d/%Y', (SELECT MAX(loot_date) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('Back', 'Charm', 'Ear', 'Face', 'Neck', 'Range', 'Ring', 'Shield', 'Shoulders', 'Waist'))) AS last_non_vis_loot_date");
                sql.AppendLine("FROM   roster AS r");
                sql.AppendLine("WHERE  r.active = 'Yes'");
                sql.AppendLine("AND    r.name IN (" + selectedNames.ToString().Trim().Trim(',') + ")");
                sql.AppendLine("ORDER BY r.name");

                rs = DBManager.getManager().executeQuery(sql.ToString());

                while (rs.Read()) {
                    dgvNonVisibleSummary.Rows.Add(new string[] { rs[0].ToString(), rs[1].ToString(), rs[2].ToString(), rs[3].ToString(), rs[4].ToString(), rs[5].ToString(), rs[6].ToString(), rs[7].ToString(), rs[8].ToString(), rs[9].ToString(), rs[10].ToString(), rs[11].ToString(), rs[12].ToString() });
                }

                rs.Close();



                sql = new StringBuilder();
                sql.AppendLine("SELECT r.name,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('1HB')) AS blunt_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('1HP')) AS piercing_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('1HS')) AS slash_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('2HB')) AS two_hand_blunt_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('2HP')) AS two_hand_pierce_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('2HS')) AS two_hand_slash_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('HTH')) AS hth_total,");
                sql.AppendLine("       (SELECT COUNT(1) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('1HB', '1HP', '1HS', '2HB', '2HP', '2HS', 'HTH')) AS weapon_total,");
                sql.AppendLine("       strftime('%m/%d/%Y', (SELECT MAX(loot_date) FROM loot WHERE name = r.name AND rot <> 'Yes' AND slot IN ('1HB', '1HP', '1HS', '2HB', '2HP', '2HS', 'HTH'))) AS last_weapon_loot_date");
                sql.AppendLine("FROM   roster AS r");
                sql.AppendLine("WHERE  r.active = 'Yes'");
                sql.AppendLine("AND    r.name IN (" + selectedNames.ToString().Trim().Trim(',') + ")");
                sql.AppendLine("ORDER BY r.name");

                rs = DBManager.getManager().executeQuery(sql.ToString());

                while (rs.Read()) {
                    dgvWeaponSummary.Rows.Add(new string[] { rs[0].ToString(), rs[1].ToString(), rs[2].ToString(), rs[3].ToString(), rs[4].ToString(), rs[5].ToString(), rs[6].ToString(), rs[7].ToString(), rs[8].ToString(), rs[9].ToString() });
                }

                rs.Close();

                // Resort the data grid. Ignore any errors for now
                SortOrder order;
                try {
                    order = dgvLootSummary.SortOrder;
                    if (dgvLootSummary.SortedColumn != null && order != null && !order.Equals(SortOrder.None))
                        dgvLootSummary.Sort(dgvLootSummary.SortedColumn, (order.Equals(SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending));

                    order = dgvVisibleSummary.SortOrder;
                    if (dgvVisibleSummary.SortedColumn != null && order != null && !order.Equals(SortOrder.None))
                        dgvVisibleSummary.Sort(dgvVisibleSummary.SortedColumn, (order.Equals(SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending));

                    order = dgvNonVisibleSummary.SortOrder;
                    if (dgvNonVisibleSummary.SortedColumn != null && order != null && !order.Equals(SortOrder.None))
                        dgvNonVisibleSummary.Sort(dgvNonVisibleSummary.SortedColumn, (order.Equals(SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending));

                    order = dgvWeaponSummary.SortOrder;
                    if (dgvWeaponSummary.SortedColumn != null && order != null && !order.Equals(SortOrder.None))
                        dgvWeaponSummary.Sort(dgvWeaponSummary.SortedColumn, (order.Equals(SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending));
                } catch (Exception e) {
                    logger.Error("Error resorting the data grids ignored.", e);
                }

            }
        }

        private void launchLootLogWindow(string name) {
            lootLogForm.Close();
            lootLogForm = new frmLootLog(Location.X, Location.Y);

            lootLogForm.Text = "Loot Lot for: " + name;
            lootLogForm.getView().SortCompare += new DataGridViewSortCompareEventHandler(lootSummarySorter);

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT strftime('%m/%d/%Y', loot_date),");
            sql.AppendLine("       short_event_name,");
            sql.AppendLine("       item,");
            sql.AppendLine("       slot,");
            sql.AppendLine("       rot,");
            sql.AppendLine("       alt_loot");
            sql.AppendLine("FROM   loot");
            sql.AppendLine("WHERE  name = '" + DBManager.safeParam(name) + "'");
            sql.AppendLine("ORDER BY loot_date");

            DbDataReader rs = DBManager.getManager().executeQuery(sql.ToString());

            while (rs.Read()) {
                lootLogForm.getView().Rows.Add(new string[] { rs[0].ToString(), rs[1].ToString(), rs[2].ToString(), rs[3].ToString(), rs[4].ToString(), rs[5].ToString() });
            }

            rs.Close();

            lootLogForm.ShowDialog();

        }

        private void logReaderEvent(object sender, LogEventArgs e) {
            logger.Debug("logReaderEvent " + e.line);
            TextBox textBox = null;
            CheckBox checkBox = null;

            if (e.type.Equals(LogReader.logTypes.OFFICER_CHAT)) {
                textBox = txtOfficerChat;
                checkBox = chkOfficerScroll;
            } else if (e.type.Equals(LogReader.logTypes.GUILD_CHAT)) {
                textBox = txtGuildChat;
                checkBox = chkGuildScroll;
            }


            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (textBox != null) {
                if (textBox.InvokeRequired) {
                    logger.Debug("Invoking for thread saftey...");
                    LogReaderEvent d = new LogReaderEvent(logReaderEvent);
                    this.Invoke(d, new object[] { sender, e });
                } else {
                    logger.Debug("Setting the text...");
                    // Save the starting positions of the current cursor and selected text
                    int start = textBox.SelectionStart;
                    int len = textBox.SelectionLength;

                    //textBox.Text = newText;
                    textBox.AppendText(Environment.NewLine + e.line);

                    if (checkBox.Checked) {
                        textBox.SelectionStart = textBox.Text.Length;

                    } else {
                        textBox.SelectionStart = start;
                        textBox.SelectionLength = len;
                    }

                    textBox.ScrollToCaret();
                }
            }

        }

        private void filterItemList() {

            string eventName = "";
            string tier = "";

            // Items listing is mostly a convience so
            // any errors cought will be ignored and the list just cleared out
            try {
                if (cmbEvent.SelectedItem != null) {
                    tier = ((EventEntry)cmbEvent.SelectedItem).tier;
                    eventName = ((EventEntry)cmbEvent.SelectedItem).shortName;
                }
                string slot = cmbSlot.Text;

                List<ItemEntry> items = itemListing.getFilteredList(eventName, slot, tier);

                cmbItems.DataSource = items;
                cmbItems.DisplayMember = "itemName";
                cmbItems.ValueMember = "itemName";
                if (items.Count == 1) {
                    cmbItems.SelectedIndex = 0;
                } else {
                    cmbItems.SelectedIndex = -1;
                }

                cmbItems.SelectionStart = 0;
            } catch (Exception e) {
                logger.Error("Error during filtering items will be ignored.", e);
                List<ItemEntry> nullLst = new List<ItemEntry>();
                cmbItems.DataSource = nullLst;
                cmbItems.DisplayMember = "itemName";
                cmbItems.ValueMember = "itemName";

            }
        }

        private void updateSlotEventList() {

            try {
                string item = cmbItems.Text;

                if (item != null || "".Equals(item)) {
                    DbDataReader reader = DBManager.getManager().executeQuery(String.Format("SELECT DISTINCT short_event_name, slot FROM items WHERE item = '{0}';", DBManager.safeParam(item)));
                    int numRows = 0;
                    string eventName = "";
                    string slot = "";
                    if (reader.Read()) {
                        eventName = reader[0].ToString();
                        slot = reader[1].ToString();
                        numRows++;
                    }

                    if (numRows == 1) {
                        if (eventName != null || !"".Equals(eventName)) {
                            //cmbEvent.Text = eventName;
                        }

                        if (slot != null || !"".Equals(slot)) {
                            //cmbSlot.SelectedIndex = cmbSlot.Items.IndexOf(slot);
                            //cmbSlot.Text = slot;

                        }
                    }
                }
            } catch (Exception e) {
                // Ignore errors here
                logger.Error("Could not update combos based on item selection.", e);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }

        private void frmMain_Resize(object sender, EventArgs e) {
            Size tabSize = Size;
            tabSize.Height = tabSize.Height - 60;
            tabSize.Width = tabSize.Width - 10;
            tabControl1.Size = tabSize;
        }

        private void chkAlt_CheckedChanged(object sender, EventArgs e) {
            // If alt loot is checked set the rot loot to checked and disable it
            // If something is an alt loot its always going to be rot.
            if (chkAlt.Checked) {
                chkRot.Checked = true;
                chkRot.Enabled = false;
            } else {
                chkRot.Enabled = true;
            }
        }

        private void btnAddLoot_Click(object sender, EventArgs e) {
            string raidDate = dteRaidDate.Text;
            string name = cmbName.Text;
            string eventName = cmbEvent.Text;
            string slot = cmbSlot.Text;
            string itemName = cmbItems.Text;
            string isRot = chkRot.Checked ? "Yes" : "";
            string isAlt = chkAlt.Checked ? "Yes" : "";

            // Validate required fields
            if (raidDate.Equals("") || name.Equals("") || eventName.Equals("") || slot.Equals("")) {
                MessageBox.Show("Required fields not filled in. Ensure the bold fields have a value.");
            } else {
                try {
                    lootLog.insertNewLootEntry(raidDate, name, eventName, itemName, slot, isRot, isAlt);

                    // Clear some of the data
                    cmbName.SelectedIndex = -1;
                    cmbName.Text = "";
                    cmbSlot.SelectedIndex = -1;
                    cmbSlot.Text = "";
                    cmbItems.SelectedIndex = -1;
                    cmbItems.Text = "";
                    // Update the rot checkbox
                    cmbEvent_SelectedValueChanged(sender, e);
                    chkAlt.Checked = false;

                    MessageBox.Show("Loot added successfully.");
                } catch (Exception ex) {
                    MessageBox.Show("Failed to add the loot entry. Try again in a few seconds. Reason: " + ex.Message);
                }
            }
        }

        private void cmbEvent_SelectedValueChanged(object sender, EventArgs e) {
            EventEntry evt = (EventEntry)cmbEvent.SelectedItem;

            if (evt != null) {
                if (evt.tier.Equals("0") || evt.tier.Equals("1")) {
                    chkRot.Checked = true;
                } else {
                    // TODO Maybe not do this...
                    chkRot.Checked = false;
                }
            }

            filterItemList();
        }

        protected override void OnClosing(CancelEventArgs e) {
            logReader.end();
            logRefresher.Abort();
            base.OnClosing(e);
        }

        private void selectLogFileToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog fd = new OpenFileDialog();

            if (fd.ShowDialog() == DialogResult.OK) {
                logReader.end();
                if (logReader.setLogFile(fd.FileName)) {
                    logReader.start();
                    grpChatLogs.Text = String.Format("Chat Logs ({0})", fd.FileName);
                }
            }
        }

        private void chkGuildScroll_CheckedChanged(object sender, EventArgs e) {
            if (chkGuildScroll.Checked) {
                txtGuildChat.SelectionStart = txtGuildChat.Text.Length;
                txtGuildChat.ScrollToCaret();
            }
        }

        private void chkOfficerScroll_CheckedChanged(object sender, EventArgs e) {
            if (chkOfficerScroll.Checked) {
                txtOfficerChat.SelectionStart = txtOfficerChat.Text.Length;
                txtOfficerChat.ScrollToCaret();
            }
        }

        private void cmbSlot_SelectedValueChanged(object sender, EventArgs e) {
            filterItemList();
        }

        private void cmbItems_SelectedValueChanged(object sender, EventArgs e) {
            updateSlotEventList();
        }

        private void miRainOfFear_Click(object sender, EventArgs e) {
            lootLog.reloadLog(Constants.ROI_ROF_LOOT_URI);
            updateEqLogControls();
        }

        private void miTestLogFile_Click(object sender, EventArgs e) {
            lootLog.reloadLog(Constants.ROI_LOOT_TEST_URI);
            updateEqLogControls();
        }

        private void btnLootSummaryClear_Click(object sender, EventArgs e) {
            summaryRosterChanging = true;
            for (int i = 0; i < lvRosterNames.Items.Count; i++) {
                lvRosterNames.Items[i].Checked = false;
            }
            summaryRosterChanging = false;
            getLootSummary();
        }

        private void btnLootSummaryAll_Click(object sender, EventArgs e) {
            summaryRosterChanging = true;
            for (int i = 0; i < lvRosterNames.Items.Count; i++) {
                lvRosterNames.Items[i].Checked = true;
            }
            summaryRosterChanging = false;
            getLootSummary();
        }

        private void loadAttendanceToolStripMenuItem_Click(object sender, EventArgs e) {
            roster.loadAttendance();
            getLootSummary();
        }

        private void lvRosterNames_ItemChecked(object sender, ItemCheckedEventArgs e) {
            if (!summaryRosterChanging) {
                getLootSummary();
            }
        }

        private void dgvLootSummary_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && e.RowIndex < dgvLootSummary.Rows.Count) {
                launchLootLogWindow(dgvLootSummary.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void dgvVisibleSummary_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && e.RowIndex < dgvVisibleSummary.Rows.Count) {
                launchLootLogWindow(dgvVisibleSummary.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void dgvNonVisibleSummary_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && e.RowIndex < dgvNonVisibleSummary.Rows.Count) {
                launchLootLogWindow(dgvNonVisibleSummary.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void dgvWeaponSummary_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && e.RowIndex < dgvWeaponSummary.Rows.Count) {
                launchLootLogWindow(dgvWeaponSummary.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void reloadLootLogToolStripMenuItem_Click(object sender, EventArgs e) {
            lootLog.loadLootInfo();
            getLootSummary();
        }

        private void dgvLootSummary_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.C) {
                DataGridViewRow row = ((DataGridView)sender).CurrentRow;
                if (row != null && row.Cells.Count > 0) {
                    string str = String.Format("{0}: Attendance: {1}, Total: {2}, Vis: {3}, Non-Vis: {4}, Weapon: {5}, Rot: {6}, Vulak: {7}, Sp: {8}, LL: {9}",
                         row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(),
                           row.Cells[4].Value.ToString(), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), row.Cells[7].Value.ToString(),
                             row.Cells[8].Value.ToString(), row.Cells[9].Value.ToString());

                    Clipboard.SetText(str);
                    e.Handled = true;
                }
            }
        }

        private void btnParseNames_Click(object sender, EventArgs e) {
            string names = txtParseNames.Text;

            if (names != null && !"".Equals(names)) {
                string[] splitSpace = names.Replace(',', ' ').Replace('\'', ' ').Split(' ');
                ListView.ListViewItemCollection items = lvRosterNames.Items;

                btnLootSummaryClear_Click(null, null);

                summaryRosterChanging = true;
                for (int i = 0; i < splitSpace.Length; i++) {
                    string tmpStr = splitSpace[i].Trim();

                    if (!"".Equals(tmpStr)) {
                        foreach (ListViewItem itm in items) {
                            if (itm.Text.StartsWith(tmpStr, StringComparison.InvariantCultureIgnoreCase)) {
                                itm.Checked = true;
                            }
                        }
                    }
                }

                getLootSummary();
                summaryRosterChanging = false;
            }
        }

        private void txtParseNames_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar.Equals('\r')) {
                btnParseNames_Click(sender, e);
            }
        }

        private void dgvVisibleSummary_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            String columnName = dgvVisibleSummary.Columns[e.ColumnIndex].Name;
            if (!"Name".Equals(columnName) && !"Total".Equals(columnName) && !columnName.StartsWith("Last") &&
                !"0".Equals(dgvVisibleSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())) {
                e.CellStyle.BackColor = Color.Green;
            }
        }

        private void addMissingItemsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!lootLog.getLogUri().Equals(Constants.ROI_LOOT_TEST_URI)) {
                itemListing.addMissingItems();
            }
        }

        private void loadFromRosterToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                SpellManager spellManager = new SpellManager();
                int spellsAdded = spellManager.loadSpellLogFromRoster();
                MessageBox.Show(this, "Added " + spellsAdded + " spells.");
            } catch (Exception ex) {
                MessageBox.Show("An error occured loading the spell data. Try loading again.\nError: " + ex);
            }
        }

        private void reloadPrioritiesToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                SpellManager spellManager = new SpellManager();
                int spellsUpdated = spellManager.updateSpellPriority();
                MessageBox.Show(this, "Updated " + spellsUpdated + " spells.");
            } catch (Exception ex) {
                MessageBox.Show("An error occured updating the spell data. Try updating again.\nError: " + ex);
            }
        }
    }
}
