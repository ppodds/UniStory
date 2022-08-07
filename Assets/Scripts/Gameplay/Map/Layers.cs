using MapleLib.WzLib;
using UnityEditor.Tilemaps;
using UnityEngine;
using Util;

namespace Gameplay.Map
{
    public class Layers : MonoBehaviour
    {
        public static Layers Create(WzObject src)
        {
            var obj = new GameObject("Layers");

            for (var i = 0; i < 8; i++)
            {
                var layer = Layer.Create(src, i);
                layer.transform.SetParent(obj.transform);
            }

            return obj.AddComponent<Layers>();
        }
    }
}