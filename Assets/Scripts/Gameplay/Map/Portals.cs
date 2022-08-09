using MapleLib.WzLib;
using UnityEngine;
using Util;

namespace Gameplay.Map
{
    public class Portals : MonoBehaviour
    {
        public static Portals Create(WzObject src, int mapId)
        {
            var obj = new GameObject("Portals");
            var portals = obj.AddComponent<Portals>();
            foreach (var sub in new WzObjectEnumerable(src))
            {
                var portal = Portal.Create(sub, mapId);
                portal.transform.SetParent(obj.transform);
            }
            return portals;
        }
    }
}