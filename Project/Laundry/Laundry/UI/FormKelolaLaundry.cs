using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Laundry.Model;

namespace Laundry.UI
{
    public partial class FormKelolaLaundry : Form
    {
        private const string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Elrico coding dekstop/Project/Laundry/Laundry/DB/DB_Project.accdb;Persist Security Info=False;";

        readonly OleDbConnection dbConn = new OleDbConnection(connectString);
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        readonly DataTable dt = new DataTable();
        public FormKelolaLaundry()
        {
            InitializeComponent();
        }

        private void add(Laundries obj)
        {
            const string sql = "insert into Laundries(Nama_Customer, Jenis, Berat, Harga_Total, Tanggal_Pengembalian) values(@namaCustomer, @jenis, @berat, @hargaTotal, @tanggalPengembalian)";
            cmd = new OleDbCommand(sql, dbConn);
            cmd.Parameters.AddWithValue("@nama", obj.NamaCustomer);
            cmd.Parameters.AddWithValue("@jenis", obj.Jenis);
            cmd.Parameters.AddWithValue("@berat", obj.Berat);
            cmd.Parameters.AddWithValue("@harga", obj.HargaTotal);
            cmd.Parameters.AddWithValue("@tanggal", obj.TanggalPengembalian);

            try
            {
                dbConn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Berhasil menyimpan data ke database");
                }
                dbConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dbConn.Close();
            }
        }

        private void update(Laundries obj)
        {



            string sql = "update Laundries set Jenis='" + obj.Jenis + "', Berat='" + obj.Berat + "', Harga_Total='" + obj.HargaTotal + "', Tanggal_Pengembalian='" + obj.TanggalPengembalian + "' where Nama_Customer='" + obj.NamaCustomer + "'";
            cmd = new OleDbCommand(sql, dbConn);


            try
            {
                dbConn.Open();
                adapter = new OleDbDataAdapter(cmd) { UpdateCommand = dbConn.CreateCommand() };

                adapter.UpdateCommand.CommandText = sql;
                if (adapter.UpdateCommand.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Berhasil mengupdate data ke database");
                }
                dbConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dbConn.Close();
            }
        }

        private void delete(Laundries obj)
        {
            string sql = "delete from Laundries where Nama_Customer='" + obj.NamaCustomer + "'";
            cmd = new OleDbCommand(sql, dbConn);

            try
            {
                dbConn.Open();
                adapter = new OleDbDataAdapter(cmd) { DeleteCommand = dbConn.CreateCommand() };

                adapter.DeleteCommand.CommandText = sql;
                if (adapter.DeleteCommand.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Berhasil menghapus data yang ada di database");
                }
                dbConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dbConn.Close();
            }
        }

        private void populate(Laundries obj)
        {
            dgvLaundries.Rows.Add(obj.NamaCustomer, obj.Jenis, obj.Berat, obj.HargaTotal, obj.TanggalPengembalian);
        }

