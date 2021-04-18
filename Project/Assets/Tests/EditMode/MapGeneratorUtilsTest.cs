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
using NUnit.Framework;
using MapGenerator.Core;
using MapGenerator.DataTypes;
using Random = UnityEngine.Random;

namespace Tests.EditMode
{
    public class MapGeneratorUtilsTest
    {
        [Test]
        public void Swap()
        {
            //Test of Swap<T>(ref T first, ref T second)
            int a = 1, aCopy = a;
            int b = 2, bCopy = b;

            MapGeneratorUtils.Swap(ref a, ref b);
            Assert.AreEqual(b, aCopy);
            Assert.AreEqual(a, bCopy);

            //Test of Swap<T>(IList<T> collection, int firstIndex, int secondIndex)
            string l1 = "a", l2 = "b", l3 = "c";
            List<string> list = new List<string>();
            list.Add(l1);
            list.Add(l2);
            list.Add(l3);

            MapGeneratorUtils.Swap(list, 0, 2);
            Assert.AreEqual(list[0], l3);
            Assert.AreEqual(list[2], l1);
        }

        [Test]
        public void FindLongestListSize()
        {
            List<string>[] lists = new List<string>[3];
            lists[0] = new List<string>();
            lists[1] = new List<string>();
            lists[2] = new List<string>();
            lists[0].Add("a");
            lists[1].Add("a");
            lists[1].Add("a");
            lists[2].Add("a");
            lists[2].Add("a");
            lists[2].Add("a");

            Assert.AreEqual(MapGeneratorUtils.FindLongestListSize(lists), 3);
        }

        [Test]
        public void Resize3DArray()
        {
            string[,,] source = new string[10, 10, 10];
            source[0, 0, 0] = "a";
            string[,,] destination = new string[9, 9, 9];
            MapGeneratorUtils.Resize3DArray(in source, ref destination);

            Assert.AreEqual(destination.GetLength(0), 10);
            Assert.AreEqual(destination.GetLength(1), 10);
            Assert.AreEqual(destination.GetLength(2), 10);
            Assert.AreEqual(destination[0, 0, 0], "a");
        }

        [Test]
        public void FindHighestRoomInARow()
        {
            Room[,] rooms =
            {
                {new Office(10, 2), new Office(11, 1), new Office(9, 4)},
                {new Office(10, 2), new Office(4, 1), new Office(22, 4)}
            };

            Assert.AreEqual(MapGeneratorUtils.FindHighestRoomInARow(rooms, 0), 11);
            Assert.AreEqual(MapGeneratorUtils.FindHighestRoomInARow(rooms, 1), 22);
        }

        [Test]
        public void FindUpperPositionBound()
        {
            (int y, int x)[] positions = {(5, 3), (6, 10), (13, 5), (23, 0)};
            
            Assert.AreEqual(23, MapGeneratorUtils.FindUpperPositionBound(positions).yBound);
            Assert.AreEqual(10, MapGeneratorUtils.FindUpperPositionBound(positions).xBound);
        }
        
        [Test]
        public void Randomize()
        {
            var array = new int[]
            {
                Random.Range(0, 200),
                Random.Range(0, 200),
                Random.Range(0, 200),
                Random.Range(0, 200),
                Random.Range(0, 200),
                Random.Range(0, 200),
                Random.Range(0, 200)
            };
            var arrayCopy = new int[array.Length];
            
            Array.Copy(array, arrayCopy, array.Length);
            MapGeneratorUtils.Randomize(arrayCopy);
            
            Assert.IsFalse(array.Equals(arrayCopy));
        }

        //Additional classes for testing
        #region AdditionalClasses
        class Office : Room
        {
            public Office(int heightY, int lengthX) : base(heightY, lengthX)
            {
            }

            public Office() : base()
            {
            }

            protected override void CreateRoomObjectMap()
            {
                IsSpawned = true;
                //========================Layer 0=======================
                AddRoomLayer();
                Layers[0].FillWholeLayerMap("nameof(OfficeFloor)");
                Layers[0].SetHorizontalLayerLine(HeightY - 1, null);
                
                AddRoomLayer();
                AddRoomLayer();
                AddRoomLayer();
                
                SetDefaultExitAndLayerZ(new Test(), 2);

                SetExit(new Test(), 2, ExitPosition.LEFT, Random.Range(1, HeightY - 2));
                SetExit(new Test(), 2, ExitPosition.RIGHT, Random.Range(1, HeightY - 2));
            }
        }
        
        class Test : ComplexObject
        {
            public Test() : base(1, 1)
            {
            }

            protected override void InstComplexObject()
            {
                //========================Layer 0=======================
                AddCOLayer();
                COLayers[0].FillWholeLayerMap("GymFloor");
                //========================Layer 1=======================
                AddCOLayer();
                COLayers[1].FillWholeLayerMap("GymFloor");
            }
        }
        #endregion
    }
}