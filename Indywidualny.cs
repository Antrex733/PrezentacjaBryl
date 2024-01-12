using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PrezentacjaBryl.KlasyBryl;

namespace PrezentacjaBryl
{
    public partial class Indywidualny : Form
    {
        const int mfMargines = 10;
        Graphics mfRysownica/*, mfPowierzchniaGragicznaWziernikaLinii*/;
        Pen mfPióro;
        List<BryłaAbstrakcyjna> mfLBG = new List<BryłaAbstrakcyjna>();
        //deklaracja pomocniczej zmeinnej dla rzechowania współrzędnych wybranego munktu rysownicy
        Point mfPunktLokalizaacjiBryły = new Point(-1, 1);
        bool slajdy = false;
        public Indywidualny()
        {
            InitializeComponent();
            mfpbRysownica.Image = new Bitmap(mfpbRysownica.Width, mfpbRysownica.Height);

            //utworzenie egzemplarza Rysownicy
            mfRysownica = Graphics.FromImage(mfpbRysownica.Image);

            //sformatowanie Pióra
            mfPióro = new Pen(Color.Black, 1f);
            mfPióro.DashStyle = DashStyle.DashDot;

            mflbl2.Text = mftrbWysokośćBryły.Value.ToString();
            mflbl3.Text = mftrbPromieńBryły.Value.ToString();
            mflbl4.Text = mftrbKątPochyleniaBryły.Value.ToString();
            mflbl5.Text = mftrUstawGruboscLinii.Value.ToString();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Form mfitem in Application.OpenForms)
            {
                if (mfitem.Name == "Form1")
                {
                    Hide();
                    mfitem.Show();
                    return;
                }
            }
            Indywidualny nowy = new Indywidualny();
            Hide();
            nowy.Show();
        }

        private void mfbtnkolorLiniiBryły_Click(object sender, EventArgs e)
        {
            ColorDialog mfPaletaKolorów = new ColorDialog();
            mfPaletaKolorów.Color = mfPióro.Color;
            if (mfPaletaKolorów.ShowDialog() == DialogResult.OK)
                mfPióro.Color = mfPaletaKolorów.Color;
            //uaktualnienie WziernikaLinii
            mftxtKolorLinii.BackColor = mfPaletaKolorów.Color;
            //zwolnienie okna dialogowego
            mfPaletaKolorów.Dispose();
        }

