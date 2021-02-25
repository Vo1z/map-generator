/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

namespace MapGenerator.Exceptions
{
    class StartPointIsSmallerThenEndPointException : System.Exception
    {
        public StartPointIsSmallerThenEndPointException(int startPoint, int endPoint) : base(nameof(endPoint) +
            " is bigger then " + nameof(startPoint))
        {
        }
    }
}