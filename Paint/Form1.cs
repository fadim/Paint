using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        
        int CizgiKalinlik = 1;
        enum ECizimTipi { Serbest, Dikdortgen, Elips, Cizgi, Silgi };
        
        ECizimTipi cizimtipi = ECizimTipi.Serbest;
        bool cizimbasladi = false;
        Point onceki, sonraki;
        Graphics tuval;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlRenk.BackColor = (sender as Button).BackColor;
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            Panel p = sender as Panel;
            CizgiKalinlik = p.Height/4;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tuval = pnlResim.CreateGraphics();
        }

        private void pnlResim_MouseDown(object sender, MouseEventArgs e)
        {
            cizimbasladi = true;
            onceki = e.Location;
        }

        private void pnlResim_MouseUp(object sender, MouseEventArgs e)
        {
            
            sonraki = e.Location;
            if (cizimbasladi)
            {
                if (cizimtipi == ECizimTipi.Cizgi)
                {
                    tuval.DrawLine(new Pen(pnlRenk.BackColor, CizgiKalinlik), onceki, sonraki);
                    //onceki = sonraki;
                }
                else if (cizimtipi == ECizimTipi.Elips)
                {
                    tuval.DrawEllipse(new Pen(pnlRenk.BackColor,CizgiKalinlik), 
                                      onceki.X, onceki.Y, Math.Abs(onceki.X-sonraki.X)
                                      , Math.Abs(onceki.Y - sonraki.Y));
                    onceki = sonraki;
                }
                else if (cizimtipi == ECizimTipi.Dikdortgen)
                {
                    tuval.DrawRectangle(new Pen(pnlRenk.BackColor, CizgiKalinlik),
                                      onceki.X, onceki.Y, Math.Abs(onceki.X - sonraki.X)
                                      , Math.Abs(onceki.Y - sonraki.Y));
                    onceki = sonraki;
                }
            }

            cizimbasladi = false;
        }

        private void pnlResim_MouseMove(object sender, MouseEventArgs e)
        {
            sonraki = e.Location;
            if (cizimbasladi)
            {
                if (cizimtipi==ECizimTipi.Serbest)
                {
                    tuval.DrawLine(new Pen(pnlRenk.BackColor,CizgiKalinlik), onceki, sonraki);
                    onceki = sonraki;
                }
                else if (cizimtipi == ECizimTipi.Silgi)
                {
                    tuval.FillEllipse(new SolidBrush(pnlResim.BackColor), onceki.X-15,onceki.Y-15,30,30);
                    onceki = sonraki;

                    //tuval.DrawString
                }
            }
            this.Text = e.Location.ToString();
        }

        private void button25_Click(object sender, EventArgs e)
        {//silgi
            cizimtipi = ECizimTipi.Silgi;
        }

        private void button22_Click(object sender, EventArgs e)
        {//cizgi
            cizimtipi = ECizimTipi.Cizgi;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            cizimtipi = ECizimTipi.Elips;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            cizimtipi = ECizimTipi.Dikdortgen;

        }
    }
}
