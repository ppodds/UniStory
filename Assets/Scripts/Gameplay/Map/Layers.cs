using MapleLib.WzLib;
using UnityEngine;

namespace Gameplay.Map
{
    public class Layers : MonoBehaviour
    {
        public static Layers Create(WzObject src)
        {
            var obj = new GameObject("Layers");

            for (var i = 0; i < Constant.LayerAmount; i++)
            {
                var layer = Layer.Create(src, i);
                layer.transform.SetParent(obj.transform);
            }

            return obj.AddComponent<Layers>();
        }
    }
}