        private void mfbtnDodajNowąBryłę_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            //wymazanie 'kropki' lokalizacji nowej bryły
            using (SolidBrush Pędzel = new SolidBrush(mfpbRysownica.BackColor))
            {
                //wymazanie ustalonego wcześniej położenia bryły
                mfRysownica.FillEllipse(Pędzel, mfPunktLokalizaacjiBryły.X - 3,
                                              mfPunktLokalizaacjiBryły.Y - 3,
                                              6, 6);
            }
            //pobieranie atrybutów ustawionych dla wybranej bryły
            int mfWysokośćBryły = mftrbWysokośćBryły.Value;
            int mfPromieńBryły = mftrbPromieńBryły.Value;
            int mfStopieńWielokąta = (int)mfnudStopieńWielokąta.Value;
            float mfKątPochylenia = mftrbKątPochyleniaBryły.Value;
            int mfXsP = mfPunktLokalizaacjiBryły.X;
            int mfYsP = mfPunktLokalizaacjiBryły.Y;
            mfPióro.Width = mftrUstawGruboscLinii.Value;
            ZmienStylLinii(mfcmbStylLinii.SelectedItem);
            //rozpoznanie wybranej bryły
            switch (mfcmbListaBrył.SelectedItem)
            {
                case "Walec":
                    Walec mfwalec =
                        new Walec(mfPromieńBryły, mfWysokośćBryły, mfStopieńWielokąta,
                                  mfXsP, mfYsP, mfPióro.Color, mfPióro.DashStyle, mfPióro.Width, KierunekObrotu());
                    mfwalec.Wykreśl(mfRysownica);

                    //dodanie egzemplarza Walca do listy LBG
                    mfLBG.Add(mfwalec);
                    break;
                case "Stożek":
                    Stożek mfstożek =
                        new Stożek(mfPromieńBryły, mfWysokośćBryły, mfStopieńWielokąta,
                                   mfXsP, mfYsP, mfPióro.Color, mfPióro.DashStyle, mfPióro.Width, KierunekObrotu());
                    mfstożek.Wykreśl(mfRysownica);
                    //dodanie egzemplarza Walca do losty LBG
                    mfLBG.Add(mfstożek);
                    break;
                case "Stożek Pochylony":
                    Stożek mfstożekpochylony =
                        new StożekPochylony(mfPromieńBryły, mfWysokośćBryły, mfStopieńWielokąta, mfKątPochylenia,
                                            mfXsP, mfYsP, mfPióro.Color, mfPióro.DashStyle, mfPióro.Width, KierunekObrotu());
                    mfstożekpochylony.Wykreśl(mfRysownica);
                    //dodanie egzemplarza Walca do losty LBG
                    mfLBG.Add(mfstożekpochylony);
                    break;
                case "Graniastosłup":
                    Graniastosłup mfgraniastosłup =
                        new Graniastosłup(mfPromieńBryły, mfWysokośćBryły, mfStopieńWielokąta,
                                   mfXsP, mfYsP, mfPióro.Color, mfPióro.DashStyle, mfPióro.Width);
                    mfgraniastosłup.Wykreśl(mfRysownica);
                    //dodanie egzemplarza Walca do losty LBG
                    mfLBG.Add(mfgraniastosłup);
                    break; 
                case "Sześcian":  
                    Graniastosłup mfszescian =
                        new Graniastosłup(mfWysokośćBryły, mfWysokośćBryły, 4,
                                   mfXsP, mfYsP, mfPióro.Color, mfPióro.DashStyle, mfPióro.Width);
                    mfszescian.Wykreśl(mfRysownica);
                    //dodanie egzemplarza Walca do losty LBG
                    mfLBG.Add(mfszescian);
                    break;
                case "Prostopadłościan":
                    Graniastosłup mfprostopadloscian =
                        new Graniastosłup(mfPromieńBryły, mfWysokośćBryły, 4,
                                   mfXsP, mfYsP, mfPióro.Color, mfPióro.DashStyle, mfPióro.Width);
                    mfprostopadloscian.Wykreśl(mfRysownica);
                    //dodanie egzemplarza Walca do losty LBG
                    mfLBG.Add(mfprostopadloscian);
                    break;
                default:
                    break;
            }
            mfZegar.Enabled = true;
            mfpbRysownica.Refresh();
        }
        private void ZmienStylLinii(object styl)
        {
            switch (styl)
            {
                case "Kreskowa (Dash)":
                    mfPióro.DashStyle = DashStyle.Dash;
                    break;
                case "KreskowoKropkowa (DashDot)":
                    mfPióro.DashStyle = DashStyle.Dash;
                    break;
                case "KreskowoKropkowaKropkowa (DashDotDot)":
                    mfPióro.DashStyle = DashStyle.Dash;
                    break;
                case "Kropkowa (Dot)":
                    mfPióro.DashStyle = DashStyle.Dash;
                    break;
                case "Linia Ciągła (Solid)":
                    mfPióro.DashStyle = DashStyle.Dash;
                    break;
            }
        }

        private void mfbtnUsunOstatnioDodBryle_Click(object sender, EventArgs e)
        {
            if (mfLBG.Count == 0)
            {
                errorProvider1.SetError(mfbtnUsunOstatnioDodBryle, "Rysownica jest pusta!");
                return;
            }

            mfLBG[mfLBG.Count - 1].Wymaż(mfpbRysownica, mfRysownica);
            mfLBG.RemoveAt(mfLBG.Count - 1);
        }

        private void mfbtnUsunPierwszaDodBryle_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if (mfLBG.Count == 0)
            {
                errorProvider1.SetError(mfbtnUsunPierwszaDodBryle, "Rysownica jest pusta!");
                return;
            }
            mfLBG[0].Wymaż(mfpbRysownica, mfRysownica);
            mfLBG.RemoveAt(0);
            mfpbRysownica.Refresh();
        }

        private void mfbtnUsunWybFigure_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if (mfLBG.Count < mfnudUsunWyb.Value)
            {
                errorProvider1.SetError(mfbtnUsunWybFigure, "Nie wykreśliłeś " + mfnudUsunWyb.Value + " brył!");
                return;
            }
            mfLBG[(int)mfnudUsunWyb.Value - 1].Wymaż(mfpbRysownica, mfRysownica);
            mfLBG.RemoveAt((int)mfnudUsunWyb.Value - 1);
            mfpbRysownica.Refresh();
        }

        private void mfbtnWłączenieSlajdera_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if (mfLBG.Count == 0)
            {
                errorProvider1.SetError(mfbtnWłączenieSlajdera, "Nie wykreśliłeś żadnej bryły!");
                return;
            }
            mfgbPokaz.Enabled = false;
            mfRysownica.Clear(mfpbRysownica.BackColor);
            slajdy = true;

            slajder.Tag = 0;
            mftxtIndeksTabeli.Text = 0.ToString();

            int anXmax = mfpbRysownica.Width;
            int anYmax = mfpbRysownica.Height;

            mfLBG[0].PrzesuńDoNowegoXY(mfpbRysownica, mfRysownica, anXmax / 2, anYmax / 2);
            mfpbRysownica.Refresh();

            if (mfrbZegarowy.Checked)
            {
                //uaktywnienie zegara
                slajder.Enabled = true;
            }
            else
            { //stawienie stanu braku aktywności dla kontrolek slajdera manualnego
                mfbtnNastępny.Enabled = true;
                mfbtnPoprzedni.Enabled = true;
                //mftxtIndeksTabeli.Enabled = true;
            }
            //ustawienie stanu braku aktywności dla przycisku włącz slajder
            mfbtnWłączenieSlajdera.Enabled = false;
            //uaktywnienie przycisku btnWyłączenieSlajdera
            mfbtnWyłączenieSlajdera.Enabled = true;
        }

        private void mfbtnPoprzedni_Click(object sender, EventArgs e)
        {
            //deklaracja pomocnicza
            ushort mfIndexFigury;
            // pobieranie z kontrolki textbox indexu aktualnie wykreślonej figury
            mfIndexFigury = ushort.Parse(mftxtIndeksTabeli.Text);
            //wyznaczenie indeksu dla następnej figury
            if (mfIndexFigury != 0)
            {
                mfIndexFigury--;
            }
            else
            {
                mfIndexFigury = (ushort)(mfLBG.Count - 1);
            }

            //deklaracje pomocnicze
            //wyznaczenie rozmiarów rysownicy
            int anXmax = mfpbRysownica.Width;
            int anYmax = mfpbRysownica.Height;

            mfRysownica.Clear(mfpbRysownica.BackColor);

            //zmiana lokalizacji i-tej figury geometrycznej i wykreślenie
            mfLBG[mfIndexFigury].PrzesuńDoNowegoXY(mfpbRysownica, mfRysownica, anXmax / 2, anYmax / 2);
            //odświeżenie powierzchni graficznej
            mfpbRysownica.Refresh();
            //uaktualnienie zapisu indeksu aktualnie wykreślonej figury
            mftxtIndeksTabeli.Text = mfIndexFigury.ToString();
        }

        private void mfbtnNastępny_Click(object sender, EventArgs e)
        {
            //deklaracja pomocnicza
            ushort mfIndexFigury;
            // pobieranie z kontrolki textbox indexu aktualnie wykreślonej figury
            mfIndexFigury = ushort.Parse(mftxtIndeksTabeli.Text);
            //wyznaczenie indeksu dla następnej figury
            if (mfIndexFigury < mfLBG.Count - 1)
            {
                mfIndexFigury++;
            }
            else
            {
                mfIndexFigury = 0;
            }

            //deklaracje pomocnicze
            //wyznaczenie rozmiarów rysownicy
            int anXmax = mfpbRysownica.Width;
            int anYmax = mfpbRysownica.Height;

            mfRysownica.Clear(mfpbRysownica.BackColor);

            //zmiana lokalizacji i-tej figury geometrycznej i wykreślenie
            mfLBG[mfIndexFigury].PrzesuńDoNowegoXY(mfpbRysownica, mfRysownica, anXmax / 2, anYmax / 2);
            //odświeżenie powierzchni graficznej
            mfpbRysownica.Refresh();
            //uaktualnienie zapisu indeksu aktualnie wykreślonej figury
            mftxtIndeksTabeli.Text = mfIndexFigury.ToString();
        }

        private void mfbtnWyłączenieSlajdera_Click(object sender, EventArgs e)
        {
            slajdy = false;
            mfgbPokaz.Enabled = true;
            mfRysownica.Clear(mfpbRysownica.BackColor);
            //wyłączenie zegara
            slajder.Enabled = false;
            //ustawienie indeksu na 0
            mftxtIndeksTabeli.Text = "";
            //uaktywnienie przycisku poleceń WączenieSlajdera
            mfbtnWłączenieSlajdera.Enabled = true;
            //ustawienie stanu braku aktywności dla przycisku btnWyłączenieSlajdera
            mfbtnWyłączenieSlajdera.Enabled = false;
            //uaktywnienie/brak aktywności przycisków slajdera
            mfrbZegarowy.Checked = true;
            mfbtnNastępny.Enabled = false;
            mfbtnPoprzedni.Enabled = false;
            mftxtIndeksTabeli.Enabled = false;
            //ponowne wykreślenie wszystkich figur "zapisanych" w TFG
            Random rnd = new Random();
            //deklaracje pomocnicze
            int mfx, mfy;
            //wyznaczenie rozmiarów rysownicy
            int anXmax = mfpbRysownica.Width;
            int anYmax = mfpbRysownica.Height;
            //wykreślenie wszystkich figur z TFG w losowej lokalizacji
            for (int mfi = 0; mfi < mfLBG.Count; mfi++)
            {
                //wylosowanie nowej lokalizacji: (x, y) dla i-tej figury
                mfx = rnd.Next(mfMargines, anXmax - mfMargines);
                mfy = rnd.Next(mfMargines, anYmax - mfMargines);
                //zmiana lokalizacji i-tej figury geometrycznej i wykreślenie
                mfLBG[mfi].PrzesuńDoNowegoXY(mfpbRysownica, mfRysownica, mfx, mfy);
            }
            //odświeżenie powierzchni graficznej
            mfpbRysownica.Refresh();
        }
        public bool KierunekObrotu()
        {
            if (mfrbtnLewo.Checked)
                return true;
            return false;
        }

        private void mfpbRysownica_MouseClick(object sender, MouseEventArgs e)
        {
            //zaznaczony punkt wykreślamy o możliwie małych rozmiarach
            using (SolidBrush anPędzel = new SolidBrush(Color.Red))
            {
                if (mfPunktLokalizaacjiBryły.X != -1)
                {
                    anPędzel.Color = mfpbRysownica.BackColor;
                    //wymazanie ustalonego wcześniej położenia bryły
                    mfRysownica.FillEllipse(anPędzel, mfPunktLokalizaacjiBryły.X - 3,
                                                  mfPunktLokalizaacjiBryły.Y - 3,
                                                  6, 6);
                    //przywrócenie pierwotnego koloru pdzla
                    anPędzel.Color = Color.Red;
                }
                //przechowanie współrzędnych miejsca kliknięcia lewym przyciskiem myszy
                mfPunktLokalizaacjiBryły = e.Location;
                //wykreślenie punktu 'kontrolnego'
                mfRysownica.FillEllipse(anPędzel, mfPunktLokalizaacjiBryły.X - 3,
                                              mfPunktLokalizaacjiBryły.Y - 3,
                                              6, 6);
                //uaktywnienie przycisku Dodaj Nową Bryłę
                mfbtnDodajNowąBryłę.Enabled = true;
                mfbtnUsunPierwszaDodBryle.Enabled = true;
                mfpbRysownica.Refresh();
            }
        }

        private void mfZegar_Tick(object sender, EventArgs e)
        {
            const float anKątObrotu = 5f;
            if (slajdy)
            {
                mfLBG[int.Parse(mftxtIndeksTabeli.Text)].Obróć_i_Wykreśl(mfpbRysownica, mfRysownica, anKątObrotu);
            }
            else
            {
                //obracamy wszystkie bryły w LBG o kąt obrotu
                for (int i = 0; i < mfLBG.Count; i++)
                    mfLBG[i].Obróć_i_Wykreśl(mfpbRysownica, mfRysownica, anKątObrotu);
            }


            mfpbRysownica.Refresh();
        }

        private void slajder_Tick(object sender, EventArgs e)
        {
            //wymazanie całej powierzchni graficznej
            mfRysownica.Clear(mfpbRysownica.BackColor);
            //wyznaczenie rozmiarów powierzchni graficznej
            int anXmax = mfpbRysownica.Width;
            int anYmax = mfpbRysownica.Height;
            //wpisanie do kontrolki slajder indeksu TFG pokazywanej figury
            mftxtIndeksTabeli.Text = slajder.Tag.ToString();
            //wykreślenie figury o indeksie timer1.Tag w środku powierzchni graficznej
            mfLBG[(int)(slajder.Tag)].PrzesuńDoNowegoXY(mfpbRysownica, mfRysownica, anXmax / 2, anYmax / 2);
            //odświeżenie pow. graficznej
            mfpbRysownica.Refresh();
            //ustawienie indeksu dla następnej figury do pokazu
            slajder.Tag = ((int)(slajder.Tag) + 1) % (mfLBG.Count);
        }

        private void Indywidualny_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Indywidualny_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult wynik = MessageBox.Show("Czy na pewno chcesz zamknąć okno?", this.Text,
                                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (wynik != DialogResult.OK)
                e.Cancel = true;
            else
                e.Cancel = false;
        }

        private void mfcmbListaBrył_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (mfcmbListaBrył.SelectedItem.ToString())
            {
                case "Sześcian":
                MessageBox
                .Show("Sześcian będzie miał boki równe ustawionej wysokości", this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
                case "Prostopadłościan":
                    MessageBox
                    .Show("Prostopadłościan będzie miał boki podstawy równe ustawionemu promieniowi bryły", this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private void mftrbWysokośćBryły_Scroll(object sender, EventArgs e)
        {
            mflbl2.Text = mftrbWysokośćBryły.Value.ToString();
        }

        private void mftrbPromieńBryły_Scroll(object sender, EventArgs e)
        {
            mflbl3.Text = mftrbPromieńBryły.Value.ToString();
        }

        private void mftrbKątPochyleniaBryły_Scroll(object sender, EventArgs e)
        {
            mflbl4.Text = mftrbKątPochyleniaBryły.Value.ToString();
        }

        private void mftrUstawGruboscLinii_Scroll(object sender, EventArgs e)
        {
            mflbl5.Text = mftrUstawGruboscLinii.Value.ToString();
        }
    }
}
