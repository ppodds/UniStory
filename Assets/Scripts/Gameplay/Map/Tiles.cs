using MapleLib.WzLib;
using UnityEngine;
using Util;

namespace Gameplay.Map
{
    public class Tiles : MonoBehaviour
    {
        public static Tiles Create(WzObject src, int layer)
        {
            var obj = new GameObject("Tiles");
            var tileSet = src["info"]["tS"] + ".img";
            foreach (var tileNode in new WzObjectEnumerable(src["tile"]))
            {
                var tile = MapleTile.Create(tileNode, tileSet, layer);
                tile.transform.SetParent(obj.transform);
            }

            return obj.AddComponent<Tiles>();
        }
    }
}