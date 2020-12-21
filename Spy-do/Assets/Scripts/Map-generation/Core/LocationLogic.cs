/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

using System;
using System.Collections.Generic;
using System.Linq;
using MapGenerator.Exceptions;
using Random = UnityEngine.Random;

namespace MapGenerator
{
    namespace Core
    {
        //Class that provides tools for creating particular sequence of rooms
        public abstract class LocationLogic
        {
            //Array that holds sequence of rooms that have to be spawned in location (Final output of this class)
            public Room[,] Rooms => GetAsArray();
            
            //2D list of rooms that is responsible for comfortable modification of Room Sequence;
            private List<List<Room>> _roomsList;
            //Room that will replace all nulls in array
            private readonly Type _defaultRoom;
            //Sizes that specifies _defaultRoom height and length
            private readonly int _defaultRoomHeightY, _defaultRoomLengthX;
            //Sizes that responsible for upper bounds size of location 
            private int LocationHeightBoundY, LocationLengthBoundX;
            
            public LocationLogic(Type defaultRoom, int defaultRoomHeightY, int defaultRoomLengthX,
                int locationHeightBoundY, int locationLengthBoundX)
            {
                _roomsList = new List<List<Room>>();
                
                _defaultRoom = defaultRoom;
                _defaultRoomHeightY = defaultRoomHeightY;
                _defaultRoomLengthX = defaultRoomLengthX;

                LocationHeightBoundY = locationHeightBoundY < 0 ? throw new BadInputException("locationHeightY < 0") : locationHeightBoundY;
                LocationLengthBoundX = locationLengthBoundX < 0 ? throw new BadInputException("locationLengthX < 0") : locationLengthBoundX;;
            }
            
            public LocationLogic(Type defaultRoom, int defaultRoomHeightY, int defaultRoomLengthX)
            {
                _roomsList = new List<List<Room>>();
                
                _defaultRoom = defaultRoom;
                _defaultRoomHeightY = defaultRoomHeightY;
                _defaultRoomLengthX = defaultRoomLengthX;

                LocationHeightBoundY = Int32.MaxValue;
                LocationLengthBoundX = Int32.MaxValue;
            }

            //Not tested
            protected void ConsumeForParticularRooms(Action<Room> action, params Type[] roomTypes)
            {
                foreach (List<Room> roomList in _roomsList)
                    foreach (Room room in roomList) 
                        if (roomTypes.Contains(room.GetType()))
                            action(room);
            }

            //Not tested
            protected void ReplaceAllWithSuchCondition(Predicate<Room> predicate, params Type[] roomTypes)
            {
                foreach (List<Room> roomList in _roomsList)
                    foreach (Room room in roomList)
                        if (roomTypes.Contains(room.GetType()) && predicate(room))
                            roomList.Remove(room);
            }

            //Not tested
            protected void MixPositionsOfRooms()
            {
                Random random = new Random();
                
                for (var y = 0; y < _roomsList.Count; y++)
                {
                    for (var x = 0; x < _roomsList[y].Count; x++)
                    {
                        MapGeneratorUtils.Swap(_roomsList[y], y,Random.Range(0, _roomsList[y].Count - 1));
                    }
                }
            }

            //Not tested
            //Method that is responsible for converting 2D list of Rooms into 2D Array of Rooms
            protected virtual Room[,] GetAsArray()
            {
                if (_roomsList == null || _roomsList.Contains(null))
                    throw new NullReferenceException();

                Room[,] roomsArray = new Room[_roomsList.Count, MapGeneratorUtils.FindLongestListSize(_roomsList)];

                for (var y = 0; y < _roomsList.Count && y < LocationHeightBoundY; y++)
                {
                    for (var x = 0; x < MapGeneratorUtils.FindLongestListSize(_roomsList) && x < LocationLengthBoundX; x++)
                    {
                        roomsArray[y, x] = _roomsList[y][x] == null
                            ? (Room) Activator.CreateInstance(_defaultRoom, _defaultRoomHeightY, _defaultRoomLengthX)
                            : _roomsList[y][x];
                    }
                }

                return roomsArray;
            }

            //Method that sets all logic of Room sequence
            protected abstract void CreateRoomSequence();
        }
    }
}