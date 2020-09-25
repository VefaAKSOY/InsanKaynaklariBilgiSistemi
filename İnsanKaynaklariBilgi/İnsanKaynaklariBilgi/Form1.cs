using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace İnsanKaynaklariBilgi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int i = 0;
        int k = 0;
        int j = 0;
        
        
        İkiliAramaAgaci ikiliAramaAgaci = new İkiliAramaAgaci();

        DataTable tablo = new DataTable();
        IsDeneyimi isdeneyimi;
        EgitimDurumu egitimdurumu;
        HashIlanlar hashIlanlar = new HashIlanlar();
        HashSirketler hashSirketler = new HashSirketler();
        List<Kisi> KisiListesi = new List<Kisi>();
      
        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            MedeniDurum medeniDurum = new MedeniDurum();
            Kisi kisi = new Kisi();
            kisi.KimlikBilgileri.Ad = txtMusteriAd.Text;
            kisi.KimlikBilgileri.Soyad = txtMusteriSoyad.Text;
            kisi.Adres = txtMusteriAdres.Text;
            kisi.TelNo = txtMusteriTelefon.Text;
            kisi.Eposta = txtMusteriEposta.Text;
            kisi.KimlikBilgileri.Uyruk = txtMusteriUyruk.Text;
            kisi.MusteriNo = 300003 + k;
            k++;
            kisi.KimlikBilgileri.TcKimlikNo = Convert.ToUInt64(txtMusteriTCNo.Text); // ÇALIŞMAZSA İLAN BAŞVUR ' A TC GİRİLSİN
            kisi.KimlikBilgileri.DogumYeri = txtMusteriDYeri.Text;
            kisi.KimlikBilgileri.DogumTarihi = txtMusteriDTarihi.Text; // TODO
            if (chcbxMedeniHal.Checked == true)
                medeniDurum = MedeniDurum.Evli;
            else
                medeniDurum = MedeniDurum.Bekar;
            kisi.KimlikBilgileri.MedeniDurum = medeniDurum;
            kisi.YabanciDil = txtMusteriDilEgitimi.Text.ToUpper();
            kisi.IlgiAlanlari = txtMusteriIlgiAlanlari.Text;
            kisi.Referans = txtMusteriReferansKisiler.Text;
            if (Convert.ToInt32(txtMusteriDeneyim.Text) == 0 || txtMusteriDeneyim.Text == "")
            {
                kisi.Deneyim = 0;
            }
            else if (Convert.ToInt32(txtMusteriDeneyim.Text) >= 2)
            {
                kisi.Deneyim = Convert.ToInt32(txtMusteriDeneyim.Text);
                ikiliAramaAgaci.ikiYilUstu.Add(kisi);
                
            } 
            else
            {
                kisi.Deneyim = 1;
            }

            egitimdurumu = new EgitimDurumu();
            egitimdurumu.OkulAd = txtMusteriMezunOlunanOkul.Text;
            egitimdurumu.Bolum = txtMusteriBolum.Text;
            egitimdurumu.BaslangicYili = txtMusteriBaslangicYili.Text; // TODO
            egitimdurumu.BitisTarihi = txtMusteriBitirmeYili.Text; // TODO
            egitimdurumu.NotOrtalamasi = float.Parse(txtMusteriNotOrtalamasi.Text);
            kisi.EgitimDurumuEkle(egitimdurumu);

            isdeneyimi = new IsDeneyimi();
            isdeneyimi.IsyeriAd = txtMusteriOncekiCalisilanYerler.Text;
            if (Convert.ToInt32(txtMusteriDeneyim.Text) == 0 || txtMusteriDeneyim.Text == "")
            {
                isdeneyimi.CalistigiYil = 0;
            }
            else
            {
                isdeneyimi.CalistigiYil = Convert.ToInt32(txtMusteriCalismaTarihleri.Text);

            }
            
            isdeneyimi.Adres = txtMusteriCalismaAdresleri.Text;
            isdeneyimi.Pozisyon = txtMusteriPozisyon.Text;
            kisi.IsDeneyimiEkle(isdeneyimi);
            ikiliAramaAgaci.Ekle(kisi.KimlikBilgileri.TcKimlikNo);
            KisiListesi.Add(kisi);


            // BOS BIRAKILMAMASI ICIN ;

            if (txtMusteriAd.Text == "")
            {
                MessageBox.Show("AD BOS BIRAKILAMAZ");
            }
            if (txtMusteriSoyad.Text == "")
            {
                MessageBox.Show("SOYAD BOS BIRAKILAMAZ");
            }
            if (txtMusteriNotOrtalamasi.Text == "")
            {
                MessageBox.Show("NOT ORTALAMASI BOS BIRAKILAMAZ");
            }

            MessageBox.Show("Kayıt işlemi tamamlandı...");

        }
        private void btnIlanVer_Click(object sender, EventArgs e)
        {
            Isyeri isyeri = new Isyeri();
            isyeri = hashSirketler.IsyeriGetir(Convert.ToInt32(txtSirketNoIlanVer.Text.ToString()));
            if (isyeri.IsyeriNo == Convert.ToInt32(txtSirketNoIlanVer.Text))
            {
                    isyeri.IlanVer().ArananOzellikler = txtArananElemanOzellikleriIlanVer.Text;
                    isyeri.IlanVer().ArananPozisyon = txtPozisyonIlanVer.Text;
                    isyeri.IlanVer().IlanNo = 100003 + i;
                    i++;
                    isyeri.IlanVer().IsTanimi = txtIsTanitimiIlanVer.Text;//YABANCI DİL,ŞİRKET NO 
                   
                    hashIlanlar.IlanEkle(isyeri.IlanVer().IlanNo, isyeri.IlanVer());
            }
            
        }

        private void btnKayitGuncelle_Click(object sender, EventArgs e)
        {
            
            MedeniDurum medeniDurum = new MedeniDurum();
            // kisi = (Kisi)ikiliAramaAgaci.Ara(Convert.ToUInt64(txtGMusteriTCNo.Text)).veri;
            foreach (Kisi kisi in KisiListesi)
            {
                if (kisi.KimlikBilgileri.TcKimlikNo==Convert.ToUInt64(txtGMusteriTCNo.Text))
                { 
                    kisi.KimlikBilgileri.Ad = txtGMusteriAd.Text;
                    kisi.KimlikBilgileri.Soyad = txtGMusteriSoyad.Text;
                    kisi.Adres = txtGMusteriSoyad.Text;///ARADIĞIMIZ BULANACAK
                    kisi.TelNo = txtGMusteriTelefon.Text;
                    kisi.Eposta = txtGMusteriEposta.Text;
                    kisi.KimlikBilgileri.Uyruk = txtGMusteriUyruk.Text;
                    kisi.KimlikBilgileri.TcKimlikNo = Convert.ToUInt64(txtGMusteriTCNo.Text);
                    kisi.KimlikBilgileri.DogumYeri = txtGMusteriDYeri.Text;
                    kisi.KimlikBilgileri.DogumTarihi = txtGMusteriDTarihi.Text;
                    if (chcbxEvli.Checked == true)
                        medeniDurum = MedeniDurum.Evli;
                    else
                        medeniDurum = MedeniDurum.Bekar;
                    kisi.KimlikBilgileri.MedeniDurum = medeniDurum;
                    kisi.YabanciDil = txtGMusteriDilEgitimi.Text.ToLower();
                    kisi.IlgiAlanlari = txtGMusteriIlgiAlanlari.Text;
                    kisi.Referans = txtGMusteriReferansKisiler.Text;
                    kisi.Deneyim = Convert.ToInt32(txtGMusteriDeneyim.Text.ToString());
                    if (kisi.Deneyim >= 2)
                    {
                        ikiliAramaAgaci.ikiYilUstu.Add(kisi);
                    }
                    
                    //egitimdurumu = kisi.GetEgitimDurumu(kisi.MusteriNo % 100);
                    egitimdurumu.OkulAd = txtGMusteriMezunOlunanOkul.Text.ToString();
                    egitimdurumu.Bolum = txtGMusteriBolum.Text.ToString();
                    egitimdurumu.BaslangicYili = txtGMusteriBaslangicYili.Text.ToString();
                    egitimdurumu.BitisTarihi = txtGMusteriBitirmeYili.Text.ToString();
                    egitimdurumu.NotOrtalamasi = Convert.ToInt32(txtGMusteriNotOrtalamasi.Text.ToString());
                    egitimdurumu = new EgitimDurumu();
                    kisi.EgitimDurumuEkle(egitimdurumu);
                    
                    //Linked listten çekerken esnenin örneğine ayarlanamadı hatası. 
                    //isdeneyimi = kisi.GetIsDeneyimi(kisi.MusteriNo % 100);//Müsterino yüz kişi için son basamağını alıp hep sona eklendiği için positiona atadım .
                    isdeneyimi.IsyeriAd = txtGMusteriOncekiCalistiginizYerler.Text.ToString();
                    isdeneyimi.CalistigiYil = Convert.ToInt32(txtGMusteriCalismaTarihleri.Text.ToString());
                    isdeneyimi.Adres = txtGMusteriCalismaAdresleri.Text.ToString();
                    isdeneyimi.Pozisyon = txtGMusteriPozisyon.Text.ToString();
                    isdeneyimi = new IsDeneyimi();
                    kisi.IsDeneyimiEkle(isdeneyimi);    
                }
            }
                        
            // BOS BIRAKILMAMASI ICIN ;

            if (txtGMusteriAd.Text == "")
            {
                MessageBox.Show("AD BOS BIRAKILAMAZ");
            }
            if (txtGMusteriSoyad.Text == "")
            {
                MessageBox.Show("SOYAD BOS BIRAKILAMAZ");
            }
            if (txtGMusteriNotOrtalamasi.Text == "")
            {
                MessageBox.Show("NOT ORTALAMASI BOS BIRAKILAMAZ");
            }
        }

        private void btnSirketTamamla_Click(object sender, EventArgs e)
        {

            Isyeri isyeri = new Isyeri();
            isyeri.IsyeriNo = 200003 + j;
            j++;
            isyeri.IsyeriAdi = txtSirketIsyeriAdi.Text;
            isyeri.Adres = txtSirketAdres.Text;
            isyeri.Faks = txtSirketFaks.Text;
            isyeri.Telefon = txtSirketTelefon.Text;
            isyeri.Eposta = txtSirketEposta.Text;
            hashSirketler.SirketEkle(isyeri.IsyeriNo,isyeri);
            MessageBox.Show("Kayıt Ekleme Başarılı.");
            Temizle();

        }

        //private void btnIngEgitim_Click(object sender, EventArgs e)//Yapılacak
        //{
        //    foreach (Kisi kisi in KisiListesi)
        //    {
        //        if((string)ikiliAramaAgaci.YabanciDilAra(kisi.YabanciDil).veri =="INGILIZCE")
        //        {
        //            txtAdayIseAlmaListesi.Text = kisi.KimlikBilgileri.Ad;
        //        }
                
        //    }
        //}

        //private void btnIkiYilDeneyim_Click(object sender, EventArgs e)
        //{
        //    txtAdayIseAlmaListesi.Items.Clear();
        //    for (int i = 0; i < ikiliAramaAgaci.ikiYilUstu.Count; i++)
        //    {

        //        txtAdayIseAlmaListesi.Items.Add(ikiliAramaAgaci.ikiYilUstu[i].KimlikBilgileri.Ad +
        //                                        ikiliAramaAgaci.ikiYilUstu[i].KimlikBilgileri.Soyad);

        //    }
        //}

        private void btnGSirketKayitGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                Isyeri isyeri = new Isyeri();
                isyeri= hashSirketler.IsyeriGetir(Convert.ToInt32(txtSirketKayitGBul.Text.ToString()));
                isyeri.IsyeriAdi = txtGSirketIsyeriAdi.Text.ToString();
                isyeri.Adres = txtGSirketAdres.Text.ToString();
                isyeri.Eposta = txtGSirketEposta.Text.ToString();
                isyeri.Faks = txtGSirketFaks.Text.ToString();
                isyeri.Telefon = txtGSirketTelefon.Text.ToString();

            }
            catch (Exception)
            { 
                throw new Exception("Şirket adı sistemde mevcut değil.");
            }


        }

        private void btnIsyeriAdinaGoreListele_Click(object sender, EventArgs e)
        {
            dgvIsIlanlari.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvIsIlanlari.Columns.Clear();
          
            tablo.Columns.Add("İşyeri Adı",typeof(string));
            tablo.Columns.Add("İsyeri No", typeof(string));
            List<Isyeri> SiraliPozisyon = hashSirketler.IsyeriListesi().OrderBy(b => b.IsyeriAdi).ToList();
            foreach (Isyeri isyeri in SiraliPozisyon)
            {
                string[] row = new string[] { isyeri.IsyeriAdi.ToString(), isyeri.IsyeriNo.ToString()};
                tablo.Rows.Add(row);
                dgvIsIlanlari.DataSource = tablo;

            }
        }

        private void btnDerinlik_Click(object sender, EventArgs e)
        {
            txtAgacDerinligi.Text = ikiliAramaAgaci.AgacDerinlikSayisi().ToString();
        }

        private void btnElemanSayisi_Click(object sender, EventArgs e)
        {
            txtElemanSayisi.Text = ikiliAramaAgaci.ElemanSayisi().ToString();
        }

        private void btnGezinmeYap_Click(object sender, EventArgs e)
        {
            if (ikiliAramaAgaci != null)
            {
                switch (cmbGezinmeSekli.SelectedIndex)
                {
                    case 0:
                        ikiliAramaAgaci.PreOrder();
                        break;
                    case 1:
                        ikiliAramaAgaci.InOrder();
                        break;
                    case 2:
                        ikiliAramaAgaci.PostOrder();
                        break;
                    default:
                        break;
                }
                txtAdayListele.Text = ikiliAramaAgaci.DugumleriYazdir();

            }
            else
            {
                MessageBox.Show("Öncelikle Ağacı Oluşturmalısınız.");
            }
        }

        private void btnAdayKaydiSil_Click(object sender, EventArgs e)
        {
            if (txtKayitSilmeTCNo.Text != "")
            {
                bool is_removed = ikiliAramaAgaci.Sil(Convert.ToUInt64(txtKayitSilmeTCNo.Text.ToString()));
                if (is_removed == true)
                {
                    MessageBox.Show(txtKayitSilmeTCNo.Text.ToString() + "Tc Numaralı Aday Silindi.");
                }
                else
                {
                    MessageBox.Show("Aday Bulunamadı.");
                }
            }

            else
                MessageBox.Show("Önce Silmek İstediğiniz Tc No Giriniz.");
        }

        private void btnSirketKaydiSil_Click(object sender, EventArgs e)
        {
            if (txtSirketNoKayitSil.Text != "")
            {
                try
                {
                    hashSirketler.IsyeriKaldir(Convert.ToInt32(txtSirketNoKayitSil.Text.ToString()));
                    MessageBox.Show(txtSirketNoKayitSil.Text.ToString() + " Numaralı İşyeri Silindi.");
                }
                catch
                {
                    MessageBox.Show("İşyeri Bulunamadı.");
                }
            }

            else
                MessageBox.Show("Önce Silmek İstediğiniz İşyeri No Giriniz.");
        }

        private void btnAdayListele_Click(object sender, EventArgs e)
        {
            IsIlani isIlani;
            isIlani = hashIlanlar.IsIlaniGetir(Convert.ToInt32(txtIlanNoListeleme.Text));
            foreach (Kisi kisi in isIlani.UygunAdayHeap.Basvur())
            {
                txtAdayListele.Text = "İsim: " + kisi.KimlikBilgileri.Ad + "  Soyisim: " + kisi.KimlikBilgileri.Soyad + Environment.NewLine ;
                

            }
        }

        private void btnIseAl_Click(object sender, EventArgs e)
        {
            IsIlani isIlani;
            isIlani= hashIlanlar.IsIlaniGetir(Convert.ToInt32(txtAdayIseAlIlanNo.Text));
            
            MessageBox.Show(isIlani.UygunAdayHeap.UygunAday().KimlikBilgileri.Ad+" İsimli Aday İşe Alındı.");

        }

        private void btnKayitSil_Click(object sender, EventArgs e)
        {
            if (txtSirketNoSilme.Text != "" && txtIlanNoSilme.Text != "")
            {
                try
                {
                    hashIlanlar.IlanKaldır(Convert.ToInt32(txtIlanNoSilme.Text.ToString()));
                    MessageBox.Show(txtIlanNoSilme.Text.ToString() + " Numaralı ilan Silindi.");
                }
                catch(Exception)
                {
                    MessageBox.Show("Ilan Bulunamadı.");
                }
            }

            else
                MessageBox.Show("Ilan no ve Isyeri no boş bırakılamaz !");
        }

        private void btnIlanGuncelle_Click(object sender, EventArgs e)
        {
            if (txtSirketNoIlanGuncelle.Text != "")
            {
                Isyeri isyeri = new Isyeri();
                isyeri= hashSirketler.IsyeriGetir(Convert.ToInt32(txtSirketNoIlanGuncelle.Text.ToString()));
                IsIlani isIlani = new IsIlani();
                
                try
                {
                    isIlani=hashIlanlar.IsIlaniGetir(isyeri.IlanVer().IlanNo);
                    isIlani.ArananOzellikler = txtArananElemanOzellikleriIlanGuncelleme.Text.ToString();
                    isIlani.ArananPozisyon = txtPozisyonIlanGuncelleme.Text.ToString();
                    isIlani.IsTanimi = txtIsTanitimiIlanGuncelleme.Text.ToString();
                    MessageBox.Show(txtIlanNoSilme.Text.ToString() + " Numaralı ilan Silindi.");
                }
                catch(Exception)
                {
                    MessageBox.Show("Ilan Bulunamadı.");
                }
            }

            else
                MessageBox.Show("Ilan no ve Isyeri no boş bırakılamaz !");
        }

        private void btnPozisyonaGoreListele_Click(object sender, EventArgs e)
        {
            dgvIsIlanlari.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvIsIlanlari.Columns.Clear();
            tablo.Columns.Add("İlan No", typeof(int));
            tablo.Columns.Add("Aranan Pozisyon", typeof(string));
            tablo.Columns.Add("Aranan Özellik", typeof(string));
            List<IsIlani> SiraliPozisyon = hashIlanlar.IlanListesi().OrderBy(a => a.ArananPozisyon).ToList();     
            foreach (IsIlani isilani in SiraliPozisyon)
            {
                string[] row = new string[] {isilani.IlanNo.ToString(), isilani.ArananPozisyon, isilani.ArananOzellikler };
                tablo.Rows.Add(row);
                dgvIsIlanlari.DataSource = tablo;
                
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {           
            dgvIsIlanlari.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            Kisi kisi = new Kisi();
            kisi.KimlikBilgileri.Ad = "VEFA";
            kisi.KimlikBilgileri.Soyad = "KARASOY";
            kisi.KimlikBilgileri.TcKimlikNo = 12412412400;
            kisi.Deneyim = 5;
            kisi.Adres = "Denizli";
            kisi.Eposta = "vefa@";
            kisi.TelNo = "055322626";
            kisi.KimlikBilgileri.DogumYeri = "Muğla";
            kisi.KimlikBilgileri.DogumTarihi = "02.05.1999";
            kisi.KimlikBilgileri.Uyruk = "Türk";
            kisi.Referans = "Deniz Kılınç";
            kisi.YabanciDil = "Almanca";
            kisi.MusteriNo = 300000;
            isdeneyimi = new IsDeneyimi();
            isdeneyimi.IsyeriAd = "SEYREK BUTIK";
            isdeneyimi.Adres = "seyrek";
            isdeneyimi.CalistigiYil = kisi.Deneyim;
            isdeneyimi.Pozisyon = "Kasiyer";
            kisi.IsDeneyimiEkle(isdeneyimi);
            ikiliAramaAgaci.Ekle(kisi.KimlikBilgileri.TcKimlikNo);
            KisiListesi.Add(kisi);
            egitimdurumu = new EgitimDurumu();
            egitimdurumu.BaslangicYili = "2011";
            egitimdurumu.BitisTarihi = "2015";
            egitimdurumu.Bolum = "Bilgisayar müh.";
            egitimdurumu.NotOrtalamasi = 3;
            egitimdurumu.OkulAd = "Bakırçay Üniversitesi";
            kisi.EgitimDurumuEkle(egitimdurumu);
            Kisi kisi2 = new Kisi();
            kisi2.KimlikBilgileri.Ad = "FATMA";
            kisi2.KimlikBilgileri.Soyad = "VICDAN";
            kisi2.KimlikBilgileri.TcKimlikNo = 12412412401;
            kisi2.Deneyim = 0;
            kisi2.Adres = "Buca";
            kisi2.Eposta = "fatma@";
            kisi2.TelNo = "055425358";
            kisi2.MusteriNo = 300001;
            kisi2.KimlikBilgileri.DogumYeri = "Aydın";
            kisi2.KimlikBilgileri.DogumTarihi = "12.05.2000";
            kisi2.KimlikBilgileri.Uyruk = "Türk";
            kisi2.Referans = "Murat Ertan";
            kisi2.YabanciDil = "ingilizce";
            isdeneyimi = new IsDeneyimi();
            isdeneyimi.IsyeriAd = "SEYREK BUTIK";
            isdeneyimi.Adres = "seyrek";
            isdeneyimi.CalistigiYil = kisi2.Deneyim;
            isdeneyimi.Pozisyon = "Kasiyer";
            ikiliAramaAgaci.Ekle(kisi2.KimlikBilgileri.TcKimlikNo);
            KisiListesi.Add(kisi2);
            egitimdurumu = new EgitimDurumu();
            egitimdurumu.BaslangicYili = "2011";
            egitimdurumu.BitisTarihi = "2015";
            egitimdurumu.Bolum = "Bilgisayar müh.";
            egitimdurumu.NotOrtalamasi = 3;
            egitimdurumu.OkulAd = "Bakırçay Üniversitesi";
            kisi2.EgitimDurumuEkle(egitimdurumu);
            Kisi kisi3 = new Kisi();
            kisi3.KimlikBilgileri.Ad = "CAGLARRRBABA";
            kisi3.KimlikBilgileri.Soyad = "DOKTORCU";
            kisi3.KimlikBilgileri.TcKimlikNo = 12412412402;
            kisi3.Deneyim = 2;
            kisi3.Adres = "Fethiye";
            kisi3.Eposta = "caglar@";
            kisi3.TelNo = "055728374";
            kisi3.KimlikBilgileri.DogumTarihi = "07.09.1998";
            kisi3.KimlikBilgileri.DogumYeri = "Edirne";
            kisi3.KimlikBilgileri.Uyruk = "Türk";
            kisi3.Referans = "Fatma Bozyiğit";
            kisi3.YabanciDil = "ingilizce";
            isdeneyimi = new IsDeneyimi();
            isdeneyimi.IsyeriAd = "SEYREK BUTIK";
            isdeneyimi.Adres = "seyrek";
            isdeneyimi.CalistigiYil = kisi3.Deneyim;
            kisi3.MusteriNo = 300002;
            isdeneyimi.Pozisyon = "Kasiyer";
            ikiliAramaAgaci.Ekle(kisi3.KimlikBilgileri.TcKimlikNo);
            KisiListesi.Add(kisi3);
            egitimdurumu = new EgitimDurumu();
            egitimdurumu.BaslangicYili = "2011";
            egitimdurumu.BitisTarihi = "2015";
            egitimdurumu.Bolum = "Bilgisayar müh.";
            egitimdurumu.NotOrtalamasi = 3;
            egitimdurumu.OkulAd = "Bakırçay Üniversitesi";
            kisi.EgitimDurumuEkle(egitimdurumu);
            Isyeri isyeri = new Isyeri();
            isyeri.IsyeriAdi = "SEYREK BUTIK";
            isyeri.IsyeriNo = 200000;
            isyeri.Adres = "Menemen";
            isyeri.Eposta = "seyrek@";
            isyeri.Faks = "213";
            isyeri.Telefon = "055317421";
            hashSirketler.SirketEkle(isyeri.IsyeriNo,isyeri);  
            isyeri.IlanVer().IlanNo = 100000;
            isyeri.IlanVer().ArananPozisyon = "Data Analizi";
            isyeri.IlanVer().IsTanimi = "Yönetici";
            isyeri.IlanVer().ArananOzellikler = "Sabırlı";
            hashIlanlar.IlanEkle(isyeri.IlanVer().IlanNo, isyeri.IlanVer());
            Isyeri isyeri2 = new Isyeri();
            isyeri2.IsyeriAdi = "FibaBank";
            isyeri2.IsyeriNo = 200001;
            isyeri2.Adres = "Menemen";
            isyeri2.Eposta = "bankfiba@";
            isyeri2.Faks = "123";
            isyeri2.Telefon = "055318454";
            hashSirketler.SirketEkle(isyeri2.IsyeriNo, isyeri2);
            isyeri2.IlanVer().IlanNo = 100001;
            isyeri2.IlanVer().ArananPozisyon = "Bilgisayar Mühendisi";
            isyeri2.IlanVer().IsTanimi = "Yönetici";
            isyeri2.IlanVer().ArananOzellikler = "Azimli";
            hashIlanlar.IlanEkle(isyeri2.IlanVer().IlanNo, isyeri2.IlanVer());
            Isyeri isyeri3 = new Isyeri();
            isyeri3.IsyeriAdi = "Microsoft";
            isyeri3.IsyeriNo = 200002;
            isyeri3.Adres = "Menemen";
            isyeri3.Eposta = "microsoft01@";
            isyeri3.Faks = "321";
            isyeri3.Telefon = "055314654";
            hashSirketler.SirketEkle(isyeri3.IsyeriNo, isyeri3);
            isyeri3.IlanVer().IlanNo = 100002;
            isyeri3.IlanVer().ArananPozisyon = "Bilgisayar Mühendisi";
            isyeri3.IlanVer().IsTanimi = "Ekip elemanı";
            isyeri3.IlanVer().ArananOzellikler = "Hırslı";
            hashIlanlar.IlanEkle(isyeri3.IlanVer().IlanNo, isyeri3.IlanVer());

        }
        public void Temizle()
        {
            txtSirketTelefon.Text = "";
            txtSirketIsyeriAdi.Text = "";
            txtSirketFaks.Text = "";
            txtSirketEposta.Text = "";
            txtMusteriAd.Text = "";
            txtMusteriAdres.Text = "";
            txtMusteriBaslangicYili.Text = "";
            txtMusteriBitirmeYili.Text = "";
            txtMusteriBolum.Text = "";
            txtMusteriCalismaAdresleri.Text = "";
            txtMusteriCalismaTarihleri.Text = "";
            txtMusteriDeneyim.Text = "";
            txtMusteriDilEgitimi.Text = "";
            txtMusteriDTarihi.Text = "";
            txtMusteriDYeri.Text = "";
            txtMusteriEposta.Text = "";
            txtMusteriIlgiAlanlari.Text = "";
            txtMusteriMezunOlunanOkul.Text = "";
            txtMusteriNotOrtalamasi.Text = "";
            txtMusteriOncekiCalisilanYerler.Text = "";
            txtMusteriPozisyon.Text = "";
            txtMusteriReferansKisiler.Text = "";
            txtMusteriSoyad.Text = "";
            txtMusteriTCNo.Text = "";
            txtMusteriTelefon.Text = "";
            txtMusteriUyruk.Text = "";
            txtPozisyonIlanGuncelleme.Text = "";
            txtSirketEposta.Text = "";
            txtSirketFaks.Text = "";
            txtSirketAdres.Text = "";

        }

        private void btnSirketKayitGBul_Click(object sender, EventArgs e)
        {
            try
            {
                Isyeri isyeri = new Isyeri();
                isyeri = hashSirketler.IsyeriGetir(Convert.ToInt32(txtSirketKayitGBul.Text.ToString()));
                txtGSirketIsyeriAdi.Text = isyeri.IsyeriAdi;
                txtGSirketTelefon.Text = isyeri.Telefon;
                txtGSirketFaks.Text = isyeri.Faks;
                txtGSirketAdres.Text = isyeri.Adres;
                txtGSirketEposta.Text = isyeri.Eposta;
            }
            catch (Exception)
            {
                throw new Exception("Şirket adı sistemde mevcut değil.");
            }
        }

        private void btnBasvur_Click(object sender, EventArgs e)
        {
            dgvIsIlanlari.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            IsIlani isIlani;
            foreach (Kisi kisi in KisiListesi)
            {
                if (kisi.KimlikBilgileri.TcKimlikNo.ToString()==txtIlanlariGosterTcNo.Text.ToString())
                {


                    for (int i = 0; i < dgvIsIlanlari.ColumnCount; i++)
                    {

                        if (dgvIsIlanlari.Rows[i].Selected)
                        {
                            isIlani = hashIlanlar.IsIlaniGetir((int)dgvIsIlanlari.Rows[i].Cells["İlan No"].Value);


                            if (isIlani.UygunAdayHeap.Insert(kisi))
                            {
                                MessageBox.Show("Başvuru İşlemi Başarılı.");
                            }
                        }
                    }
            }
             }
        }

        // SADEECE HARF VE SADECE RAKAM GIRISINI DENETLEMEK ICIN ;

        // SADECE HARF GIRISLERI
        // MUSTERI EKLE
        private void txtMusteriAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
               && !char.IsSeparator(e.KeyChar);
        }

        private void txtMusteriSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void txtMusteriUyruk_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }
        // MUSTERI GUNCELLE
        private void txtGMusteriUyruk_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void txtGMusteriSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void txtGMusteriAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }
        // SADECE RAKAM GIRISLERI
        // MUSTERI EKLE
        private void txtMusteriTCNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtMusteriBaslangicYili_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtMusteriBitirmeYili_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtMusteriNotOrtalamasi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtMusteriDeneyim_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        // MUSTERI GUNCELLE
        private void txtGMusteriTCNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtGMusteriDeneyim_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtGMusteriNotOrtalamasi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtGMusteriBaslangicYili_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtGMusteriBitirmeYili_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        // KAYIT SILME
        private void txtKayitSilmeTCNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        // ILANLARI GOSTER
        private void txtIlanlariGosterTcNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        // SIRKET GUNCELLE
        private void txtSirketKayitGBul_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        // SIRKET KAYDI SIL
        private void txtSirketNoKayitSil_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        // ILAN VER
        private void txtSirketNoIlanVer_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        // ILAN GUNCELLE
        private void txtSirketNoIlanGuncelle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        // ILAN SIL
        private void txtIlanNoSilme_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtSirketNoSilme_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        // ADAY LISTELE
        private void txtIlanNoListeleme_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}

