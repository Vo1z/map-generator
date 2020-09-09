/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_
 */
namespace MapGenerator
{
    namespace Exceptions
    {
        //Exceptions 
        class DefaultLayerZWasNotFoundException : System.Exception
        {
            public DefaultLayerZWasNotFoundException() : base("Room does not have default LayerZ for Exit")
            {
            }
        }
    }
}