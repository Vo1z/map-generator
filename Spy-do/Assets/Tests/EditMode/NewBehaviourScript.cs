using System.Collections.Generic;
using MapGenerator.Core;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    public class LocationLogicTest
    {
        [Test]
        public void CreatePairsForVentilation()
        {
            List<Vector2> listOfVectors = new List<Vector2>();
            listOfVectors.Add(new Vector2(0,0));
            listOfVectors.Add(new Vector2(1,1));
            listOfVectors.Add(new Vector2(8,-3));
            listOfVectors.Add(new Vector2(11,4));
            listOfVectors.Add(new Vector2(-3,-5));
            listOfVectors.Add(new Vector2(-3,1));
            
            var res = LocationLogic.CreatePairsForVentilation(listOfVectors, 10, Random.Range(0, 30));
            HashSet<(Vector2, Vector2, int)> set = new HashSet<(Vector2, Vector2, int)>();

            foreach (var unit in res)
                set.Add(unit);
            
            Assert.AreEqual(res.Length, set.Count);
        }
    }
}