using Moving_Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moving_Object
{
    public partial class FormView : Form
    {
        Objects Mario;
        const int size = 8;
        string[,] table = new string[size,size];
        PictureBox[] arrPicture;
        Bases[] arrBase = new Bases[4];
        Pins[] arrPin = new Pins[4];
        bool isPin = false;
        int vicCount = 0;

        public FormView()
        {
            InitializeComponent();
            arrPicture = new PictureBox[] {Character, Base_A, Base_B, Base_C, Base_D, Pin_A, Pin_B, Pin_C, Pin_D, Stone1,Stone2, Stone3, Stone4, Stone5, Stone6, Stone7,
                                            Stone8, Stone9, Stone10, Stone11, Stone12, Stone13, Stone14, Stone15,Stone16, Stone17, Stone18, Stone19, Stone20,
                                            Stone21, Stone22, Stone23, Stone24, Stone25, Stone26, Stone27, Stone28, Stone29, Stone30, Stone31, Stone32};
            Mario = new Objects(arrPicture[0].Left, arrPicture[0].Top, "Mario");
            arrBase[0] = new Bases(arrPicture[1].Left, arrPicture[1].Top, "BaseA");
            arrBase[1] = new Bases(arrPicture[2].Left, arrPicture[2].Top, "BaseB");
            arrBase[2] = new Bases(arrPicture[3].Left, arrPicture[3].Top, "BaseC");
            arrBase[3] = new Bases(arrPicture[4].Left, arrPicture[4].Top, "BaseD");
            arrPin[0] = new Pins(arrPicture[5].Left, arrPicture[5].Top, "PinA");
            arrPin[1] = new Pins(arrPicture[6].Left, arrPicture[6].Top, "PinB");
            arrPin[2] = new Pins(arrPicture[7].Left, arrPicture[7].Top, "PinC");
            arrPin[3] = new Pins(arrPicture[8].Left, arrPicture[8].Top, "PinD");
        }

        private void FormView_Paint(object sender, PaintEventArgs e)
        {
            
            
            //e.Graphics.FillRectangle(Brushes.LightGoldenrodYellow, Stone.PosX, Stone.PosY, Stone.Width, Stone.Heigth);
            
        }

        private void tmrMoving_Tick(object sender, EventArgs e)
        {
            //_x += 10;

            //Invalidate();
        }

        private void FormView_Load(object sender, EventArgs e)
        {            
            int auxI = 0;
            int auxJ = 0;
            int j = 0, i=0;

            for (i = 0; i < arrPicture.Length; i++)
            {
                if (i == 0)
                {//Mario
                    auxI = Mario.PosX / 49;
                    auxJ = Mario.PosY / 49;
                    table[auxI, auxJ] = Mario.Type;
                }

                if (i > 0 && i < 5)
                {//Bases
                    auxI = arrBase[i - 1].PosX / 49;
                    auxJ = arrBase[i - 1].PosY / 49;
                    table[auxI, auxJ] = arrBase[i - 1].Name;
                }

                if (i > 4 && i < 9)
                {//Pins
                    auxI = arrPin[i - 5].PosX / 49;
                    auxJ = arrPin[i - 5].PosY / 49;
                    table[auxI, auxJ] = arrPin[i - 5].Name;
                }
                if (i > 8)
                {//Stones
                    auxI = arrPicture[i].Left / 49;
                    auxJ = arrPicture[i].Top / 49;
                    table[auxI, auxJ] = "Stone";
                }
            }

            //Empty Spaces
            for (i = 1;i< table.GetLength(1)-1; i++)
            {
                for (j = 1;j<table.GetLength(0)-1;j++)
                {
                    if (table[i, j] == null)
                    { table[i, j] = "Empty"; }
                }
            }          
        }

        private bool Verify(int i, int j, int h, int v)
        {
            if (table[i + h, j+v] == "Empty")
            {
                return true;
            }

            if (table[i + h, j + v] == "Stone" || table[i + h, j + v].StartsWith("Base"))
            {
                return false;
            }

            if (table[i + h, j + v].StartsWith("Pin"))
            {
                if (table[i + 2*h, j + 2 * v] == "Empty" || table[i + 2 * h, j + 2 * v].StartsWith("Base"))
                {
                    isPin = true;
                    return true;
                }
            }

            return false;
        }

        private void MovePin(int i, int j, string dir)
        {
            int auxInd = 0;
            for (int k = 0; k < 4; k++)
            {
                if (arrPin[k].Name == table[i, j])
                {
                    auxInd = k;
                    break;
                }                
            }

            switch (dir)
            {
                case "right" : arrPin[auxInd].Right(); break;
                case "left": arrPin[auxInd].Left(); break;
                case "up": arrPin[auxInd].Up(); break;
                case "down": arrPin[auxInd].Down(); break;
            }
                        
            arrPicture[auxInd + 5].Left = arrPin[auxInd].PosX;
            arrPicture[auxInd + 5].Top = arrPin[auxInd].PosY;

            switch (dir)
            {
                case "right":
                    if (table[i + 1, j][table[i + 1, j].Length-1] == arrPin[auxInd].Name[arrPin[auxInd].Name.Length-1])
                    {//pinX met baseX
                        if (table[i + 1, j][table[i + 1, j].Length - 1] == 'A')
                        {
                            AOK.BringToFront();
                            vicCount++;
                        }

                        else if (table[i + 1, j][table[i + 1, j].Length - 1] == 'B')
                        {
                            BOK.BringToFront();
                            vicCount++;
                        }
                        else if (table[i + 1, j][table[i + 1, j].Length - 1] == 'C')
                        {
                            COK.BringToFront();
                            vicCount++;
                        }
                        else if (table[i + 1, j][table[i + 1, j].Length - 1] == 'D')
                        {
                            DOK.BringToFront();
                            vicCount++;
                        }
                    }

                    table[i + 1, j] = arrPin[auxInd].Name;
                    break;




                case "left":
                    if (table[i - 1, j][table[i - 1, j].Length - 1] == arrPin[auxInd].Name[arrPin[auxInd].Name.Length - 1])
                    {//pinX met baseX
                        if (arrPin[auxInd].Name[arrPin[auxInd].Name.Length - 1] == 'A')
                        {
                            AOK.BringToFront();
                            vicCount++;
                        }

                        else if (arrPin[auxInd].Name[arrPin[auxInd].Name.Length - 1] == 'B')
                        {
                            BOK.BringToFront();
                            vicCount++;
                        }
                        else if (arrPin[auxInd].Name[arrPin[auxInd].Name.Length - 1] == 'C')
                        {
                            COK.BringToFront();
                            vicCount++;
                        }
                        else if (arrPin[auxInd].Name[arrPin[auxInd].Name.Length - 1] == 'D')
                        {
                            DOK.BringToFront();
                            vicCount++;
                        }
                    }
                    table[i - 1, j] = arrPin[auxInd].Name;  break;
                case "up":
                    if (table[i , j-1][table[i , j-1].Length - 1] == arrPin[auxInd].Name[arrPin[auxInd].Name.Length - 1])
                    {//pinX met baseX
                        if (arrPin[auxInd].Name[arrPin[auxInd].Name.Length - 1] == 'A')
                        {
                            AOK.BringToFront();
                            vicCount++;
                        }

                        else if (arrPin[auxInd].Name[arrPin[auxInd].Name.Length - 1] == 'B')
                        {
                            BOK.BringToFront();
                            vicCount++;
                        }
                        else if (arrPin[auxInd].Name[arrPin[auxInd].Name.Length - 1] == 'C')
                        {
                            COK.BringToFront();
                            vicCount++;
                        }
                        else if (arrPin[auxInd].Name[arrPin[auxInd].Name.Length - 1] == 'D')
                        {
                            DOK.BringToFront();
                            vicCount++;
                        }
                    }
                    table[i , j-1] = arrPin[auxInd].Name;  break;
                case "down":
                    if (table[i, j + 1][table[i, j + 1].Length - 1] == arrPin[auxInd].Name[arrPin[auxInd].Name.Length - 1])
                    {//pinX met baseX
                        if (arrPin[auxInd].Name[arrPin[auxInd].Name.Length - 1] == 'A')
                        {
                            AOK.BringToFront();
                            vicCount++;
                        }

                        else if (arrPin[auxInd].Name[arrPin[auxInd].Name.Length - 1] == 'B')
                        {
                            BOK.BringToFront();
                            vicCount++;
                        }
                        else if (arrPin[auxInd].Name[arrPin[auxInd].Name.Length - 1] == 'C')
                        {
                            COK.BringToFront();
                            vicCount++;
                        }
                        else if (arrPin[auxInd].Name[arrPin[auxInd].Name.Length - 1] == 'D')
                        {
                            DOK.BringToFront();
                            vicCount++;
                        }
                    }
                    table[i , j+1] = arrPin[auxInd].Name;  break;
            }                     
        }

        private void FormView_KeyDown(object sender, KeyEventArgs e)
        {
            int auxX = (Mario.PosX) / 49; 
            int auxY = (Mario.PosY) / 49;
            bool check = false;
            isPin = false;
            
            if (e.KeyCode == Keys.Right)
            {
                check = Verify(auxX, auxY, 1, 0);
                if (check)
                {
                    if (isPin)
                    {
                        MovePin(auxX + 1, auxY, "right");                        
                    }

                    Mario.Right();
                    Character.Left = Mario.PosX;
                    table[auxX + 1, auxY] = Mario.Type;
                    table[auxX, auxY] = "Empty";
                    checkVictory();
                }
                
            }

            else if (e.KeyCode == Keys.Left)
            {
                check = Verify(auxX, auxY, -1, 0);
                if (check)
                {
                    if (isPin)
                    {
                        MovePin(auxX - 1, auxY, "left");
                    }

                    Mario.Left();
                    Character.Left = Mario.PosX;
                    table[auxX - 1, auxY] = Mario.Type;
                    table[auxX, auxY] = "Empty";
                    checkVictory();
                }
            }

            else if (e.KeyCode == Keys.Up)
            {
                check = Verify(auxX, auxY, 0, -1);
                if (check)
                {
                    if (isPin)
                    {
                        MovePin(auxX, auxY-1, "up");
                    }

                    Mario.Up();
                    Character.Top = Mario.PosY;
                    table[auxX, auxY-1] = Mario.Type;
                    table[auxX, auxY] = "Empty";
                    checkVictory();
                }
            }

            else if (e.KeyCode == Keys.Down)
            {
                check = Verify(auxX, auxY, 0, 1);
                if (check)
                {
                    if (isPin)
                    {
                        MovePin(auxX, auxY + 1, "down");
                    }

                    Mario.Down();
                    Character.Top = Mario.PosY;
                    table[auxX, auxY + 1] = Mario.Type;
                    table[auxX, auxY] = "Empty";
                    checkVictory();
                }
            }

            
            
        }

        private void checkVictory()
        {
            if (vicCount == 4)
            {
                MessageBox.Show("YOU WON!!!");
            }
        }

        private void FormView_PaddingChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox29_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox28_Click(object sender, EventArgs e)
        {

        }

        private void Pin_B_Click(object sender, EventArgs e)
        {

        }

        private void Base_B_Click(object sender, EventArgs e)
        {

        }

        private void BOK_Click(object sender, EventArgs e)
        {

        }
    }
}
