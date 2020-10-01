/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

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