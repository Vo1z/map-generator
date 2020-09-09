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
        class NumberOfRoomsInsideLocationIsSmallerThenNumberOfAllRoomsException : System.Exception
        {

            public NumberOfRoomsInsideLocationIsSmallerThenNumberOfAllRoomsException(int locationHeightY,
                int locationLengthX, int numberOfRoomsInLocation)
                : base("Number of rooms [ " + numberOfRoomsInLocation + " ] is bigger then area of location : (" +
                       (locationHeightY * locationLengthX) + ") where locatioHeightY is [ " + locationHeightY +
                       " ] and locationLengthX is [ " + locationLengthX + " ]")
            {
            }
        }
    }
}