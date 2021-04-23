using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundry.Model
{
    class Laundries
    {
        string namaCustomer;
        string jenis;
        int berat, hargaTotal;
        int id;
        DateTime tanggalPengembalian;

        public string NamaCustomer { get => namaCustomer; set => namaCustomer = value; }
        public string Jenis { get => jenis; set => jenis = value; }
        public int Berat { get => berat; set => berat = value; }
        public int HargaTotal { get => hargaTotal; set => hargaTotal = value; }
        public int Id { get => id; set => id = value; }
        public DateTime TanggalPengembalian { get => tanggalPengembalian; set => tanggalPengembalian = value; }
    }
}
