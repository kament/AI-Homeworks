namespace FrogGame
{
    internal struct Frog
    {
        private const char LeftFrogIndicator = '<';
        private const char RightFrogIndicator = '>';
        private const char EmptyFrogIndicator = '_';

        private char frogSign;

        private Frog(char frogSign)
        {
            this.frogSign = frogSign;
        }

        public bool IsEmpty()
        {
            return this.frogSign == EmptyFrogIndicator;
        }

        public bool IsLeft()
        {
            return this.frogSign == LeftFrogIndicator;
        }

        public bool IsRight()
        {
            return this.frogSign == RightFrogIndicator;
        }

        public override string ToString()
        {
            return frogSign.ToString();
        }

        public static Frog Left()
        {
            return new Frog(LeftFrogIndicator);
        }

        public static Frog Right()
        {
            return new Frog(RightFrogIndicator);
        }

        public static Frog Empty()
        {
            return new Frog(EmptyFrogIndicator);
        }
    }
}
