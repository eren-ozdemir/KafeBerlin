using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeBerlin.Data
{
    public class Urun
    {
        public string UrunAdi { get; set; }
        public decimal BirimFiyat { get; set; }

        public override string ToString()
        {
            return UrunAdi;
        }
    }
}
