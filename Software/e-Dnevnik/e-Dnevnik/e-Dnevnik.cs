﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using e_Dnevnik.Klase;

namespace e_Dnevnik
{
    public partial class frmEDnevnik : Form
    {
        bool isOpen = false;
        int sideBarWidth;
        MainForm mainForm;

        public frmEDnevnik()
        {
            InitializeComponent();
            mainForm = new MainForm();            

            pboxResize.Image = Properties.Resources.window_decrease;
            sideBarWidth = panelSideBar.Width;

            if (Klase.RepozitorijHLK.prijavljeniKorisnik.UlogaKorisnika == Klase.UlogaKorisnika.Mentor)
                mainForm.uloga = MainForm.uloge.mentor;
            else if (Klase.RepozitorijHLK.prijavljeniKorisnik.UlogaKorisnika == Klase.UlogaKorisnika.GlavniMentor)
                mainForm.uloga = MainForm.uloge.glavni_mentor;
            else if (Klase.RepozitorijHLK.prijavljeniKorisnik.UlogaKorisnika == Klase.UlogaKorisnika.Specijalizant)
                mainForm.uloga = MainForm.uloge.specijalizant;

            mainForm.programSpecijalizacije = Klase.RepozitorijHLK.prijavljeniKorisnik.ProgramSpecijalizacije;
            mainForm.panelBody = panelBody;
            
        }

        private void btnZatvori_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                pboxResize.Image = Properties.Resources.window_increase;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                pboxResize.Image = Properties.Resources.window_decrease;
            }
            pboxResize.Refresh();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Sakrivanje menia
        private void pboxHamburger_Click(object sender, EventArgs e)
        {
            timerZatvoriMeni.Start();
        }

        private void timerZatvoriMeni_Tick(object sender, EventArgs e)
        {
            if (isOpen)
            {
                panelSideBar.Width += 20;
                if (panelSideBar.Width >= sideBarWidth)
                {
                    timerZatvoriMeni.Stop();
                    isOpen = false;
                    this.Refresh();
                }
            }
            else
            {
                panelSideBar.Width -= 20;
                if (panelSideBar.Width <= 0)
                {
                    timerZatvoriMeni.Stop();
                    isOpen= true;
                    this.Refresh();
                }
            }
        }

        //Razlicit poziv jer se pocetna mijenja ovisno o tipu korisnika

        public void ucitajPocetnu()
        {
            if(mainForm.uloga == MainForm.uloge.specijalizant)
            {
                if(mainForm.openForm != null) mainForm.openForm.Close();

                btnBiljeske.Visible = true;
                btnDnevnikAktivnosti.Visible = true;
                btnStrucniRadovi.Visible = true;
                bntSlucajeviBolesnika.Visible = true;
                btnIspisDnevnika.Visible = true;
                btnPrijaviIspit.Visible = true;
                btnPregledDogadaja.Visible = false;
                btnSpecijalizantiMentori.Visible = false;
                btnProgramSpecijalizacije.Visible = false;
                btnProvjereZnanja.Visible = false;
                
            }
            else
            {
                if (mainForm.openForm != null) mainForm.openForm.Close();
                mainForm.ucitajFormu(new frmPocetnaModerator(mainForm));

                btnBiljeske.Visible = false;
                btnDnevnikAktivnosti.Visible = false;
                btnStrucniRadovi.Visible = false;
                bntSlucajeviBolesnika.Visible = false;
                btnIspisDnevnika.Visible = false;
                btnPrijaviIspit.Visible = false;
                btnPregledDogadaja.Visible = true;
                btnSpecijalizantiMentori.Visible = true;
                btnProgramSpecijalizacije.Visible = true;
                btnProvjereZnanja.Visible = true;
            }
        }

        //Ostatak koda

        private void btnDnevnikAktivnosti_Click(object sender, EventArgs e)
        {
            mainForm.ucitajFormu(new frmDnevnikAktivnosti());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ucitajPocetnu();
            
        }

        

        private void frmEDnevnik_Load(object sender, EventArgs e)
        {
            ucitajPocetnu();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPregledDogadaja_Click(object sender, EventArgs e)
        {
            mainForm.ucitajFormu(new frmDogadaji(mainForm));
        }

        private void btnDnevnikAktivnosti_click(object sender, EventArgs e)
        {
            mainForm.ucitajFormu(new frmDnevnikAktivnosti());
        }

        private void btnSpecijalizantiMentori_Click(object sender, EventArgs e)
        {
            mainForm.ucitajFormu(new frmMentorSpecijalizant(mainForm));
        }

        private void btnHome_Click_1(object sender, EventArgs e)
        {
            ucitajPocetnu();
        }

        private void btnProvjereZnanja_Click(object sender, EventArgs e)
        {
            mainForm.ucitajFormu(new frmProvjeraZnanja(mainForm));
        }

        private void btnOdjava_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIspisDnevnika_Click_1(object sender, EventArgs e)
        {
            mainForm.ucitajFormu(new frmDnevnik());
        }

        private void btnBiljeske_Click_1(object sender, EventArgs e)
        {
            mainForm.ucitajFormu(new frmBiljeske(mainForm));
        }

        private void btnPrijaviIspit_Click(object sender, EventArgs e)
        {
            mainForm.ucitajFormu(new frmPrijavaIspita());
        }

        private void btnProfil_Click(object sender, EventArgs e)
        {
            mainForm.ucitajFormu(new frmKorisnickiRacun(mainForm));
        }

        private void btnProgramSpecijalizacije_Click(object sender, EventArgs e)
        {
            mainForm.ucitajFormu(new frmProgramSpecijalizacije(mainForm));
        }
    }
}
