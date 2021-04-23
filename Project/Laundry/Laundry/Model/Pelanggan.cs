using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundry.Model
{
    class Pelanggan
    {
        string namaCustomer, alamat, idCustomer;
        int noTelepon;
        int id;

        public string NamaCustomer { get => namaCustomer; set => namaCustomer = value; }
        public string Alamat { get => alamat; set => alamat = value; }
        public string IdCustomer { get => idCustomer; set => idCustomer = value; }
        public int NoTelepon { get => noTelepon; set => noTelepon = value; }
        public int Id { get => id; set => id = value; }
    }
}
