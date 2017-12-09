using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.GameEngine
{
    public class Engine
    {
        private PlayGround playGround;

        public Engine(PlayGround playGround)
        {
            this.playGround = playGround;
        }

        public void MovePlayer(Position position)
        {
            ValidateInputAccordingToPlayGround(position);

            playGround[position.X, position.Y].Player = PlayerType.Player;

            Position computerPosition = AI.FindBestComputerMove(playGround);
            playGround[computerPosition.X, computerPosition.Y].Player = PlayerType.Computer;
        }

        private void ValidateInputAccordingToPlayGround(Position position)
        {
            //Validate
        }
    }
}
