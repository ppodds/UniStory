using UnityEngine;
using WzComparerR2.WzLib;

namespace Gameplay.Map
{
    public class Portals : MonoBehaviour
    {
        public static Portals Create(Wz_Node src, int mapId)
        {
            var obj = new GameObject("Portals");
            var portals = obj.AddComponent<Portals>();
            foreach (var sub in src.Nodes)
            {
                var portal = Portal.Create(sub, mapId);
                portal.transform.SetParent(obj.transform);
            }
            return portals;
        }
    }
}