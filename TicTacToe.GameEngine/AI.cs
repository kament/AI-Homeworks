using System;

namespace TicTacToe.GameEngine
{
    internal static class AI
    {
        internal static Position FindBestComputerMove(PlayGround playGround)
        {
            Position position = CalculateMax(playGround);
            return position;
        }

        private static Position CalculateMax(PlayGround playGround)
        {
            throw new NotImplementedException();
        }

        private enum Outcome
        {
            Playable,
            Win,
            Lost,
            Raw
        }
    }
}