        void loadKelolaLaundry(string namaCustomer)
        {
            dgvLaundries.Rows.Clear();
            try
            {
                String sql = "SELECT * FROM Laundries where nama_customer like '%" + namaCustomer + "%' ";
                cmd = new OleDbCommand(sql, dbConn);
                {
                    dbConn.Open(); adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(dt);


                    foreach (DataRow row in dt.Rows)
                    {
                        Laundries obj = new Laundries();
                        obj.NamaCustomer = row[1].ToString();
                        obj.Jenis = row[2].ToString();
                        obj.Berat = Int32.Parse(row[3].ToString());
                        obj.HargaTotal = Int32.Parse(row[4].ToString());
                        obj.TanggalPengembalian = DateTime.Parse(row[5].ToString());
                        populate(obj);
                    }
                    dbConn.Close(); //CLEAR DATATABLE
                    dt.Rows.Clear();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dbConn.Close();
            }
        }



        private void cbJenis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbJenis.Text == "PAKAIAN")
            {
                txtHarga.Text = "7000";
            }
            if (cbJenis.Text == "SELIMUT")
            {
                txtHarga.Text = "10000";
            }
            if (cbJenis.Text == "KARPET")
            {
                txtHarga.Text = "8000";
            }
            if (cbJenis.Text == "SNEAKERS")
            {
                txtHarga.Text = "9000";
            }
        }

        private void btnProses_Click(object sender, EventArgs e)
        {
            int a, b;
            int c = 5000;
            try
            {
                if (txtBerat.Text == "")
                {
                    MessageBox.Show("Inputan tidak valid");
                }
                else if (cbJenis.Text == "")
                {
                    MessageBox.Show("Silahkan pilih sebuah pilihan");
                }
                else if (radioButton1.Checked == false)
                {
                    if (radioButton2.Checked == false)
                    {
                        MessageBox.Show("Silahakan pilih sebuah pilihan");
                    }
                }
                else if (radioButton2.Checked == false)
                {
                    if (radioButton1.Checked == false)
                    {
                        MessageBox.Show("Silahakan pilih sebuah pilihan");
                    }
                }

                a = Int32.Parse(txtBerat.Text);
                b = Int32.Parse(txtHarga.Text);
                c = 5000;

                int total = 0;
                if (radioButton1.Checked)
                {
                    total = (a * b) + (c * 2);
                }
                else if (radioButton2.Checked)
                {
                    total = a * b;
                }
                txtHargaTotal.Text = total.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }

        }

        private void btnHitung_Click(object sender, EventArgs e)
        {
            int p, q;
            int kembalian = 0;
            p = Int32.Parse(txtUangCustomer.Text);
            q = Int32.Parse(txtHargaTotal.Text);
            kembalian = p - q;
            txtKembalian.Text = kembalian.ToString();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Apakah kamu mau menyimpan data?", "Simpan Data", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            int temp;
            if (txtNamaCustomer.Text.Trim().CompareTo("") == 0)
            {
                MessageBox.Show("Nama tidak boleh kosong!!!", "warning", MessageBoxButtons.OK);
            }
            if (cbJenis.Text.Trim().CompareTo("") == 0)
            {
                MessageBox.Show("Silahkan Pilih jenis barang yang ingin di laundry");
            }
            if (txtBerat.Text.Trim().CompareTo("") == 0)
            {
                MessageBox.Show("Inputan Kosong!!! silahkan isi angka...");
            }
            if (dtpTanggal.Value.Date <= DateTime.Now.Date)
            {
                MessageBox.Show("Tanggal Pengambilan tidak valid");
            }
            else 
            {


                int berat = Int32.Parse(txtBerat.Text.Trim());
                int hargaTotal = Int32.Parse(txtHargaTotal.Text.Trim());
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvLaundries);
                row.Cells[0].Value = txtNamaCustomer.Text.Trim();
                row.Cells[1].Value = cbJenis.Text.Trim();
                row.Cells[2].Value = txtBerat.Text.Trim();
                row.Cells[3].Value = txtHargaTotal.Text.Trim();
                row.Cells[4].Value = dtpTanggal.Text.Trim();
                dgvLaundries.Rows.Add(row);
                //add()

                Laundries obj = new Laundries();
                obj.NamaCustomer = txtNamaCustomer.Text.Trim();
                obj.Jenis = cbJenis.Text.Trim();
                obj.Berat = Int32.Parse(txtBerat.Text.Trim());
                obj.HargaTotal = Int32.Parse(txtHargaTotal.Text.Trim());
                obj.TanggalPengembalian = DateTime.Parse(dtpTanggal.Text.Trim());
                add(obj); //simpan data ke dalam database

                MessageBox.Show("Data berhasil disimpan");



            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtCari.Text = dtpTanggal.Text = txtHargaTotal.Text = txtBerat.Text = cbJenis.Text = txtNamaCustomer.Text = txtHarga.Text = txtKembalian.Text = txtUangCustomer.Text = String.Empty;
            radioButton1.Checked = radioButton2.Checked = false;
        }

        private void dgvLaundries_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow baris = dgvLaundries.Rows[e.RowIndex];
            lblidx.Text = e.RowIndex.ToString();
            txtNamaCustomer.Text = baris.Cells[0].Value.ToString();
            cbJenis.Text = baris.Cells[1].Value.ToString();
            txtBerat.Text = baris.Cells[2].Value.ToString();
            txtHargaTotal.Text = baris.Cells[3].Value.ToString();
            dtpTanggal.Text = baris.Cells[4].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Apakah kamu ingin menghapus data ?", "Delete data", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            int temp;
            if (dr == DialogResult.Yes)
            {
                try
                {
                    //dgvCustomer.Rows.RemoveAt(Int32.Parse(lblidx.Text));
                    Laundries obj = new Laundries();
                    obj.NamaCustomer = txtNamaCustomer.Text.Trim();
                    obj.Jenis = cbJenis.Text.Trim();
                    obj.Berat = Int32.Parse(txtBerat.Text.Trim());
                    obj.HargaTotal = Int32.Parse(txtHargaTotal.Text.Trim());
                    obj.TanggalPengembalian = DateTime.Parse(dtpTanggal.Text.Trim());
                    delete(obj); // delete data ke dalam database
                    loadKelolaLaundry("");
                    MessageBox.Show("Data berhasil dihapus");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Anda harus memilih data yang ingin dihapus");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Laundries obj = new Laundries();
            obj.NamaCustomer = txtNamaCustomer.Text.Trim();
            obj.Jenis = cbJenis.Text.Trim();
            obj.Berat = Int32.Parse(txtBerat.Text.Trim());
            obj.HargaTotal = Int32.Parse(txtHargaTotal.Text.Trim());
            obj.TanggalPengembalian = DateTime.Parse(dtpTanggal.Text.Trim());
            update(obj); //update data ke dalam database
            loadKelolaLaundry("");
            MessageBox.Show("Data telah diupdate");
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadKelolaLaundry(txtCari.Text.Trim());
        }

        private void FormKelolaLaundry_Load(object sender, EventArgs e)
        {
            loadKelolaLaundry("");
        }
    }
}
