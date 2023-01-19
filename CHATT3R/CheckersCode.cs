using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace WindowsFormsApplication1
{
    
        class CheckersCode
        {
            IchecherMove[,] pieces;
            public static int counter;
            CheckersGame checkerBoard;
            Game g;
            public static bool whitesTurn;
            public static bool blacksTurn;
            public void ExecuteAll(int columnStart, int rowStart, int columnEnd, int rowEnd)
            {
                checkerBoard = new CheckersGame();
                checkerBoard.getArray(pieces);
                g = new Game(pieces, columnStart, rowStart, columnEnd, rowEnd);         
                g.MoveValidityManager();
                checkerBoard.obtainGameReference(g);


                if(!GameEnd.WhiteArePiecesLeft(pieces))
                {
                    checkerBoard.WhiteLost();
                }
                if(!GameEnd.BlackArePiecesLeft(pieces))
                {
                    checkerBoard.BlackLost();
                }
                checkerBoard.getArray(pieces);//returns pieces to the board class
            }

     
            public IchecherMove[,] FirstLoad()
            {
                pieces = allPiecesArray();
                return pieces;
            }

            public  IchecherMove[,] PieceState()
            {
                 return pieces;
            }

            public static IchecherMove[,]  allPiecesArray()
            {
                IchecherMove[,] checker = new IchecherMove[9, 9];

                checker[8, 1] = new Checker("W");
                checker[8, 3] = new Checker("W");
                checker[8, 5] = new Checker("W");
                checker[8, 7] = new Checker("W");

                checker[7, 2] = new Checker("W");
                checker[7, 4] = new Checker("W");
                checker[7, 6] = new Checker("W");
                checker[7, 8] = new Checker("W");

                checker[6, 1] = new Checker("W");
                checker[6, 3] = new Checker("W");
                checker[6, 5] = new Checker("W");
                checker[6, 7] = new Checker("W");




                checker[1, 2] = new Checker("B");
                checker[1, 4] = new Checker("B");
                checker[1, 6] = new Checker("B");
                checker[1, 8] = new Checker("B");

                checker[2, 1] = new Checker("B");
                checker[2, 3] = new Checker("B");
                checker[2, 5] = new Checker("B");
                checker[2, 7] = new Checker("B");

                checker[3, 2] = new Checker("B");
                checker[3, 4] = new Checker("B");
                checker[3, 6] = new Checker("B");
                checker[3, 8] = new Checker("B");

                return checker;
            }

        }
 


 public class Game
{
    int b = 0;
    int rowStart = 0;
    int rowEnd = 0;
    int columnStart = 0;
    int columnEnd = 0;

    string location = "";
    string destination = "";
    bool notNull = false;
    bool moveValid = false;
    bool whitePiece = false;
    bool BlackPiece = false;
    bool movingRight = false;
    bool movingLeft = false;
    bool takeover = false;
    bool kingMove = false;
    bool movingRightSide = false;
    bool movingLeftSide = false;
    bool movingUpSide = false;
    bool movingDownSide = false;
    bool movingRightUpSide = false;
    bool movingRightDownSide = false;
    bool movingLeftUpSide = false;
    bool movingLeftDownSide = false;
    bool correctTurn = false;
    bool pieceWhite = false;
    bool pieceBlack = false;
    IchecherMove[,] checker;
    public static bool moveValidity;
    Monitor supervise;
    CheckersGame ch;
    int rowStartMove;
    int columnStartMove;


    public Game(IchecherMove[,] checker2, int rowStart2, int columnStart2, int rowEnd2, int columnEnd2)
    {
        this.checker = checker2;
        this.rowStart = rowStart2;
        this.columnStart = columnStart2;
        this.rowEnd = rowEnd2;
        this.columnEnd = columnEnd2;
        this.checker = checker2;
        supervise = new Monitor();
        ch = new CheckersGame();
        rowStartMove = rowStart2;
        columnStartMove = columnStart2;
    }
        
    public void MoveValidityManager()
    {
        WhoseTurn();
        wasGeneralMoveValid();   
      
    } 

    public bool WhoseTurn()
    {
        correctTurn = false;
        //identifies whose turn it is
        if (CheckersCode.counter % 2 == 0 && checker[rowStart, columnStart].FindPiece()[0] == 'B')
        {
            CheckersCode.blacksTurn = true;
            correctTurn = true;
            CheckersCode.whitesTurn = false;
        }
     
        if (CheckersCode.counter % 2 != 0 && checker[rowStart,columnStart].FindPiece()[0]=='W')
        {
            CheckersCode.whitesTurn = true;
            CheckersCode.blacksTurn = false;
            correctTurn = true;
        }
       

        return correctTurn;
    }

    public bool wasGeneralMoveValid()
    {
        bool whiteSupervision = false;
        bool blackSupervision = false;
        bool noDevour = false;
        bool devourDoesntExit = false;
        bool pieceElimination=false;

        if(checker[rowStart, columnStart].pieceDevour(checker, rowStart, columnStart, rowEnd, columnEnd))
        {
            pieceElimination=true;
        }

        if (CheckersCode.counter % 2 !=0 && supervise.WhiteMonitor(checker, rowStart, columnStart, rowEnd, columnEnd))
        {
            whiteSupervision = true;
        }

        if (CheckersCode.counter % 2 == 0 && supervise.BlackMonitor(checker, rowStart, columnStart, rowEnd, columnEnd))
        {
            blackSupervision = true;
        }

     
        if (whiteSupervision || blackSupervision)// if there is a devour
        {
            if (supervise.doEndPositionsMatch(rowEnd, columnEnd))
            {
                noDevour = true;// must devour for the move to commit itself
            }
        
        }
        else// if there is not devour
        {
            devourDoesntExit = true;
        }

        //checks the validity of the move. if correct move the piece.
        if ((WhoseTurn() &&  pieceElimination) || WhoseTurn() && !(whiteSupervision || blackSupervision) && (checker[rowStartMove, columnStartMove].isMoveValid(checker, rowStartMove, columnStartMove, rowEnd, columnEnd)))
        {
                CheckersCode.counter++;
            
                moveValidity = true;
                checker[rowEnd, columnEnd] = checker[rowStart, columnStart];
                checker[rowStart, columnStart] = null;    
        }
        else
        {

            if (whiteSupervision || blackSupervision)
            {

                ch.YouMustEliminateThePieceToProceed();
            }
            moveValidity= false;
        }

        return moveValidity;
    }

   

    public void piecePromotion()
    {
        // transfers the king if he reached promotion
        if (checker[rowEnd, columnEnd] != null && ((checker[rowEnd, columnEnd].FindPiece() == "W" && rowEnd == 1)))
        {
            checker[rowEnd, columnEnd] = new King("WK");    
        }

        if (checker[rowEnd, columnEnd] != null && ((checker[rowEnd, columnEnd].FindPiece() == "B" && rowEnd == 8)))
        {
            checker[rowEnd, columnEnd] = new King("BK");
        }

    
    }
}


 public interface IchecherMove
    {
        bool isMoveValid(IchecherMove[,] checker, int columnStart, int rowStart, int columnEnd, int rowEnd);
        string FindPiece();
        Image print();
        Image printMarked();
        bool pieceDevour(IchecherMove[,] checker, int columnStart, int rowStart, int columnEnd, int rowEnd);
    }



    /// <summary>
    /// /////////////////////////////CLASS  CHECKER///////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>

    class Checker : IchecherMove
    {
        int columnStart;
        int rowStart;
        string name;
        bool valid;
        string firstLetter;
        string secondLetter;
        public static bool CheckIsOnDontMoveAnything;
        string chessPiece;
        bool secondLetterNotNull;
        string findPiece;
        int columnEnd;
        string letter;
        String Bishop = "";

        public Checker(string d)
        {
            this.letter = d;
        }


        public  bool isMoveValid(IchecherMove[,] checker, int rowStart, int columnStart, int rowEnd, int columnEnd)
        {

            bool whiteMove = false;
            bool blackMove = false;
            bool moveToNull = false;
            bool notNull = false;
            //checks whether the starting and moving positions are null or not
            if (checker[rowStart, columnStart] != null)
            {
                notNull = true;
            }

            if (checker[rowEnd, columnEnd] == null)
            {
                moveToNull = true;
            }
            // checks if the direction of the correct piece is right
            if (notNull && moveToNull && checker[rowStart, columnStart].FindPiece()[0] == 'B' && ((columnStart + 1 == columnEnd) || (columnStart - 1 == columnEnd)) && (rowStart + 1 == rowEnd))
            {
                whiteMove = true;
            }

            if (notNull && moveToNull && checker[rowStart, columnStart].FindPiece()[0] == 'W' && ((columnStart + 1 == columnEnd) || (columnStart - 1 == columnEnd)) && (rowStart - 1 == rowEnd))
            {
                blackMove = true;
            }



            return whiteMove || blackMove;
        }




        public Image print()
        {
           Image check;

            if (this.letter == "B")
            {
                check = Properties.Resources.Black;//@"C:\MiniSteam\Checkers\Images\Black.png";
            }
            else
            {
                check = Properties.Resources.Yellow;//@"C:\MiniSteam\Checkers\Images\Yellow.png";
            }
            return check;
        }

        public Image printMarked()
        {
            Image check;

            if (this.letter[0] == 'B')
            {
                check = Properties.Resources.BlackChosen;//@"C:\MiniSteam\Checkers\Images\BlackChosen.png";
            }
            else if (this.letter[0] == 'W')
            {
                check = Properties.Resources.YellowMarked;//@"C:\MiniSteam\Checkers\Images\YellowMarked.png";
            }
            else
            {
                check = Properties.Resources.NullMarked;//@"C:\MiniSteam\Checkers\Images\NullMarked.png";
            }
            return check;
        }


        public string FindPiece()
        {
            return this.letter;
        }


        public bool pieceDevour(IchecherMove[,] checker, int rowStart,int columnStart , int rowEnd, int columnEnd)
        {
            bool whiteMove = false;
            bool blackMove = false;
            bool moveToNull = false;
            bool isBlack = false;
            bool isWhite = false;
            bool notNull = false;



            if (checker[rowStart, columnStart] != null)
            {
                notNull = true;
            }

            if (notNull && checker[rowStart, columnStart].FindPiece() == "W")
            {
                isWhite = true;
            }
            else
            {
                isBlack = true;
            }

            if (notNull && checker[rowEnd, columnEnd] == null)
            {
                moveToNull = true;
            }

            //White: checks if devoured piece isnt similar. while overseeing two steps
            if (notNull &&   moveToNull && isWhite && ((columnStart + 2 == columnEnd)) && (rowStart - 2 == rowEnd))
            {
                if (CheckersCode.whitesTurn && !CheckIsOnDontMoveAnything && checker[rowStart - 1, columnStart + 1] != null && checker[rowStart - 1, columnStart + 1].FindPiece()[0] == 'B')
                {
                    checker[rowStart - 1, columnStart + 1] = null;
                    whiteMove = true;
                
                }
                if (CheckIsOnDontMoveAnything && checker[rowStart - 1, columnStart + 1] != null && checker[rowStart - 1, columnStart + 1].FindPiece()[0] == 'B')
                {
                    whiteMove = true;
                }
               
            }

            if ( notNull  && moveToNull && isWhite && ((columnStart - 2 == columnEnd)) && (rowStart - 2 == rowEnd))
            {
                if (CheckersCode.whitesTurn && !CheckIsOnDontMoveAnything && checker[rowStart - 1, columnStart - 1] != null && checker[rowStart - 1, columnStart - 1].FindPiece()[0] == 'B')
                {
                    checker[rowStart - 1, columnStart - 1] = null;
                    whiteMove = true;
                }
                if (CheckIsOnDontMoveAnything && checker[rowStart - 1, columnStart - 1] != null && checker[rowStart - 1, columnStart - 1].FindPiece()[0] == 'B')
                {
                    whiteMove = true;
                }
            }
            //BlacC: checks if devoured piece isnt similar. while overseeing two steps
            if (notNull && isBlack && moveToNull && ((columnStart - 2 == columnEnd)) && (rowStart + 2 == rowEnd))
            {
                if (CheckersCode.blacksTurn && !CheckIsOnDontMoveAnything && checker[rowStart + 1, columnStart - 1] != null && checker[rowStart + 1, columnStart - 1].FindPiece()[0] == 'W')
                {
                    checker[rowStart + 1, columnStart - 1] = null;
                    whiteMove = true;
                }
                if (CheckIsOnDontMoveAnything && checker[rowStart + 1, columnStart - 1] != null && checker[rowStart + 1, columnStart - 1].FindPiece()[0] == 'W')
                {
                    whiteMove = true;
                }
            }

            if (notNull && isBlack && moveToNull && isBlack && ((columnStart + 2 == columnEnd)) && (rowStart + 2 == rowEnd))
            {
                if (CheckersCode.blacksTurn && !CheckIsOnDontMoveAnything && checker[rowStart + 1, columnStart + 1] != null && checker[rowStart + 1, columnStart + 1].FindPiece()[0] == 'W')
                {
                     checker[rowStart + 1, columnStart + 1] = null;
                     whiteMove = true;
                }
                 if (CheckIsOnDontMoveAnything && checker[rowStart + 1, columnStart + 1] != null && checker[rowStart + 1, columnStart + 1].FindPiece()[0] == 'W')
                 {
                     whiteMove = true;
                 }
             }

            return whiteMove ;
        }

        //////////////////////////////////////////////////////////////king//////////////
    }


    ///////////////////////king/////////////////////////////////////////////

    //king///////////////////////////////////////////////////////


    class King : IchecherMove
    {
        int columnEnd;
        int rowEnd;
        int columnStart;
        int rowStart;
        string name;
        string letter = "";
        public static bool CheckIsOnDontMoveAnything;

        public King(string d)
        {
            this.letter = d;
        }

        public Image print()
        {
            Image check;
            if (this.letter[0] == 'B')
            {
                check = Properties.Resources.BlackQueen;//@"C:\MiniSteam\Checkers\Images\BlackQueen.png";
            }
            else
            {
                check = Properties.Resources.YellowQueen;//@"C:\MiniSteam\Checkers\Images\YellowQueen.png";
            }
            return check;
        
        }

        public Image printMarked()
        {
            Image check;
            if (this.letter[0] == 'B')
            {
                check = Properties.Resources.BlackQueenMarked; //@"C:\MiniSteam\Checkers\Images\BlackQueenMarked.png";
            }
            else if (this.letter[0] == 'W')
            {
                check = Properties.Resources.YellowQueenMarked;//@"C:\MiniSteam\Checkers\Images\YellowQueenMarked.png";
            }
            else
            {
                check = Properties.Resources.NullMarked;//@"C:\MiniSteam\Checkers\Images\NullMarked.png";
            }

            return check;

        }


        /// <summary>
        /// ////////////// FINDPIECE METHOD//////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>

        public string FindPiece()
        {
            return this.letter;
        }





        public bool isMoveValid(IchecherMove[,] checker, int rowStart, int columnStart, int rowEnd, int columnEnd)
        {
            bool KingMoved = false;
            int b = 0;
            int rowX = rowEnd - rowStart;
            int columnY = columnEnd - columnStart;
            bool rowUpperRangeLimit = false;
            bool columnUpperRangeLimit = false;
            bool rowLowerRangeLimit = false;
            bool columnLowerRangeLimit = false;

            // checks so that the king wont go above the limit
            if (rowX < 0)
            {
                rowX = rowX * (-1);
            }
            if (columnY < 0)
            {
                columnY = columnY * (-1);
            }

            if (rowEnd < 8)
            {
                rowUpperRangeLimit = true;
            }
            if (rowEnd > 1)
            {
                rowLowerRangeLimit = true;
            }
            if (columnEnd < 8)
            {
                columnUpperRangeLimit = true;
            }
            if (columnEnd > 1)
            {
                columnLowerRangeLimit = true;
            }


            

           //checks where the king can move  
            if (columnUpperRangeLimit == true && rowUpperRangeLimit == true && rowStart + 1 == rowEnd && columnStart + 1 == columnEnd)
            {//NorthEast
                b = 1;           
            }
            else if (columnUpperRangeLimit == true && rowLowerRangeLimit == true && rowStart - 1 == rowEnd && columnStart + 1 == columnEnd)
            {//SouthEast
                b = 1;
            }
            else if (columnLowerRangeLimit == true && rowUpperRangeLimit == true && rowStart + 1 == rowEnd && columnStart - 1 == columnEnd)
            {//NorthWest
                b = 1;   
            }
            else if (columnLowerRangeLimit == true && rowLowerRangeLimit == true && rowStart - 1 == rowEnd && columnStart - 1 == columnEnd)
            {//SouthWest
                b = 1;
            }
            else
            {
              b = 0;           
            }
            if (b == 1)
            {
                KingMoved = true;
            }

            return KingMoved;
        }
        /////////////////Piece Endevour////////////////////////////////////////////////////////////////////////


        public bool pieceDevour(IchecherMove[,] checker, int rowStart, int columnStart, int rowEnd, int columnEnd)
        {
            bool whiteMove = false;
            bool blackMove = false;
            bool moveToNull = false;
            bool whiteMoveCheck = false;
            bool blackMoveCheck = false;
            bool notNull = false;
            int b = 0;
            int rowX = rowEnd - rowStart;
            int columnY = columnEnd - columnStart;
            bool rowUpperRangeLimit = false;
            bool columnUpperRangeLimit = false;
            bool rowLowerRangeLimit = false;
            bool columnLowerRangeLimit = false;
            bool safeRange = false;
            bool correctTurn = false;

            if (checker[rowStart, columnStart]!=null)
            {
                notNull = true;
            }
           // if(CheckersCode.blacksTurn 

            if (CheckersCode.counter % 2 == 0 && checker[rowStart, columnStart].FindPiece()[0] == 'B')
            {
                correctTurn = true;
            }

            if (CheckersCode.counter % 2 != 0 && checker[rowStart, columnStart].FindPiece()[0] == 'W')
            {
           
                correctTurn = true;
            }

            ///////////////////Moves Enemy to kill : Ended////////////////////////////Ended////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        
            if (  notNull && columnStart + 2 == columnEnd && rowStart + 2 == rowEnd)
            {
                if (correctTurn && !CheckIsOnDontMoveAnything && checker[rowStart + 1, columnStart + 1] != null && checker[rowStart + 1, columnStart + 1].FindPiece()[0] != checker[rowStart, columnStart].FindPiece()[0])
                {
                    checker[rowStart + 1, columnStart + 1] = null;
                    whiteMove = true;
                   
                }
                if (CheckIsOnDontMoveAnything && checker[rowStart + 1, columnStart + 1] != null && checker[rowStart + 1, columnStart + 1].FindPiece()[0] != checker[rowStart, columnStart].FindPiece()[0])
                {
                    whiteMove = true;
                }
            }

            if ( notNull && columnStart - 2 == columnEnd && rowStart + 2 == rowEnd)
            {
                if (correctTurn && !CheckIsOnDontMoveAnything && checker[rowStart + 1, columnStart - 1] != null && checker[rowStart + 1, columnStart - 1].FindPiece()[0] != checker[rowStart, columnStart].FindPiece()[0])
                {
                    checker[rowStart + 1, columnStart - 1] = null;
                    whiteMove = true;
                }
                if (CheckIsOnDontMoveAnything && checker[rowStart + 1, columnStart - 1] != null && checker[rowStart + 1, columnStart - 1].FindPiece()[0] != checker[rowStart, columnStart].FindPiece()[0])
                {
                    whiteMove = true;
                }
            }

            if ( notNull && columnStart + 2 == columnEnd && rowStart - 2 == rowEnd)
            {
                if (correctTurn && !CheckIsOnDontMoveAnything && checker[rowStart - 1, columnStart + 1] != null && checker[rowStart - 1, columnStart + 1].FindPiece()[0] != checker[rowStart, columnStart].FindPiece()[0])
                {
                    checker[rowStart - 1, columnStart + 1] = null;
                    blackMove = true;
                  
                }
                if (CheckIsOnDontMoveAnything && checker[rowStart - 1, columnStart + 1]!=null && checker[rowStart - 1, columnStart + 1].FindPiece()[0] != checker[rowStart, columnStart].FindPiece()[0])
                {
                    blackMove = true;
                }
            }
            if ( notNull && columnStart - 2 == columnEnd && rowStart - 2 == rowEnd)
            {
                if (correctTurn && !CheckIsOnDontMoveAnything && checker[rowStart - 1, columnStart - 1] != null && checker[rowStart - 1, columnStart - 1].FindPiece()[0] != checker[rowStart, columnStart].FindPiece()[0])
                {
                    checker[rowStart - 1, columnStart - 1] = null;
                    blackMove = true;
                  
                }
                if (CheckIsOnDontMoveAnything && checker[rowStart - 1, columnStart - 1] != null && checker[rowStart - 1, columnStart - 1].FindPiece()[0] != checker[rowStart, columnStart].FindPiece()[0])
                {
                    blackMove = true;
                }
            }

            return whiteMove || blackMove;
        }

    }




    class Monitor
    {
        int m;
        int n;
        Dictionary<int, int[]> endPositions;
        bool cantMoveUnleaseEaten;
        int[] arrayEndPositions;
        public Monitor()
        {
            endPositions = new Dictionary<int, int[]>();
           
        }

        public bool WhiteMonitor(IchecherMove[,] checker, int rowStart, int columnStart, int rowEnd, int columnEnd)
        {
            Checker.CheckIsOnDontMoveAnything = true;
            King.CheckIsOnDontMoveAnything = true;
            int localCounter = 0;

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (checker[i, j]!=null && checker[i, j].FindPiece()[0] == 'W')
                    {
                        for ( m = 1; m < 9; m++)
                        {   
                            for ( n = 1; n < 9; n++)
                            {
                                 if (checker[i, j].pieceDevour(checker, i, j, m, n))
                                 {
                                    
                                         arrayEndPositions =new int[]{ m , n };
                                         endPositions.Add(localCounter++,arrayEndPositions);
                                         cantMoveUnleaseEaten = true;
                                
                             
                                 }
                            }
                        }
                    }
                }
            }
            Checker.CheckIsOnDontMoveAnything =false;
            King.CheckIsOnDontMoveAnything = false;

            return cantMoveUnleaseEaten;
        }

        public bool BlackMonitor(IchecherMove[,] checker, int rowStart, int columnStart, int rowEnd, int columnEnd)
        {
            bool cantMoveUnleaseEaten = false;
            Checker.CheckIsOnDontMoveAnything = true;
            King.CheckIsOnDontMoveAnything = true;
            int localCounter = 0;

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (checker[i, j] != null && checker[i, j].FindPiece()[0] == 'B')
                    {
                        for (int m = 1; m < 9; m++)
                        {
                            for (int n = 1; n < 9; n++)
                            {
                                if (checker[i, j].pieceDevour(checker, i, j, m, n))
                                {
                                    arrayEndPositions = new int[] { m, n };
                                    endPositions.Add(localCounter++, arrayEndPositions); // will count all the possible positions
                                    cantMoveUnleaseEaten = true;
                                }
                            }
                        }
                    }
                }
            }
            Checker.CheckIsOnDontMoveAnything = false;
            King.CheckIsOnDontMoveAnything = false;
            return cantMoveUnleaseEaten;
        }

        public bool doEndPositionsMatch(int rowEnd, int columnEnd)
        {
            bool positionsMatch = false;
            int[] getEndpositions=new int[2];

            foreach (var item in endPositions)
            {
                getEndpositions = endPositions[item.Key];
                if (rowEnd == getEndpositions[0] && columnEnd == getEndpositions[1])
                {
                    positionsMatch = true;
                    break;
                }
            }
            return positionsMatch;
        }
    }


    class GameEnd
    {
        public static bool WhiteArePiecesLeft(IchecherMove[,] checker)
        {
            bool thereArePiecesLeft=false;

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                   if(checker[i,j]!=null && checker[i,j].FindPiece()[0]=='W')
                   {
                       thereArePiecesLeft=true;
                   }
                  
                }
             
            }
               return  thereArePiecesLeft;
         }


        public static bool BlackArePiecesLeft(IchecherMove[,] checker)
        {
            bool thereArePiecesLeft=false;

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                   if(checker[i,j]!=null && checker[i,j].FindPiece()[0]=='B')
                   {
                       thereArePiecesLeft=true;
                   }
                  
                }
             
            }
               return  thereArePiecesLeft;
         }

     }
      

}








