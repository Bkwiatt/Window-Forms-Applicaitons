using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Serialization;

namespace ThePirateGames
{
    public partial class Form1 : Form
    {
        //API for global hotkeys
        [DllImport("user32.dll")] private static extern bool RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")] private static extern int UnregisterHotKey(IntPtr hwnd, int id);

        int total = 0;
        string numTeams = "";
        int hours, minutes, seconds;
        public int crierCount = 600;
        public System.Media.SoundPlayer player;
        int sloops = 0, brigs = 0, galleons = 0, ghostShips = 0, megalodons = 0, cryingChests = 0, sellables = 0, ownShips = 0, deaths = 0, chicken = 0, snakes = 0, pigs = 0;
        int points1 = 0, points2 = 0, points3 = 0, points4 = 0, points5 = 0, points6 = 0, points7 = 0, points8 = 0, points9 = 0, points10 = 0, points11 = 0, points12 = 0, points13 = 0, points14 = 0, points15 = 0, sloopPoints = 0, brigPoints = 0, barfightPoints = 0, galleonPoints = 0, ghostShipPoints = 0, megalodonPoints = 0, cryingChestPoints = 0, sellablePoints = 0, ownShipPoints = 0, deathPoints = 0, chickenPoints = 0, snakePoints = 0, pigPoints = 0, bananaPoints = 0, plankPoints = 0, cannonPoints = 0, scannonPoints = 0, tntPoints = 0, notntPoints = 0, megabombPoints = 0, rowboatPoitns = 0, animalPoints = 0;




        public bool soundOn = true;
        public bool crierMult = false;
        public bool chalComplete = false;
        public bool rulessoundOn = true;
        public bool gameStart = false;
        private bool gameOver = false;

        //Booleans for menu button animation
        public bool b1 = false;
        public bool b4 = false;
        public bool b5 = false;

        public enum fsModifiers
        {
            Alt = 0x0001,
            Control = 0x0002,
            Shift = 0x0004,
            Window = 0x0008,
        }

        public Form1()
        {
            InitializeComponent();
            panelLeft.BringToFront();



            //Score add hotkeys        
            Form1.RegisterHotKey(this.Handle, 1, (int)fsModifiers.Shift | (int)fsModifiers.Alt, (int)Keys.S);
            Form1.RegisterHotKey(this.Handle, 2, (int)fsModifiers.Shift | (int)fsModifiers.Alt, (int)Keys.B);
            Form1.RegisterHotKey(this.Handle, 3, (int)fsModifiers.Shift | (int)fsModifiers.Alt, (int)Keys.G);
            Form1.RegisterHotKey(this.Handle, 4, (int)fsModifiers.Shift | (int)fsModifiers.Alt, (int)Keys.Q);
            Form1.RegisterHotKey(this.Handle, 5, (int)fsModifiers.Shift | (int)fsModifiers.Alt, (int)Keys.M);
            Form1.RegisterHotKey(this.Handle, 6, (int)fsModifiers.Shift | (int)fsModifiers.Alt, (int)Keys.C);
            Form1.RegisterHotKey(this.Handle, 7, (int)fsModifiers.Shift | (int)fsModifiers.Alt, (int)Keys.L);
            Form1.RegisterHotKey(this.Handle, 8, (int)fsModifiers.Shift | (int)fsModifiers.Alt, (int)Keys.O);
            Form1.RegisterHotKey(this.Handle, 9, (int)fsModifiers.Shift | (int)fsModifiers.Alt, (int)Keys.D);
            Form1.RegisterHotKey(this.Handle, 10, (int)fsModifiers.Shift | (int)fsModifiers.Alt, (int)Keys.D1);
            Form1.RegisterHotKey(this.Handle, 11, (int)fsModifiers.Shift | (int)fsModifiers.Alt, (int)Keys.D2);
            Form1.RegisterHotKey(this.Handle, 12, (int)fsModifiers.Shift | (int)fsModifiers.Alt, (int)Keys.D3);

            //Score subtract hotkeys        
            Form1.RegisterHotKey(this.Handle, 13, (int)fsModifiers.Shift | (int)fsModifiers.Alt | (int)fsModifiers.Control, (int)Keys.S);
            Form1.RegisterHotKey(this.Handle, 14, (int)fsModifiers.Shift | (int)fsModifiers.Alt | (int)fsModifiers.Control, (int)Keys.B);
            Form1.RegisterHotKey(this.Handle, 15, (int)fsModifiers.Shift | (int)fsModifiers.Alt | (int)fsModifiers.Control, (int)Keys.G);
            Form1.RegisterHotKey(this.Handle, 16, (int)fsModifiers.Shift | (int)fsModifiers.Alt | (int)fsModifiers.Control, (int)Keys.Q);
            Form1.RegisterHotKey(this.Handle, 17, (int)fsModifiers.Shift | (int)fsModifiers.Alt | (int)fsModifiers.Control, (int)Keys.M);
            Form1.RegisterHotKey(this.Handle, 18, (int)fsModifiers.Shift | (int)fsModifiers.Alt | (int)fsModifiers.Control, (int)Keys.C);
            Form1.RegisterHotKey(this.Handle, 19, (int)fsModifiers.Shift | (int)fsModifiers.Alt | (int)fsModifiers.Control, (int)Keys.L);
            Form1.RegisterHotKey(this.Handle, 20, (int)fsModifiers.Shift | (int)fsModifiers.Alt | (int)fsModifiers.Control, (int)Keys.O);
            Form1.RegisterHotKey(this.Handle, 21, (int)fsModifiers.Shift | (int)fsModifiers.Alt | (int)fsModifiers.Control, (int)Keys.D);
            Form1.RegisterHotKey(this.Handle, 22, (int)fsModifiers.Shift | (int)fsModifiers.Alt | (int)fsModifiers.Control, (int)Keys.D1);
            Form1.RegisterHotKey(this.Handle, 23, (int)fsModifiers.Shift | (int)fsModifiers.Alt | (int)fsModifiers.Control, (int)Keys.D2);
            Form1.RegisterHotKey(this.Handle, 24, (int)fsModifiers.Shift | (int)fsModifiers.Alt | (int)fsModifiers.Control, (int)Keys.D3);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            panelLeft.BringToFront();
            panelLeft.Top = button1.Top;
            button1.BackColor = System.Drawing.Color.Gray;

            var pos = this.PointToScreen(timer.Location);
            pos = pictureBox1.PointToClient(pos);
            timer.Parent = pictureBox4;
            timer.Location = pos;
            timer.BackColor = System.Drawing.Color.Transparent;

            maxteamsPanel.Visible = false;

            panelLeft.Height = button1.Height;
            panelLeft.Top = button1.Top;



            //sloopAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //sloopSub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //brigSub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //brigAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //galleonAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //galleonSub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //ghostAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //ghostSub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //megSub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //megAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //crierAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //crierSub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //sellAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //sellSub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //ownSub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //ownAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //deathAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //deathSub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //chickenAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //chickenSub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //snakeAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //snakeSub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //pigsAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //pugsSub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
        }


