/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

namespace MapGenerator
{
    namespace Exceptions
    {
        class NotEnoughSpaceInRoomException : System.Exception
        {
            public NotEnoughSpaceInRoomException(string dimentionName, int reqSpace, int currSpace) : base(
                "[ Required " + dimentionName + " space is " + reqSpace +
                " ] > [Current " + dimentionName + " space is " + currSpace + " ]")
            {
            }
        }
    }
}