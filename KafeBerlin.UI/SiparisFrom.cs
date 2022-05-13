﻿using KafeBerlin.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafeBerlin.UI
{
    public partial class SiparisForm : Form
    {
        private readonly KafeVeri _db;
        private readonly Siparis _siparis;
        BindingList<SiparisDetay> _blSiparisDetaylar;

        public SiparisForm(KafeVeri db, Siparis siparis)
        {
            _db = db;
            _siparis = siparis;
            InitializeComponent();
            MasaNoGuncelle();
            UrunleriListele();
            OdemeTutatiGuncelle();
            DetaylariListele();
        }

        private void DetaylariListele()
        {
            _blSiparisDetaylar = new BindingList<SiparisDetay>(_siparis.SiparisDetaylar);
            dgvDetaylar.DataSource = _blSiparisDetaylar;
            _blSiparisDetaylar.ListChanged += _blSiparisDetaylar_ListChanged;
        }

        private void _blSiparisDetaylar_ListChanged(object sender, ListChangedEventArgs e)
        {
            OdemeTutatiGuncelle();
        }

        private void OdemeTutatiGuncelle()
        {
            lblOdemeTutari.Text = _siparis.ToplamTutarTL;
        }

        private void UrunleriListele()
        {
            cboUrun.DataSource = _db.Urunler;
            cboUrun.DisplayMember = "UrunAdi";
        }

        private void MasaNoGuncelle()
        {
            Text = $"Masa {_siparis.MasaNo}";
            lblMasaNo.Text = _siparis.MasaNo.ToString("00");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if(cboUrun.SelectedItem == null)
            {
                MessageBox.Show("Önce bir ürün seçiniz.");
                return;
            }

            Urun urun = (Urun)cboUrun.SelectedItem;

            _blSiparisDetaylar.Add(new SiparisDetay()
            {
                Adet = (int)nudAdet.Value,
                UrunAd = urun.UrunAdi,
                BirimFiyat = urun.BirimFiyat
            });

            OdemeTutatiGuncelle();
        }
    }
}