        //Edit Button Functionality
        private void sloopAdd_Click(object sender, EventArgs e)
        {
            sloops += 1;
            totalSloop.Text = sloops.ToString();
            sloopPoints += 10;
            addPoints();

        }

        private void sloopSub_Click(object sender, EventArgs e)
        {
            if (sloops > 0)
            {
                sloops -= 1;
                totalSloop.Text = sloops.ToString();
                sloopPoints -= 10;
                addPoints();
            }
        }

        private void brigAdd_Click(object sender, EventArgs e)
        {
            brigs += 1;
            totalBrig.Text = brigs.ToString();
            brigPoints += 15;
            addPoints();
        }

        private void brigSub_Click(object sender, EventArgs e)
        {
            if (brigs > 0)
            {
                brigs -= 1;
                totalBrig.Text = brigs.ToString();
                brigPoints -= 15;
                addPoints();
            }
        }

        private void galleonSub_Click(object sender, EventArgs e)
        {
            if (galleons > 0)
            {
                galleons -= 1;
                totalGalleon.Text = galleons.ToString();
                galleonPoints -= 25;
                addPoints();
            }
        }

        private void galleonAdd_Click(object sender, EventArgs e)
        {
            galleons += 1;
            totalGalleon.Text = galleons.ToString();
            galleonPoints += 25;
            addPoints();
        }

        private void ghostSub_Click(object sender, EventArgs e)
        {
            if (ghostShips > 0)
            {
                ghostShips -= 1;
                totalGhost.Text = ghostShips.ToString();
                ghostShipPoints -= 7;
                addPoints();
            }
        }

        private void ghostAdd_Click(object sender, EventArgs e)
        {
            ghostShips += 1;
            totalGhost.Text = ghostShips.ToString();
            ghostShipPoints += 7;
            addPoints();
        }

        private void megSub_Click(object sender, EventArgs e)
        {
            if (megalodons > 0)
            {
                megalodons -= 1;
                totalMeg.Text = megalodons.ToString();
                megalodonPoints -= 5;
                addPoints();
            }
        }

        private void megAdd_Click(object sender, EventArgs e)
        {
            megalodons += 1;
            totalMeg.Text = megalodons.ToString();
            megalodonPoints += 5;
            addPoints();
        }

        private void crierSub_Click(object sender, EventArgs e)
        {
            if (cryingChests > 0)
            {
                cryingChests -= 1;
                totalCrying.Text = cryingChests.ToString();
                cryingChestPoints -= 3;
                addPoints();
            }
        }

        private void crierAdd_Click(object sender, EventArgs e)
        {
            cryingChests += 1;
            totalCrying.Text = cryingChests.ToString();
            cryingChestPoints += 3;
            addPoints();
        }

        private void sellSub_Click(object sender, EventArgs e)
        {
            if (sellables > 0)
            {
                sellables -= 1;
                totalSellables.Text = sellables.ToString();
                sellablePoints -= 2;
                addPoints();
            }
        }

        private void sellAdd_Click(object sender, EventArgs e)
        {
            sellables += 1;
            totalSellables.Text = sellables.ToString();
            sellablePoints += 2;
            addPoints();
        }

        private void ownSub_Click(object sender, EventArgs e)
        {
            if (ownShips > 0)
            {
                ownShips -= 1;
                totalOwnShip.Text = ownShips.ToString();
                ownShipPoints += 2;
                addPoints();
            }
        }

        private void ownAdd_Click(object sender, EventArgs e)
        {
            ownShips += 1;
            totalOwnShip.Text = ownShips.ToString();
            ownShipPoints -= 2;
            addPoints();
        }

        private void deathSub_Click(object sender, EventArgs e)
        {
            if (deaths > 0)
            {
                deaths -= 1;
                totalDeaths.Text = deaths.ToString();
                deathPoints += 1;
                addPoints();
            }
        }

