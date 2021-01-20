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
using MapGenerator.Control;
using UnityEngine;

namespace MapGenerator.Core
{
    //Class that is responsible for holding static data across whole MapGenerator
    public class Ventilation
    {
        private static List<Vector2Int> _ventilationEntrans = new List<Vector2Int>();

        public static void AddVentilationEntrance(VentilationEntrance ventilationEntrance)
        {
            int x = (int)ventilationEntrance.transform.position.x;
            int y = (int) ventilationEntrance.transform.position.y;
            
            _ventilationEntrans.Add(new Vector2Int(x, y));
        }

        public static List<DirectPathFinder> GetPaths()
        {
            List<DirectPathFinder> directPathFinders = new List<DirectPathFinder>();
            
            throw new NotImplementedException();
            //todo implement
        }
    }
}