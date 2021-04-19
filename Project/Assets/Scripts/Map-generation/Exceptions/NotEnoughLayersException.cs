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