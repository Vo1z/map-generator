﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGeneration;

//inner class that creates floor for room
class MapGenerationController : MonoBehaviour
{
    public GameObject leftCorner;
    public GameObject rightCorner;
    
    public GameObject floor;
    public GameObject wall;
    public int roomLenght;
    public int roomHight;
    private RoomGeneratorClass roomGeneratorClass;

    void Awake()
    {
        roomGeneratorClass = new RoomGeneratorClass(roomLenght, roomHight);  
    }

    void Start()
    {
        createRoomFloor();
        createRoomWalls();
        roomGeneratorClass.Test();
    }

    private void createRoomFloor() 
    {
        for (int y = 0; y < roomGeneratorClass.getRoom().GetLength(0); y++) 
        {
            for (int x = 0; x < roomGeneratorClass.getRoom().GetLength(1); x++) 
            {
                if (roomGeneratorClass.getObjectId()[y, x] == 0)
                Instantiate(floor, new Vector3(y, x), Quaternion.identity);
            }
        }
    }

    private void createRoomWalls() 
    {
        for (int y = 0; y < roomGeneratorClass.getObjectId().GetLength(0); y++)
        {
            for (int x = 0; x < roomGeneratorClass.getObjectId().GetLength(1); x++)
            {
                //Creates walls
                if (roomGeneratorClass.getObjectId()[y, x] == 1)
                {
                    Instantiate(wall, new Vector3(y, x), Quaternion.identity);
                }
                //Creates corners
                else if (roomGeneratorClass.getObjectId()[y, x] == 2)
                {
                    Instantiate(leftCorner, new Vector3(y, x), Quaternion.identity);
                }
                else if (roomGeneratorClass.getObjectId()[y, x] == 3) 
                {
                    Instantiate(rightCorner, new Vector3(y, x), Quaternion.identity);
                }
            }
        }
    }
}