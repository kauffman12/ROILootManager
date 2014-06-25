namespace ROILootManager
{
    partial class frmLootLog
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
            this.dgvLootLog = new System.Windows.Forms.DataGridView();
            this.clmLootLogDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLootLogEvent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLootLogItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLootLogSlot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLootLogRot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLootLogAltLoot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLootLogSpecialLoot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLootLog)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLootLog
            // 
            this.dgvLootLog.AllowUserToAddRows = false;
            this.dgvLootLog.AllowUserToDeleteRows = false;
            this.dgvLootLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLootLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmLootLogDate,
            this.clmLootLogEvent,
            this.clmLootLogItem,
            this.clmLootLogSlot,
            this.clmLootLogRot,
            this.clmLootLogAltLoot,
            this.clmLootLogSpecialLoot});
            this.dgvLootLog.Location = new System.Drawing.Point(13, 12);
            this.dgvLootLog.Margin = new System.Windows.Forms.Padding(4);
            this.dgvLootLog.Name = "dgvLootLog";
            this.dgvLootLog.ReadOnly = true;
            this.dgvLootLog.Size = new System.Drawing.Size(755, 380);
            this.dgvLootLog.TabIndex = 0;
            // 
            // clmLootLogDate
            // 
            this.clmLootLogDate.HeaderText = "Date";
            this.clmLootLogDate.Name = "clmLootLogDate";
            this.clmLootLogDate.ReadOnly = true;
            // 
            // clmLootLogEvent
            // 
            this.clmLootLogEvent.HeaderText = "Event";
            this.clmLootLogEvent.Name = "clmLootLogEvent";
            this.clmLootLogEvent.ReadOnly = true;
            // 
            // clmLootLogItem
            // 
            this.clmLootLogItem.HeaderText = "Item";
            this.clmLootLogItem.Name = "clmLootLogItem";
            this.clmLootLogItem.ReadOnly = true;
            // 
            // clmLootLogSlot
            // 
            this.clmLootLogSlot.HeaderText = "Slot";
            this.clmLootLogSlot.Name = "clmLootLogSlot";
            this.clmLootLogSlot.ReadOnly = true;
            // 
            // clmLootLogRot
            // 
            this.clmLootLogRot.HeaderText = "Rot";
            this.clmLootLogRot.Name = "clmLootLogRot";
            this.clmLootLogRot.ReadOnly = true;
            // 
            // clmLootLogAltLoot
            // 
            this.clmLootLogAltLoot.HeaderText = "Alt Loot";
            this.clmLootLogAltLoot.Name = "clmLootLogAltLoot";
            this.clmLootLogAltLoot.ReadOnly = true;
            // 
            // clmLootLogSpecialLoot
            // 
            this.clmLootLogSpecialLoot.HeaderText = "Is Special";
            this.clmLootLogSpecialLoot.Name = "clmLootLogSpecialLoot";
            this.clmLootLogSpecialLoot.ReadOnly = true;
            this.clmLootLogSpecialLoot.Width = 75;
            // 
            // frmLootLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 416);
            this.Controls.Add(this.dgvLootLog);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmLootLog";
            this.Text = "Loot Log";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLootLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLootLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLootLogDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLootLogEvent;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLootLogItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLootLogSlot;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLootLogRot;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLootLogAltLoot;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLootLogSpecialLoot;
    }
}