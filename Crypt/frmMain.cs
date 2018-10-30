using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypt
{
    public partial class frmMain : Form
    {
        Options options;

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
            using (OpenFileDialog selectIconDialog = new OpenFileDialog())
            {
                selectIconDialog.Filter = "Icon files|*.ico";
                if (selectIconDialog.ShowDialog() == DialogResult.OK)
                {
                    txtIcon.Text = selectIconDialog.FileName;
                }
            }
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog selectSaveDialog = new SaveFileDialog())
            {
                selectSaveDialog.Filter = "Executable files|*.exe";
                if (selectSaveDialog.ShowDialog() == DialogResult.OK)
                {
                    options.buildDir = selectSaveDialog.FileName;
                    bool buildResult = Build();
                    DisplayBuildMsg(buildResult);
                }
            }
        }

        private bool Build()
        {
            options.encryptionType = EncryptionType.XOR; //TODO: Add encryption type selection

            Byte[] payloadPE = File.ReadAllBytes(txtPayload.Text);
            Builder builder = new Builder(payloadPE, options);
            bool buildResult = builder.Build();
            return buildResult;
        }

        private void DisplayBuildMsg(bool buildResult)
        {
            string msgBoxTxt = "Build " + (buildResult ? "successful" : "failed");
            MessageBox.Show(msgBoxTxt);
        }
    }
}
