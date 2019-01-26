using System;
namespace Library.Exceptions
{
    public class InvalidCardException : Exception
    {
        public InvalidCardException()
            : base("Card information is invalid!")
        {
        }

        public InvalidCardException(string message)
            : base("Card information is invalid because of " + message + "!")
        {
        }
    }
}
