using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vs_Recorder_Tool
{
    public partial class FormGameSelect : Form
    {
        public FormGameSelect()
        {InitializeComponent();}

        private void GameSelectButtonPt_CheckedChanged(object sender, EventArgs e)
        { Functions.CurrentGame = 0; }

        private void GameSelectButtonHGSS_CheckedChanged(object sender, EventArgs e)
        { Functions.CurrentGame = 1; }

        private void GameSelectButtonBW_CheckedChanged(object sender, EventArgs e)
        { Functions.CurrentGame = 2; }

        private void GameSelectButtonB2W2_CheckedChanged(object sender, EventArgs e)
        { Functions.CurrentGame = 3; }

        private void GameSelectButtonContinue_Click(object sender, EventArgs e)
        { Close(); }
    }
}
