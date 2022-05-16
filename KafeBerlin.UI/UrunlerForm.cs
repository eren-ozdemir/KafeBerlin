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
    public partial class UrunlerForm : Form
    {
        private readonly KafeVeri _db;
        BindingList<Urun> _blUrunler;
        Urun _duzenlenen = null;

        public UrunlerForm(KafeVeri db)
        {
            
            _db = db;
            InitializeComponent();
            dgvUrunler.AutoGenerateColumns = false;
            _blUrunler = new BindingList<Urun>(_db.Urunler);
            dgvUrunler.DataSource = _blUrunler;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string ad = txtUrunAd.Text.Trim();
            if(ad == "")
            {
                MessageBox.Show("Ürün adı girmediniz");
                return;
            }

            if(_duzenlenen == null)
            {
                _blUrunler.Add(new Urun()
                {
                    UrunAdi = ad,
                    BirimFiyat = nudBirimFiyat.Value
                });
            }
            else
            {
                _duzenlenen.UrunAdi = ad;
                _duzenlenen.BirimFiyat = nudBirimFiyat.Value;
            }
             

            txtUrunAd.Clear();
            nudBirimFiyat.Value = 0;
            Sifirla();
        }

        private void dgvUrunler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var satir = dgvUrunler.Rows[e.RowIndex];
            _duzenlenen = (Urun)satir.DataBoundItem;
            btnEkle.Text = "Kaydet";
            txtUrunAd.Text = _duzenlenen.UrunAdi;
            nudBirimFiyat.Value = _duzenlenen.BirimFiyat;
            dgvUrunler.Enabled = false;
            btnIptal.Visible = true;
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            Sifirla();
        }

        private void Sifirla()
        {
            _duzenlenen = null;
            btnEkle.Text = "Ekle";
            txtUrunAd.Clear();
            nudBirimFiyat.Value = 0;
            dgvUrunler.Enabled = true;
            btnIptal.Visible = false;
        }
    }
}
