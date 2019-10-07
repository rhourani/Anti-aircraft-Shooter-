/****************************************************************************
**                               SAKARYA UNiVERSiTESi
**                      BiLGiSAYAR VE BiLiSiM BiLiMLERi FAKULTESi
**                          BiLGiSAYAR MUHENDiSLiGi BOLUMU
**                            NESNEYE DAYALI PROGRAMLAMA 
**
**                          OGRENCi ADI......:RADWAN ALHOURANI
**                          OGRENCi NUMARASi.:G**************
**                          DERS GRUBU…………………: "2.A" grubu
****************************************************************************/






using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NDP_project_g151210575
{
    /// <summary>
    /// UzaylıSavar Oyunu C# ile kodlamak, 
    /// </summary>
    //Ana kod sinifi Form1 Form'dan kalitim alinarak oluşturduk
    public partial class Form1 : Form
    {
        
        Random rand = new Random(); //Random objesi
        public List<int> DusmanX = new List<int>();//Dusmana X kordinati
        public List<int> DusmanY = new List<int>();//Dusmana Y kordinati
        

        //Global Degisken ListDusmanlar PictureBox'ten object olusturma
        List<PictureBox> ListDusmanlar = new List<PictureBox>();

        //Global Degisken UzaySavar Listesi PictureBox'ten object olusturma
        PictureBox UzaySavar = new PictureBox();

        //global Degisken UzaySavar Yonu icin
        int UzaySavarYonu = 0;

        //kurucu fonk. program basladiginda burdan baslar .
        public Form1()
        {

           //For dongusu, yukarida olusturdugumuz listelerine X ve Y dusman obje kordinati vermek
            for (int j = 0; j < 200; j++)
            {
                int AniDusmanY; //şimdiki dusman y nin kordinati
                ///Listenin sifirinci elemanin icindeki ayarlamak icin 
                if (j == 0)
                {
                    AniDusmanY = 0;
                }
                else //Simdi ki dusman y'de
                {

                    AniDusmanY = DusmanY[j-1] - rand.Next(10, 300);//rastgele y ver
                }

                DusmanX.Add(rand.Next(530));
                    DusmanY.Add(AniDusmanY);
                    Invalidate();         //bi olay olusturmak ve sonucta resim ekrana yazdirmak
                }

                KeyDown += Form1_KeyDown;//C# alakali bir format kod anahtarlara cagirmak
                InitializeComponent();   // ana fonk. Cagirmak
        }

                // Anahtarlar fonk.
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
                //key Down islemleri
            if (e.KeyCode == Keys.Return)
            {
                OyunZamanlayici.Start();
            }
            if (e.KeyCode==Keys.Space)
            {
                MermiOlustur();
            }
            if (e.KeyCode == Keys.Left)
            {
                UzaySavarYonu = -30;
            }
            if (e.KeyCode == Keys.Right)
            {
                UzaySavarYonu = +30;
            }

            if (e.KeyCode == Keys.Down)
            {
                UzaySavarYonu = 0;
            }
            if (e.KeyCode == Keys.P)
            {
                OyunZamanlayici.Stop();
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

           //istedigimiz ilk fonksiyonlarimiz ve olaylarimiz burada yazdik,Form Calistiginda bunlar ilk olark yapar.
        private void Form1_Load1(object sender, EventArgs e)
        {
            UzaySavarOlustur();
            DusmanlarOlustur();
            UzaySavar = (PictureBox)Controls["UzaySavar"]; //uzaySavari kontol edebilmesi için onu ilk calistirilmali, nesnesi cagirdik
            MessageBox.Show(" Oyun Anahtarları: \n \n\"Enter\"  Başlatmak için. \n \n\"Space\" Mermi atmak için. \n \n\"Sola ve Soğa\"  Yön tuşları.\n \n\" Aşaği \" UzaySavai durdurmak için.\n \n\" P \" Pause için.  \n \n\" Esc\" Çıkmak için.");
            moveAliens();
        }

           //uzaySavar olusturma metodu
        private void UzaySavarOlustur()
        {
            PictureBox UzaySavar = new PictureBox(); //pictureBox tan nesne olusturma ve ozellikler verme, ondan sonra obje ogeleri (ortak) Control Box'a ekleme 
            UzaySavar.Name = "UzaySavar";
            UzaySavar.Size = new Size(64,64);
            UzaySavar.Location = new Point(this.Width / 2, this.Height - 100);
            Controls.Add(UzaySavar);
            UzaySavar.ImageLocation = "ucakSavari.png";
           
        }
 
           //Dusmanlar olusturma metodu
        private void DusmanlarOlustur()
        {
               // 200 defa resim yazdir hemen
            for (int i = 0; i < 200; i++)
                 {
                     PictureBox Dusman = new PictureBox();  //pictureBox tan nesne olusturma ve ozellikler verme, ondan sonra obje ogeleri (ortak) Control Box'a ekleme 
                     Dusman.Name = "Dusman";
                     Dusman.ImageLocation = "UzayUcak.png";
                     Dusman.Size = new Size(72,64);
                     Dusman.Location = new Point(this.DusmanX[i], DusmanY[i]);
                     ListDusmanlar.Add(Dusman);             // ve burada ListDusmanlar listesine ekle, sonrada carpisma isleri kullanilir.
                     Controls.Add(Dusman);
            }
        }

            //Mermi olusturma metodu
        private void MermiOlustur()
        {
            PictureBox Mermi = new PictureBox(); //pictureBox tan nesne olusturma ve ozellikler verme, ondan sonra obje ogeleri (ortak) Control Box'a ekleme 
            Mermi.Name = "kabuk";
            Mermi.Size = new Size(20, 26);
            Mermi.Location = new Point(UzaySavar.Left + ( UzaySavar.Width / 2) , UzaySavar.Top);
            Controls.Add(Mermi);
            Mermi.ImageLocation = "Mermi.png";
        }
        
        //Zamanlayivi
        private void OyunZamanlayici_Tick(object sender, EventArgs e)
        {
            //metodlar cagirma
            moveAliens();
            MermiSil();
            UzaySavariHareket();
            Carpisiyorum();
            DoubleBuffered = true; //resimler gitme gelmeleri gidermek icin kullanillan bir yapı.
        }

            //Dusmanlar inmesi ve program yeniden baslamasi saglayan metodu
        private void moveAliens()
        {
                //foreach dongu butun dusman ogelerdan tahkuk eder 
            foreach(PictureBox Dusman in ListDusmanlar)
            {
                Dusman.Top += 7;             //y dusman kordinati sürekli azaltir.(biz eksi Y-ekseninde yazdirdik)

                if (Dusman.Top >= 580)       // y>=580 ise 
                { 
                    OyunZamanlayici.Stop();
                    MessageBox.Show("Game Over.");
                    Application.Restart();   // program yeniden baslat.
                }
            }
        }
          //Mermi Hareket etmesi ve silmesi saglayan metodu
        private void MermiSil()
        {
            // foreach dongusu  mermi icin uste ulasınca ekrandan kayibolur aksi halde devam eder -15
            foreach (PictureBox mElement in Controls)
            {
                if (mElement.Name == "kabuk") 
                {
                    if (mElement.Top < 0)
                    {
                        Controls.Remove(mElement);
                    }
                    else
                    {
                        mElement.Top += -15;
                    }
                }
            }
         }
        // UzaySavari Hareket saglayan metodu
        private void UzaySavariHareket()
        {
            UzaySavar.Left += UzaySavarYonu;
            if (UzaySavar.Left <= 0)
            {
                UzaySavarYonu = +30;
            }
            if (UzaySavar.Left >= 800)
            {
                UzaySavarYonu = -30;
            }

        }
        // Carisma varsa ikisin kaibolsun hem mermi hem dusman ve yerine arkplan renki normala donustursun.
        private void Carpisiyorum()
        {

            foreach (PictureBox dElement in ListDusmanlar) // foreach Dusman
            {
                foreach (PictureBox mElement in Controls.OfType<PictureBox>()) //Foreach mermi, ListDusmanlar pictureBoxlar turunden herhangi bir item mElement bul.
                {
                    if (mElement.Name == "kabuk" && mElement.Bounds.IntersectsWith(dElement.Bounds))// hem mermi -isimle cagirdik, yukarida tanimladik - hem de dusman yeri ayni ise bunu rectangle Bounds.intersectWith metodu kullanarak c# papmasi saglar.
                    {
                        /// hemen listlerden bir oge silmek mümkün degil, bunu imkansiz oldugu icin bi hile yapmaliyiz ki tag kullandik.
                        mElement.Tag = "dead"; 
                        dElement.Tag = "dead"; 
                        mElement.BackColor = this.BackColor;
                        dElement.BackColor = this.BackColor;
                    }
                }
            }
            foreach (PictureBox dm in Controls) //burada tag verdikten sonra ListDusmanlar listeden silmek imkani oldu, siliyoruz controldan ve ListDusmanlar.
            {
                if (dm.Tag == "dead")
                {
                    Controls.Remove(dm);
                    if (dm.Name == "Dusman")
                    {
                        ListDusmanlar.Remove(dm);
                    }
                }
            }
        }
    }
 }

