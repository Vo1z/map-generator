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
using System.Net.NetworkInformation;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MapGenerator.Core
{
    //Class that provides tools for creating particular sequence of rooms
    public abstract class LocationLogic
    {
        //Tested
        public static Room[,] CreateRoomMapByDefaultLogic(uint numOfRowsY, uint numOfColumns,
            params (Type roomType, uint minY, uint maxY, uint minX, uint maxX, uint probability)[] roomsInfo)
        {
            Room[,] roomsArray = new Room[numOfRowsY, numOfColumns];
            string probabilityHolder = "";
            for (var roomTypeIter = 0; roomTypeIter < roomsInfo.Length; roomTypeIter++)
            for (var probIter = 0; probIter < roomsInfo[roomTypeIter].probability; probIter++)
                probabilityHolder += roomTypeIter + "";

            for (var roomIterY = 0; roomIterY < roomsArray.GetLength(0); roomIterY++)
            {
                for (var roomIterX = 0; roomIterX < roomsArray.GetLength(1); roomIterX++)
                {
                    int roomTypeNumber =
                        Int32.Parse(probabilityHolder[Random.Range(0, probabilityHolder.Length - 1)] + "");
                    var selectedTuple = roomsInfo[roomTypeNumber];
                    int roomHeightY = (int) Random.Range(selectedTuple.minY, selectedTuple.maxY);
                    int roomLenghtX = (int) Random.Range(selectedTuple.minX, selectedTuple.maxX);

                    roomsArray[roomIterY, roomIterX] =
                        (Room) Activator.CreateInstance(selectedTuple.roomType, roomHeightY, roomLenghtX);
                }
            }
            
            return roomsArray;
        }

        
        //Tested
        public static (Vector2 startPos, Vector2 endPos, int turnProbability)[] CreatePairsForVentilation(
            List<Vector2> entrances, int maxNumberOfPaths, int turnProbability)
        {
            var pairs = new List<(Vector2, Vector2, int)>();
            

            for (var pair1 = 0; pair1 < entrances.Count; pair1++)
            {
                for(var pair2 = pair1 + 1; pair2 < entrances.Count; pair2++)
                {
                    if (!entrances[pair1].Equals(entrances[pair2]) && maxNumberOfPaths > 0)
                    {
                        pairs.Add((entrances[pair1], entrances[pair2], turnProbability));
                        maxNumberOfPaths--;
                    }
                }
            }
            
            var result = pairs.ToArray();
            MapGeneratorUtils.Randomize(result);

            return result;
        }
    }
}