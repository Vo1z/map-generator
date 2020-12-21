using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MapGenerator;
using MapGenerator.Core;

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
        public void FindLongestList()
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

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        // [UnityTest]
        // public IEnumerator MpGenerationTestWithEnumeratorPasses()
        // {
        //     // Use the Assert class to test conditions.
        //     // Use yield to skip a frame.
        //     yield return null;
        // }
    }
}