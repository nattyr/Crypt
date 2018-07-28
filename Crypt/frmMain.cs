using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypt
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnBrowsePayload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog selectPayloadDialog = new OpenFileDialog())
            {
                selectPayloadDialog.Filter = "Executable files|*.exe";
                if(selectPayloadDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPayload.Text = selectPayloadDialog.FileName;
                }
            }
        }

        private void btnBrowseIcon_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog selectIconloadDialog = new OpenFileDialog())
            {
                selectIconloadDialog.Filter = "Icon files|*.ico";
                if (selectIconloadDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPayload.Text = selectIconloadDialog.FileName;
                }
            }
        }
    }
}
