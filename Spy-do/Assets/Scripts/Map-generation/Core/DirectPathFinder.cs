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
using UnityEngine;
using Random = UnityEngine.Random;

namespace MapGenerator.Core
{
    //Class that is responsible for computing and holding path between two points 
    public class DirectPathFinder
    {
        private readonly Vector2Int _startPoint;
        private readonly Vector2Int _endPoint;
        //Variable that holds probability of generating turns in the path
        private readonly int _turnProbability;
        //List which holds coordinates of tiles that creates path
        public List<Vector2Int> Path { get; private set; } = new List<Vector2Int>();

        public DirectPathFinder(Vector2Int startPoint, Vector2Int endPoint, int turnProbability)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            _turnProbability = turnProbability > 0? turnProbability : throw new ArgumentException("Probability can not be <= 0");

            FindPath();
        }

        //Tested
        //Generates Path between _startPoint and _endPoint
        private void FindPath()
        {
            Vector2Int currentPos = new Vector2Int(_startPoint.x, _startPoint.y);
            Path.Add(new Vector2Int(currentPos.x, currentPos.y));

            while (!currentPos.Equals(_endPoint))
            {
                if (Random.Range(0, _turnProbability * 2) < _turnProbability)
                {
                    if (currentPos.x < _endPoint.x)
                        currentPos.x++;
                    else if (currentPos.x > _endPoint.x)
                        currentPos.x--;
                }
                else
                {
                    if (currentPos.y < _endPoint.y)
                        currentPos.y++;
                    else if (currentPos.y > _endPoint.y)
                        currentPos.y--;
                }
                
                Path.Add(new Vector2Int(currentPos.x, currentPos.y));
            }
        }
    }
}