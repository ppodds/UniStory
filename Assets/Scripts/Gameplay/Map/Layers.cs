using UnityEngine;
using WzComparerR2.WzLib;

namespace Gameplay.Map
{
    public class Layers : MonoBehaviour
    {
        public static Layers Create(Wz_Node src)
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