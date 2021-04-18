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
    //Exceptions 
    class DefaultLayerZWasNotFoundException : System.Exception
    {
        public DefaultLayerZWasNotFoundException() : base("Room does not have default LayerZ for Exit")
        {
        }
    }
}