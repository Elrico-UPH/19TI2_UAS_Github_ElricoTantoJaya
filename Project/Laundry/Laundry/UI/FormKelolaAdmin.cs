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
    public partial class FormKelolaAdmin : Form
    {
        public FormKelolaAdmin()
        {
            InitializeComponent();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtNama.Text.Trim().CompareTo("") == 0)
            {
                MessageBox.Show("Nama tidak boleh kosong!!!", "Warning", MessageBoxButtons.OK);
            }
            else
            {
                if (dtpTanggalLahir.Value.Year >= DateTime.Now.Year)
                {
                    MessageBox.Show("Tanggal lahir tidak valid!!!");
                }
                else
                {
                    String a = txtNama.Text;
                    String b = txtTempatLahir.Text;
                    String c = dtpTanggalLahir.Value.ToString();
                    String d = rtbAlamat.Text;
                    String jk = String.Empty;
                    if (rbPria.Checked)
                    {
                        jk = "Pria";
                    }
                    if (rbWanita.Checked == true)
                    {
                        jk = "Wanita";
                    }
                    String temp = "Nama : " + a + "\n Tempat Lahir : " + b + "\n Tanggal Lahir : " + c + "\n Jenis Kelamin : " + jk + "\n Alamat : " + d;
                    MessageBox.Show(temp);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtNama.Text = txtTempatLahir.Text = rtbAlamat.Text = String.Empty;
            rbPria.Checked = rbWanita.Checked = false;
            dtpTanggalLahir.Value = DateTime.Now;
        }
    }
}
