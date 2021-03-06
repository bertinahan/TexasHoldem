﻿using System;

namespace Library.Exceptions
{
    public class InvalidPlayerException : Exception
    {
        public InvalidPlayerException()
            : base("Player information is invalid!")
        {
        }

        public InvalidPlayerException(string message)
            : base("Player's information for " + message + " is invalid!")
        {
        }
    }
}
