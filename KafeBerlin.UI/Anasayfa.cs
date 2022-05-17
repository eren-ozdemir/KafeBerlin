﻿using KafeBerlin.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafeBerlin.UI
{
    public partial class Anasayfa : Form
    {
        KafeVeri db = new KafeVeri();
        public Anasayfa()
        {
            VerileriYukle();
            InitializeComponent();
            MasalariYukle();
        }

        private void VerileriYukle()
        {
            try
            {
                string json = File.ReadAllText("data.json");
                db = JsonConvert.DeserializeObject<KafeVeri>(json);
            }
            catch (Exception)
            {
                OrnekUrunleriYukle();
            }
        }

        private void OrnekUrunleriYukle()
        {
            db.Urunler.Add(new Urun() { UrunAdi = "Çay", BirimFiyat = 6.00m });
            db.Urunler.Add(new Urun() { UrunAdi = "Simit", BirimFiyat = 3.5m });
        }

        private void MasalariYukle()
        {
            for (int i = 1; i <= db.MasaAdet; i++)
            {
                var lvi = new ListViewItem($"Masa {i}");
                lvi.ImageKey = db.AktifSiparisler.Any(x => x.MasaNo == i) ? "dolu" : "bos";
                lvi.Tag = i; //masa numarasını daha kolay erişmek için tag'de saklıyoruz
                lvwMasalar.Items.Add(lvi);
            }
        }

        private void lvwMasalar_DoubleClick(object sender, EventArgs e)
        {
            var lvi = lvwMasalar.SelectedItems[0];
            int masaNo = (int)lvi.Tag;
            List<int> vs = new List<int>();
            int a = vs.Find(x => x == masaNo);
            Siparis siparis = db.AktifSiparisler.Find(x => x.MasaNo == masaNo);
            if (siparis == null)
            {
                siparis = new Siparis() { MasaNo = masaNo };
                db.AktifSiparisler.Add(siparis);
                lvi.ImageKey = "dolu";
            }
            DialogResult dr = new SiparisForm(db, siparis).ShowDialog();
            if (dr == DialogResult.OK)
            {
                lvi.ImageKey = "bos";
                lvi.Selected = false;
            }
        }

        private void tsmiUrunler_Click(object sender, EventArgs e)
        {
            new UrunlerForm(db).ShowDialog();
        }

        private void tsmiGecmisSiparisler_Click(object sender, EventArgs e)
        {
            new GecmisSiparislerForm(db).ShowDialog();
        }

        private void Anasayfa_FormClosed(object sender, FormClosedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(db);
            File.WriteAllText("data.json", json);
        }
    }
}
