using System;
using System.Windows.Forms;

namespace T_Rex_Jeu
{
    public partial class T_Rex_Jeu_Form : Form
    {
        bool jumping = false;
        int jumpSpeed = 12;
        int force = 12;
        int score = 0;
        int obstacleSpeed = 10;
        Random rand = new Random();
        int position;
        bool isGameOver = false;
        bool isStarted = false;
        int valInit = 259;
        int MaxPause = 5;
        int nbPause = 0;
        bool isDead= false;




        public T_Rex_Jeu_Form()
        {
            InitializeComponent();

            GameReset();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            trex.Top += jumpSpeed;

            txtScore.Text = "Score " + score;
            txtPause.Text = "Pause " + nbPause;
            txtRunningSpeed.Text = "RunningSpeed " + obstacleSpeed;
            Dead.Text = "Dead " + isDead;

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }
            if (trex.Top > valInit - 1 && jumping == false)
            {
                force = 12;
                trex.Top = valInit;
                jumpSpeed = 0;

            }
            foreach(Control x in this.Controls)
            {
                 if (x is PictureBox && (string)x.Tag == "obstacle")
         
                {
                    if (x.Bounds.IntersectsWith(trex.Bounds))
                    {
                        GameReset();
                    }
                    x.BringToFront();
                }
                    


            }
            if (isStarted)
            {
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Name.Contains("obstacle"))
                    {
                        x.Left -= obstacleSpeed;

                        if (x.Left < -100)
                        {
                            x.Left = this.ClientSize.Width + rand.Next(200, 500) + (x.Width * 15);

                        }

                        if (x.Left<=252 && (trex.Top+trex.Height) >= x.Top)
                        {
                            isDead = true;
                        }
                        else
                        {
                            isDead = false;
                        }
                    }
                }
            }

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
            if (e.KeyCode == Keys.S)
            {
                if (nbPause < MaxPause)
                {
                    if(isStarted) nbPause++;
                    isStarted = !isStarted;
                }
                else if(!isStarted)
                {
                    isStarted = true;
                }
            }
            if (e.KeyCode == Keys.Up && obstacleSpeed < 10)
            {
                obstacleSpeed++;
            }
            if (e.KeyCode == Keys.Down && obstacleSpeed > 1)
            {
                obstacleSpeed--;
            }

        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.R)// && isGameOver == true)
            {
                GameReset();
            }


        }

        private void GameReset()
        {
            force = 12;
            jumpSpeed = 0;
            jumping = false;
            score = 0;
            obstacleSpeed = 10;
            txtScore.Text = "Score: " + score;
            trex.Image = Properties.Resources.running;
            isGameOver = false;
            trex.Top = valInit;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Name.Contains("obstacle"))
                {
                    if (x.Name.Contains("obstacle1")) x.Left = 453;
                    if (x.Name.Contains("obstacle2")) x.Left = 648;
                    // position = this.ClientSize.Width + rand.Next(500, 800) + (x.Width * 10);

                    // x.Left = position;
                }


            }

            GameTimer.Start();






        }

        private void obstacle_Click(object sender, EventArgs e)
        {

        }

        private void T_Rex_Jeu_Form_Load(object sender, EventArgs e)
        {

        }

        private void trex_Click(object sender, EventArgs e)
        {

        }

        private void obstacle1_Click(object sender, EventArgs e)
        {

        }
    }
}
