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
        class NotEnoughLayersException : System.Exception
        {
            public NotEnoughLayersException(int numberOfRoomLayers, int numberOfCOLayers, System.Object invokedFrom)
                : base("Room does not have enough layers to implement " + invokedFrom.GetType() +
                       " [ RoomLayers: " + numberOfRoomLayers + " ]" + " [ComplexObjectLayers : " + numberOfCOLayers +
                       " ]")
            {
            }
        }
    }
}