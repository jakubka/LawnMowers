using System;

namespace LawnMowers.Console
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base (message)
        {
        }
    }
}