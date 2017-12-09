namespace TicTacToe.GameEngine
{
    public class PlayGroundCell
    {
        internal PlayGroundCell(Position position)
        {
            this.Position = position;
            this.Player = PlayerType.Neutral;
        }

        public PlayerType Player { get; set; }

        public Position Position { get; private set; }
    }
}