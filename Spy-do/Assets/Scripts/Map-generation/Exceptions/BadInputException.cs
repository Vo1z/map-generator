namespace MapGenerator
{
    namespace Exceptions
    {
        public class BadInputException : System.Exception
        {
            public BadInputException(string message) : base(message)
            {
            }
        }
    }
}