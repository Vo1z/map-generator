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
    ///<summary>Class that is responsible for computing and holding path between two points</summary>
    public class DirectPathFinder
    {
        private readonly Vector2 _startPoint;
        private readonly Vector2 _endPoint;
        //Variable that holds probability of generating turns in the path
        private readonly int _turnProbability;
        //List which holds coordinates of tiles that creates path
        public List<Vector2> Path { get; } = new List<Vector2>();

        public DirectPathFinder(Vector2 startPoint, Vector2 endPoint, int turnProbability)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            _turnProbability = turnProbability > 0? turnProbability : throw new ArgumentException("Probability can not be < 1");

            FindPath();
        }

        //Tested
        ///<summary>Generates Path between start point and end point</summary>
        private void FindPath()
        {
            Vector2 currentPos = new Vector2(_startPoint.x, _startPoint.y);
            Path.Add(new Vector2(currentPos.x, currentPos.y));

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
                
                Path.Add(new Vector2(currentPos.x, currentPos.y));
            }
        }
    }
}