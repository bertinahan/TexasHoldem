using System;
namespace Library.Exceptions
{
    public class InvalidPokerRuleException : Exception
    {

        public InvalidPokerRuleException()
             : this("Rule does not exist in game!")
        {
        }

        public InvalidPokerRuleException(string message)
            : base("Rule " + message + " is invalid!")
        {
        }
    }
}
