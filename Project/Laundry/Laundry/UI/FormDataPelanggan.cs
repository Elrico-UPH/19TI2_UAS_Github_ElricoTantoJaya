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
    public partial class FormDataPelanggan : Form
    {
        private const string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Elrico coding dekstop/Project/Laundry/Laundry/DB/DB_Project.accdb;Persist Security Info=False;";

        readonly OleDbConnection dbConn = new OleDbConnection(connectString);
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        readonly DataTable dt = new DataTable();
        public FormDataPelanggan()
        {
            InitializeComponent();
        }


        private void add(Pelanggan obj)
        {
            const string sql = "insert into Pelanggan(Id_Customer, Nama_Customer, Alamat, No_Telepon) values(@idCustomer, @namaCustomer, @alamat, @noTelepon)";
            cmd = new OleDbCommand(sql, dbConn);
            cmd.Parameters.AddWithValue("@idCustomer", obj.IdCustomer);
            cmd.Parameters.AddWithValue("@nama", obj.NamaCustomer);
            cmd.Parameters.AddWithValue("@alamat", obj.Alamat);
            cmd.Parameters.AddWithValue("@noTelepon", obj.NoTelepon);

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

        private void update(Pelanggan obj)
        {


            
            string sql = "update Pelanggan set Nama_Customer='" + obj.NamaCustomer + "', Alamat='" + obj.Alamat + "', No_Telepon='" + obj.NoTelepon + "' where Id_Customer='" + obj.IdCustomer + "'";
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

        private void delete(Pelanggan obj)
        {
            string sql = "delete from Pelanggan where Id_Customer='" + obj.IdCustomer + "'";
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

        private void populate(Pelanggan obj)
        {
            dgvCustomer.Rows.Add(obj.IdCustomer, obj.NamaCustomer, obj.Alamat, obj.NoTelepon);
        }

        void loadDataPelanggan(string namaCustomer)
        {
            dgvCustomer.Rows.Clear();
            try
            {
                String sql = "SELECT * FROM Pelanggan where nama_customer like '%" + namaCustomer + "%' ";
                cmd = new OleDbCommand(sql, dbConn);
                {
                    dbConn.Open(); adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(dt);


                    foreach (DataRow row in dt.Rows)
                    {
                        Pelanggan obj = new Pelanggan();
                        obj.IdCustomer = row[1].ToString();
                        obj.NamaCustomer = row[2].ToString();
                        obj.Alamat = row[3].ToString();
                        obj.NoTelepon = Int32.Parse(row[4].ToString());
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

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Apakah kamu mau menyimpan data?", "Simpan Data", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            int temp;
            if (txtNamaCustomer.Text.Trim().CompareTo("") == 0)
            {
                MessageBox.Show("Nama tidak boleh kosong!!!", "warning", MessageBoxButtons.OK);
            }
            if (txtAlamat.Text.Trim().CompareTo("") == 0)
            {
                MessageBox.Show("Alamat tidak boleh kosong!!!", "warning", MessageBoxButtons.OK);
            }
            if (txtNoTelepon.Text.Trim().CompareTo("") == 0)
            {
                MessageBox.Show("No telepon tidak boleh kosong!!!", "warning", MessageBoxButtons.OK);
            }
            else 
            {
                
                
                    
                    int noTelepon = Int32.Parse(txtNoTelepon.Text.Trim());
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgvCustomer);
                    row.Cells[0].Value = txtIdCustomer.Text.Trim();
                    row.Cells[1].Value = txtNamaCustomer.Text.Trim();
                    row.Cells[2].Value = txtAlamat.Text.Trim();
                    row.Cells[3].Value = txtNoTelepon.Text.Trim();
                    dgvCustomer.Rows.Add(row);
                    //add()

                    Pelanggan obj = new Pelanggan();
                obj.IdCustomer = txtIdCustomer.Text.Trim();
                    obj.NamaCustomer = txtNamaCustomer.Text.Trim();
                    obj.Alamat = txtAlamat.Text.Trim();
                    obj.NoTelepon = Int32.Parse(txtNoTelepon.Text.Trim());
                    add(obj); //simpan data ke dalam database

                    MessageBox.Show("Data berhasil disimpan");
                
           

            }
           
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtCari.Text = txtNoTelepon.Text = txtAlamat.Text = txtNamaCustomer.Text = txtIdCustomer.Text = String.Empty;
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow baris = dgvCustomer.Rows[e.RowIndex];
            lblidx.Text = e.RowIndex.ToString();
            txtIdCustomer.Text = baris.Cells[0].Value.ToString();
            txtNamaCustomer.Text = baris.Cells[1].Value.ToString();
            txtAlamat.Text = baris.Cells[2].Value.ToString();
            txtNoTelepon.Text = baris.Cells[3].Value.ToString();
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
                    Pelanggan obj = new Pelanggan();
                    obj.IdCustomer = txtIdCustomer.Text.Trim();
                    obj.NamaCustomer = txtNamaCustomer.Text.Trim();
                    obj.Alamat = txtAlamat.Text.Trim();
                    obj.NoTelepon = Int32.Parse(txtNoTelepon.Text.Trim());
                    delete(obj); // delete data ke dalam database
                    loadDataPelanggan("");
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
            Pelanggan obj = new Pelanggan();
            obj.IdCustomer = txtIdCustomer.Text.Trim();
            obj.NamaCustomer = txtNamaCustomer.Text.Trim();
            obj.Alamat = txtAlamat.Text.Trim();
            obj.NoTelepon = Int32.Parse(txtNoTelepon.Text.Trim());
            update(obj); //update data ke dalam database
            loadDataPelanggan("");
            MessageBox.Show("Data telah diupdate");
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            loadDataPelanggan(txtCari.Text.Trim());
        }

        private void FormDataPelanggan_Load(object sender, EventArgs e)
        {
            loadDataPelanggan("");
        }
    }
}
