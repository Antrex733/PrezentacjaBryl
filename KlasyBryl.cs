using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrezentacjaBryl
{
    internal class KlasyBryl
    {
        const float mfKątProsty = 90.0f;
        //deklaracja klasy abstrakcyjnej
        public abstract class BryłaAbstrakcyjna
        {//deklaracja typu wyliczeniowego, którego elementy będą "znacznikami" wpisywanymi w egzemplarzu każdej bryły do pola RodzajBryły

            public enum mfTypyBrył
            { mfBG_Walec, mfBG_Stożek, mfBG_Kula, mfBG_Ostrosłup, 
                mfBG_Graniastosłup, mfBG_Sześcian, mfBG_StożekPochylony };
            //deklaracja zmiennych dla wspólnych atrybutów geometrycznych
            protected int mfXsP, mfYsP;
            protected int mfWysokośćBryły;
            protected float mfKątPochylenia = 90.0f;
            protected bool mfWidoczny;
            //deklaracja zmiennych dla wspólnych atrybutów graficznych
            protected Color mfKolor_Linii;
            protected DashStyle mfStyl_Linii;
            protected float mfGrubość_Linii;
            //deklaracja zmiennych dla implementacji przyszłych funkcjalności
            public mfTypyBrył mfRodzajBryły;
            protected bool mfKierunekObrotu; // false: w prawo, true: w lewo;
            public float mfPowierzchniaBryły;
            public float mfObjętośćBryły;
            //deklaracja konstruktora
            public BryłaAbstrakcyjna(Color mfKolorLinii, DashStyle mfStylLinii, float mfGrubośćLinii)
            {
                mfKolor_Linii = mfKolorLinii;
                mfStyl_Linii = mfStylLinii;
                mfGrubość_Linii = mfGrubośćLinii;
                mfKątPochylenia = mfKątProsty;
            }
            //deklaracja meto abstrakcyjnych (dla których nie jesteśmy w stanie zapisać ich implementacji)
            public abstract void PowierzchniaIPole();
            public abstract void Wykreśl(Graphics mfRysownica);
            public abstract void Wymaż(Control mfKontrolka, Graphics mfRysownica);
            public abstract void Obróć_i_Wykreśl(Control mfKontrolka, Graphics mfRysownica, float mfKątObrotu);
            public abstract void PrzesuńDoNowegoXY(Control mfKontrolka, Graphics mfRysownica, int mfX, int mfY);
            //deklaracja metod publicznych z ich pełną implementacją
            public void UstalAtrybutyGraficzne(Color mfKolorLinii, DashStyle mfStylLinii, int mfGrubośćLinii)
            {
                mfKolor_Linii = mfKolorLinii;
                mfStyl_Linii = mfStylLinii;
                mfGrubość_Linii = mfGrubośćLinii;
            }

        }//od klasy BryłaAbstrakcyjna
        //deklaracja klasy Bryły Obrotowe
        public class BryłyObrotowe : BryłaAbstrakcyjna
        {
            protected int mfPromieńBryły; 
            //deklaracja konstruktora
            public BryłyObrotowe(int mfR, Color mfKolorLinii, DashStyle mfStylLinii, float mfGrubośćLinii) :
                base(mfKolorLinii, mfStylLinii, mfGrubośćLinii)
            {
                //zapisanie (przechowanie) promienia r
                mfPromieńBryły = mfR;
            }
            //naddpisanie WSZYSTKICH metod abstrakcyjnych z klasy BryłaAbstrakcyjna
            public override void PowierzchniaIPole()
            {

            }
            public override void Wykreśl(Graphics mfRysownica)
            {

            }
            public override void Wymaż(Control mfKontrolka, Graphics mfRysownica)
            {

            }
            public override void Obróć_i_Wykreśl(Control mfKontrolka, Graphics mfRysownica, float mfKątObrotu)
            {

            }
            public override void PrzesuńDoNowegoXY(Control mfKontrolka, Graphics mfRysownica, int mfX, int mfY)
            {

            }

        }//od BryłyObrotowe
        //deklaracja klasy potomnej Walec
        public class Walec : BryłyObrotowe
        {
            //deklaracje uzupełniające dla bryły Walec
            Point[] mfWielokątPodłogi; // podastawy Walca
            Point[] mfWielokątSufitu; //druga podstawa Walca
            int mfXsS, mfYsS;
            //stopień wielokątapodstawy i sufitu Walca
            ////...////// 
            int mfStopieńWielokątaPodstawy;
            float mfOś_duża, mfOś_mała;
            //kąt środkowy między wierzchołkami wielokąta podstawy
            float mfKątMiędzyWierzchołkami;
            //kąt położenia pierwszego wierzchołka wielokąta podstawy
            float mfKątPołożenia;
            //wektor przesunięcia środka sufitu pochylonego Walca
            int mfWektorPrzesunięciaŚrodkaSufituWalca;

            //deklaracja konstruktora
            public Walec(int mfR, int mfWysokośćWalca, int mfStopieńWielokątaPodstawy,
                        int mfXsP, int mfYsP, Color mfKolorLinii,
                        DashStyle mfStylLinii, float mfGrubośćLinii, bool mfKierunekObrotu) :
                base(mfR, mfKolorLinii, mfStylLinii, mfGrubośćLinii)
            {//ustawienie roddzaju bryły
                mfRodzajBryły = mfTypyBrył.mfBG_Walec;
                mfWidoczny = false;
                this.mfKierunekObrotu = mfKierunekObrotu;
                mfWysokośćBryły = mfWysokośćWalca;
                this.mfXsP = mfXsP; this.mfYsP = mfYsP;
                this.mfStopieńWielokątaPodstawy = mfStopieńWielokątaPodstawy;

                //wyznaczenie osi elipsy wykreślanej w "podłoddze" i  "suficie" Walca
                mfOś_duża = mfR * 2;
                mfOś_mała = mfR / 2;

                mfXsS = mfXsP;//??????
                /*
                //sprawdzenie pochylenia Walca 
                if (KątPochylenia == KątProsty)
                    XsS = base.XsP;
                else
                    MessageBox.Show("Sory: pracuję nad tą możliwością");
                */

                //wyznaczenie pozostałych współrzędnych środka sufitu Walca
                mfYsS = mfYsP - mfWysokośćWalca;

                //wyznaczenie kąta kątów położenia
                mfKątMiędzyWierzchołkami = 360 / mfStopieńWielokątaPodstawy;
                mfKątPołożenia = 0f;
                //wyznaczenie współrzędnych punktów w "podłoddze" i "suficie"
                //dla wykreślania prążków na ścianie bocznej Walca
                mfWielokątPodłogi = new Point[mfStopieńWielokątaPodstawy + 1];
                mfWielokątSufitu = new Point[mfStopieńWielokątaPodstawy + 1];
                //utworzenie egzemplarzy punktów w "podłoddze" i "suficie"
                //oraz wpisanie do nich wyznaczonych współrzędnych na obwodzie Elipsy
                for (int i = 0; i < mfStopieńWielokątaPodstawy; i++)
                {
                    mfWielokątPodłogi[i] = new Point();
                    mfWielokątSufitu[i] = new Point();
                    // "podłoga" Walca:
                    //Równanie parametryczne okręgu: Xi == Xs + R*cos(Pi), Yi = Ys + R*sin(Pi)
                    //Równanie parametryczne elipsy: Xi == Xs + Oś_Duża/2*cos(Pi), Yi = Ys + Oś_Mała/2*sin(Pi)
                    mfWielokątPodłogi[i].X = (int)(base.mfXsP + mfOś_duża / 2
                        * Math.Cos(Math.PI * (mfKątPołożenia + i * mfKątMiędzyWierzchołkami) / 180f));
                    mfWielokątPodłogi[i].Y = (int)(base.mfYsP + mfOś_mała / 2
                        * Math.Sin(Math.PI * (mfKątPołożenia + i * mfKątMiędzyWierzchołkami) / 180f));

                    // "sufit" Walca:
                    mfWielokątSufitu[i].X = (int)(mfXsS + mfOś_duża / 2
                        * Math.Cos(Math.PI * (mfKątPołożenia + i * mfKątMiędzyWierzchołkami) / 180f));
                    mfWielokątSufitu[i].Y = (int)(base.mfYsP + mfOś_mała / 2
                        * Math.Sin(Math.PI * (mfKątPołożenia + i * mfKątMiędzyWierzchołkami) / 180f));

                }// od for
                //obliczenie powierzchni Walca
                //....
                //obliczenie objętości Walca
                //....

            }//od konstruktora Walca
            //nadpisanie metod abstrakcyjnych zadeklarowanych w klasie abstrakcyjnej
            public override void PowierzchniaIPole()
            {
                mfObjętośćBryły = (float)Math.PI * mfPromieńBryły * mfPromieńBryły * mfWysokośćBryły;
                mfPowierzchniaBryły = (float)(Math.PI * 2) * mfWysokośćBryły + (float)(Math.PI * 2) * mfPromieńBryły * mfPromieńBryły;
            }
            public override void Wykreśl(Graphics mfRysownica)
            {
                mfWidoczny = true;
                using (Pen Pióro = new Pen(mfKolor_Linii, mfGrubość_Linii))
                {
                    Pióro.DashStyle = mfStyl_Linii;
                    //wykreślanie "podłogi" Walca
                    mfRysownica.DrawEllipse(Pióro, mfXsP - mfOś_duża / 2, mfYsP - mfOś_mała / 2, mfOś_duża, mfOś_mała);
                    //wykreślanie "sufitu" Walca
                    mfRysownica.DrawEllipse(Pióro, mfXsS - mfOś_duża / 2, mfYsS - mfOś_mała / 2, mfOś_duża, mfOś_mała);
                    //wykreślenie próżków na ścuanie bocznej Walca
                    using (Pen PióroPrążków = new Pen(mfKolor_Linii, 1))
                    {
                        PióroPrążków.DashStyle = DashStyle.Dot;
                        //wykreślenie prążków na ścianie bocznej Walca
                        for (int i = 0; i < mfStopieńWielokątaPodstawy; i++)
                            mfRysownica.DrawLine(PióroPrążków, mfWielokątPodłogi[i], mfWielokątSufitu[i]);

                    }//od using PióroPrążków

                    //wykreślenie lewej krawędzi bocznej Walca
                    mfRysownica.DrawLine(Pióro, mfXsP - mfOś_duża / 2, mfYsP, mfXsS - mfOś_duża / 2, mfYsS);
                    //wykreślenie prawej krawędzi bocznej Walca
                    mfRysownica.DrawLine(Pióro, mfXsP + mfOś_duża / 2, mfYsP, mfXsS + mfOś_duża / 2, mfYsS);

                }//od using Pióro
            }
            public override void Wymaż(Control mfKontrolka, Graphics mfRysownica)
            {
                if (mfWidoczny)
                {
                    using (Pen Pióro = new Pen(mfKontrolka.BackColor, mfGrubość_Linii))
                    {
                        Pióro.DashStyle = mfStyl_Linii;
                        //wykreślanie "podłogi" Walca
                        mfRysownica.DrawEllipse(Pióro, mfXsP - mfOś_duża / 2, mfYsP - mfOś_mała / 2, mfOś_duża, mfOś_mała);
                        //wykreślanie "sufitu" Walca
                        mfRysownica.DrawEllipse(Pióro, mfXsS - mfOś_duża / 2, mfYsS - mfOś_mała / 2, mfOś_duża, mfOś_mała);
                        //wykreślenie próżków na ścuanie bocznej Walca
                        using (Pen PióroPrążków = new Pen(mfKontrolka.BackColor, 0.5f))
                        {
                            PióroPrążków.DashStyle = DashStyle.Dot;
                            //wykreślenie prążków na ścianie bocznej Walca
                            for (int i = 0; i < mfStopieńWielokątaPodstawy; i++)
                                mfRysownica.DrawLine(PióroPrążków, mfWielokątPodłogi[i], mfWielokątSufitu[i]);

                        }//od using PióroPrążków

                        //wykreślenie lewej krawędzi bocznej Walca
                        mfRysownica.DrawLine(Pióro, mfXsP - mfOś_duża / 2, mfYsP, mfXsS - mfOś_duża / 2, mfYsS);
                        //wykreślenie prawej krawędzi bocznej Walca
                        mfRysownica.DrawLine(Pióro, mfXsP + mfOś_duża / 2, mfYsP, mfXsS + mfOś_duża / 2, mfYsS);
                    }
                }
            }//od Wymaż
            public override void Obróć_i_Wykreśl(Control mfKontrolka, Graphics mfRysownica, float mfKątObrotu)
            {
                //obracamy tylko bryłę, która jest wykreślona, czyli widoczna
                if (mfWidoczny)
                {
                    Wymaż(mfKontrolka, mfRysownica);
                    //wyznaczenie nowego położenia pierwszego wierzchołka wielokoąta wpisanego w elipsę "podłogi"
                    if (mfKierunekObrotu)
                        mfKątPołożenia -= mfKątObrotu;
                    else
                        mfKątPołożenia += mfKątObrotu;
                    //wyznaczenie nowych wartości dla współrzędnych wszystkich wierzchołków wielokąta "podłogi"
                    for (int i = 0; i < mfStopieńWielokątaPodstawy; i++)
                    {

                        // "podłoga" Walca:
                        mfWielokątPodłogi[i].X = (int)(mfXsP + mfOś_duża / 2
                            * Math.Cos(Math.PI * (mfKątPołożenia + i * mfKątMiędzyWierzchołkami) / 180f));
                        mfWielokątPodłogi[i].Y = (int)(mfYsP + mfOś_mała / 2
                            * Math.Sin(Math.PI * (mfKątPołożenia + i * mfKątMiędzyWierzchołkami) / 180f));

                        // "sufit" Walca:
                        mfWielokątSufitu[i].X = (int)(mfXsS + mfOś_duża / 2
                            * Math.Cos(Math.PI * (mfKątPołożenia + i * mfKątMiędzyWierzchołkami) / 180f));
                        mfWielokątSufitu[i].Y = (int)(mfYsS + mfOś_mała / 2
                            * Math.Sin(Math.PI * (mfKątPołożenia + i * mfKątMiędzyWierzchołkami) / 180f));

                    }// od for
                    //wykreślenie Walca po obrocie
                    Wykreśl(mfRysownica);
                }//od if
            }//od Obróć_i_Wykreśl
            public override void PrzesuńDoNowegoXY(Control mfKontrolka, Graphics mfRysownica, int mfX, int mfY)
            {
                //przesuwamy tylko bryłę, która jest wykreślona, czyli widoczna
                if (mfWidoczny)
                {
                    //deklaracja zmiennych pomocniczych dla wyznaczenia wektora przesunięcia
                    int dX, dY;
                    Wymaż(mfKontrolka, mfRysownica);
                    //wyznaczenie wektora przesunięcia
                    dX = mfXsP < mfX ? mfX - mfXsP : -(mfXsP - mfX);
                    dY = mfYsP < mfY ? mfY - mfYsP : -(mfYsP - mfY);
                    //wyznaczamy nowe położenie dla środka 'podłogi' oraz 'sufitu'
                    mfXsP = mfXsP + dX;
                    mfYsP = mfYsP + dY;
                    mfXsS = mfXsS + dX;
                    mfYsS = mfYsS + dY;

                    //wyznaczenie nowych wartości dla współrzędnych wszystkich wierzchołków wielokąta "podłogi"
                    for (int i = 0; i < mfStopieńWielokątaPodstawy; i++)
                    {

                        // "podłoga" Walca:
                        mfWielokątPodłogi[i].X = (int)(mfXsP + mfOś_duża / 2
                            * Math.Cos(Math.PI * (mfKątPołożenia + i * mfKątMiędzyWierzchołkami) / 180f));
                        mfWielokątPodłogi[i].Y = (int)(mfYsP + mfOś_mała / 2
                            * Math.Sin(Math.PI * (mfKątPołożenia + i * mfKątMiędzyWierzchołkami) / 180f));

                        // "sufit" Walca:
                        mfWielokątSufitu[i].X = (int)(mfXsS + mfOś_duża / 2
                            * Math.Cos(Math.PI * (mfKątPołożenia + i * mfKątMiędzyWierzchołkami) / 180f));
                        mfWielokątSufitu[i].Y = (int)(mfYsP + mfOś_mała / 2
                            * Math.Sin(Math.PI * (mfKątPołożenia + i * mfKątMiędzyWierzchołkami) / 180f));

                    }// od for
                    //wykreślenie Walca po obrocie
                    Wykreśl(mfRysownica);

                }//od if(Widoczny)

            }// od PrzesuńDoNowegoXY

        }// od Walca
        public class Stożek : BryłyObrotowe
        {
            //deklaracje uzupełniające dla bryły Stożek
            protected int mfXsS, mfYsS;//wierzchołek Stożka
            protected int mfStopienWielokataPodstawy;
            protected int mfOś_duża, mfOś_mała;
            protected float mfKątŚrodkowyMiędzyWierzchołkami;
            protected float mfKątPołożeniaPierwszegoWierzchołka;
            //int KątPołożenia; // pierwszego wierzchołka wielokąta podstawy Stożka
            //deklaracja tablicy dla przechowania referancji do egzemplarzy wierzchołków wielokąta podstawy Stożka
            Point[] mfWielokątPodłogi;
            //deklaracja konstruktora klasy Stożek
            public Stożek(int mfR, int mfWysokośćStożka, int mfStopienWielokata,
                int mfXsP, int mfYsP, Color mfKolorLinii, DashStyle mfStylLinii, float mfGrubośćLinii, bool mfKierunekObrotu)
                : base(mfR, mfKolorLinii, mfStylLinii, mfGrubośćLinii)
            {
                mfRodzajBryły = mfTypyBrył.mfBG_Stożek;
                mfWidoczny = false;
                this.mfKierunekObrotu = mfKierunekObrotu;
                mfWysokośćBryły = mfWysokośćStożka;
                this.mfStopienWielokataPodstawy = mfStopienWielokata;
                mfOś_duża = mfR * 2;
                mfOś_mała = mfR / 2;
                this.mfXsP = mfXsP;
                this.mfYsP = mfYsP;
                //wyznaczenie współrzędnych wierzchołka Stożka
                mfXsS = mfXsP;
                mfYsS = mfYsP - mfWysokośćStożka;
                mfOś_duża = 2 * mfR;
                mfOś_mała = mfR / 2;
                mfKątPołożeniaPierwszegoWierzchołka = 0f;
                //KątPołożenia = 2;
                mfKątŚrodkowyMiędzyWierzchołkami = 360 / mfStopienWielokata;//Stopień Wielokąta???
                mfWielokątPodłogi = new Point[mfStopienWielokataPodstawy];
                //wyznaczenie współrzędnych wierzchołków wielokąta podstawy Stożka
                for (int i = 0; i < mfStopienWielokataPodstawy; i++)
                {
                    mfWielokątPodłogi[i] = new Point();
                    //wyznaczenie wartości współrzędnych i-tego wierzchołka wielokąta
                    //z równania parametrycznego elipsy: Xi = XsP + Oś_duża/2 * cos(Fi)
                    //                                   Yi = YsP+ Oś_mała * sin(Fi)
                    mfWielokątPodłogi[i].X = (int)(mfXsP + mfOś_duża / 2 * Math.Cos(Math.PI *
                        (mfKątPołożeniaPierwszegoWierzchołka + i * mfKątŚrodkowyMiędzyWierzchołkami) / 180));
                    mfWielokątPodłogi[i].Y = (int)(mfYsP + mfOś_mała / 2 * Math.Sin(Math.PI *
                        (mfKątPołożeniaPierwszegoWierzchołka + i * mfKątŚrodkowyMiędzyWierzchołkami) / 180));
                }
                //obliczenie pola powierzchni stożka
                //.  .  .

                //obliczenia objętośći stożka
                //.  .  . 

            }//od konstruktora Stożka
            //nadpisanie metod abstrakcyjnych, które zostały zadeklarowane w głównej klasie bazowej
            public override void PowierzchniaIPole()
            {
                mfObjętośćBryły = (float)(Math.PI * mfPromieńBryły * mfPromieńBryły * mfWysokośćBryły)/3;
                mfPowierzchniaBryły = (float)((Math.PI) * mfPromieńBryły) 
                    * (mfPromieńBryły 
                    + (float)Math.Sqrt(mfPromieńBryły*mfPromieńBryły + mfWysokośćBryły* mfWysokośćBryły));
            }
            public override void Wykreśl(Graphics mfRysownica)
            {
                using (Pen Pióro = new Pen(mfKolor_Linii, mfGrubość_Linii))
                {
                    Pióro.DashStyle = mfStyl_Linii;
                    //wykreślenie podstawy Stożka
                    mfRysownica.DrawEllipse(Pióro, mfXsP - mfOś_duża / 2, mfYsP - mfOś_mała / 2,
                                                 mfOś_duża, mfOś_mała);
                    //wykreślenie "prążków" (lini)
                    using (Pen PióroPróążków = new Pen(Pióro.Color, Pióro.Width / 3))
                    {
                        for (int i = 0; i < mfStopienWielokataPodstawy; i++)
                            mfRysownica.DrawLine(PióroPróążków, mfWielokątPodłogi[i], new Point(mfXsS, mfYsS));

                    }// koniec using PióroPrążków
                    //wykreślenie lewej krawędzi bocznej
                    mfRysownica.DrawLine(Pióro, mfXsP - mfOś_duża / 2, mfYsP, mfXsS, mfYsS);
                    //wykreślenie prawej krawędzi bocznej
                    mfRysownica.DrawLine(Pióro, mfXsP + mfOś_duża / 2, mfYsP, mfXsS, mfYsS);

                    mfWidoczny = true;
                }// koniec using Pióro
            }// od Wykreśl
            public override void Wymaż(Control mfKontrolka, Graphics mfRysownica)
            {
                if (mfWidoczny)
                {
                    using (Pen Pióro = new Pen(mfKontrolka.BackColor, mfGrubość_Linii))
                    {
                        Pióro.DashStyle = mfStyl_Linii;
                        //wykreślenie podstawy Stożka
                        mfRysownica.DrawEllipse(Pióro, mfXsP - mfOś_duża / 2, mfYsP - mfOś_mała / 2,
                                                     mfOś_duża, mfOś_mała);
                        //wykreślenie "prążków" (lini)
                        using (Pen PióroPróążków = new Pen(Pióro.Color, Pióro.Width / 3))
                        {
                            for (int i = 0; i < mfStopienWielokataPodstawy; i++)
                                mfRysownica.DrawLine(PióroPróążków, mfWielokątPodłogi[i], new Point(mfXsS, mfYsS));

                        }// koniec using PióroPrążków
                         //wykreślenie lewej krawędzi bocznej
                        mfRysownica.DrawLine(Pióro, mfXsP - mfOś_duża / 2, mfYsP, mfXsS, mfYsS);
                        //wykreślenie prawej krawędzi bocznej
                        mfRysownica.DrawLine(Pióro, mfXsP + mfOś_duża / 2, mfYsP, mfXsS, mfYsS);

                        mfWidoczny = true;
                    }// koniec using Pióro
                }
            }//od Wymaż
            public override void Obróć_i_Wykreśl(Control mfKontrolka, Graphics mfRysownica, float mfKątObrotu)
            {
                if (mfWidoczny)
                {
                    Wymaż(mfKontrolka, mfRysownica);
                    //wyznaczenie nowego kąta położenia wierzchołka wielokąta podstawy
                    if (mfKierunekObrotu)
                        mfKątŚrodkowyMiędzyWierzchołkami -= mfKątObrotu;
                    else
                        mfKątŚrodkowyMiędzyWierzchołkami += mfKątObrotu;
                    //wyznaczenie nowych współrzędnych dla wierzchołków wielokąta podstawy
                    for (int i = 0; i < mfStopienWielokataPodstawy; i++)
                    {
                        //wyznaczenie wartości współrzędnych i-tego wierzchołka wielokąta
                        //z równania parametrycznego elipsy: Xi = XsP + Oś_duża/2 * cos(Fi)
                        //                                   Yi = YsP+ Oś_mała * sin(Fi)
                        mfWielokątPodłogi[i].X = (int)(mfXsP + mfOś_duża / 2 * Math.Cos(Math.PI *
                         (mfKątPołożeniaPierwszegoWierzchołka + i * mfKątŚrodkowyMiędzyWierzchołkami) / 180));
                        mfWielokątPodłogi[i].Y = (int)(mfYsP + mfOś_mała / 2 * Math.Sin(Math.PI *
                            (mfKątPołożeniaPierwszegoWierzchołka + i * mfKątŚrodkowyMiędzyWierzchołkami) / 180));
                    }
                    //wykreślenie Stożka po obrocie
                    Wykreśl(mfRysownica);
                }
            }// od Obróć_i_Wykreśl
            public override void PrzesuńDoNowegoXY(Control mfKontrolka, Graphics mfRysownica, int mfX, int mfY)
            {
                if (mfWidoczny)
                {
                    int dX, dY;
                    Wymaż(mfKontrolka, mfRysownica);
                    //wyznaczanie przyrostów zmian dX i dY
                    dX = mfXsP < mfX ? mfX - mfXsP : -(mfXsP - mfX);
                    dY = mfYsP < mfY ? mfY - mfYsP : -(mfYsP - mfY);
                    // ustalenie nowych współrzędnych dla środka podstawy Stożka i jego wierzchołka
                    mfXsP = mfXsP + dX;
                    mfYsP = mfYsP + dY;
                    mfXsS = mfXsS + dX;
                    mfYsS = mfYsS + dY;
                    //wyznaczenie nowego położenia dla wszystkich wierzvhołków wielokąta podstawy
                    for (int i = 0; i < mfStopienWielokataPodstawy; i++)
                    {
                        mfWielokątPodłogi[i] = new Point();
                        //wyznaczenie wartości współrzędnych i-tego wierzchołka wielokąta
                        //z równania parametrycznego elipsy: Xi = XsP + Oś_duża/2 * cos(Fi)
                        //                                   Yi = YsP+ Oś_mała * sin(Fi)
                        mfWielokątPodłogi[i].X = (int)(mfXsP + mfOś_duża / 2 * Math.Cos(Math.PI *
                            (mfKątPołożeniaPierwszegoWierzchołka + i * mfKątŚrodkowyMiędzyWierzchołkami) / 180));
                        mfWielokątPodłogi[i].Y = (int)(mfYsP + mfOś_mała / 2 * Math.Sin(Math.PI *
                            (mfKątPołożeniaPierwszegoWierzchołka + i * mfKątŚrodkowyMiędzyWierzchołkami) / 180));
                    }
                    Wykreśl(mfRysownica);

                }// od if

            }// od PrzesuńDoNowegoXY

        }// od Stożka
        public class StożekPochylony : Stożek
        {

            //int KątPołożenia; // pierwszego wierzchołka wielokąta podstawy Stożka
            //deklaracja tablicy dla przechowania referancji do egzemplarzy wierzchołków wielokąta podstawy Stożka
            Point[] mfWielokątPodłogi;
            //deklaracja konstruktora klasy Stożek
            public StożekPochylony(int mfR, int mfWysokośćStożka, int mfStopienWielokata, float mfKątPochyleniaStożka,
                int mfXsP, int mfYsP, Color mfKolorLinii, DashStyle mfStylLinii, float mfGrubośćLinii, bool mfKierunekObrotu)
                : base(mfR, mfWysokośćStożka, mfStopienWielokata,
                       mfXsP, mfYsP, mfKolorLinii, mfStylLinii, mfGrubośćLinii, mfKierunekObrotu)
            {
                mfRodzajBryły = mfTypyBrył.mfBG_StożekPochylony;
                mfWidoczny = false;

                //wyznaczenie współrzędnych wierzchołka Stożka przesuniętego względem środka 'podłogi' Stożka
                mfXsS = mfXsP + (int)(mfWysokośćStożka / Math.Tan(Math.PI * mfKątPochyleniaStożka / 180f));
                mfYsS = mfYsP - mfWysokośćStożka;
                mfOś_duża = 2 * mfR;
                mfOś_mała = mfR / 2;
                mfKątPołożeniaPierwszegoWierzchołka = 0f;
                //KątPołożenia = 2;
                mfKątŚrodkowyMiędzyWierzchołkami = 360 / mfStopienWielokata;//Stopień Wielokąta???
                mfWielokątPodłogi = new Point[mfStopienWielokataPodstawy];
                //wyznaczenie współrzędnych wierzchołków wielokąta podstawy Stożka
                for (int i = 0; i < mfStopienWielokataPodstawy; i++)
                {
                    mfWielokątPodłogi[i] = new Point();
                    //wyznaczenie wartości współrzędnych i-tego wierzchołka wielokąta
                    //z równania parametrycznego elipsy: Xi = XsP + Oś_duża/2 * cos(Fi)
                    //                                   Yi = YsP+ Oś_mała * sin(Fi)
                    mfWielokątPodłogi[i].X = (int)(mfXsP + mfOś_duża / 2 * Math.Cos(Math.PI *
                        (mfKątPołożeniaPierwszegoWierzchołka + i * mfKątŚrodkowyMiędzyWierzchołkami) / 180));
                    mfWielokątPodłogi[i].Y = (int)(mfYsP + mfOś_mała / 2 * Math.Sin(Math.PI *
                        (mfKątPołożeniaPierwszegoWierzchołka + i * mfKątŚrodkowyMiędzyWierzchołkami) / 180));
                }
                //obliczenie pola powierzchni stożka
                //.  .  .

                //obliczenia objętośći stożka
                //.  .  . 

            }//od konstruktora Stożka
            //nadpisanie metod abstrakcyjnych, które zostały zadeklarowane w głównej klasie bazowej
            public override void PowierzchniaIPole()
            {
                mfObjętośćBryły = (float)(Math.PI * mfPromieńBryły * mfPromieńBryły * mfWysokośćBryły) / 3;
                mfPowierzchniaBryły = (float)(Math.PI) * mfPromieńBryły * mfPromieńBryły;
            }
            public override void Wykreśl(Graphics anRysownica)
            {
                using (Pen Pióro = new Pen(mfKolor_Linii, mfGrubość_Linii))
                {
                    Pióro.DashStyle = mfStyl_Linii;
                    //wykreślenie podstawy Stożka
                    anRysownica.DrawEllipse(Pióro, mfXsP - mfOś_duża / 2, mfYsP - mfOś_mała / 2,
                                                 mfOś_duża, mfOś_mała);
                    //wykreślenie "prążków" (lini)
                    using (Pen PióroPróążków = new Pen(Pióro.Color, Pióro.Width / 3))
                    {
                        for (int i = 0; i < mfStopienWielokataPodstawy; i++)
                            anRysownica.DrawLine(PióroPróążków, mfWielokątPodłogi[i], new Point(mfXsS, mfYsS));

                    }// koniec using PióroPrążków
                    //wykreślenie lewej krawędzi bocznej
                    anRysownica.DrawLine(Pióro, mfXsP - mfOś_duża / 2, mfYsP, mfXsS, mfYsS);
                    //wykreślenie prawej krawędzi bocznej
                    anRysownica.DrawLine(Pióro, mfXsP + mfOś_duża / 2, mfYsP, mfXsS, mfYsS);

                    mfWidoczny = true;
                }// koniec using Pióro
            }// od Wykreśl
            public override void Wymaż(Control anKontrolka, Graphics anRysownica)
            {
                if (mfWidoczny)
                {
                    using (Pen Pióro = new Pen(anKontrolka.BackColor, mfGrubość_Linii))
                    {
                        Pióro.DashStyle = mfStyl_Linii;
                        //wykreślenie podstawy Stożka
                        anRysownica.DrawEllipse(Pióro, mfXsP - mfOś_duża / 2, mfYsP - mfOś_mała / 2,
                                                     mfOś_duża, mfOś_mała);
                        //wykreślenie "prążków" (lini)
                        using (Pen PióroPróążków = new Pen(Pióro.Color, Pióro.Width / 3))
                        {
                            for (int i = 0; i < mfStopienWielokataPodstawy; i++)
                                anRysownica.DrawLine(PióroPróążków, mfWielokątPodłogi[i], new Point(mfXsS, mfYsS));

                        }// koniec using PióroPrążków
                         //wykreślenie lewej krawędzi bocznej
                        anRysownica.DrawLine(Pióro, mfXsP - mfOś_duża / 2, mfYsP, mfXsS, mfYsS);
                        //wykreślenie prawej krawędzi bocznej
                        anRysownica.DrawLine(Pióro, mfXsP + mfOś_duża / 2, mfYsP, mfXsS, mfYsS);

                        mfWidoczny = true;
                    }// koniec using Pióro
                }
            }//od Wymaż
            public override void Obróć_i_Wykreśl(Control anKontrolka, Graphics anRysownica, float anKątObrotu)
            {
                if (mfWidoczny)
                {
                    Wymaż(anKontrolka, anRysownica);
                    //wyznaczenie nowego kąta położenia wierzchołka wielokąta podstawy
                    if (mfKierunekObrotu)
                        mfKątŚrodkowyMiędzyWierzchołkami -= anKątObrotu;
                    else
                        mfKątŚrodkowyMiędzyWierzchołkami += anKątObrotu;
                    //wyznaczenie nowych współrzędnych dla wierzchołków wielokąta podstawy
                    for (int i = 0; i < mfStopienWielokataPodstawy; i++)
                    {
                        //wyznaczenie wartości współrzędnych i-tego wierzchołka wielokąta
                        //z równania parametrycznego elipsy: Xi = XsP + Oś_duża/2 * cos(Fi)
                        //                                   Yi = YsP+ Oś_mała * sin(Fi)
                        mfWielokątPodłogi[i].X = (int)(mfXsP + mfOś_duża / 2 * Math.Cos(Math.PI *
                         (mfKątPołożeniaPierwszegoWierzchołka + i * mfKątŚrodkowyMiędzyWierzchołkami) / 180));
                        mfWielokątPodłogi[i].Y = (int)(mfYsP + mfOś_mała / 2 * Math.Sin(Math.PI *
                            (mfKątPołożeniaPierwszegoWierzchołka + i * mfKątŚrodkowyMiędzyWierzchołkami) / 180));
                    }
                    //wykreślenie Stożka po obrocie
                    Wykreśl(anRysownica);
                }
            }// od Obróć_i_Wykreśl
            public override void PrzesuńDoNowegoXY(Control anKontrolka, Graphics anRysownica, int anX, int anY)
            {
                if (mfWidoczny)
                {
                    int dX, dY;
                    Wymaż(anKontrolka, anRysownica);
                    //wyznaczanie przyrostów zmian dX i dY
                    dX = mfXsP < anX ? anX - mfXsP : -(mfXsP - anX);
                    dY = mfYsP < anY ? anY - mfYsP : -(mfYsP - anY);
                    // ustalenie nowych współrzędnych dla środka podstawy Stożka i jego wierzchołka
                    mfXsP = mfXsP + dX;
                    mfYsP = mfYsP + dY;
                    mfXsS = mfXsS + dX;
                    mfYsS = mfYsS + dY;
                    //wyznaczenie nowego położenia dla wszystkich wierzvhołków wielokąta podstawy
                    for (int i = 0; i < mfStopienWielokataPodstawy; i++)
                    {
                        mfWielokątPodłogi[i] = new Point();
                        //wyznaczenie wartości współrzędnych i-tego wierzchołka wielokąta
                        //z równania parametrycznego elipsy: Xi = XsP + Oś_duża/2 * cos(Fi)
                        //                                   Yi = YsP+ Oś_mała * sin(Fi)
                        mfWielokątPodłogi[i].X = (int)(mfXsP + mfOś_duża / 2 * Math.Cos(Math.PI *
                            (mfKątPołożeniaPierwszegoWierzchołka + i * mfKątŚrodkowyMiędzyWierzchołkami) / 180));
                        mfWielokątPodłogi[i].Y = (int)(mfYsP + mfOś_mała / 2 * Math.Sin(Math.PI *
                            (mfKątPołożeniaPierwszegoWierzchołka + i * mfKątŚrodkowyMiędzyWierzchołkami) / 180));
                    }
                    Wykreśl(anRysownica);

                }// od if

            }// od PrzesuńDoNowegoXY
        }// od Stożka
        public class StożekPodwójny : Stożek
        {
            Point[] anWielokątPodłogi;
            public StożekPodwójny(int mfR, int mfWysokośćStożka, int mfStopienWielokata,
                int mfXsP, int mfYsP, Color mfKolorLinii, DashStyle mfStylLinii, float mfGrubośćLinii, bool mfKierunekObrotu)
                : base(mfR, mfWysokośćStożka, mfStopienWielokata, mfXsP, mfYsP, mfKolorLinii, mfStylLinii, mfGrubośćLinii, mfKierunekObrotu)
            {

            }
            public override void PowierzchniaIPole()
            {
                mfObjętośćBryły = (float)((Math.PI * mfPromieńBryły * mfPromieńBryły * mfWysokośćBryły) / 3) * 2;
                mfPowierzchniaBryły = (float)((Math.PI) * mfPromieńBryły)
                    * (mfPromieńBryły
                    + (float)Math.Sqrt(mfPromieńBryły * mfPromieńBryły + mfWysokośćBryły * mfWysokośćBryły))
                    - 2* (float)((Math.PI) * mfPromieńBryły * mfPromieńBryły);
            }
            public override void Wykreśl(Graphics mfRysownica)
            {
                using (Pen Pióro = new Pen(mfKolor_Linii, mfGrubość_Linii))
                {
                    Pióro.DashStyle = mfStyl_Linii;
                    //Góra
                    //wykreślenie podstawy Stożka
                    mfRysownica.DrawEllipse(Pióro, mfXsP - mfOś_duża / 2, mfYsP - mfOś_mała / 2,
                                                 mfOś_duża, mfOś_mała);

                    //wykreślenie lewej krawędzi bocznej
                    mfRysownica.DrawLine(Pióro, mfXsP - mfOś_duża / 2, mfYsP, mfXsS, mfYsS);
                    //wykreślenie prawej krawędzi bocznej
                    mfRysownica.DrawLine(Pióro, mfXsP + mfOś_duża / 2, mfYsP, mfXsS, mfYsS);

                    //Dół
                    //wykreślenie lewej krawędzi bocznej
                    mfRysownica.DrawLine(Pióro, mfXsP - mfOś_duża / 2, mfYsP, mfXsS, mfYsP + mfYsP - mfYsS);
                    //wykreślenie prawej krawędzi bocznej
                    mfRysownica.DrawLine(Pióro, mfXsP + mfOś_duża / 2, mfYsP, mfXsS, mfYsP + mfYsP - mfYsS);
                    mfWidoczny = true;
                }// koniec using Pióro
            }
            public override void Wymaż(Control mfKontrolka, Graphics mfRysownica)
            {
                using (Pen Pióro = new Pen(mfKontrolka.BackColor, mfGrubość_Linii))
                {
                    Pióro.DashStyle = mfStyl_Linii;
                    //Góra
                    //wykreślenie podstawy Stożka
                    mfRysownica.DrawEllipse(Pióro, mfXsP - mfOś_duża / 2, mfYsP - mfOś_mała / 2,
                                                 mfOś_duża, mfOś_mała);

                    //wykreślenie lewej krawędzi bocznej
                    mfRysownica.DrawLine(Pióro, mfXsP - mfOś_duża / 2, mfYsP, mfXsS, mfYsS);
                    //wykreślenie prawej krawędzi bocznej
                    mfRysownica.DrawLine(Pióro, mfXsP + mfOś_duża / 2, mfYsP, mfXsS, mfYsS);

                    //Dół
                    //wykreślenie lewej krawędzi bocznej
                    mfRysownica.DrawLine(Pióro, mfXsP - mfOś_duża / 2, mfYsP, mfXsS, mfYsP + mfYsP - mfYsS);
                    //wykreślenie prawej krawędzi bocznej
                    mfRysownica.DrawLine(Pióro, mfXsP + mfOś_duża / 2, mfYsP, mfXsS, mfYsP + mfYsP - mfYsS);
                    mfWidoczny = true;
                }// koniec using Pióro
            }
        }
        public class Wielościany : BryłaAbstrakcyjna
        {
            protected Point[] mfWielokątPodłogi;
            protected int mfStopieńWielokątaPodłogi;
            protected int mfXsS, mfYsS;
            protected int mfPromieńBryły;
            //deklaracja konstruktora
            public Wielościany(int mfR, int mfStopieńWieloikąta, Color mfKolorLinii, DashStyle mfStylLinii,
                float mfGrubośćLinii) : base(mfKolorLinii, mfStylLinii, mfGrubośćLinii)
            {
                mfPromieńBryły = mfR;
                mfStopieńWielokątaPodłogi = mfStopieńWieloikąta;
            }

            public override void PowierzchniaIPole()
            {
                throw new NotImplementedException();
            }//nadpisanie metod abstarcyjnych zadeklarowanych w klasie bazowej (abstrakcyjnej)
            public override void Wykreśl(Graphics mfRysownica)
            {
                throw new NotImplementedException();
            }
            public override void Wymaż(Control mfKontrolka, Graphics mfRysownica)
            {
                throw new NotImplementedException();
            }
            public override void Obróć_i_Wykreśl(Control mfKontrolka, Graphics mfRysownica, float mfKątObrotu)
            {
                throw new NotImplementedException();
            }
            public override void PrzesuńDoNowegoXY(Control mfKontrolka, Graphics mfRysownica, int mfX, int mfY)
            {
                throw new NotImplementedException();
            }
        }// od Wielościanu
        public class Graniastosłup : Wielościany
        {
            //deklaracje uzupełniające
            Point[] mfmWielokątSufitu;
            float mfKątŚrodkowyMiędzyWierzchołkamiWielokąta;
            float mfKątPołożeniaPierwszegoWierzchołkaWielokąta;
            float mfOś_duża, mfOś_mała;
            //deklaracja konstruktora
            public Graniastosłup(int mfR, int mfWysokośćGraniastosłupa, int mfStopieńWielokąta, int mfXsP,
                int mfYsP, Color mfKolorLinii, DashStyle mfStylLinii, float mfGrubośćLinii) : base(mfR, mfStopieńWielokąta, mfKolorLinii, mfStylLinii, mfGrubośćLinii)
            {
                mfRodzajBryły = mfTypyBrył.mfBG_Graniastosłup;
                mfWidoczny = false;
                mfKierunekObrotu = false;
                mfWysokośćBryły = mfWysokośćGraniastosłupa;
                //wyznaczenie wartośći dla pozostałych atrybutów (zmiennych) Graniastosłupa
                this.mfXsP = mfXsP;
                this.mfYsP = mfYsP;
                mfXsS = mfXsP;
                mfYsS = mfYsP - mfWysokośćGraniastosłupa;
                mfOś_duża = 2 * mfR;
                mfOś_mała = mfR / 2;
                mfKątŚrodkowyMiędzyWierzchołkamiWielokąta = 360 / mfStopieńWielokąta;
                mfKątPołożeniaPierwszegoWierzchołkaWielokąta = 0f;
                //utworzenie egzemplarzy tablic dla wpisania wspólnych wierzchołkąt wielokąta "podługi" oraz "sufitu"
                mfWielokątPodłogi = new Point[mfStopieńWielokąta + 1];
                mfmWielokątSufitu = new Point[mfStopieńWielokąta + 1];
                //utworzenie egzemplarzy klasy Point dla każdego wierzchołka wielokąta
                //podłogi oraz sufitu, a nastęnie wpisanie do nich współrzędnych wierzchołka
                for (int i = 0; i < mfStopieńWielokąta + 1; i++)
                {
                    mfWielokątPodłogi[i] = new Point();
                    mfmWielokątSufitu[i] = new Point();
                    //wuznaczenie współrzędnych dla i-tego wierzchołka wielokąta "podłogi"
                    mfWielokątPodłogi[i].X = (int)(mfXsP + mfOś_duża / 2 * Math.Cos(Math.PI *
                        (mfKątPołożeniaPierwszegoWierzchołkaWielokąta + i * mfKątŚrodkowyMiędzyWierzchołkamiWielokąta) / 180));
                    mfWielokątPodłogi[i].Y = (int)(mfYsP + mfOś_mała / 2 * Math.Sin(Math.PI *
                        (mfKątPołożeniaPierwszegoWierzchołkaWielokąta + i * mfKątŚrodkowyMiędzyWierzchołkamiWielokąta) / 180));
                    //sufit
                    mfmWielokątSufitu[i].X = mfWielokątPodłogi[i].X;
                    mfmWielokątSufitu[i].Y = mfWielokątPodłogi[i].Y - mfWysokośćGraniastosłupa;
                }
                //obliczenie pola powierzchni oraz objętości Graniastosłupa
                // . . .
            }//od Konstruktora Graniastosłupa
            //nadpisanie metod abstrakcyjnych klasy BryłaAbstrakcyjna
            public override void PowierzchniaIPole()
            {
                throw new NotImplementedException();
            }
            public override void Wykreśl(Graphics mfRysownica)
            {
                using (Pen Pióro = new Pen(mfKolor_Linii, mfGrubość_Linii))
                {
                    Pióro.DashStyle = mfStyl_Linii;
                    //wykreślenie "podłogi" Graniastosłupa
                    for (int i = 0; i < mfWielokątPodłogi.Length - 1; i++)
                        mfRysownica.DrawLine(Pióro, mfWielokątPodłogi[i], mfWielokątPodłogi[i + 1]);
                    //wykreślenie "sufitu" Graniastosłupa
                    for (int i = 0; i < mfmWielokątSufitu.Length - 1; i++)
                        mfRysownica.DrawLine(Pióro, mfmWielokątSufitu[i], mfmWielokątSufitu[i + 1]);

                    //wykreślenie krawędzi bocznych 
                    for (int i = 0; i < mfStopieńWielokątaPodłogi + 1; i++)
                        mfRysownica.DrawLine(Pióro, mfWielokątPodłogi[i], mfmWielokątSufitu[i]);

                    mfWidoczny = true;
                }//koniec using Pióro
            }//od metody Wykreśl
            public override void Wymaż(Control mfKontrolka, Graphics mfRysownica)
            {
                if (mfWidoczny)
                {
                    using (Pen Pióro = new Pen(mfKontrolka.BackColor, mfGrubość_Linii))
                    {
                        Pióro.DashStyle = mfStyl_Linii;
                        //wykreślenie "podłogi" Graniastosłupa
                        for (int i = 0; i < mfWielokątPodłogi.Length - 1; i++)
                            mfRysownica.DrawLine(Pióro, mfWielokątPodłogi[i], mfWielokątPodłogi[i + 1]);
                        //wykreślenie "sufitu" Graniastosłupa
                        for (int i = 0; i < mfmWielokątSufitu.Length - 1; i++)
                            mfRysownica.DrawLine(Pióro, mfmWielokątSufitu[i], mfmWielokątSufitu[i + 1]);

                        //wykreślenie krawędzi bocznych 
                        for (int i = 0; i < mfStopieńWielokątaPodłogi + 1; i++)
                            mfRysownica.DrawLine(Pióro, mfWielokątPodłogi[i], mfmWielokątSufitu[i]);

                        mfWidoczny = false;
                    }//koniec using Pióro
                }//if
            }//od Wymaż
            public override void Obróć_i_Wykreśl(Control mfKontrolka, Graphics mfRysownica, float mfKątObrotu)
            {
                if (mfWidoczny)
                {
                    //wymazanie
                    Wymaż(mfKontrolka, mfRysownica);
                    //wyznaczenie nowego położenia pierwszego wierzchołka wielokąta
                    if (mfKierunekObrotu)
                        mfKątPołożeniaPierwszegoWierzchołkaWielokąta -= mfKątObrotu;
                    else
                        mfKątPołożeniaPierwszegoWierzchołkaWielokąta += mfKątObrotu;
                    //wyznaczenie nowych współrzędnych wielokąta po obrocie 
                    for (int i = 0; i < mfStopieńWielokątaPodłogi + 1; i++)
                    {
                        //wuznaczenie współrzędnych dla i-tego wierzchołka wielokąta "podłogi"
                        mfWielokątPodłogi[i].X = (int)(mfXsP + mfOś_duża / 2 * Math.Cos(Math.PI *
                            (mfKątPołożeniaPierwszegoWierzchołkaWielokąta + i * mfKątŚrodkowyMiędzyWierzchołkamiWielokąta) / 180));
                        mfWielokątPodłogi[i].Y = (int)(mfYsP + mfOś_mała / 2 * Math.Sin(Math.PI *
                            (mfKątPołożeniaPierwszegoWierzchołkaWielokąta + i * mfKątŚrodkowyMiędzyWierzchołkamiWielokąta) / 180));
                        //sufit
                        mfmWielokątSufitu[i].X = mfWielokątPodłogi[i].X;
                        mfmWielokątSufitu[i].Y = mfWielokątPodłogi[i].Y - mfWysokośćBryły;
                    }
                    Wykreśl(mfRysownica);
                }
            }// od Obróć_i_Wykreśl
            public override void PrzesuńDoNowegoXY(Control mfKontrolka, Graphics mfRysownica, int mfX, int mfY)
            {
                if (mfWidoczny)
                {
                    Wymaż(mfKontrolka, mfRysownica);
                    //przesunięcie do punktu: (X, Y) 
                    mfXsP = mfX;
                    mfYsP = mfY;
                    mfXsS = mfXsP;
                    mfYsS = mfYsP - mfWysokośćBryły;
                    // wyznaczenie nowych współrzędnych wierzchołków wielokątów po przesunięciu
                    for (int i = 0; i < mfStopieńWielokątaPodłogi + 1; i++)
                    {
                        //wuznaczenie współrzędnych dla i-tego wierzchołka wielokąta "podłogi"
                        mfWielokątPodłogi[i].X = (int)(mfXsP + mfOś_duża / 2 * Math.Cos(Math.PI *
                            (mfKątPołożeniaPierwszegoWierzchołkaWielokąta + i * mfKątŚrodkowyMiędzyWierzchołkamiWielokąta) / 180));
                        mfWielokątPodłogi[i].Y = (int)(mfYsP + mfOś_mała / 2 * Math.Sin(Math.PI *
                            (mfKątPołożeniaPierwszegoWierzchołkaWielokąta + i * mfKątŚrodkowyMiędzyWierzchołkamiWielokąta) / 180));
                        //sufit
                        mfmWielokątSufitu[i].X = mfWielokątPodłogi[i].X;
                        mfmWielokątSufitu[i].Y = mfWielokątPodłogi[i].Y - mfWysokośćBryły;
                    }
                    Wykreśl(mfRysownica);
                }
            }//od PrzesuńDoNowegoXY
        }

    }
}
