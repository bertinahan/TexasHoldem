using System;
namespace Library.Exceptions
{
    public class InvalidPokerRuleException : Exception
    {

        public InvalidPokerRuleException()
             : base("Rule does not exist in game!")
        {

        }

        public InvalidPokerRuleException(string message)
            : base("Rule " + message + " is invalid!")
        {
        }
    }
}
