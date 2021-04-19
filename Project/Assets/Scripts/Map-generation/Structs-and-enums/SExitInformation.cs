/*
 * Sirex production code:
 * Project: map-generator (Spy-Do asset)
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

namespace MapGenerator.DataTypes
{
    /// <summary>Struct that represents information about exit in a room</summary>
    public struct SExitInformation
    {
        public readonly ExitPosition WallPosition;
        public readonly int ExitIndexZ;

        public SExitInformation(ExitPosition wallPosition, int exitIndexZ)
        {
            WallPosition = wallPosition;
            ExitIndexZ = exitIndexZ;
        }
    }
}