        private void deathAdd_Click(object sender, EventArgs e)
        {
            deaths += 1;
            totalDeaths.Text = deaths.ToString();
            deathPoints -= 1;
            addPoints();
        }

        private void chickenSub_Click(object sender, EventArgs e)
        {
            if (chicken > 0)
            {
                chicken -= 1;
                totalChicken.Text = chicken.ToString();
                chickenPoints -= 3;
                addPoints();
            }
        }

        private void chickenAdd_Click(object sender, EventArgs e)
        {
            chicken += 1;
            totalChicken.Text = chicken.ToString();
            chickenPoints += 3;
            allAnimals();
            addPoints();
        }

        private void snakeSub_Click(object sender, EventArgs e)
        {
            if (snakes > 0)
            {
                snakes -= 1;
                totalSnakes.Text = snakes.ToString();
                snakePoints -= 4;
                addPoints();
            }
        }

        private void snakeAdd_Click(object sender, EventArgs e)
        {
            snakes += 1;
            totalSnakes.Text = snakes.ToString();
            snakePoints += 4;
            allAnimals();
            addPoints();
        }

        private void pugsSub_Click(object sender, EventArgs e)
        {
            if (pigs > 0)
            {
                pigs -= 1;
                totalPigs.Text = pigs.ToString();
                pigPoints -= 5;
                addPoints();
            }

        }

        private void pigsAdd_Click(object sender, EventArgs e)
        {
            pigs += 1;
            totalPigs.Text = pigs.ToString();
            pigPoints += 5;
            allAnimals();
            addPoints();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0.0)
            {
                this.Opacity -= 0.025;
            }
            else
            {
                timer2.Stop();
                Application.Exit();
            }
        }


        //Animation for panelLeft 
        private void panelLeftTimer_Tick(object sender, EventArgs e)
        {
            if (b1 == true && b4 == false && b5 == false)
            {

                while (panelLeft.Top > button1.Top)
                {
                    panelLeft.Top -= 1;
                }

                b1 = false;

            }

            if (b4 == true && b1 == false && b5 == false)
            {
                if (panelLeft.Top < button4.Top)
                {
                    while (panelLeft.Top <= button4.Top)
                    {
                        panelLeft.Top += 1;
                    }

                }
                else if (panelLeft.Top > button4.Top)
                {
                    while (panelLeft.Top > button4.Top)
                    {
                        panelLeft.Top -= 1;
                    }

                }
                else
                {
                    b4 = false;
                }
            }



            if (b5 == true && b1 == false && b4 == false)
            {

                while (panelLeft.Top < button5.Top)
                {
                    panelLeft.Top += 1;
                }

                b5 = false;
            }

        }

        private const int CS_DROPSHADOW = 0x00020000;

