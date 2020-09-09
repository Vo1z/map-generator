/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_
 */
namespace MapGenerator
{
    
    class StartPointIsSmallerThenEndPointException : System.Exception
    {
        public StartPointIsSmallerThenEndPointException(int startPoint, int endPoint) : base(nameof(endPoint) + " is bigger then " + nameof(startPoint)) { }
    }
}