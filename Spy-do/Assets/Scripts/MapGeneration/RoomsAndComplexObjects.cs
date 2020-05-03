﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    //Rooms
    class EmptySpace : Room 
    {
        public EmptySpace(int roomHeightY, int roomLengthX) : base(roomHeightY, roomLengthX) { }

        protected override void instRoom() 
        {
            //========================Layer 0=======================
            AddRoomLayer();
            RoomLayers[0].FillWholeLayerMap("GRFloor1");
            RoomLayers[0].SetOnRandomLayerID("GRFloor2", 2);
            RoomLayers[0].SetOnRandomLayerID("GRFloor3", 2);
            
            //========================Layer 1=======================
            AddRoomLayer();
            RoomLayers[1].SetOnRandomLayerID("GRInnerObject", 8);
        }
    }

    class Gym : Room
    {
        public Gym(int roomHeightY, int roomLengthX) : base(roomHeightY, roomLengthX) { }

        protected override void instRoom()
        {
            //========================Layer 0=======================
            AddRoomLayer(RoomHeightY - 2, RoomLengthX);
            RoomLayers[0].FillWholeLayerMap("GymFloor");           
            
            //========================Layer 1=======================
            AddRoomLayer(RoomHeightY - 2, RoomLengthX);
            RoomLayers[1].SetOnRandomLayerID("GymInnerObject", 5);

            //========================Layer 2=======================
            AddRoomLayer();
            RoomLayers[2].SetHorizontalLayerLine(0, "GymWall");
            RoomLayers[2].SetHorizontalLayerLine(RoomHeightY - 2, "GymWall");

            //========================Layer 3=======================
            AddRoomLayer();
            RoomLayers[3].SetVerticalLayerLine(0, "GymLeftWall");
            RoomLayers[3].SetVerticalLayerLine(RoomLengthX - 1, "GymRightWall");
            RoomLayers[3].SetHorizontalLayerLine(0, null);
            RoomLayers[3].SetHorizontalLayerLine(RoomHeightY - 1, null);

            //========================Layer 4=======================
            AddRoomLayer();
            RoomLayers[4].SetHorizontalLayerLine(1, "GymTopWallBrink");
            RoomLayers[4].SetHorizontalLayerLine(RoomHeightY - 1, "GymTopWallBrink");
        }
    }

    class Office : Room 
    {
        public Office(int roomHeightY, int roomLengthX) : base(roomHeightY, roomLengthX) { }

        protected override void instRoom()
        {
            //========================Layer 0=======================
            AddRoomLayer(RoomHeightY - 2, RoomLengthX);
            RoomLayers[0].FillWholeLayerMap("OfficeFloor");

            //========================Layer 1=======================
            AddRoomLayer(RoomHeightY - 2, RoomLengthX);
            RoomLayers[1].SetOnRandomLayerID("OfficeTable", 5);            

            //========================Layer 2=======================
            AddRoomLayer();
            RoomLayers[2].SetOnUniqueObject(RoomLayers[1].LayerObjectMap, "OfficeTable", "OfficeComputer", 4);
            RoomLayers[2].SetHorizontalLayerLine(0, "OfficeWall");
            RoomLayers[2].SetHorizontalLayerLine(RoomHeightY - 2, "OfficeWall");            

            //========================Layer 3=======================
            AddRoomLayer();
            RoomLayers[3].SetVerticalLayerLine(0, "OfficeLeftWall");
            RoomLayers[3].SetVerticalLayerLine(RoomLengthX - 1, "OfficeRightWall");
            RoomLayers[3].SetHorizontalLayerLine(0, null);
            RoomLayers[3].SetHorizontalLayerLine(RoomHeightY - 1, null);

            //========================Layer 4=======================
            AddRoomLayer();
            RoomLayers[4].SetHorizontalLayerLine(1, "OfficeTopWallBrink");
            RoomLayers[4].SetHorizontalLayerLine(RoomHeightY - 1, "OfficeTopWallBrink");
        }
    }

    class GeneralRoom : Room
    {        
        public GeneralRoom(int roomHeightY, int roomLengthX) : base(roomHeightY, roomLengthX) { }

        protected override void instRoom()
        {            
            //========================Layer 0=======================
            AddRoomLayer(RoomHeightY - 1, RoomLengthX);
            RoomLayers[0].FillWholeLayerMap("GRFloor1");
            RoomLayers[0].SetOnRandomLayerID("GRFloor2", 2);
            RoomLayers[0].SetOnRandomLayerID("GRFloor3", 2);
            RoomLayers[0].SetHorizontalLayerLine(RoomHeightY - 2, "GRTopWall");

            //========================Layer 1=======================
            AddRoomLayer(RoomHeightY - 1, RoomLengthX);
            RoomLayers[1].SetOnRandomLayerID("GRInnerObject", 8);
            RoomLayers[1].SetHorizontalLayerLine(RoomHeightY - 2, "GRTopWall");

            //========================Layer 2=======================
            AddRoomLayer();
            RoomLayers[2].SetHorizontalLayerLine(0, "GRBottomWall");
            RoomLayers[2].SetHorizontalLayerLine(RoomHeightY - 2, "GRTopWall");

            //========================Layer 3=======================
            AddRoomLayer();            

            //========================Layer 4=======================
            AddRoomLayer();

            //========================Layer 5=======================
            AddRoomLayer();
            RoomLayers[5].SetVerticalLayerLine(0, "GRLeftWall");
            RoomLayers[5].SetVerticalLayerLine(RoomLengthX - 1, "GRRightWall");
            RoomLayers[5].SetHorizontalLayerLine(0, null);
            RoomLayers[5].SetHorizontalLayerLine(RoomHeightY - 1, null);

            //========================Layer 6=======================
            AddRoomLayer();
            RoomLayers[6].SetHorizontalLayerLine(1, "GRTopWallBrink");
            RoomLayers[6].SetHorizontalLayerLine(RoomHeightY - 1, "GRTopWallBrink");
        }
    }


    //ComplexObjects
    class Test : ComplexObject
    {
        public Test() : base(3, 3) { }
         
        protected override void instCO()
        {
        //========================Layer 0=======================
            AddCOLayer();
            COLayers[0].FillWholeLayerMap("OfficeWall");

        //========================Layer 1=======================
            AddCOLayer();
            COLayers[1].SetOnUniqueLayerID(0, 0, "OfficeComputer");
            COLayers[1].SetOnUniqueLayerID(0, 1, "OfficeComputer");
            COLayers[1].SetOnUniqueLayerID(0, 2, "OfficeComputer");
        }
    }
}