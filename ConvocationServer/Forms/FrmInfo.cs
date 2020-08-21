using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace ConvocationServer.Forms
{
    public partial class FrmInfo : Form
    {
        private readonly FrmServer _parent;

        public FrmInfo(FrmServer parent)
        {
            InitializeComponent();
            _parent = parent;

            Text = $"{_parent.Text} - Information";

            FileVersionInfo version = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);

            lblName.Text = version.ProductName;
            lblVersion.Text = version.FileVersion;
            lblCompany.Text = version.CompanyName;
            lblCopyright.Text = version.LegalCopyright;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Dispose();
        }
    }
}
