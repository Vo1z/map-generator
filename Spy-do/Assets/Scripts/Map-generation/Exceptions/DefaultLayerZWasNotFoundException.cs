/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator
{
    //Exceptions 
    class DefaultLayerZWasNotFoundException : System.Exception
    {
        public DefaultLayerZWasNotFoundException() : base("Room does not have default LayerZ for Exit") { }
    }
}