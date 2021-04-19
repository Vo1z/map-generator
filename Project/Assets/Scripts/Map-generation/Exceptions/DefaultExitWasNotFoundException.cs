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
    class DefaultExitWasNotFoundException : System.Exception
    {
        public DefaultExitWasNotFoundException() : base("Room does not have default Exit")
        {
        }
    }
}