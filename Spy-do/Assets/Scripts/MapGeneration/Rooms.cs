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
            AddRoomLayer(RoomHeightY - 1, RoomLengthX);
            Layers[0].FillWholeLayerMap("GymFloor");           
            
            //========================Layer 1=======================
            AddRoomLayer(RoomHeightY - 1, RoomLengthX);
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
            AddRoomLayer(RoomHeightY - 1, RoomLengthX);
            Layers[0].FillWholeLayerMap("OfficeFloor");

            //========================Layer 1=======================
            AddRoomLayer(RoomHeightY - 1, RoomLengthX);
            Layers[1].SetOnRandomLayerID("OfficeTable", 5);            

            //========================Layer 2=======================
            AddRoomLayer();
            Layers[2].SetHorizontalLayerLine(0, "OfficeWall");
            Layers[2].SetHorizontalLayerLine(RoomHeightY - 2, "OfficeWall");
            //Layers[2].SetOnUniqueObject(Layers[1].LayerObjectMap, "OfficeTable", "OfficeComputer", 1);

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

    class LocationGeneralRoom : Room //TODO
    {
        public LocationGeneralRoom(int roomHeightY, int roomLengthX) : base(roomHeightY, roomLengthX) { }

        private protected override void instRoom()
        {
            //========================Layer 0=======================
            AddRoomLayer();
            Layers[0].FillWholeLayerMap("OfficeFloor");
            //Layers[0].SetOnRandomLayerID("GymFloor", 2);
        }
    }
}
