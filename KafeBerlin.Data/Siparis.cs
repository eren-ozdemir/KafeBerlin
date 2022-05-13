using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeBerlin.Data
{
    public class Siparis
    {
        public int MasaNo { get; set; }
        public SiparisDurum Durum { get; set; } = SiparisDurum.Aktif; // Yazmasak da default olarak 0'a karşılık gelen Aktif değerini alacaktı fakat bu şekilde hem okunurluk hem güvenlik artırılmış oluyor
        public decimal OdenenTutar { get; set; }
        public DateTime? AcilisZamani { get; set; } = DateTime.Now;
        public DateTime? KapanisZamani { get; set; }
        public List<SiparisDetay> SiparisDetaylar { get; set; } = new List<SiparisDetay>();
        public string ToplamTutarTL => $"{ToplamTutar():c2}";

        public decimal ToplamTutar()
        {
            return SiparisDetaylar.Sum(s => s.Tutar());
        }
    }
}
