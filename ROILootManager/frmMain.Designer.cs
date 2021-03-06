﻿namespace ROILootManager
{
  partial class frmMain
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.mnuMainMenu = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.selectLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadAttendanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.reloadLootLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.addMissingItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.removeLoot = new System.Windows.Forms.Button();
      this.lootLogView = new System.Windows.Forms.ListView();
      this.playerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.itemName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.grpChatLogs = new System.Windows.Forms.GroupBox();
      this.findGuildChat = new System.Windows.Forms.TextBox();
      this.chkGuildLootOnly = new System.Windows.Forms.CheckBox();
      this.chkTellsScroll = new System.Windows.Forms.CheckBox();
      this.lblOfficerTells = new System.Windows.Forms.Label();
      this.txtTells = new System.Windows.Forms.RichTextBox();
      this.chkOfficerScroll = new System.Windows.Forms.CheckBox();
      this.chkGuildScroll = new System.Windows.Forms.CheckBox();
      this.lblOfficerChat = new System.Windows.Forms.Label();
      this.txtOfficerChat = new System.Windows.Forms.RichTextBox();
      this.lblGuildChat = new System.Windows.Forms.Label();
      this.txtGuildChat = new System.Windows.Forms.RichTextBox();
      this.gbAddNewLoot = new System.Windows.Forms.GroupBox();
      this.lblStatus = new System.Windows.Forms.Label();
      this.cmbItems = new System.Windows.Forms.ComboBox();
      this.lblSlot = new System.Windows.Forms.Label();
      this.lblItem = new System.Windows.Forms.Label();
      this.lblEvent = new System.Windows.Forms.Label();
      this.lblName = new System.Windows.Forms.Label();
      this.lblDate = new System.Windows.Forms.Label();
      this.btnAddLoot = new System.Windows.Forms.Button();
      this.dteRaidDate = new System.Windows.Forms.DateTimePicker();
      this.chkAlt = new System.Windows.Forms.CheckBox();
      this.cmbName = new System.Windows.Forms.ComboBox();
      this.chkRot = new System.Windows.Forms.CheckBox();
      this.cmbEvent = new System.Windows.Forms.ComboBox();
      this.cmbSlot = new System.Windows.Forms.ComboBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.lvTierSelection = new System.Windows.Forms.ListView();
      this.clmTiers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.chkIncludeRots = new System.Windows.Forms.CheckBox();
      this.btnParseNames = new System.Windows.Forms.Button();
      this.txtParseNames = new System.Windows.Forms.TextBox();
      this.lvRosterNames = new System.Windows.Forms.ListView();
      this.clmRosterName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.btnLootSummaryClear = new System.Windows.Forms.Button();
      this.btnLootSummaryAll = new System.Windows.Forms.Button();
      this.dgvLootSummary = new System.Windows.Forms.DataGridView();
      this.clmLootSummaryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmLootSummaryAttendance = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmLootSummaryTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmLootSummaryVisibles = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmLootSummaryNonVisibles = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmLootSummaryWeapons = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmLootSummaryRots = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmLootSummarySpecial = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmLootSummaryAlt = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmLootSummaryAltLastLootDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmLootSummaryLastLootDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.dgvVisibleSummary = new System.Windows.Forms.DataGridView();
      this.clmVisibleSummaryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmVisibleSummaryArms = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmVisibleSummaryChest = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmVisibleSummaryFeet = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmVisibleSummaryHands = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmVisibleSummaryHead = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmVisibleSummaryLegs = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmVisibleSummaryWrist = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmVisibleSummaryTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmVisibleSummaryLastLoot = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.dgvNonVisibleSummary = new System.Windows.Forms.DataGridView();
      this.clmNonVisibleSummaryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmNonVisibleSummaryBack = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmNonVisibleSummaryCharm = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmNonVisibleSummaryEar = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmNonVisibleSummaryFace = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmNonVisibleSummaryNeck = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmNonVisibleSummaryRange = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmNonVisibleSummaryRing = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmNonVisibleSummaryShield = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmNonVisibleSummaryShoulders = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmNonVisibleSummaryWaist = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmNonVisibleSummaryTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmNonVisibleSummaryLastLootDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tabPage5 = new System.Windows.Forms.TabPage();
      this.dgvWeaponSummary = new System.Windows.Forms.DataGridView();
      this.clmWeaponSummaryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmWeaponSummary1hb = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmWeaponSummary1hp = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmWeaponSummary1hs = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmWeaponSummary2hb = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmWeaponSummary2hp = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmWeaponSummary2hs = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmWeaponSummaryhth = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmWeaponSummaryTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmWeaponSummaryLastLoot = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmSummaryLastLootDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmSummaryRot = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmSummaryWeapons = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmSummaryNonVisibles = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmSummaryVisibleTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmSummaryLoot = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmAttendance = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmSummaryname = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.mnuMainMenu.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.grpChatLogs.SuspendLayout();
      this.gbAddNewLoot.SuspendLayout();
      this.tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvLootSummary)).BeginInit();
      this.tabPage3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvVisibleSummary)).BeginInit();
      this.tabPage4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvNonVisibleSummary)).BeginInit();
      this.tabPage5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvWeaponSummary)).BeginInit();
      this.SuspendLayout();
      // 
      // mnuMainMenu
      // 
      this.mnuMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionsToolStripMenuItem});
      this.mnuMainMenu.Location = new System.Drawing.Point(0, 0);
      this.mnuMainMenu.Name = "mnuMainMenu";
      this.mnuMainMenu.Size = new System.Drawing.Size(1184, 24);
      this.mnuMainMenu.TabIndex = 0;
      this.mnuMainMenu.Text = "Main Menu";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectLogFileToolStripMenuItem,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // selectLogFileToolStripMenuItem
      // 
      this.selectLogFileToolStripMenuItem.Name = "selectLogFileToolStripMenuItem";
      this.selectLogFileToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
      this.selectLogFileToolStripMenuItem.Text = "Select Log File";
      this.selectLogFileToolStripMenuItem.Click += new System.EventHandler(this.selectLogFileToolStripMenuItem_Click);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // actionsToolStripMenuItem
      // 
      this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadAttendanceToolStripMenuItem,
            this.reloadLootLogToolStripMenuItem,
            this.addMissingItemsToolStripMenuItem});
      this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
      this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
      this.actionsToolStripMenuItem.Text = "Actions";
      // 
      // loadAttendanceToolStripMenuItem
      // 
      this.loadAttendanceToolStripMenuItem.Name = "loadAttendanceToolStripMenuItem";
      this.loadAttendanceToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
      this.loadAttendanceToolStripMenuItem.Text = "Load Attendance";
      this.loadAttendanceToolStripMenuItem.Click += new System.EventHandler(this.loadAttendanceToolStripMenuItem_Click);
      // 
      // reloadLootLogToolStripMenuItem
      // 
      this.reloadLootLogToolStripMenuItem.Name = "reloadLootLogToolStripMenuItem";
      this.reloadLootLogToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
      this.reloadLootLogToolStripMenuItem.Text = "Reload Loot Log";
      this.reloadLootLogToolStripMenuItem.Click += new System.EventHandler(this.reloadLootLogToolStripMenuItem_Click);
      // 
      // addMissingItemsToolStripMenuItem
      // 
      this.addMissingItemsToolStripMenuItem.Name = "addMissingItemsToolStripMenuItem";
      this.addMissingItemsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
      this.addMissingItemsToolStripMenuItem.Text = "Add Missing Items";
      this.addMissingItemsToolStripMenuItem.Click += new System.EventHandler(this.addMissingItemsToolStripMenuItem_Click);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Controls.Add(this.tabPage4);
      this.tabControl1.Controls.Add(this.tabPage5);
      this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tabControl1.Location = new System.Drawing.Point(0, 27);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(1190, 766);
      this.tabControl1.TabIndex = 1;
      // 
      // tabPage1
      // 
      this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
      this.tabPage1.Controls.Add(this.groupBox1);
      this.tabPage1.Controls.Add(this.grpChatLogs);
      this.tabPage1.Controls.Add(this.gbAddNewLoot);
      this.tabPage1.Location = new System.Drawing.Point(4, 25);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(1182, 737);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Add Loot";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.removeLoot);
      this.groupBox1.Controls.Add(this.lootLogView);
      this.groupBox1.Location = new System.Drawing.Point(8, 6);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(335, 341);
      this.groupBox1.TabIndex = 11;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Items Looted";
      // 
      // removeLoot
      // 
      this.removeLoot.Location = new System.Drawing.Point(202, 289);
      this.removeLoot.Name = "removeLoot";
      this.removeLoot.Size = new System.Drawing.Size(98, 34);
      this.removeLoot.TabIndex = 1;
      this.removeLoot.Text = "Clear";
      this.removeLoot.UseVisualStyleBackColor = true;
      this.removeLoot.Click += new System.EventHandler(this.removeLoot_Click);
      // 
      // lootLogView
      // 
      this.lootLogView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.playerName,
            this.itemName});
      this.lootLogView.FullRowSelect = true;
      this.lootLogView.HideSelection = false;
      this.lootLogView.Location = new System.Drawing.Point(6, 26);
      this.lootLogView.MultiSelect = false;
      this.lootLogView.Name = "lootLogView";
      this.lootLogView.Size = new System.Drawing.Size(323, 247);
      this.lootLogView.TabIndex = 0;
      this.lootLogView.UseCompatibleStateImageBehavior = false;
      this.lootLogView.View = System.Windows.Forms.View.Details;
      // 
      // playerName
      // 
      this.playerName.Text = "Player Name";
      this.playerName.Width = 129;
      // 
      // itemName
      // 
      this.itemName.Text = "Item";
      this.itemName.Width = 188;
      // 
      // grpChatLogs
      // 
      this.grpChatLogs.Controls.Add(this.findGuildChat);
      this.grpChatLogs.Controls.Add(this.chkGuildLootOnly);
      this.grpChatLogs.Controls.Add(this.chkTellsScroll);
      this.grpChatLogs.Controls.Add(this.lblOfficerTells);
      this.grpChatLogs.Controls.Add(this.txtTells);
      this.grpChatLogs.Controls.Add(this.chkOfficerScroll);
      this.grpChatLogs.Controls.Add(this.chkGuildScroll);
      this.grpChatLogs.Controls.Add(this.lblOfficerChat);
      this.grpChatLogs.Controls.Add(this.txtOfficerChat);
      this.grpChatLogs.Controls.Add(this.lblGuildChat);
      this.grpChatLogs.Controls.Add(this.txtGuildChat);
      this.grpChatLogs.Location = new System.Drawing.Point(349, 6);
      this.grpChatLogs.Name = "grpChatLogs";
      this.grpChatLogs.Size = new System.Drawing.Size(827, 723);
      this.grpChatLogs.TabIndex = 10;
      this.grpChatLogs.TabStop = false;
      this.grpChatLogs.Text = "Chat Logs (No Log Selected)";
      // 
      // findGuildChat
      // 
      this.findGuildChat.Location = new System.Drawing.Point(678, 17);
      this.findGuildChat.Name = "findGuildChat";
      this.findGuildChat.Size = new System.Drawing.Size(138, 23);
      this.findGuildChat.TabIndex = 17;
      this.findGuildChat.TextChanged += new System.EventHandler(this.findGuildChat_TextChanged);
      this.findGuildChat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.findGuildChat_KeyPress);
      // 
      // chkGuildLootOnly
      // 
      this.chkGuildLootOnly.AutoSize = true;
      this.chkGuildLootOnly.Checked = true;
      this.chkGuildLootOnly.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkGuildLootOnly.Enabled = false;
      this.chkGuildLootOnly.Location = new System.Drawing.Point(234, 23);
      this.chkGuildLootOnly.Name = "chkGuildLootOnly";
      this.chkGuildLootOnly.Size = new System.Drawing.Size(159, 21);
      this.chkGuildLootOnly.TabIndex = 16;
      this.chkGuildLootOnly.Text = "Show Only Loot Chat";
      this.chkGuildLootOnly.UseVisualStyleBackColor = true;
      this.chkGuildLootOnly.CheckedChanged += new System.EventHandler(this.chkGuildLootOnly_CheckedChanged);
      // 
      // chkTellsScroll
      // 
      this.chkTellsScroll.AutoSize = true;
      this.chkTellsScroll.Checked = true;
      this.chkTellsScroll.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkTellsScroll.Location = new System.Drawing.Point(101, 490);
      this.chkTellsScroll.Name = "chkTellsScroll";
      this.chkTellsScroll.Size = new System.Drawing.Size(133, 21);
      this.chkTellsScroll.TabIndex = 12;
      this.chkTellsScroll.Text = "Scroll to bottom?";
      this.chkTellsScroll.UseVisualStyleBackColor = true;
      this.chkTellsScroll.CheckedChanged += new System.EventHandler(this.chkTellsScroll_CheckedChanged);
      // 
      // lblOfficerTells
      // 
      this.lblOfficerTells.AutoSize = true;
      this.lblOfficerTells.Location = new System.Drawing.Point(6, 491);
      this.lblOfficerTells.Name = "lblOfficerTells";
      this.lblOfficerTells.Size = new System.Drawing.Size(76, 17);
      this.lblOfficerTells.TabIndex = 15;
      this.lblOfficerTells.Text = "Your Tells:";
      // 
      // txtTells
      // 
      this.txtTells.Location = new System.Drawing.Point(7, 516);
      this.txtTells.Name = "txtTells";
      this.txtTells.ReadOnly = true;
      this.txtTells.Size = new System.Drawing.Size(812, 200);
      this.txtTells.TabIndex = 13;
      this.txtTells.Text = "";
      // 
      // chkOfficerScroll
      // 
      this.chkOfficerScroll.AutoSize = true;
      this.chkOfficerScroll.Checked = true;
      this.chkOfficerScroll.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkOfficerScroll.Location = new System.Drawing.Point(101, 256);
      this.chkOfficerScroll.Name = "chkOfficerScroll";
      this.chkOfficerScroll.Size = new System.Drawing.Size(133, 21);
      this.chkOfficerScroll.TabIndex = 10;
      this.chkOfficerScroll.Text = "Scroll to bottom?";
      this.chkOfficerScroll.UseVisualStyleBackColor = true;
      this.chkOfficerScroll.CheckedChanged += new System.EventHandler(this.chkOfficerScroll_CheckedChanged);
      // 
      // chkGuildScroll
      // 
      this.chkGuildScroll.AutoSize = true;
      this.chkGuildScroll.Checked = true;
      this.chkGuildScroll.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkGuildScroll.Location = new System.Drawing.Point(95, 23);
      this.chkGuildScroll.Name = "chkGuildScroll";
      this.chkGuildScroll.Size = new System.Drawing.Size(133, 21);
      this.chkGuildScroll.TabIndex = 8;
      this.chkGuildScroll.Text = "Scroll to bottom?";
      this.chkGuildScroll.UseVisualStyleBackColor = true;
      this.chkGuildScroll.CheckedChanged += new System.EventHandler(this.chkGuildScroll_CheckedChanged);
      // 
      // lblOfficerChat
      // 
      this.lblOfficerChat.AutoSize = true;
      this.lblOfficerChat.Location = new System.Drawing.Point(6, 257);
      this.lblOfficerChat.Name = "lblOfficerChat";
      this.lblOfficerChat.Size = new System.Drawing.Size(87, 17);
      this.lblOfficerChat.TabIndex = 10;
      this.lblOfficerChat.Text = "Officer Chat:";
      // 
      // txtOfficerChat
      // 
      this.txtOfficerChat.Location = new System.Drawing.Point(7, 281);
      this.txtOfficerChat.Name = "txtOfficerChat";
      this.txtOfficerChat.ReadOnly = true;
      this.txtOfficerChat.Size = new System.Drawing.Size(812, 200);
      this.txtOfficerChat.TabIndex = 11;
      this.txtOfficerChat.Text = "";
      // 
      // lblGuildChat
      // 
      this.lblGuildChat.AutoSize = true;
      this.lblGuildChat.Location = new System.Drawing.Point(6, 24);
      this.lblGuildChat.Name = "lblGuildChat";
      this.lblGuildChat.Size = new System.Drawing.Size(78, 17);
      this.lblGuildChat.TabIndex = 10;
      this.lblGuildChat.Text = "Guild Chat:";
      // 
      // txtGuildChat
      // 
      this.txtGuildChat.Location = new System.Drawing.Point(7, 46);
      this.txtGuildChat.Name = "txtGuildChat";
      this.txtGuildChat.ReadOnly = true;
      this.txtGuildChat.Size = new System.Drawing.Size(812, 200);
      this.txtGuildChat.TabIndex = 9;
      this.txtGuildChat.Text = "";
      // 
      // gbAddNewLoot
      // 
      this.gbAddNewLoot.Controls.Add(this.lblStatus);
      this.gbAddNewLoot.Controls.Add(this.cmbItems);
      this.gbAddNewLoot.Controls.Add(this.lblSlot);
      this.gbAddNewLoot.Controls.Add(this.lblItem);
      this.gbAddNewLoot.Controls.Add(this.lblEvent);
      this.gbAddNewLoot.Controls.Add(this.lblName);
      this.gbAddNewLoot.Controls.Add(this.lblDate);
      this.gbAddNewLoot.Controls.Add(this.btnAddLoot);
      this.gbAddNewLoot.Controls.Add(this.dteRaidDate);
      this.gbAddNewLoot.Controls.Add(this.chkAlt);
      this.gbAddNewLoot.Controls.Add(this.cmbName);
      this.gbAddNewLoot.Controls.Add(this.chkRot);
      this.gbAddNewLoot.Controls.Add(this.cmbEvent);
      this.gbAddNewLoot.Controls.Add(this.cmbSlot);
      this.gbAddNewLoot.Location = new System.Drawing.Point(8, 353);
      this.gbAddNewLoot.Name = "gbAddNewLoot";
      this.gbAddNewLoot.Size = new System.Drawing.Size(335, 376);
      this.gbAddNewLoot.TabIndex = 8;
      this.gbAddNewLoot.TabStop = false;
      this.gbAddNewLoot.Text = "Add New Loot";
      // 
      // lblStatus
      // 
      this.lblStatus.AutoSize = true;
      this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblStatus.Location = new System.Drawing.Point(185, 348);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(0, 13);
      this.lblStatus.TabIndex = 13;
      // 
      // cmbItems
      // 
      this.cmbItems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cmbItems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cmbItems.FormattingEnabled = true;
      this.cmbItems.Location = new System.Drawing.Point(100, 208);
      this.cmbItems.Name = "cmbItems";
      this.cmbItems.Size = new System.Drawing.Size(200, 24);
      this.cmbItems.TabIndex = 4;
      this.cmbItems.SelectedValueChanged += new System.EventHandler(this.cmbItems_SelectedValueChanged);
      // 
      // lblSlot
      // 
      this.lblSlot.AutoSize = true;
      this.lblSlot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblSlot.Location = new System.Drawing.Point(49, 163);
      this.lblSlot.Name = "lblSlot";
      this.lblSlot.Size = new System.Drawing.Size(41, 17);
      this.lblSlot.TabIndex = 12;
      this.lblSlot.Text = "Slot:";
      this.lblSlot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblItem
      // 
      this.lblItem.AutoSize = true;
      this.lblItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblItem.Location = new System.Drawing.Point(56, 208);
      this.lblItem.Name = "lblItem";
      this.lblItem.Size = new System.Drawing.Size(43, 17);
      this.lblItem.TabIndex = 11;
      this.lblItem.Text = "Item:";
      this.lblItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblEvent
      // 
      this.lblEvent.AutoSize = true;
      this.lblEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblEvent.Location = new System.Drawing.Point(39, 118);
      this.lblEvent.Name = "lblEvent";
      this.lblEvent.Size = new System.Drawing.Size(54, 17);
      this.lblEvent.TabIndex = 10;
      this.lblEvent.Text = "Event:";
      this.lblEvent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblName
      // 
      this.lblName.AutoSize = true;
      this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblName.Location = new System.Drawing.Point(39, 73);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(54, 17);
      this.lblName.TabIndex = 9;
      this.lblName.Text = "Name:";
      this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblDate
      // 
      this.lblDate.AutoSize = true;
      this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblDate.Location = new System.Drawing.Point(10, 32);
      this.lblDate.Name = "lblDate";
      this.lblDate.Size = new System.Drawing.Size(85, 17);
      this.lblDate.TabIndex = 8;
      this.lblDate.Text = "Raid Date:";
      this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // btnAddLoot
      // 
      this.btnAddLoot.Location = new System.Drawing.Point(100, 286);
      this.btnAddLoot.Name = "btnAddLoot";
      this.btnAddLoot.Size = new System.Drawing.Size(200, 40);
      this.btnAddLoot.TabIndex = 7;
      this.btnAddLoot.Text = "Add Loot";
      this.btnAddLoot.UseVisualStyleBackColor = true;
      this.btnAddLoot.Click += new System.EventHandler(this.btnAddLoot_Click);
      // 
      // dteRaidDate
      // 
      this.dteRaidDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dteRaidDate.Location = new System.Drawing.Point(100, 32);
      this.dteRaidDate.Name = "dteRaidDate";
      this.dteRaidDate.Size = new System.Drawing.Size(200, 23);
      this.dteRaidDate.TabIndex = 0;
      this.dteRaidDate.Value = new System.DateTime(2013, 1, 5, 15, 59, 34, 0);
      // 
      // chkAlt
      // 
      this.chkAlt.AutoSize = true;
      this.chkAlt.Location = new System.Drawing.Point(217, 249);
      this.chkAlt.Name = "chkAlt";
      this.chkAlt.Size = new System.Drawing.Size(83, 21);
      this.chkAlt.TabIndex = 6;
      this.chkAlt.Text = "Alt Loot?";
      this.chkAlt.UseVisualStyleBackColor = true;
      this.chkAlt.CheckedChanged += new System.EventHandler(this.chkAlt_CheckedChanged);
      // 
      // cmbName
      // 
      this.cmbName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cmbName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cmbName.FormattingEnabled = true;
      this.cmbName.Location = new System.Drawing.Point(100, 73);
      this.cmbName.Name = "cmbName";
      this.cmbName.Size = new System.Drawing.Size(200, 24);
      this.cmbName.TabIndex = 1;
      // 
      // chkRot
      // 
      this.chkRot.AutoSize = true;
      this.chkRot.Location = new System.Drawing.Point(100, 249);
      this.chkRot.Name = "chkRot";
      this.chkRot.Size = new System.Drawing.Size(89, 21);
      this.chkRot.TabIndex = 5;
      this.chkRot.Text = "Rot Loot?";
      this.chkRot.UseVisualStyleBackColor = true;
      // 
      // cmbEvent
      // 
      this.cmbEvent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cmbEvent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cmbEvent.FormattingEnabled = true;
      this.cmbEvent.Location = new System.Drawing.Point(100, 118);
      this.cmbEvent.Name = "cmbEvent";
      this.cmbEvent.Size = new System.Drawing.Size(200, 24);
      this.cmbEvent.TabIndex = 2;
      this.cmbEvent.SelectedValueChanged += new System.EventHandler(this.cmbEvent_SelectedValueChanged);
      // 
      // cmbSlot
      // 
      this.cmbSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbSlot.FormattingEnabled = true;
      this.cmbSlot.Location = new System.Drawing.Point(100, 163);
      this.cmbSlot.Name = "cmbSlot";
      this.cmbSlot.Size = new System.Drawing.Size(200, 24);
      this.cmbSlot.TabIndex = 3;
      this.cmbSlot.SelectedValueChanged += new System.EventHandler(this.cmbSlot_SelectedValueChanged);
      // 
      // tabPage2
      // 
      this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
      this.tabPage2.Controls.Add(this.lvTierSelection);
      this.tabPage2.Controls.Add(this.chkIncludeRots);
      this.tabPage2.Controls.Add(this.btnParseNames);
      this.tabPage2.Controls.Add(this.txtParseNames);
      this.tabPage2.Controls.Add(this.lvRosterNames);
      this.tabPage2.Controls.Add(this.btnLootSummaryClear);
      this.tabPage2.Controls.Add(this.btnLootSummaryAll);
      this.tabPage2.Controls.Add(this.dgvLootSummary);
      this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tabPage2.Location = new System.Drawing.Point(4, 25);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(1182, 737);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "View Loot";
      // 
      // lvTierSelection
      // 
      this.lvTierSelection.CheckBoxes = true;
      this.lvTierSelection.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmTiers});
      this.lvTierSelection.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      this.lvTierSelection.Location = new System.Drawing.Point(8, 12);
      this.lvTierSelection.Name = "lvTierSelection";
      this.lvTierSelection.ShowGroups = false;
      this.lvTierSelection.Size = new System.Drawing.Size(123, 116);
      this.lvTierSelection.TabIndex = 11;
      this.lvTierSelection.UseCompatibleStateImageBehavior = false;
      this.lvTierSelection.View = System.Windows.Forms.View.Details;
      // 
      // clmTiers
      // 
      this.clmTiers.Width = 100;
      // 
      // chkIncludeRots
      // 
      this.chkIncludeRots.AutoSize = true;
      this.chkIncludeRots.Location = new System.Drawing.Point(204, 12);
      this.chkIncludeRots.Name = "chkIncludeRots";
      this.chkIncludeRots.Size = new System.Drawing.Size(113, 21);
      this.chkIncludeRots.TabIndex = 10;
      this.chkIncludeRots.Text = "Include Rots?";
      this.chkIncludeRots.UseVisualStyleBackColor = true;
      this.chkIncludeRots.CheckedChanged += new System.EventHandler(this.chkIncludeRots_CheckedChanged);
      // 
      // btnParseNames
      // 
      this.btnParseNames.Location = new System.Drawing.Point(1015, 6);
      this.btnParseNames.Name = "btnParseNames";
      this.btnParseNames.Size = new System.Drawing.Size(120, 30);
      this.btnParseNames.TabIndex = 8;
      this.btnParseNames.Text = "Parse Names";
      this.btnParseNames.UseVisualStyleBackColor = true;
      this.btnParseNames.Click += new System.EventHandler(this.btnParseNames_Click);
      // 
      // txtParseNames
      // 
      this.txtParseNames.Location = new System.Drawing.Point(319, 10);
      this.txtParseNames.Name = "txtParseNames";
      this.txtParseNames.Size = new System.Drawing.Size(690, 23);
      this.txtParseNames.TabIndex = 7;
      this.txtParseNames.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtParseNames_KeyPress);
      // 
      // lvRosterNames
      // 
      this.lvRosterNames.CheckBoxes = true;
      this.lvRosterNames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmRosterName});
      this.lvRosterNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lvRosterNames.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      this.lvRosterNames.Location = new System.Drawing.Point(8, 143);
      this.lvRosterNames.Name = "lvRosterNames";
      this.lvRosterNames.Size = new System.Drawing.Size(179, 462);
      this.lvRosterNames.TabIndex = 6;
      this.lvRosterNames.UseCompatibleStateImageBehavior = false;
      this.lvRosterNames.View = System.Windows.Forms.View.Details;
      // 
      // clmRosterName
      // 
      this.clmRosterName.Width = 150;
      // 
      // btnLootSummaryClear
      // 
      this.btnLootSummaryClear.Location = new System.Drawing.Point(137, 98);
      this.btnLootSummaryClear.Name = "btnLootSummaryClear";
      this.btnLootSummaryClear.Size = new System.Drawing.Size(50, 30);
      this.btnLootSummaryClear.TabIndex = 5;
      this.btnLootSummaryClear.Text = "Clear";
      this.btnLootSummaryClear.UseVisualStyleBackColor = true;
      this.btnLootSummaryClear.Click += new System.EventHandler(this.btnLootSummaryClear_Click);
      // 
      // btnLootSummaryAll
      // 
      this.btnLootSummaryAll.Location = new System.Drawing.Point(137, 62);
      this.btnLootSummaryAll.Name = "btnLootSummaryAll";
      this.btnLootSummaryAll.Size = new System.Drawing.Size(50, 30);
      this.btnLootSummaryAll.TabIndex = 4;
      this.btnLootSummaryAll.Text = "All";
      this.btnLootSummaryAll.UseVisualStyleBackColor = true;
      this.btnLootSummaryAll.Click += new System.EventHandler(this.btnLootSummaryAll_Click);
      // 
      // dgvLootSummary
      // 
      this.dgvLootSummary.AllowUserToAddRows = false;
      this.dgvLootSummary.AllowUserToDeleteRows = false;
      this.dgvLootSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvLootSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmLootSummaryName,
            this.clmLootSummaryAttendance,
            this.clmLootSummaryTotal,
            this.clmLootSummaryVisibles,
            this.clmLootSummaryNonVisibles,
            this.clmLootSummaryWeapons,
            this.clmLootSummaryRots,
            this.clmLootSummarySpecial,
            this.clmLootSummaryAlt,
            this.clmLootSummaryAltLastLootDate,
            this.clmLootSummaryLastLootDate});
      this.dgvLootSummary.Location = new System.Drawing.Point(204, 45);
      this.dgvLootSummary.Name = "dgvLootSummary";
      this.dgvLootSummary.ReadOnly = true;
      this.dgvLootSummary.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgvLootSummary.Size = new System.Drawing.Size(972, 560);
      this.dgvLootSummary.TabIndex = 3;
      this.dgvLootSummary.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLootSummary_CellDoubleClick);
      this.dgvLootSummary.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvLootSummary_KeyDown);
      // 
      // clmLootSummaryName
      // 
      this.clmLootSummaryName.HeaderText = "Name";
      this.clmLootSummaryName.Name = "clmLootSummaryName";
      this.clmLootSummaryName.ReadOnly = true;
      // 
      // clmLootSummaryAttendance
      // 
      this.clmLootSummaryAttendance.HeaderText = "Attendance";
      this.clmLootSummaryAttendance.Name = "clmLootSummaryAttendance";
      this.clmLootSummaryAttendance.ReadOnly = true;
      this.clmLootSummaryAttendance.Width = 120;
      // 
      // clmLootSummaryTotal
      // 
      this.clmLootSummaryTotal.HeaderText = "Loot Total";
      this.clmLootSummaryTotal.Name = "clmLootSummaryTotal";
      this.clmLootSummaryTotal.ReadOnly = true;
      this.clmLootSummaryTotal.Width = 75;
      // 
      // clmLootSummaryVisibles
      // 
      this.clmLootSummaryVisibles.HeaderText = "Visibles";
      this.clmLootSummaryVisibles.Name = "clmLootSummaryVisibles";
      this.clmLootSummaryVisibles.ReadOnly = true;
      this.clmLootSummaryVisibles.Width = 75;
      // 
      // clmLootSummaryNonVisibles
      // 
      this.clmLootSummaryNonVisibles.HeaderText = "Non-Visibles";
      this.clmLootSummaryNonVisibles.Name = "clmLootSummaryNonVisibles";
      this.clmLootSummaryNonVisibles.ReadOnly = true;
      this.clmLootSummaryNonVisibles.Width = 90;
      // 
      // clmLootSummaryWeapons
      // 
      this.clmLootSummaryWeapons.HeaderText = "Weapons";
      this.clmLootSummaryWeapons.Name = "clmLootSummaryWeapons";
      this.clmLootSummaryWeapons.ReadOnly = true;
      this.clmLootSummaryWeapons.Width = 80;
      // 
      // clmLootSummaryRots
      // 
      this.clmLootSummaryRots.HeaderText = "Rots";
      this.clmLootSummaryRots.Name = "clmLootSummaryRots";
      this.clmLootSummaryRots.ReadOnly = true;
      this.clmLootSummaryRots.Width = 60;
      // 
      // clmLootSummarySpecial
      // 
      this.clmLootSummarySpecial.HeaderText = "Special";
      this.clmLootSummarySpecial.Name = "clmLootSummarySpecial";
      this.clmLootSummarySpecial.ReadOnly = true;
      this.clmLootSummarySpecial.Width = 60;
      // 
      // clmLootSummaryAlt
      // 
      this.clmLootSummaryAlt.HeaderText = "Alt";
      this.clmLootSummaryAlt.Name = "clmLootSummaryAlt";
      this.clmLootSummaryAlt.ReadOnly = true;
      this.clmLootSummaryAlt.Width = 50;
      // 
      // clmLootSummaryAltLastLootDate
      // 
      this.clmLootSummaryAltLastLootDate.HeaderText = "Alt LL";
      this.clmLootSummaryAltLastLootDate.Name = "clmLootSummaryAltLastLootDate";
      this.clmLootSummaryAltLastLootDate.ReadOnly = true;
      // 
      // clmLootSummaryLastLootDate
      // 
      this.clmLootSummaryLastLootDate.HeaderText = "Last Loot Date";
      this.clmLootSummaryLastLootDate.Name = "clmLootSummaryLastLootDate";
      this.clmLootSummaryLastLootDate.ReadOnly = true;
      // 
      // tabPage3
      // 
      this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
      this.tabPage3.Controls.Add(this.dgvVisibleSummary);
      this.tabPage3.Location = new System.Drawing.Point(4, 25);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Size = new System.Drawing.Size(1182, 737);
      this.tabPage3.TabIndex = 0;
      this.tabPage3.Text = "Visible Summary";
      // 
      // dgvVisibleSummary
      // 
      this.dgvVisibleSummary.AllowUserToAddRows = false;
      this.dgvVisibleSummary.AllowUserToDeleteRows = false;
      this.dgvVisibleSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvVisibleSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmVisibleSummaryName,
            this.clmVisibleSummaryArms,
            this.clmVisibleSummaryChest,
            this.clmVisibleSummaryFeet,
            this.clmVisibleSummaryHands,
            this.clmVisibleSummaryHead,
            this.clmVisibleSummaryLegs,
            this.clmVisibleSummaryWrist,
            this.clmVisibleSummaryTotal,
            this.clmVisibleSummaryLastLoot});
      this.dgvVisibleSummary.Location = new System.Drawing.Point(10, 10);
      this.dgvVisibleSummary.Name = "dgvVisibleSummary";
      this.dgvVisibleSummary.ReadOnly = true;
      this.dgvVisibleSummary.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgvVisibleSummary.Size = new System.Drawing.Size(1160, 590);
      this.dgvVisibleSummary.TabIndex = 0;
      this.dgvVisibleSummary.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVisibleSummary_CellDoubleClick);
      this.dgvVisibleSummary.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvVisibleSummary_CellFormatting);
      // 
      // clmVisibleSummaryName
      // 
      this.clmVisibleSummaryName.HeaderText = "Name";
      this.clmVisibleSummaryName.Name = "clmVisibleSummaryName";
      this.clmVisibleSummaryName.ReadOnly = true;
      // 
      // clmVisibleSummaryArms
      // 
      this.clmVisibleSummaryArms.HeaderText = "Arms";
      this.clmVisibleSummaryArms.Name = "clmVisibleSummaryArms";
      this.clmVisibleSummaryArms.ReadOnly = true;
      this.clmVisibleSummaryArms.Width = 80;
      // 
      // clmVisibleSummaryChest
      // 
      this.clmVisibleSummaryChest.HeaderText = "Chest";
      this.clmVisibleSummaryChest.Name = "clmVisibleSummaryChest";
      this.clmVisibleSummaryChest.ReadOnly = true;
      this.clmVisibleSummaryChest.Width = 80;
      // 
      // clmVisibleSummaryFeet
      // 
      this.clmVisibleSummaryFeet.HeaderText = "Feet";
      this.clmVisibleSummaryFeet.Name = "clmVisibleSummaryFeet";
      this.clmVisibleSummaryFeet.ReadOnly = true;
      this.clmVisibleSummaryFeet.Width = 80;
      // 
      // clmVisibleSummaryHands
      // 
      this.clmVisibleSummaryHands.HeaderText = "Hands";
      this.clmVisibleSummaryHands.Name = "clmVisibleSummaryHands";
      this.clmVisibleSummaryHands.ReadOnly = true;
      this.clmVisibleSummaryHands.Width = 80;
      // 
      // clmVisibleSummaryHead
      // 
      this.clmVisibleSummaryHead.HeaderText = "Head";
      this.clmVisibleSummaryHead.Name = "clmVisibleSummaryHead";
      this.clmVisibleSummaryHead.ReadOnly = true;
      this.clmVisibleSummaryHead.Width = 80;
      // 
      // clmVisibleSummaryLegs
      // 
      this.clmVisibleSummaryLegs.HeaderText = "Legs";
      this.clmVisibleSummaryLegs.Name = "clmVisibleSummaryLegs";
      this.clmVisibleSummaryLegs.ReadOnly = true;
      this.clmVisibleSummaryLegs.Width = 80;
      // 
      // clmVisibleSummaryWrist
      // 
      this.clmVisibleSummaryWrist.HeaderText = "Wrist";
      this.clmVisibleSummaryWrist.Name = "clmVisibleSummaryWrist";
      this.clmVisibleSummaryWrist.ReadOnly = true;
      this.clmVisibleSummaryWrist.Width = 80;
      // 
      // clmVisibleSummaryTotal
      // 
      this.clmVisibleSummaryTotal.HeaderText = "Total";
      this.clmVisibleSummaryTotal.Name = "clmVisibleSummaryTotal";
      this.clmVisibleSummaryTotal.ReadOnly = true;
      this.clmVisibleSummaryTotal.Width = 80;
      // 
      // clmVisibleSummaryLastLoot
      // 
      this.clmVisibleSummaryLastLoot.HeaderText = "Last Visible Date";
      this.clmVisibleSummaryLastLoot.Name = "clmVisibleSummaryLastLoot";
      this.clmVisibleSummaryLastLoot.ReadOnly = true;
      // 
      // tabPage4
      // 
      this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
      this.tabPage4.Controls.Add(this.dgvNonVisibleSummary);
      this.tabPage4.Location = new System.Drawing.Point(4, 25);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(1182, 737);
      this.tabPage4.TabIndex = 2;
      this.tabPage4.Text = "Non-Visible Summary";
      // 
      // dgvNonVisibleSummary
      // 
      this.dgvNonVisibleSummary.AllowUserToAddRows = false;
      this.dgvNonVisibleSummary.AllowUserToDeleteRows = false;
      this.dgvNonVisibleSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvNonVisibleSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmNonVisibleSummaryName,
            this.clmNonVisibleSummaryBack,
            this.clmNonVisibleSummaryCharm,
            this.clmNonVisibleSummaryEar,
            this.clmNonVisibleSummaryFace,
            this.clmNonVisibleSummaryNeck,
            this.clmNonVisibleSummaryRange,
            this.clmNonVisibleSummaryRing,
            this.clmNonVisibleSummaryShield,
            this.clmNonVisibleSummaryShoulders,
            this.clmNonVisibleSummaryWaist,
            this.clmNonVisibleSummaryTotal,
            this.clmNonVisibleSummaryLastLootDate});
      this.dgvNonVisibleSummary.Location = new System.Drawing.Point(10, 10);
      this.dgvNonVisibleSummary.Name = "dgvNonVisibleSummary";
      this.dgvNonVisibleSummary.ReadOnly = true;
      this.dgvNonVisibleSummary.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgvNonVisibleSummary.Size = new System.Drawing.Size(1160, 590);
      this.dgvNonVisibleSummary.TabIndex = 0;
      this.dgvNonVisibleSummary.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNonVisibleSummary_CellDoubleClick);
      this.dgvNonVisibleSummary.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvNonVisibleSummary_CellFormatting);
      // 
      // clmNonVisibleSummaryName
      // 
      this.clmNonVisibleSummaryName.HeaderText = "Name";
      this.clmNonVisibleSummaryName.Name = "clmNonVisibleSummaryName";
      this.clmNonVisibleSummaryName.ReadOnly = true;
      // 
      // clmNonVisibleSummaryBack
      // 
      this.clmNonVisibleSummaryBack.HeaderText = "Back";
      this.clmNonVisibleSummaryBack.Name = "clmNonVisibleSummaryBack";
      this.clmNonVisibleSummaryBack.ReadOnly = true;
      this.clmNonVisibleSummaryBack.Width = 80;
      // 
      // clmNonVisibleSummaryCharm
      // 
      this.clmNonVisibleSummaryCharm.HeaderText = "Charm";
      this.clmNonVisibleSummaryCharm.Name = "clmNonVisibleSummaryCharm";
      this.clmNonVisibleSummaryCharm.ReadOnly = true;
      this.clmNonVisibleSummaryCharm.Width = 80;
      // 
      // clmNonVisibleSummaryEar
      // 
      this.clmNonVisibleSummaryEar.HeaderText = "Ear";
      this.clmNonVisibleSummaryEar.Name = "clmNonVisibleSummaryEar";
      this.clmNonVisibleSummaryEar.ReadOnly = true;
      this.clmNonVisibleSummaryEar.Width = 80;
      // 
      // clmNonVisibleSummaryFace
      // 
      this.clmNonVisibleSummaryFace.HeaderText = "Face";
      this.clmNonVisibleSummaryFace.Name = "clmNonVisibleSummaryFace";
      this.clmNonVisibleSummaryFace.ReadOnly = true;
      this.clmNonVisibleSummaryFace.Width = 80;
      // 
      // clmNonVisibleSummaryNeck
      // 
      this.clmNonVisibleSummaryNeck.HeaderText = "Neck";
      this.clmNonVisibleSummaryNeck.Name = "clmNonVisibleSummaryNeck";
      this.clmNonVisibleSummaryNeck.ReadOnly = true;
      this.clmNonVisibleSummaryNeck.Width = 80;
      // 
      // clmNonVisibleSummaryRange
      // 
      this.clmNonVisibleSummaryRange.HeaderText = "Range";
      this.clmNonVisibleSummaryRange.Name = "clmNonVisibleSummaryRange";
      this.clmNonVisibleSummaryRange.ReadOnly = true;
      this.clmNonVisibleSummaryRange.Width = 80;
      // 
      // clmNonVisibleSummaryRing
      // 
      this.clmNonVisibleSummaryRing.HeaderText = "Ring";
      this.clmNonVisibleSummaryRing.Name = "clmNonVisibleSummaryRing";
      this.clmNonVisibleSummaryRing.ReadOnly = true;
      this.clmNonVisibleSummaryRing.Width = 80;
      // 
      // clmNonVisibleSummaryShield
      // 
      this.clmNonVisibleSummaryShield.HeaderText = "Shield";
      this.clmNonVisibleSummaryShield.Name = "clmNonVisibleSummaryShield";
      this.clmNonVisibleSummaryShield.ReadOnly = true;
      this.clmNonVisibleSummaryShield.Width = 80;
      // 
      // clmNonVisibleSummaryShoulders
      // 
      this.clmNonVisibleSummaryShoulders.HeaderText = "Shoulders";
      this.clmNonVisibleSummaryShoulders.Name = "clmNonVisibleSummaryShoulders";
      this.clmNonVisibleSummaryShoulders.ReadOnly = true;
      this.clmNonVisibleSummaryShoulders.Width = 80;
      // 
      // clmNonVisibleSummaryWaist
      // 
      this.clmNonVisibleSummaryWaist.HeaderText = "Waist";
      this.clmNonVisibleSummaryWaist.Name = "clmNonVisibleSummaryWaist";
      this.clmNonVisibleSummaryWaist.ReadOnly = true;
      this.clmNonVisibleSummaryWaist.Width = 80;
      // 
      // clmNonVisibleSummaryTotal
      // 
      this.clmNonVisibleSummaryTotal.HeaderText = "Total";
      this.clmNonVisibleSummaryTotal.Name = "clmNonVisibleSummaryTotal";
      this.clmNonVisibleSummaryTotal.ReadOnly = true;
      this.clmNonVisibleSummaryTotal.Width = 80;
      // 
      // clmNonVisibleSummaryLastLootDate
      // 
      this.clmNonVisibleSummaryLastLootDate.HeaderText = "Last Non-Visible Date";
      this.clmNonVisibleSummaryLastLootDate.Name = "clmNonVisibleSummaryLastLootDate";
      this.clmNonVisibleSummaryLastLootDate.ReadOnly = true;
      // 
      // tabPage5
      // 
      this.tabPage5.BackColor = System.Drawing.SystemColors.Control;
      this.tabPage5.Controls.Add(this.dgvWeaponSummary);
      this.tabPage5.Location = new System.Drawing.Point(4, 25);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage5.Size = new System.Drawing.Size(1182, 737);
      this.tabPage5.TabIndex = 3;
      this.tabPage5.Text = "Weapon Summary";
      // 
      // dgvWeaponSummary
      // 
      this.dgvWeaponSummary.AllowUserToAddRows = false;
      this.dgvWeaponSummary.AllowUserToDeleteRows = false;
      this.dgvWeaponSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvWeaponSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmWeaponSummaryName,
            this.clmWeaponSummary1hb,
            this.clmWeaponSummary1hp,
            this.clmWeaponSummary1hs,
            this.clmWeaponSummary2hb,
            this.clmWeaponSummary2hp,
            this.clmWeaponSummary2hs,
            this.clmWeaponSummaryhth,
            this.clmWeaponSummaryTotal,
            this.clmWeaponSummaryLastLoot});
      this.dgvWeaponSummary.Location = new System.Drawing.Point(10, 10);
      this.dgvWeaponSummary.Name = "dgvWeaponSummary";
      this.dgvWeaponSummary.ReadOnly = true;
      this.dgvWeaponSummary.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgvWeaponSummary.Size = new System.Drawing.Size(1160, 590);
      this.dgvWeaponSummary.TabIndex = 0;
      this.dgvWeaponSummary.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWeaponSummary_CellDoubleClick);
      this.dgvWeaponSummary.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvWeaponSummary_CellFormatting);
      // 
      // clmWeaponSummaryName
      // 
      this.clmWeaponSummaryName.HeaderText = "Name";
      this.clmWeaponSummaryName.Name = "clmWeaponSummaryName";
      this.clmWeaponSummaryName.ReadOnly = true;
      // 
      // clmWeaponSummary1hb
      // 
      this.clmWeaponSummary1hb.HeaderText = "1HB";
      this.clmWeaponSummary1hb.Name = "clmWeaponSummary1hb";
      this.clmWeaponSummary1hb.ReadOnly = true;
      this.clmWeaponSummary1hb.Width = 80;
      // 
      // clmWeaponSummary1hp
      // 
      this.clmWeaponSummary1hp.HeaderText = "1HP";
      this.clmWeaponSummary1hp.Name = "clmWeaponSummary1hp";
      this.clmWeaponSummary1hp.ReadOnly = true;
      this.clmWeaponSummary1hp.Width = 80;
      // 
      // clmWeaponSummary1hs
      // 
      this.clmWeaponSummary1hs.HeaderText = "1HS";
      this.clmWeaponSummary1hs.Name = "clmWeaponSummary1hs";
      this.clmWeaponSummary1hs.ReadOnly = true;
      this.clmWeaponSummary1hs.Width = 80;
      // 
      // clmWeaponSummary2hb
      // 
      this.clmWeaponSummary2hb.HeaderText = "2HB";
      this.clmWeaponSummary2hb.Name = "clmWeaponSummary2hb";
      this.clmWeaponSummary2hb.ReadOnly = true;
      this.clmWeaponSummary2hb.Width = 80;
      // 
      // clmWeaponSummary2hp
      // 
      this.clmWeaponSummary2hp.HeaderText = "2HP";
      this.clmWeaponSummary2hp.Name = "clmWeaponSummary2hp";
      this.clmWeaponSummary2hp.ReadOnly = true;
      this.clmWeaponSummary2hp.Width = 80;
      // 
      // clmWeaponSummary2hs
      // 
      this.clmWeaponSummary2hs.HeaderText = "2HS";
      this.clmWeaponSummary2hs.Name = "clmWeaponSummary2hs";
      this.clmWeaponSummary2hs.ReadOnly = true;
      this.clmWeaponSummary2hs.Width = 80;
      // 
      // clmWeaponSummaryhth
      // 
      this.clmWeaponSummaryhth.HeaderText = "HTH";
      this.clmWeaponSummaryhth.Name = "clmWeaponSummaryhth";
      this.clmWeaponSummaryhth.ReadOnly = true;
      this.clmWeaponSummaryhth.Width = 80;
      // 
      // clmWeaponSummaryTotal
      // 
      this.clmWeaponSummaryTotal.HeaderText = "Total";
      this.clmWeaponSummaryTotal.Name = "clmWeaponSummaryTotal";
      this.clmWeaponSummaryTotal.ReadOnly = true;
      this.clmWeaponSummaryTotal.Width = 80;
      // 
      // clmWeaponSummaryLastLoot
      // 
      this.clmWeaponSummaryLastLoot.HeaderText = "Last Weapon Date";
      this.clmWeaponSummaryLastLoot.Name = "clmWeaponSummaryLastLoot";
      this.clmWeaponSummaryLastLoot.ReadOnly = true;
      // 
      // clmSummaryLastLootDate
      // 
      this.clmSummaryLastLootDate.HeaderText = "Last Loot Date";
      this.clmSummaryLastLootDate.Name = "clmSummaryLastLootDate";
      this.clmSummaryLastLootDate.ReadOnly = true;
      // 
      // clmSummaryRot
      // 
      this.clmSummaryRot.HeaderText = "Rots";
      this.clmSummaryRot.Name = "clmSummaryRot";
      this.clmSummaryRot.ReadOnly = true;
      // 
      // clmSummaryWeapons
      // 
      this.clmSummaryWeapons.HeaderText = "Weapons";
      this.clmSummaryWeapons.Name = "clmSummaryWeapons";
      this.clmSummaryWeapons.ReadOnly = true;
      // 
      // clmSummaryNonVisibles
      // 
      this.clmSummaryNonVisibles.HeaderText = "Non-Visibles";
      this.clmSummaryNonVisibles.Name = "clmSummaryNonVisibles";
      this.clmSummaryNonVisibles.ReadOnly = true;
      // 
      // clmSummaryVisibleTotal
      // 
      this.clmSummaryVisibleTotal.HeaderText = "Visibles";
      this.clmSummaryVisibleTotal.Name = "clmSummaryVisibleTotal";
      this.clmSummaryVisibleTotal.ReadOnly = true;
      // 
      // clmSummaryLoot
      // 
      this.clmSummaryLoot.HeaderText = "Loot Total";
      this.clmSummaryLoot.Name = "clmSummaryLoot";
      this.clmSummaryLoot.ReadOnly = true;
      // 
      // clmAttendance
      // 
      this.clmAttendance.HeaderText = "Attendance";
      this.clmAttendance.Name = "clmAttendance";
      this.clmAttendance.ReadOnly = true;
      // 
      // clmSummaryname
      // 
      this.clmSummaryname.HeaderText = "Name";
      this.clmSummaryname.Name = "clmSummaryname";
      this.clmSummaryname.ReadOnly = true;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1184, 791);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.mnuMainMenu);
      this.MainMenuStrip = this.mnuMainMenu;
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(1200, 830);
      this.MinimumSize = new System.Drawing.Size(1200, 830);
      this.Name = "frmMain";
      this.Text = "ROI Loot Manager - v0.2";
      this.Resize += new System.EventHandler(this.frmMain_Resize);
      this.mnuMainMenu.ResumeLayout(false);
      this.mnuMainMenu.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.grpChatLogs.ResumeLayout(false);
      this.grpChatLogs.PerformLayout();
      this.gbAddNewLoot.ResumeLayout(false);
      this.gbAddNewLoot.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvLootSummary)).EndInit();
      this.tabPage3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvVisibleSummary)).EndInit();
      this.tabPage4.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvNonVisibleSummary)).EndInit();
      this.tabPage5.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvWeaponSummary)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMainMenu;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.GroupBox gbAddNewLoot;
    private System.Windows.Forms.Label lblSlot;
    private System.Windows.Forms.Label lblItem;
    private System.Windows.Forms.Label lblEvent;
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.Label lblDate;
    private System.Windows.Forms.Button btnAddLoot;
    private System.Windows.Forms.DateTimePicker dteRaidDate;
    private System.Windows.Forms.CheckBox chkAlt;
    private System.Windows.Forms.ComboBox cmbName;
    private System.Windows.Forms.CheckBox chkRot;
    private System.Windows.Forms.ComboBox cmbEvent;
    private System.Windows.Forms.ComboBox cmbSlot;
    private System.Windows.Forms.GroupBox grpChatLogs;
    private System.Windows.Forms.Label lblGuildChat;
    private System.Windows.Forms.RichTextBox txtGuildChat;
    private System.Windows.Forms.Label lblOfficerChat;
    private System.Windows.Forms.RichTextBox txtOfficerChat;
    private System.Windows.Forms.Label lblOfficerTells;
    private System.Windows.Forms.RichTextBox txtTells;
    private System.Windows.Forms.ToolStripMenuItem selectLogFileToolStripMenuItem;
    private System.Windows.Forms.CheckBox chkOfficerScroll;
    private System.Windows.Forms.CheckBox chkGuildScroll;
    private System.Windows.Forms.CheckBox chkTellsScroll;
    private System.Windows.Forms.ComboBox cmbItems;
    private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Button btnLootSummaryClear;
    private System.Windows.Forms.Button btnLootSummaryAll;
    private System.Windows.Forms.DataGridView dgvLootSummary;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmSummaryLastLootDate;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmSummaryRot;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmSummaryWeapons;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmSummaryNonVisibles;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmSummaryVisibleTotal;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmSummaryLoot;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmAttendance;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmSummaryname;
    private System.Windows.Forms.ToolStripMenuItem loadAttendanceToolStripMenuItem;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.ListView lvRosterNames;
    private System.Windows.Forms.ColumnHeader clmRosterName;
    private System.Windows.Forms.DataGridView dgvVisibleSummary;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.DataGridView dgvNonVisibleSummary;
    private System.Windows.Forms.TabPage tabPage5;
    private System.Windows.Forms.DataGridView dgvWeaponSummary;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmVisibleSummaryName;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmVisibleSummaryArms;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmVisibleSummaryChest;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmVisibleSummaryFeet;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmVisibleSummaryHands;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmVisibleSummaryHead;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmVisibleSummaryLegs;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmVisibleSummaryWrist;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmVisibleSummaryTotal;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmVisibleSummaryLastLoot;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmNonVisibleSummaryName;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmNonVisibleSummaryBack;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmNonVisibleSummaryCharm;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmNonVisibleSummaryEar;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmNonVisibleSummaryFace;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmNonVisibleSummaryNeck;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmNonVisibleSummaryRange;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmNonVisibleSummaryRing;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmNonVisibleSummaryShield;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmNonVisibleSummaryShoulders;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmNonVisibleSummaryWaist;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmNonVisibleSummaryTotal;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmNonVisibleSummaryLastLootDate;
    private System.Windows.Forms.ToolStripMenuItem reloadLootLogToolStripMenuItem;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmWeaponSummaryName;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmWeaponSummary1hb;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmWeaponSummary1hp;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmWeaponSummary1hs;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmWeaponSummary2hb;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmWeaponSummary2hp;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmWeaponSummary2hs;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmWeaponSummaryhth;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmWeaponSummaryTotal;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmWeaponSummaryLastLoot;
    private System.Windows.Forms.Button btnParseNames;
    private System.Windows.Forms.TextBox txtParseNames;
    private System.Windows.Forms.ToolStripMenuItem addMissingItemsToolStripMenuItem;
    private System.Windows.Forms.CheckBox chkIncludeRots;
    private System.Windows.Forms.ListView lvTierSelection;
    private System.Windows.Forms.ColumnHeader clmTiers;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmLootSummaryName;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmLootSummaryAttendance;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmLootSummaryTotal;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmLootSummaryVisibles;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmLootSummaryNonVisibles;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmLootSummaryWeapons;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmLootSummaryRots;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmLootSummarySpecial;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmLootSummaryAlt;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmLootSummaryAltLastLootDate;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmLootSummaryLastLootDate;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ListView lootLogView;
    private System.Windows.Forms.ColumnHeader playerName;
    private System.Windows.Forms.ColumnHeader itemName;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Button removeLoot;
    private System.Windows.Forms.CheckBox chkGuildLootOnly;
    private System.Windows.Forms.TextBox findGuildChat;
  }
}

