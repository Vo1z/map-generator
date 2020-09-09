/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_
 */
namespace MapGenerator
{
    namespace Exceptions
    {
        class DefaultExitWasNotFoundException : System.Exception
        {
            public DefaultExitWasNotFoundException() : base("Room does not have default Exit")
            {
            }
        }
    }
}