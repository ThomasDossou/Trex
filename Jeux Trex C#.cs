using System;
using System.Windows.Forms;

namespace T_Rex_Jeu
{
    public partial class T_Rex_Jeu_Form : Form
    {
        bool jumping = false;
        int jumpSpeed = 12;                                    //   Ce sont des variables gflobales que l'on va initialiser pour le jeu
        int force = 12;
        int score = 0;                                          //  par exemple la vitesse des obstacles les mots afficher lors de la mort         
        int obstacleSpeed = 10;                                 // la génération aléatoire des obstacles
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

            txtScore.Text = "Score " + score;                                     // Texte permettant d'afficher des paramètres comme le texte la gestion de vitesse et autre
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
                        GameReset();                                    // fonction permettantb de reinitialiser le jeux au contact d'un obstacle
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
                            x.Left = this.ClientSize.Width + rand.Next(200, 500) + (x.Width * 15);               // on determine la largfeur du formuulaire de la picturebox

                        }

                        if (x.Left<=252 && (trex.Top+trex.Height) >= x.Top)                                      // lorsque l'on meurt il affiche un message vrai et quand on est en vie un message faux
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
            if (e.KeyCode == Keys.Space && jumping == false)      // on defini espace pour que le Trex saute lorsque que l'on appui sur la touche 
            {                                                     // on veut pouvoir sauter qu'une seule fois pour qu'il puisse y avoir un effet de gravite et eviter ded suater à lo'infini

                jumping = true;
            }
            if (e.KeyCode == Keys.S)
            {
                if (nbPause < MaxPause)                            // on permet de pouvoir mettre pause sur kle jeu_ q'un certain nombre de fois
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

            if (e.KeyCode == Keys.R)// && isGameOver == true)             cette partie permet de pouvoir recommencer le jeu lorsque l'on appui sur R reset
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
            obstacleSpeed = 10;                                         // vitesse des obstacles 
            txtScore.Text = "Score: " + score;                          // affichage du score
            trex.Image = Properties.Resources.running;
            isGameOver = false;
            trex.Top = valInit;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Name.Contains("obstacle"))         // on prend l'entier de position que l'on a defini avant 
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
