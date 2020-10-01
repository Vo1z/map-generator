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
        class LayerIsBiggerThanRoomException : System.Exception
        {
            public LayerIsBiggerThanRoomException(int coordinate)
                : base("Layer is bigger than room: " + "[" + coordinate + "]")
            {
            }

            public LayerIsBiggerThanRoomException(int coordinateX, int coordinateY)
                : base("Layer is bigger than room: " + "[" + coordinateX + ", " + coordinateY + "]")
            {
            }
        }
    }
}