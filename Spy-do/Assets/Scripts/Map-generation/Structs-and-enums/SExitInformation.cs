/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_
 */
namespace MapGenerator
{
    struct SExitInformation
    {
        public readonly EPosition WallPosition;
        public readonly int ExitIndexZ;

        public SExitInformation(EPosition wallPosition, int exitIndexZ) 
        {
            this.WallPosition = wallPosition;
            this.ExitIndexZ = exitIndexZ;
        }
    }
}