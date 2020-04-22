using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    class Gym : Room
    {
        public Gym(int roomHeightY, int roomLengthX) : base(roomHeightY, roomLengthX) { }

        private protected override void instRoom()
        {
            //========================Layer 0=======================
            AddRoomLayer(RoomHeightY - 2, RoomLengthX);
            Layers[0].FillWholeLayerMap("GymFloor");           
            
            //========================Layer 1=======================
            AddRoomLayer(RoomHeightY - 2, RoomLengthX);
            Layers[1].SetOnRandomLayerID("GymInnerObject", 5);

            //========================Layer 2=======================
            AddRoomLayer();
            Layers[2].SetHorizontalLayerLine(0, "GymWall");
            Layers[2].SetHorizontalLayerLine(RoomHeightY - 2, "GymWall");

            //========================Layer 3=======================
            AddRoomLayer();
            Layers[3].SetVerticalLayerLine(0, "GymLeftWall");
            Layers[3].SetVerticalLayerLine(RoomLengthX - 1, "GymRightWall");
            Layers[3].SetHorizontalLayerLine(0, null);
            Layers[3].SetHorizontalLayerLine(RoomHeightY - 1, null);

            //========================Layer 4=======================
            AddRoomLayer();
            Layers[4].SetHorizontalLayerLine(1, "GymTopWallBrink");
            Layers[4].SetHorizontalLayerLine(RoomHeightY - 1, "GymTopWallBrink");
        }
    }

    class Office : Room 
    {
        public Office(int roomHeightY, int roomLengthX) : base(roomHeightY, roomLengthX) { }

        private protected override void instRoom()
        {
            //========================Layer 0=======================
            AddRoomLayer(RoomHeightY - 2, RoomLengthX);
            Layers[0].FillWholeLayerMap("OfficeFloor");

            //========================Layer 1=======================
            AddRoomLayer(RoomHeightY - 2, RoomLengthX);
            Layers[1].SetOnRandomLayerID("OfficeTable", 5);            

            //========================Layer 2=======================
            AddRoomLayer();
            Layers[2].SetOnUniqueObject(Layers[1].LayerObjectMap, "OfficeTable", "OfficeComputer", 4);
            Layers[2].SetHorizontalLayerLine(0, "OfficeWall");
            Layers[2].SetHorizontalLayerLine(RoomHeightY - 2, "OfficeWall");            

            //========================Layer 3=======================
            AddRoomLayer();
            Layers[3].SetVerticalLayerLine(0, "OfficeLeftWall");
            Layers[3].SetVerticalLayerLine(RoomLengthX - 1, "OfficeRightWall");
            Layers[3].SetHorizontalLayerLine(0, null);
            Layers[3].SetHorizontalLayerLine(RoomHeightY - 1, null);

            //========================Layer 4=======================
            AddRoomLayer();
            Layers[4].SetHorizontalLayerLine(1, "OfficeTopWallBrink");
            Layers[4].SetHorizontalLayerLine(RoomHeightY - 1, "OfficeTopWallBrink");

        }
    }

    class GeneralRoom : Room //TODO
    {        
        public GeneralRoom(int roomHeightY, int roomLengthX) : base(roomHeightY, roomLengthX) { }

        private protected override void instRoom()
        {            
            //========================Layer 0=======================
            AddRoomLayer(RoomHeightY - 1, RoomLengthX);
            Layers[0].FillWholeLayerMap("GRFloor1");
            Layers[0].SetOnRandomLayerID("GRFloor2", 2);
            Layers[0].SetOnRandomLayerID("GRFloor3", 2);

            //========================Layer 1=======================
            AddRoomLayer(RoomHeightY - 1, RoomLengthX);
            Layers[1].SetOnRandomLayerID("GRInnerObject", 8);

            //========================Layer 2=======================
            AddRoomLayer();
            Layers[2].SetHorizontalLayerLine(0, "GRBottomWall");
            Layers[2].SetHorizontalLayerLine(RoomHeightY - 2, "GRTopWall");

            //========================Layer 3=======================
            AddRoomLayer();            

            //========================Layer 4=======================
            AddRoomLayer();

            //========================Layer 5=======================
            AddRoomLayer();
            Layers[5].SetVerticalLayerLine(0, "GRLeftWall");
            Layers[5].SetVerticalLayerLine(RoomLengthX - 1, "GRRightWall");
            Layers[5].SetHorizontalLayerLine(0, null);
            Layers[5].SetHorizontalLayerLine(RoomHeightY - 1, null);

            //========================Layer 6=======================
            AddRoomLayer();
            Layers[6].SetHorizontalLayerLine(1, "GRTopWallBrink");
            Layers[6].SetHorizontalLayerLine(RoomHeightY - 1, "GRTopWallBrink");
        }
    }
}
