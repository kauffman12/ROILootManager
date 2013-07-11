using System.Windows.Forms;
using System;

namespace ROILootManager {
    public partial class frmLootLog : Form {
        private int startX;
        private int startY;
        
        public frmLootLog() {
            InitializeComponent();
        }

        public frmLootLog(int x, int y) : this() {

            startX = x;
            startY = y;

            Load += new EventHandler(loadForm);
        }

        private void loadForm(object sender, EventArgs e) {
            SetDesktopLocation(startX, startY);
        }

        public DataGridView getView() {
            return dgvLootLog;
        }
    }
}
