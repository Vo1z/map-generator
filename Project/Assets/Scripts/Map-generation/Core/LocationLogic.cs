/*
 * Sirex production code:
 * Project: map-generator (Spy-Do asset)
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MapGenerator.Core
{
    ///<summary>Class that provides tools for creating particular sequences of instances for map generating process</summary>
    public abstract class LocationLogic
    {
        //Tested
        ///<summary>Return a sequence of randomly generated rooms of a given type regarding input parameters</summary>
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
        ///<summary>Randomly generates pairs of ventilation entrances</summary>
        public static (Vector2 startPos, Vector2 endPos, int turnProbability)[] CreatePairsForVentilation(
            List<Vector2> entrances, int maxNumberOfPaths, int turnProbability)
        {
            var pairs = new List<(Vector2, Vector2, int)>();
            
            for (var pair1 = 0; pair1 < entrances.Count; pair1++)
                for(var pair2 = pair1 + 1; pair2 < entrances.Count; pair2++)
                    if (!entrances[pair1].Equals(entrances[pair2]))
                        pairs.Add((entrances[pair1], entrances[pair2], turnProbability));
            
            MapGeneratorUtils.Randomize(pairs);
            if(pairs.Count > maxNumberOfPaths && pairs.Count > 0)
                pairs.RemoveRange(maxNumberOfPaths, pairs.Count - maxNumberOfPaths);

            return pairs.ToArray();;
        }
    }
}