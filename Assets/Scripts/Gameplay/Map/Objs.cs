using Graphic;
using MapleLib.WzLib;
using UnityEngine;
using Util;

namespace Gameplay.Map
{
    public class Objs : MonoBehaviour
    {
        public static Objs Create(WzObject src, int layer)
        {
            var obj = new GameObject("Objs");
            
            foreach (var objNode in new WzObjectEnumerable(src["obj"]))
            {
                var tile = MapleObj.Create(objNode, layer);
                tile.transform.SetParent(obj.transform);
            }
            
            return obj.AddComponent<Objs>();
        }
    }
}