using System;
namespace Library.Exceptions
{
    public class InvalidGameException : Exception
    {
        public InvalidGameException()
            : base("Game rule is violated!")
        {
        }

        public InvalidGameException(string message)
            : base("Game rule is violated for " + message + "!")
        {
        }
    }
}