        private void label47_Click(object sender, EventArgs e)
        {

        }


        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            //form2 = new Form2();
            //form2.Show();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                // add the drop shadow flag for automatically drawing
                // a drop shadow around the form
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (rulessoundOn == true)
            {
                rulessoundOn = false;
                button3.BackColor = System.Drawing.Color.Red;
            }
            else if (rulessoundOn == false)
            {
                rulessoundOn = true;
                button3.BackColor = System.Drawing.Color.Transparent;
            }
        }

        private void rulesPanel_MouseClick(object sender, MouseEventArgs e)
        {

        }

        public bool isOpen = true;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (isOpen == true)
            {
                isOpen = false;
            }
            else if (isOpen == false)
            {
                isOpen = true;
            }
            else
            {
                timer3.Start();
            }
            timer3.Start();
        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            if (pictureBox2.Right >= 800 && isOpen == false)
            {
                pictureBox2.Left -= 10;
                rulesLabel.Visible = false;
            }

            else if (isOpen == true)
            {
                if (pictureBox2.Right <= 1455)
                {
                    pictureBox2.Left += 10;
                    rulesLabel.Visible = true;
                }

            }
        }

        //1st 2nd and 3rd place points
        private void tntCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            points1 = 0;
            points2 = 0;
            points3 = 0;
            if (tntCombo.SelectedItem.ToString() == "1st")
            {

                points1 += 3;
                addPoints();
            }
            else if (tntCombo.SelectedItem.ToString() == "2nd")
            {

                points2 += 2;
                addPoints();
            }
            else if (tntCombo.SelectedItem.ToString() == "3rd")
            {

                points3 += 1;
                addPoints();
            }
            else
            {
                points1 = 0;
                points2 = 0;
                points3 = 0;
                addPoints();
            }
        }

        private void bananaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            points4 = 0;
            points5 = 0;
            points6 = 0;
            if (bananaCombo.SelectedItem.ToString() == "1st")
            {
                points4 += 3;
                addPoints();
            }
            else if (bananaCombo.SelectedItem.ToString() == "2nd")
            {
                points5 += 2;
                addPoints();
            }
            else if (bananaCombo.SelectedItem.ToString() == "3rd")
            {
                points6 += 1;
                addPoints();
            }
            else
            {
                points4 = 0;
                points5 = 0;
                points6 = 0;
                addPoints();
            }
        }

        private void planksCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            points7 = 0;
            points8 = 0;
            points9 = 0;
            if (planksCombo.SelectedItem.ToString() == "1st")
            {
                points7 += 3;
                addPoints();
            }
            else if (planksCombo.SelectedItem.ToString() == "2nd")
            {
                points8 += 2;
                addPoints();
            }
            else if (planksCombo.SelectedItem.ToString() == "3rd")
            {
                points9 += 1;
                addPoints();
            }
            else
            {
                points7 = 0;
                points8 = 0;
                points9 = 0;
                addPoints();
            }
        }

        private void cannonCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            points10 = 0;
            points11 = 0;
            points12 = 0;
            if (cannonCombo.SelectedItem.ToString() == "1st")
            {
                points10 += 3;
                addPoints();
            }
            else if (cannonCombo.SelectedItem.ToString() == "2nd")
            {
                points11 += 2;
                addPoints();
            }
            else if (cannonCombo.SelectedItem.ToString() == "3rd")
            {
                points12 += 1;
                addPoints();
            }
            else
            {
                points10 = 0;
                points11 = 0;
                points12 = 0;
                addPoints();
            }
        }

        private void specialCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            points13 = 0;
            points14 = 0;
            points15 = 0;
            if (specialCombo.SelectedItem.ToString() == "1st")
            {
                points13 += 3;
                addPoints();
            }
            else if (specialCombo.SelectedItem.ToString() == "2nd")
            {
                points14 += 2;
                addPoints();
            }
            else if (specialCombo.SelectedItem.ToString() == "3rd")
            {
                points15 += 1;
                addPoints();
            }
            else
            {
                points13 = 0;
                points14 = 0;
                points15 = 0;
                addPoints();
            }
        }

        //---------------------------------------------------------------

        private void teamStartButton_Click(object sender, EventArgs e)
        {
            if (teamBox.SelectedItem != null)
            {
                numTeams = teamBox.SelectedItem.ToString();
                gameStart = true;
                checkNumTeams();
                startGame();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (soundOn == true)
            {
                soundOn = false;
                button2.BackColor = System.Drawing.Color.Red;

            }
            else if (soundOn == false)
            {
                soundOn = true;
                button2.BackColor = System.Drawing.Color.Transparent;
            }
        }






        //-----------------------------------------------------------


        private void ceezLink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.twitch.tv/cdnthe3rd");
        }

        private void ceezLink_MouseHover(object sender, EventArgs e)
        {
            ceezLink.ForeColor = System.Drawing.Color.Yellow;

        }

        private void ceezLink_MouseLeave(object sender, EventArgs e)
        {
            ceezLink.ForeColor = System.Drawing.Color.Gray;
        }

        private void reqLink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.twitch.tv/requiemslaps");
        }

        private void reqLink_MouseHover(object sender, EventArgs e)
        {
            reqLink.ForeColor = System.Drawing.Color.DodgerBlue;
        }

        private void reqLink_MouseLeave(object sender, EventArgs e)
        {
            reqLink.ForeColor = System.Drawing.Color.Gray;
        }




        //---------Checkbox Logic----------
        public void allAnimals()
        {
            if (chicken >= 1 && snakes >= 1 && pigs >= 1 && chalComplete == false)
            {
                animalPoints = 3;
                addPoints();
                chalComplete = true;
                player = new System.Media.SoundPlayer(Properties.Resources.allAnimalsSound);
                player.Play();
            }
        }

        private void barfightCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (barfightCheck.Checked)
            {
                barfightPoints = 2;
                addPoints();

                if (soundOn)
                {
                    player = new System.Media.SoundPlayer(Properties.Resources.buttonclick);
                    player.Play();
                }
            }
            else
            {
                barfightPoints = 0;
                addPoints();
            }
        }

        private void bananaCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (bananaCheck.Checked)
            {
                bananaPoints = 3;
                addPoints();

                if (soundOn)
                {
                    player = new System.Media.SoundPlayer(Properties.Resources.buttonclick);
                    player.Play();
                }
            }
            else
            {
                bananaPoints = 0;
                addPoints();
            }
        }

        private void specialCannon_CheckedChanged(object sender, EventArgs e)
        {
            if (specialCannon.Checked)
            {
                scannonPoints = 3;
                addPoints();

                if (soundOn)
                {
                    player = new System.Media.SoundPlayer(Properties.Resources.buttonclick);
                    player.Play();
                }
            }
            else
            {
                scannonPoints = 0;
                addPoints();
            }
        }

        private void cannonCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (cannonCheck.Checked)
            {
                cannonPoints = 3;
                addPoints();

                if (soundOn)
                {
                    player = new System.Media.SoundPlayer(Properties.Resources.buttonclick);
                    player.Play();
                }
            }
            else
            {
                cannonPoints = 0;
                addPoints();
            }
        }

        private void plankCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (plankCheck.Checked)
            {
                plankPoints = 3;
                addPoints();

                if (soundOn)
                {
                    player = new System.Media.SoundPlayer(Properties.Resources.buttonclick);
                    player.Play();
                }
            }
            else
            {
                plankPoints = 0;
                addPoints();
            }
        }

        private void tntCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (tntCheck.Checked)
            {
                tntPoints = 3;
                addPoints();

                if (soundOn)
                {
                    player = new System.Media.SoundPlayer(Properties.Resources.buttonclick);
                    player.Play();
                }
            }
            else
            {
                tntPoints = 0;
                addPoints();
            }
        }

        private void nonTNTCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (nonTNTCheck.Checked)
            {
                notntPoints = -5;
                addPoints();

                if (soundOn)
                {
                    player = new System.Media.SoundPlayer(Properties.Resources.buttonclick);
                    player.Play();
                }
            }
            else
            {
                notntPoints = 0;
                addPoints();
            }
        }

        private void rowBoatCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (rowBoatCheck.Checked)
            {
                rowboatPoitns = 2;
                addPoints();

                if (soundOn)
                {
                    player = new System.Media.SoundPlayer(Properties.Resources.buttonclick);
                    player.Play();
                }
            }
            else
            {
                rowboatPoitns = 0;
                addPoints();
            }
        }

        private void mbCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (mbCheck.Checked)
            {
                megabombPoints = 7;
                addPoints();

                if (soundOn)
                {
                    player = new System.Media.SoundPlayer(Properties.Resources.buttonclick);
                    player.Play();
                }
            }
            else
            {
                megabombPoints = 0;
                addPoints();
            }
        }


        //hotkey actions
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();

                switch (id)
                {
                    case 1:
                        sloops += 1;
                        sloopPoints += 10;
                        totalSloop.Text = sloops.ToString();
                        addPoints();

                        if (soundOn)
                        {
                            player = new System.Media.SoundPlayer(Properties.Resources.sloopsound);
                            player.Play();
                        }
                        break;
                    case 2:
                        brigs += 1;
                        brigPoints += 15;
                        totalBrig.Text = brigs.ToString();
                        addPoints();

                        if (soundOn)
                        {
                            player = new System.Media.SoundPlayer(Properties.Resources.brigsound);
                            player.Play();
                        }
                        break;
                    case 3:
                        galleons += 1;
                        galleonPoints += 25;
                        totalGalleon.Text = galleons.ToString();
                        addPoints();

                        if (soundOn)
                        {
                            player = new System.Media.SoundPlayer(Properties.Resources.galleon);
                            player.Play();
                        }
                        break;
                    case 4:
                        ghostShips += 1;
                        ghostShipPoints += 7;
                        totalGhost.Text = ghostShips.ToString();
                        addPoints();

                        if (soundOn)
                        {
                            player = new System.Media.SoundPlayer(Properties.Resources.ghostaudio);
                            player.Play();
                        }
                        break;
                    case 5:
                        megalodons += 1;
                        megalodonPoints += 5;
                        totalMeg.Text = megalodons.ToString();
                        addPoints();

                        if (soundOn)
                        {
                            player = new System.Media.SoundPlayer(Properties.Resources.megsound);
                            player.Play();
                        }
                        break;
                    case 6:
                        cryingChests += 1;
                        cryingChestPoints += 3;

                        totalCrying.Text = cryingChests.ToString();

                        if (cryingChests > 0)
                        {
                            crierMult = true;
                        }
                        addPoints();


                        if (soundOn)
                        {
                            player = new System.Media.SoundPlayer(Properties.Resources.crieraudio);
                            player.Play();
                        }
                        break;
                    case 7:
                        sellables += 1;
                        sellablePoints += 2;
                        totalSellables.Text = sellables.ToString();
                        addPoints();

                        if (soundOn)
                        {
                            player = new System.Media.SoundPlayer(Properties.Resources.sellablesaudio);
                            player.Play();
                        }
                        break;
                    case 8:
                        ownShips += 1;
                        ownShipPoints -= 2;
                        totalOwnShip.Text = ownShips.ToString();
                        addPoints();

                        if (soundOn)
                        {
                            player = new System.Media.SoundPlayer(Properties.Resources.sunkship);
                            player.Play();
                        }
                        break;
                    case 9:
                        deaths += 1;
                        deathPoints -= 1;
                        totalDeaths.Text = deaths.ToString();
                        addPoints();

                        if (soundOn)
                        {
                            player = new System.Media.SoundPlayer(Properties.Resources.deathsound);
                            player.Play();
                        }
                        break;
                    case 10:
                        chicken += 1;
                        chickenPoints += 3;
                        totalChicken.Text = chicken.ToString();
                        addPoints();

                        if (soundOn)
                        {
                            player = new System.Media.SoundPlayer(Properties.Resources.chickensound);
                            player.Play();
                            allAnimals();
                        }
                        break;
                    case 11:
                        snakes += 1;
                        snakePoints += 4;
                        totalSnakes.Text = snakes.ToString();
                        addPoints();

                        if (soundOn)
                        {
                            player = new System.Media.SoundPlayer(Properties.Resources.snakeaudio);
                            player.Play();
                            allAnimals();
                        }
                        break;
                    case 12:
                        pigs += 1;
                        pigPoints += 5;
                        totalPigs.Text = pigs.ToString();
                        addPoints();

                        if (soundOn)
                        {
                            player = new System.Media.SoundPlayer(Properties.Resources.pigsound);
                            player.Play();
                            allAnimals();
                        }
                        break;
                    case 13:
                        if (sloops > 0)
                        {
                            sloops -= 1;
                            sloopPoints -= 10;
                            totalSloop.Text = sloops.ToString();
                            addPoints();

                            if (soundOn)
                            {
                                player = new System.Media.SoundPlayer(Properties.Resources.pop);
                                player.Play();
                            }
                        }
                        break;
                    case 14:
                        if (brigs > 0)
                        {
                            brigs -= 1;
                            brigPoints -= 15;
                            totalBrig.Text = brigs.ToString();
                            addPoints();

                            if (soundOn)
                            {
                                player = new System.Media.SoundPlayer(Properties.Resources.pop);
                                player.Play();
                            }
                        }
                        break;
                    case 15:
                        if (galleons > 0)
                        {
                            galleons -= 1;
                            galleonPoints -= 25;
                            totalGalleon.Text = galleons.ToString();
                            addPoints();

                            if (soundOn)
                            {
                                player = new System.Media.SoundPlayer(Properties.Resources.pop);
                                player.Play();
                            }
                        }
                        break;
                    case 16:
                        if (ghostShips > 0)
                        {
                            ghostShips -= 1;
                            ghostShipPoints -= 7;
                            totalGhost.Text = ghostShips.ToString();
                            addPoints();

                            if (soundOn)
                            {
                                player = new System.Media.SoundPlayer(Properties.Resources.pop);
                                player.Play();
                            }
                        }
                        break;
                    case 17:
                        if (megalodons > 0)
                        {
                            megalodons -= 1;
                            megalodonPoints -= 5;
                            totalMeg.Text = megalodons.ToString();
                            addPoints();

                            if (soundOn)
                            {
                                player = new System.Media.SoundPlayer(Properties.Resources.pop);
                                player.Play();
                            }
                        }
                        break;
                    case 18:
                        if (cryingChests > 0)
                        {
                            cryingChests -= 1;
                            cryingChestPoints -= 3;
                            totalCrying.Text = cryingChests.ToString();

                            if (cryingChests == 0)
                            {
                                crierMult = false;
                            }
                            addPoints();

                            if (soundOn)
                            {
                                player = new System.Media.SoundPlayer(Properties.Resources.pop);
                                player.Play();
                            }
                        }
                        break;
                    case 19:
                        if (sellables > 0)
                        {
                            sellables -= 1;
                            sellablePoints -= 2;
                            totalSellables.Text = sellables.ToString();
                            addPoints();

                            if (soundOn)
                            {
                                player = new System.Media.SoundPlayer(Properties.Resources.pop);
                                player.Play();
                            }
                        }
                        break;
                    case 20:
                        if (ownShips > 0)
                        {
                            ownShips -= 1;
                            ownShipPoints += 2;
                            totalOwnShip.Text = ownShips.ToString();
                            addPoints();

                            if (soundOn)
                            {
                                player = new System.Media.SoundPlayer(Properties.Resources.pop);
                                player.Play();
                            }
                        }
                        break;
                    case 21:
                        if (deaths > 0)
                        {
                            deaths -= 1;
                            deathPoints += 1;
                            totalDeaths.Text = deaths.ToString();
                            addPoints();

                            if (soundOn)
                            {
                                player = new System.Media.SoundPlayer(Properties.Resources.pop);
                                player.Play();
                            }
                        }
                        break;
                    case 22:
                        if (chicken > 0)
                        {
                            chicken -= 1;
                            chickenPoints -= 3;
                            totalChicken.Text = chicken.ToString();
                            addPoints();

                            if (soundOn)
                            {
                                player = new System.Media.SoundPlayer(Properties.Resources.pop);
                                player.Play();
                            }
                        }
                        break;
                    case 23:
                        if (snakes > 0)
                        {
                            snakes -= 1;
                            snakePoints -= 4;
                            totalSnakes.Text = snakes.ToString();
                            addPoints();

                            if (soundOn)
                            {
                                player = new System.Media.SoundPlayer(Properties.Resources.pop);
                                player.Play();
                            }
                        }
                        break;
                    case 24:
                        if (pigs > 0)
                        {
                            pigs -= 1;
                            pigPoints -= 5;
                            totalPigs.Text = pigs.ToString();
                            addPoints();

                            if (soundOn)
                            {
                                player = new System.Media.SoundPlayer(Properties.Resources.pop);
                                player.Play();
                            }
                        }
                        break;

                }
            }

            base.WndProc(ref m);
        }


        //Total points
        private void addPoints()
        {
            total = megalodonPoints + galleonPoints + pigPoints +
                    snakePoints + chickenPoints + deathPoints +
                    ownShipPoints + sellablePoints + cryingChestPoints +
                    megabombPoints + ghostShipPoints + brigPoints + sloopPoints +
                    rowboatPoitns + notntPoints + tntPoints + bananaPoints +
                    plankPoints + cannonPoints + scannonPoints + animalPoints + barfightPoints +
                    points1 + points2 + points3 + points4 + points5 + points6 + points7 + points8 +
                    points9 + points10 + points11 + points12 + points13 + points14 + points15;

            totalPoints.Text = total.ToString();
        }



        //--------Start menu button controls-----------//
        private void button1_Click(object sender, EventArgs e)
        {
            b1 = true;
            panelLeftTimer.Start();

            homePanel.BringToFront();
            panelLeft.BringToFront();

            rulesPanel.Visible = false;
            hotkeyPanel.Visible = false;

            button1.BackColor = System.Drawing.Color.Gray;
            button4.BackColor = System.Drawing.ColorTranslator.FromHtml("#292c33");
            button5.BackColor = System.Drawing.ColorTranslator.FromHtml("#292c33");
        }



        private void button4_Click(object sender, EventArgs e)
        {
            b4 = true;
            panelLeftTimer.Start();

            rulesPanel.Visible = true;
            hotkeyPanel.Visible = false;
            rulesPanel.BringToFront();

            button4.BackColor = System.Drawing.Color.Gray;
            button1.BackColor = System.Drawing.ColorTranslator.FromHtml("#292c33");
            button5.BackColor = System.Drawing.ColorTranslator.FromHtml("#292c33");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            b5 = true;
            panelLeftTimer.Start();


            hotkeyPanel.Visible = true;
            rulesPanel.Visible = true;
            hotkeyPanel.BringToFront();
            panelLeft.BringToFront();

            button5.BackColor = System.Drawing.Color.Gray;
            button4.BackColor = System.Drawing.ColorTranslator.FromHtml("#292c33");
            button1.BackColor = System.Drawing.ColorTranslator.FromHtml("#292c33");
        }
        //--------End menu button controls-----------//


        //For Window Movement
        public void panel5_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        Point lastPoint;


        public void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel4_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void minButton_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        //Reset Logic
        private void resetButton_Click(object sender, EventArgs e)
        {
            string resetString = "Are you sure you want to reset?";
            gameReset(resetString);
        }


        //Game Reset
        private void gameReset(string resetString)
        {
            DialogResult dr = MessageBox.Show(resetString,
                      "Reset", MessageBoxButtons.YesNo);

            switch (dr)
            {
                case DialogResult.Yes:
                    timer1.Stop();
                    startButton.Enabled = true;
                    timer.Text = "00:00:00";

                    //Reset Count
                    sloops = 0;
                    brigs = 0;
                    galleons = 0;
                    ghostShips = 0;
                    megalodons = 0;
                    cryingChests = 0;
                    sellables = 0;
                    ownShips = 0;
                    deaths = 0;
                    chicken = 0;
                    snakes = 0;
                    pigs = 0;

                    //Reset points
                    sloopPoints = 0;
                    brigPoints = 0;
                    galleonPoints = 0;
                    ghostShipPoints = 0;
                    megalodonPoints = 0;
                    cryingChestPoints = 0;
                    sellablePoints = 0;
                    ownShipPoints = 0;
                    deathPoints = 0;
                    chickenPoints = 0;
                    snakePoints = 0;
                    pigPoints = 0;
                    animalPoints = 0;
                    barfightPoints = 0;
                    points1 = 0;
                    points2 = 0;
                    points3 = 0;
                    points4 = 0;
                    points5 = 0;
                    points6 = 0;
                    points7 = 0;
                    points8 = 0;
                    points9 = 0;
                    points10 = 0;
                    points11 = 0;
                    points12 = 0;
                    points13 = 0;
                    points14 = 0;
                    points15 = 0;

                    //Reset Labels
                    totalSloop.Text = sloops.ToString();
                    totalBrig.Text = brigs.ToString();
                    totalGalleon.Text = galleons.ToString();
                    totalGhost.Text = ghostShips.ToString();
                    totalMeg.Text = megalodons.ToString();
                    totalCrying.Text = cryingChests.ToString();
                    totalSellables.Text = sellables.ToString();
                    totalOwnShip.Text = ownShips.ToString();
                    totalDeaths.Text = deaths.ToString();
                    totalChicken.Text = chicken.ToString();
                    totalSnakes.Text = snakes.ToString();
                    totalPigs.Text = pigs.ToString();
                    totalOwnShip.Text = ownShips.ToString();
                    totalPoints.Text = "0";
                    total = 0;

                    //Uncheck boxes
                    mbCheck.CheckState = CheckState.Unchecked;
                    rowBoatCheck.CheckState = CheckState.Unchecked;
                    nonTNTCheck.CheckState = CheckState.Unchecked;
                    tntCheck.CheckState = CheckState.Unchecked;
                    bananaCheck.CheckState = CheckState.Unchecked;
                    plankCheck.CheckState = CheckState.Unchecked;
                    cannonCheck.CheckState = CheckState.Unchecked;
                    specialCannon.CheckState = CheckState.Unchecked;
                    barfightCheck.CheckState = CheckState.Unchecked;

                    crierMult = false;
                    chalComplete = false;
                    gameStart = false;


                    sloopAdd.Visible = false;
                    sloopSub.Visible = false;
                    brigSub.Visible = false;
                    brigAdd.Visible = false;
                    galleonAdd.Visible = false;
                    galleonSub.Visible = false;
                    ghostAdd.Visible = false;
                    ghostSub.Visible = false;
                    megSub.Visible = false;
                    megAdd.Visible = false;
                    crierAdd.Visible = false;
                    crierSub.Visible = false;
                    sellAdd.Visible = false;
                    sellSub.Visible = false;
                    ownSub.Visible = false;
                    ownAdd.Visible = false;
                    deathAdd.Visible = false;
                    deathSub.Visible = false;
                    chickenAdd.Visible = false;
                    chickenSub.Visible = false;
                    snakeAdd.Visible = false;
                    snakeSub.Visible = false;
                    pigsAdd.Visible = false;
                    pugsSub.Visible = false;

                    teamBox.SelectedIndex = -1;

                    maxteamsPanel.Visible = false;

                    tntCombo.Items.Remove("1st");
                    tntCombo.Items.Remove("2nd");
                    tntCombo.Items.Remove("3rd");
                    tntCombo.Items.Remove("");

                    planksCombo.Items.Remove("1st");
                    planksCombo.Items.Remove("2nd");
                    planksCombo.Items.Remove("3rd");
                    planksCombo.Items.Remove("");

                    bananaCombo.Items.Remove("1st");
                    bananaCombo.Items.Remove("2nd");
                    bananaCombo.Items.Remove("3rd");
                    bananaCombo.Items.Remove("");

                    cannonCombo.Items.Remove("1st");
                    cannonCombo.Items.Remove("2nd");
                    cannonCombo.Items.Remove("3rd");
                    cannonCombo.Items.Remove("");

                    specialCombo.Items.Remove("1st");
                    specialCombo.Items.Remove("2nd");
                    specialCombo.Items.Remove("3rd");
                    specialCombo.Items.Remove("");

                    tntCheck.Enabled = false;
                    bananaCheck.Enabled = false;
                    plankCheck.Enabled = false;
                    cannonCheck.Enabled = false;
                    specialCannon.Enabled = false;

                    crierCountLabel.Visible = false;
                    crierCount = 600;
                    ;

                    timer.Font = new Font("Old English Text MT", 80f);
                    timer.TextAlign = ContentAlignment.MiddleLeft;
                    break;

                case DialogResult.No:
                    break;
            }
        }


        //Timer Logic
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (seconds > 0)
            {
                timer1.Interval = 1000;
                seconds = seconds - 1;
                timer.Text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");

                if (crierMult == true)
                {
                    crierCountLabel.Visible = true;
                    crierCount -= 1;
                    crierCountLabel.Text = crierCount.ToString();
                    if (crierCount == 0)
                    {
                        crierCount = 600;
                        cryingChestPoints += (cryingChests * 3);
                        addPoints();

                        if (soundOn)
                        {
                            player = new System.Media.SoundPlayer(Properties.Resources.crieraudio);
                            player.Play();

                        }
                        
                    }
                }
                else
                {
                    crierCountLabel.Visible = false;
                }

                if (hours == 00 && minutes == 45 && seconds == 00)
                {
                    player = new System.Media.SoundPlayer(Properties.Resources.hourMark);
                    player.Play();
                }

            }
            else if (seconds == 0 && minutes > 0)
            {
                timer1.Interval = 1;
                minutes -= 1;
                seconds = 60;
            }
            else if (minutes == 0 && hours > 0)
            {
                timer1.Interval = 1;
                hours -= 1;
                minutes = 60;
            }
            else
            {
                timer1.Stop();
                startButton.Enabled = true;
                gameOver = true;
                timer.Font = new Font("Old English Text MT", 50f);
                timer.TextAlign = ContentAlignment.MiddleCenter;
                timer.Text = "Times Up!";

                player = new System.Media.SoundPlayer(Properties.Resources.timeralarm);
                player.Play();

                //player1 = new System.Media.SoundPlayer(Properties.Resources.lowerCannons);
                //player1.Play();

            }

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            string resetString = "Do you want to start a new game?";
            if (gameOver == false)
            {
                startGame();
            }
            else
            {
                gameReset(resetString);
                startGame();
            }


        }

        private void startGame()
        {
            teamsPanel.Visible = true;

            if (gameStart == true)
            {
                teamsPanel.Visible = false;

                hours = 1;
                minutes = 30;
                seconds = 00;
                timer1.Start();
                startButton.Enabled = false;

                if (soundOn)
                {
                    player = new System.Media.SoundPlayer(Properties.Resources.startsound);
                    player.Play();
                }


                sloopAdd.Visible = true;
                sloopSub.Visible = true;
                brigSub.Visible = true;
                brigAdd.Visible = true;
                galleonAdd.Visible = true;
                galleonSub.Visible = true;
                ghostAdd.Visible = true;
                ghostSub.Visible = true;
                megSub.Visible = true;
                megAdd.Visible = true;
                crierAdd.Visible = true;
                crierSub.Visible = true;
                sellAdd.Visible = true;
                sellSub.Visible = true;
                ownSub.Visible = true;
                ownAdd.Visible = true;
                deathAdd.Visible = true;
                deathSub.Visible = true;
                chickenAdd.Visible = true;
                chickenSub.Visible = true;
                snakeAdd.Visible = true;
                snakeSub.Visible = true;
                pigsAdd.Visible = true;
                pugsSub.Visible = true;

                tntCheck.Enabled = true;
                bananaCheck.Enabled = true;
                plankCheck.Enabled = true;
                cannonCheck.Enabled = true;
                specialCannon.Enabled = true;
            }
        }




        private void checkNumTeams()
        {
            if (numTeams == "2")
            {
                maxteamsPanel.Visible = false;
            }
            else if (numTeams == "3")
            {
                maxteamsPanel.Visible = true;
                tntCombo.Items.Add("1st");
                tntCombo.Items.Add("2nd");
                tntCombo.Items.Add("");

                planksCombo.Items.Add("1st");
                planksCombo.Items.Add("2nd");
                planksCombo.Items.Add("");

                bananaCombo.Items.Add("1st");
                bananaCombo.Items.Add("2nd");
                bananaCombo.Items.Add("");

                cannonCombo.Items.Add("1st");
                cannonCombo.Items.Add("2nd");
                cannonCombo.Items.Add("");

                specialCombo.Items.Add("1st");
                specialCombo.Items.Add("2nd");
                specialCombo.Items.Add("");
            }
            else if (numTeams == "4" | numTeams == "5" | numTeams == "6")
            {
                maxteamsPanel.Visible = true;

                tntCombo.Items.Add("1st");
                tntCombo.Items.Add("2nd");
                tntCombo.Items.Add("3rd");
                tntCombo.Items.Add("");

                planksCombo.Items.Add("1st");
                planksCombo.Items.Add("2nd");
                planksCombo.Items.Add("3rd");
                planksCombo.Items.Add("");

                bananaCombo.Items.Add("1st");
                bananaCombo.Items.Add("2nd");
                bananaCombo.Items.Add("3rd");
                bananaCombo.Items.Add("");

                cannonCombo.Items.Add("1st");
                cannonCombo.Items.Add("2nd");
                cannonCombo.Items.Add("3rd");
                cannonCombo.Items.Add("");

                specialCombo.Items.Add("1st");
                specialCombo.Items.Add("2nd");
                specialCombo.Items.Add("3rd");
                specialCombo.Items.Add("");
            }
        }



        //Dunno
        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
