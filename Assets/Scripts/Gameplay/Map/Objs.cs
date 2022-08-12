using UnityEngine;
using Util;
using WzComparerR2.WzLib;

namespace Gameplay.Map
{
    public class Objs : MonoBehaviour
    {
        public static Objs Create(Wz_Node src, int layer)
        {
            var obj = new GameObject("Objs");
            
            foreach (var objNode in src.FindNodeByPath("obj").Nodes)
            {
                var tile = MapleObj.Create(objNode, layer);
                tile.transform.SetParent(obj.transform);
            }
            
            return obj.AddComponent<Objs>();
        }
    }
}