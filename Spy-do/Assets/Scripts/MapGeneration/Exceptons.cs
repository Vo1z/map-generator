using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration 
{
    //Exceptions 
    class LayerIsBiggerThanRoomException : System.Exception
    {
        public LayerIsBiggerThanRoomException(int coordinate)
            : base("Layer is bigger than room: " + "[" + coordinate + "]")
        { }

        public LayerIsBiggerThanRoomException(int coordinateX, int coordinateY)
            : base("Layer is bigger than room: " + "[" + coordinateX + ", " + coordinateY + "]")
        { }
    }

    class NumberOfRoomsInsideLocationIsSmalerThenNumberOfAllRoomsException : System.Exception
    {

        public NumberOfRoomsInsideLocationIsSmalerThenNumberOfAllRoomsException(int locationHeightY, int locationLengthX, int numberOfRoomsInLocation)
            : base("Number of rooms [ " + numberOfRoomsInLocation + " ] is bigger then area of location : (" + (locationHeightY * locationLengthX) + ") where locatioHeightY is [ " + locationHeightY + " ] and locationLengthX is [ " + locationLengthX + " ]")
        { }
    }

    class AnyRoomsWereNotChoosenException : System.Exception
    {
        public AnyRoomsWereNotChoosenException()
            : base("Select at least one room type")
        { }
    }

    class StartPoitIsSmallerThenEndPointException : System.Exception
    {
        public StartPoitIsSmallerThenEndPointException(int startPoint, int endPoint) : base(nameof(endPoint) + " is bigger then " + nameof(startPoint)) { }
    }

    class NotEnoughLayersException : System.Exception
    {
        public NotEnoughLayersException(int numberOfRoomLayers, int numberOfCOLayers)
            : base("Room does not have enough layers to implement ComplexObject" +
                  " [ RoomLayers: " + numberOfRoomLayers +" ]" + " [ComplexObjectLayers : " + numberOfCOLayers + " ]" )
        { }
    }

    class NotEnoughSpaceInRoomException : System.Exception 
    {
        public NotEnoughSpaceInRoomException() : base() { }
    }
}