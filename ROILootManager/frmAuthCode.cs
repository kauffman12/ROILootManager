using System;
using System.Diagnostics;
using System.Windows.Forms;
using Google.GData.Client;
using log4net;

namespace ROILootManager {
    public partial class frmAuthCode : Form {
        private static ILog logger = LogManager.GetLogger(typeof(frmAuthCode));

        public frmAuthCode() {
            InitializeComponent();
            logger.Info("Authentication code form initialized.");
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (!txtAuthCode.Text.Equals("")) {
                logger.Debug("OK pressed.");
                Close();
            } else {
                MessageBox.Show("Please enter the Authorization code.");
                logger.Info("No Authorization code entered.");
            }
        }

        public static String getAuthCode(OAuth2Parameters parameters) {
            MessageBox.Show("About to launch a browser to get the Authorization Code. Please copy and paste the code given into the applciation.");
            string authorizationUrl = OAuthUtil.CreateOAuth2AuthorizationUrl(parameters);
            logger.Debug("Auth URL: " + authorizationUrl);
            Process.Start(authorizationUrl);

            frmAuthCode frm = new frmAuthCode();
            frm.ShowDialog();

            return frm.txtAuthCode.Text;
        }

        private void frmAuthCode_Load(object sender, EventArgs e) {

        }

        private void txtAuthCode_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar.Equals('\r')) {
                btnOK_Click(sender, e);
            }
        }

    }
}
