using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laundry.UI
{
    public partial class FormMainPage : Form
    {
        public FormMainPage()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void laundryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormKelolaLaundry fkl = new FormKelolaLaundry();
            fkl.Show();
        }

        private void FormMainPage_Load(object sender, EventArgs e)
        {

        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormKelolaAdmin fka = new FormKelolaAdmin();
            fka.Show();
        }

        private void pelangganToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDataPelanggan fda = new FormDataPelanggan();
            fda.Show();
        }
    }
}
