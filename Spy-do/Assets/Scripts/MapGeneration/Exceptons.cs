/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator
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
        public NotEnoughLayersException(int numberOfRoomLayers, int numberOfCOLayers, System.Object invokedFrom)
            : base("Room does not have enough layers to implement " + invokedFrom.GetType() +
                  " [ RoomLayers: " + numberOfRoomLayers + " ]" + " [ComplexObjectLayers : " + numberOfCOLayers + " ]")
        { }
    }

    class NotEnoughSpaceInRoomException : System.Exception
    {
        public NotEnoughSpaceInRoomException(string dimentionName, int reqSpace, int currSpace) : base("[ Required " + dimentionName + " space is " + reqSpace +
                                            " ] > [Current " + dimentionName + " space is " + currSpace + " ]") { }
    }

    class DefaultExitWasNotFoundException : System.Exception
    {
        public DefaultExitWasNotFoundException() : base("Room does not have default Exit") { }
    }

    class DefaultLayerZWasNotFoundException : System.Exception
    {
        public DefaultLayerZWasNotFoundException() : base("Room does not have default LayerZ for Exit") { }
    }
}