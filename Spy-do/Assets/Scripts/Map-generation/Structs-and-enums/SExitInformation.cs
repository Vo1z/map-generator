/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

namespace MapGenerator.DataTypes
{
    public struct SExitInformation
    {
        public readonly ExitPosition WallPosition;
        public readonly int ExitIndexZ;

        public SExitInformation(ExitPosition wallPosition, int exitIndexZ)
        {
            this.WallPosition = wallPosition;
            this.ExitIndexZ = exitIndexZ;
        }
    }
}