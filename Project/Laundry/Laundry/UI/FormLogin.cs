using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laundry.UI;

namespace Laundry
{
    public partial class FormLogin : System.Windows.Forms.Form
    {
        Controller.LoginController cek = new Controller.LoginController();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (cek.loginValidator(txtName.Text, txtPassword.Text))
            {
                MessageBox.Show("Login berhasil");
                FormMainPage fmp = new FormMainPage();
                fmp.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login gagal");
            }
            MessageBox.Show("Nama : " + txtName.Text + " - Password : " + txtPassword.Text);
        }
    }
}
