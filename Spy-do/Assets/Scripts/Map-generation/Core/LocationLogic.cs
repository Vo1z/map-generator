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

        public static (Vector2 startPos, Vector2 endPos, int turnProbability)[] CreatePairsForVentilation(
            List<Vector2> entrances, int turnProbability, int maxNumberOfPaths)
        {
            if (turnProbability < 0 || maxNumberOfPaths < 0 || entrances == null)
                throw new ArgumentException();
            
            var result = new (Vector2 startPos, Vector2 endPos, int turnProbability)[maxNumberOfPaths];

            for (var i = 0; i < maxNumberOfPaths; i++)
            {
                Vector2 entrance1 = entrances[Random.Range(0, entrances.Count)];
                Vector2 entrance2 = entrances[Random.Range(0, entrances.Count)];
                var pathUnit = (entrance1, entrance2, turnProbability);
                
                if (entrance1 != entrance2 && !result.Contains(pathUnit))
                    result[i] = pathUnit;
                else
                    i--;
            }

            return result;
        }
    }
}