using UnityEngine;
using WzComparerR2.WzLib;

namespace Gameplay.Map
{
    public class Tiles : MonoBehaviour
    {
        public static Tiles Create(Wz_Node src, int layer)
        {
            var obj = new GameObject("Tiles");
            var tiles = obj.AddComponent<Tiles>();
            // tileSet == null means we don't need tiles
            if (src.Nodes["info"].Nodes["tS"] == null)
                return tiles;
            
            var tileSet = src.Nodes["info"].Nodes["tS"].GetValue<string>() + ".img";
            foreach (var tileNode in src.Nodes["tile"].Nodes)
            {
                var tile = MapleTile.Create(tileNode, tileSet, layer);
                tile.transform.SetParent(obj.transform);
            }

            return tiles;
        }
    }
}