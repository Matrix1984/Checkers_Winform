using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class CheckersGame : Form
    {
        public CheckersGame()
        {
            InitializeComponent();
        }
        CheckersCode codeFile;
        IchecherMove[,] checkerPieces;
       
        private void Checkers_Load(object sender, EventArgs e)
        {
            codeFile = new CheckersCode();
            chessPics = Pattern();
            PrintPieces(codeFile.FirstLoad());
            checkerPieces = codeFile.FirstLoad();
            
        }
        public void YouMustEliminateThePieceToProceed()
        {
            MessageBox.Show("You must eliminate enemy piece to proceed");
        }

        public static Game gameRef;
        public void obtainGameReference(Game g)
        {

            gameRef=g;
            g=gameRef ;
        }

        public void InvalidOrValidMove()
        {
            if (Game.moveValidity)
            {
                Valid2.Text = "valid";
            }
            else
            {
                Valid2.Text = "invalid";
            }

        }

        public void WhiteLost()
        {
            MessageBox.Show("White player has lost the game");
        }
        public void BlackLost()
        {
            MessageBox.Show("Black player has lost the game");
        }


        PictureBox[,] chessPics = new PictureBox[9, 9];

        public void PrintPieces(IchecherMove[,] pieces)
        {
          
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (pieces[i, j] is Object)
                    {
                        chessPics[i, j].Image=pieces[i, j].print();
                    }
                    else
                    {
                        chessPics[i, j].Image = null;
                    }
                }
            }
        }


        public PictureBox[,] Pattern()
        {
            chessPics[1, 1] = a1;
            chessPics[1, 2] = b1;
            chessPics[1, 3] = c1;
            chessPics[1, 4] = d1;
            chessPics[1, 5] = e1;
            chessPics[1, 6] = f1;
            chessPics[1, 7] = g1;
            chessPics[1, 8] = h1;
            chessPics[2, 1] = a2;
            chessPics[2, 2] = b2;
            chessPics[2, 3] = c2;
            chessPics[2, 4] = d2;
            chessPics[2, 5] = e2;
            chessPics[2, 6] = f2;
            chessPics[2, 7] = g2;
            chessPics[2, 8] = h2;
            chessPics[3, 1] = c1;
            chessPics[3, 2] = b3;
            chessPics[3, 3] = c3;
            chessPics[3, 4] = d3;
            chessPics[3, 5] = e3;
            chessPics[3, 6] = f3;
            chessPics[3, 7] = g3;
            chessPics[3, 8] = h3;
            chessPics[4, 1] = a4;
            chessPics[4, 2] = b4;
            chessPics[4, 3] = c4;
            chessPics[4, 4] = d4;
            chessPics[4, 5] = e4;
            chessPics[4, 6] = f4;
            chessPics[4, 7] = g4;
            chessPics[4, 8] = h4;
            chessPics[5, 1] = e1;
            chessPics[5, 2] = b5;
            chessPics[5, 3] = c5;
            chessPics[5, 4] = d5;
            chessPics[5, 5] = e5;
            chessPics[5, 6] = f5;
            chessPics[5, 7] = g5;
            chessPics[5, 8] = h5;
            chessPics[6, 1] = a6;
            chessPics[6, 2] = b6;
            chessPics[6, 3] = c6;
            chessPics[6, 4] = d6;
            chessPics[6, 5] = e6;
            chessPics[6, 6] = f6;
            chessPics[6, 7] = g6;
            chessPics[6, 8] = h6;
            chessPics[7, 1] = g1;
            chessPics[7, 2] = b7;
            chessPics[7, 3] = c7;
            chessPics[7, 4] = d7;
            chessPics[7, 5] = e7;
            chessPics[7, 6] = f7;
            chessPics[7, 7] = g7;
            chessPics[7, 8] = h7;
            chessPics[8, 1] = a8;
            chessPics[8, 2] = b8;
            chessPics[8, 3] = c8;
            chessPics[8, 4] = d8;
            chessPics[8, 5] = e8;
            chessPics[8, 6] = f8;
            chessPics[8, 7] = g8;
            chessPics[8, 8] = h8;
            return chessPics;

        }

       public static bool NextTurn;
        int columnStart;
        int columnEnd;
        int rowEnd;
        int rowStart;

        public void YourTurn()
        {
            if (codeFile.PieceState() != null)
            {
                checkerPieces = codeFile.PieceState();
            }

            if (CheckersCode.counter % 2 == 0)
            {  
                label1.Text = "Black";
            }
            else
            {
                label1.Text = "Yellow";
            }
        }
  
        public void getArray(IchecherMove[,] checker)
        {
            checkerPieces = checker;
          
        }

        private void a1_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 1;
                rowEnd = 1;
                a1.Image = Properties.Resources.NullMarked;//(@"C:\MiniSteam\Checkers\Images\NullMarked.png");
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 1;
                rowStart = 1;
                a1.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;       
            }
        }

        private void b1_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 2;
                rowEnd = 1;
                b1.Image = Properties.Resources.NullMarked; //(@"C:\MiniSteam\Checkers\Images\NullMarked.png");
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 2;
                rowStart = 1;
                b1.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void c1_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 3;
                rowEnd = 1;
                c1.Image=Properties.Resources.NullMarked;//(@"C:\MiniSteam\Checkers\Images\NullMarked.png");
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 3;
                rowStart = 1;
                c1.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void d1_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 4;
                rowEnd = 1;
                d1.Image = Properties.Resources.NullMarked; //(@"C:\MiniSteam\Checkers\Images\NullMarked.png");
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 4;
                rowStart = 1;
                d1.Image=checkerPieces[rowStart,columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void e1_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 5;
                rowEnd = 1;
                d1.Image = Properties.Resources.NullMarked; // (@"C:\MiniSteam\Checkers\Images\NullMarked.png");
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 5;
                rowStart = 1;
                e1.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void f1_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 6;
                rowEnd = 1;
                f1.Image = Properties.Resources.NullMarked; //(@"C:\MiniSteam\Checkers\Images\NullMarked.png");
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 6;
                rowStart = 1;
                f1.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void g1_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 7;
                rowEnd = 1;
                g1.Image = Properties.Resources.NullMarked; // (@"C:\MiniSteam\Checkers\Images\NullMarked.png");//@"C:\MiniSteam\Checkers\Images\NullMarked.png"
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 7;
                rowStart = 1;
                g1.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void h1_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 8;
                rowEnd = 1;
                h1.Image = Properties.Resources.NullMarked;//(@"C:\MiniSteam\Checkers\Images\NullMarked.png");
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 8;
                rowStart = 1;
                h1.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void a2_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 1;
                rowEnd = 2;
                a2.Image = Properties.Resources.NullMarked; //(@"C:\MiniSteam\Checkers\Images\NullMarked.png");
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 1;
                rowStart = 2;
                a2.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void b2_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 2;
                rowEnd = 2;
                b2.Image=Properties.Resources.NullMarked;//.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 2;
                rowStart = 2;
                b2.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void c2_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 3;
                rowEnd = 2;
                c2.Image = Properties.Resources.NullMarked;//.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 3;
                rowStart = 2;
                c2.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void d2_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 4;
                rowEnd = 2;
                d2.Image = Properties.Resources.NullMarked;//.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 4;
                rowStart = 2;
                d2.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void e2_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 5;
                rowEnd = 2;
                e2.Image = Properties.Resources.NullMarked;//.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 5;
                rowStart = 2;
                e2.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void f2_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 6;
                rowEnd = 2;
                f2.Image = Properties.Resources.NullMarked;//.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 6;
                rowStart = 2;
                f2.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void g2_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 7;
                rowEnd = 2;
                g2.Image=Properties.Resources.NullMarked;//.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 7;
                rowStart = 2;

                g2.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void h2_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 8;
                rowEnd = 2;
                h2.Image=Properties.Resources.NullMarked;//.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 8;
                rowStart = 2;
                h2.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void a3_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 1;
                rowEnd = 3;
                a3.Image=Properties.Resources.NullMarked;//.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 1;
                rowStart = 3;
                a3.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void b3_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 2;
                rowEnd = 3;
                b3.Image = Properties.Resources.NullMarked;//.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 2;
                rowStart = 3;
                b3.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void c3_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 3;
                rowEnd = 3;
                c3.Image = Properties.Resources.NullMarked;//.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 3;
                rowStart = 3;
                c3.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void d3_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 4;
                rowEnd = 3;
                d3.Image = Properties.Resources.NullMarked;//.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 4;
                rowStart = 3;
                d3.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void e3_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 5;
                rowEnd = 3;
                e3.Image = Properties.Resources.NullMarked;//.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 5;
                rowStart = 3;
                e3.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void f3_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 6;
                rowEnd = 3;
                f3.Image = Properties.Resources.NullMarked;//.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                
                columnStart = 6;
                rowStart = 3;
                f3.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void g3_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 7;
                rowEnd = 3;
                g3.Image = Properties.Resources.NullMarked;//.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 7;
                rowStart = 3;
                g3.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void h3_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 8;
                rowEnd = 3;
                h3.Image = Properties.Resources.NullMarked;//.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 8;
                rowStart = 3;
                h3.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void a4_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 1;
                rowEnd = 4;
                a4.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 1;
                rowStart = 4;
                a4.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void b4_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 2;
                rowEnd = 4;
                b4.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 2;
                rowStart = 4;
                b4.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void c4_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 3;
                rowEnd = 4;
                c4.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 3;
                rowStart = 4;
                c4.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void d4_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 4;
                rowEnd = 4;
                d4.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 4;
                rowStart = 4;
                d4.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void e4_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 5;
                rowEnd = 4;
                e4.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 5;
                rowStart = 4;
                e4.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void f4_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 6;
                rowEnd = 4;
                f4.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 6;
                rowStart = 4;
                f4.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void g4_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 7;
                rowEnd = 4;
                g4.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 7;
                rowStart = 4;
                g4.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void h4_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 8;
                rowEnd = 4;
                h4.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 8;
                rowStart = 4;
                h4.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void a5_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 1;
                rowEnd = 5;
                a5.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 1;
                rowStart = 5;
                a5.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void b5_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 2;
                rowEnd = 5;
                b5.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 2;
                rowStart = 5;
                b5.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void c5_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 3;
                rowEnd = 5;
                c5.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 3;
                rowStart = 5;
                c5.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void d5_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 4;
                rowEnd = 5;
                d5.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 4;
                rowStart = 5;
                d5.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void e5_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 5;
                rowEnd = 5;
                e5.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 5;
                rowStart = 5;
                e5.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void f5_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 6;
                rowEnd = 5;
                f5.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 6;
                rowStart = 5;
                f5.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void g5_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 7;
                rowEnd = 5;
                g5.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 7;
                rowStart = 5;
                g5.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void h5_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 8;
                rowEnd = 5;
                h5.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 8;
                rowStart = 5;
                h5.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void a6_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 1;
                rowEnd = 6;
                a6.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 1;
                rowStart = 6;
                a6.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void b6_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 2;
                rowEnd = 6;
                b6.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 2;
                rowStart = 6;
                b6.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void c6_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 3;
                rowEnd = 6;
                c6.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 3;
                rowStart = 6;
                c6.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void d6_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 4;
                rowEnd = 6;
                d6.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 4;
                rowStart = 6;
                d6.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void e6_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 5;
                rowEnd = 6;
                e6.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 5;
                rowStart =  6;
                e6.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void f6_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 6;
                rowEnd = 6;
                f6.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 6;
                rowStart = 6;
                f6.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void g6_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 7;
                rowEnd = 6;
                g6.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 7;
                rowStart = 6;
                g6.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void h6_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 8;
                rowEnd = 6;
                h6.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 8;
                rowStart = 6;
                h6.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void a7_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 1;
                rowEnd = 7;
                a7.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 1;
                rowStart = 7;
                a7.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void b7_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 2;
                rowEnd = 7;
                b7.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 2;
                rowStart = 7;
                b7.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void c7_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 3;
                rowEnd = 7;
                c7.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 3;
                rowStart = 7;
                c7.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void d7_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 4;
                rowEnd = 7;
                d7.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 4;
                rowStart = 7;
                d7.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void e7_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 5;
                rowEnd = 7;
                e7.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 5;
                rowStart = 7;
                e7.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void f7_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 6;
                rowEnd = 7;
                f7.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 6;
                rowStart = 7;
                f7.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void g7_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 7;
                rowEnd = 7;
                g7.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 7;
                rowStart = 7;
                g7.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void h7_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 8;
                rowEnd = 7;
                h7.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 8;
                rowStart = 7;
                h7.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void a8_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 1;
                rowEnd = 8;
                a8.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 1;
                rowStart = 8;
                a8.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void b8_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 2;
                rowEnd = 8;
                b8.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 2;
                rowStart = 8;
                b8.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void c8_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 3;
                rowEnd = 8;
                c8.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 3;
                rowStart = 8;
                c8.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void d8_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 4;
                rowEnd = 8;
                d8.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 4;
                rowStart = 8;
                d8.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void e8_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 5;
                rowEnd = 8;
                e8.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 5;
                rowStart = 8;
                e8.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void f8_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 6;
                rowEnd = 8;
                f8.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 6;
                rowStart = 8;
                f8.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void g8_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 7;
                rowEnd = 8;
                g8.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 7;
                rowStart = 8;
                g8.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void h8_Click(object sender, EventArgs e)
        {
            if (NextTurn)
            {
                columnEnd = 8;
                rowEnd = 8;
                h8.Image = Properties.Resources.NullMarked;
                codeFile.ExecuteAll(rowStart, columnStart, rowEnd, columnEnd);
                gameRef.piecePromotion();
                InvalidOrValidMove();
                PrintPieces(codeFile.PieceState());
                NextTurn = false;
                YourTurn();
            }
            else
            {
                columnStart = 8;
                rowStart = 8;
                h8.Image=checkerPieces[rowStart, columnStart].printMarked();
                NextTurn = true;
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NextTurn = false;
        }
    }
}
