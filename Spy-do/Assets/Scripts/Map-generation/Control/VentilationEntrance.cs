/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

using MapGenerator.Core;
using UnityEngine;

namespace MapGenerator.Control
{
    public class VentilationEntrance : MonoBehaviour
    {
        private void Awake()
        {
            Ventilation.AddVentilationEntrance(this);
        }
    }
}