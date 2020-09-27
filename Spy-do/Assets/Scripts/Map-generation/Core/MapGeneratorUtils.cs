using MapGenerator.Core;

namespace MapGenerator
{
    namespace Core
    {
        static class MapGeneratorUtils
        {
            //Not tested
            public static int FindHighestRoomInARow(in Room[,] roomRow, int rowIndex)
            {
                int maxHeightX = 0;

                for (int roomNumber = 0; roomNumber < roomRow.GetLength(1); roomNumber++)
                {
                    if (maxHeightX < roomRow[rowIndex, roomNumber].RoomHeightY)
                        maxHeightX = roomRow[rowIndex, roomNumber].RoomHeightY;
                }

                return maxHeightX;
            }
        }
    }
}
        