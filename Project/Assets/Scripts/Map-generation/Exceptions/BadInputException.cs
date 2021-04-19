/*
 * Sirex production code:
 * Project: map-generator (Spy-Do asset)
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

namespace MapGenerator.Exceptions
{
    public class BadInputException : System.Exception
    {
        public BadInputException(string message) : base(message)
        {
        }
    }
}