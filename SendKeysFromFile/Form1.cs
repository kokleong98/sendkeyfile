using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace SendKeysFromFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = fdPath.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtContent.Text = File.ReadAllText(fdPath.FileName);
            }
        }

        private void btnType_Click(object sender, EventArgs e)
        {
            Thread.Sleep(1000 * (int)numStartDelay.Value);
            StringBuilder translated = new StringBuilder(1000);
            string special = "+%()^{}";
            foreach (var line in File.ReadLines(fdPath.FileName))
            {
                
                foreach(var character in line)
                {
                    if (special.IndexOf(character) >= 0)
                    {
                        translated.Append("{");
                        translated.Append(character);
                        translated.Append("}");
                    }
                    else
                    {
                        translated.Append(character);
                    }
                }
                SendKeys.Send(translated.ToString());
                SendKeys.Send("{ENTER}");
                Thread.Sleep((int)numDelay.Value);
                translated.Length = 0;
            }
            
        }
    }
}
