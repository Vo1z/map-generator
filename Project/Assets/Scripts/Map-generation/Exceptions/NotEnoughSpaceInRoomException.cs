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
    class NotEnoughSpaceInRoomException : System.Exception
    {
        public NotEnoughSpaceInRoomException(string dimentionName, int reqSpace, int currSpace) : base(
            "[ Required " + dimentionName + " space is " + reqSpace +
            " ] > [Current " + dimentionName + " space is " + currSpace + " ]")
        {
        }
    }
}