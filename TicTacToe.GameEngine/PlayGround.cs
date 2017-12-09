using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.GameEngine
{
    public class PlayGround
    {
        private int matrixIndex;
        private List<List<PlayGroundCell>> matrix;

        public PlayGround(int matrixIndex)
        {
            this.matrixIndex = matrixIndex;
            InitializePlayground();
        }
        
        public int Rows { get { return this.matrixIndex; } }

        public int Columns { get { return this.matrixIndex; } }

        public IEnumerable<PlayGroundCell> Cells { get; private set; }

        public PlayGroundCell this[int row, int col]
        {
            get
            {
                return this.matrix[row][col];
            }
        }
        
        private void InitializePlayground()
        {
            this.matrix = new List<List<PlayGroundCell>>();

            for (int row = 0; row < matrixIndex; row++)
            {
                for (int col = 0; col < matrixIndex; col++)
                {
                    matrix[row][col] = new PlayGroundCell(new Position(row, col));
                }
            }

            this.Cells = matrix.SelectMany(x => x).ToList();
        }

        public PlayGround Clone()
        {
            var playGround = new PlayGround(this.matrixIndex);
            for (int row = 0; row < this.matrixIndex; row++)
            {
                for (int col = 0; col < this.matrixIndex; col++)
                {
                    playGround[row, col].Player = this[row, col].Player;
                }
            }

            return playGround;
        }
    }
}
