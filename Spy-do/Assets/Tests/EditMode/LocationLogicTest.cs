using System.Collections.Generic;
using System.Linq;
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
            List<Vector2> listOfVectors = new List<Vector2>
            {
                new Vector2(0, 0),
                new Vector2(1, 1),
                new Vector2(8, -3),
                new Vector2(11, 4),
                new Vector2(-3, -5),
                new Vector2(-3, 1)
            };
            var copyList = listOfVectors.ToList();
            
            int numberOfPaths = Random.Range(0, listOfVectors.Count);
            LocationLogic.CreatePairsForVentilation(listOfVectors, numberOfPaths, Random.Range(0, 20));

            Assert.AreEqual(listOfVectors.Count, copyList.Count);
            Assert.IsFalse(copyList.Equals(listOfVectors));
        }
    }
}