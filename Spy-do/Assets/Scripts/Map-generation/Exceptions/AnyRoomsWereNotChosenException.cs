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
        class AnyRoomsWereNotChosenException : System.Exception
        {
            public AnyRoomsWereNotChosenException()
                : base("Select at least one room type")
            {
            }
        }
    }
}