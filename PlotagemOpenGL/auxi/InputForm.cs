using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.auxi
{
    public partial class InputForm : Form
    {
        public string InputValue { get; private set; }

        public InputForm(string prompt)
        {
            InitializeComponent();
            this.Text = prompt;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            InputValue = txtInput.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

}
