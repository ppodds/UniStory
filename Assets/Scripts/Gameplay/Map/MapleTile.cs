using Graphic;
using MapleLib.WzLib;
using Unity.VisualScripting;
using UnityEngine;
using Util;

namespace Gameplay.Map
{
    public class MapleTile : MonoBehaviour
    {
        public static MapleTile Create(WzObject src, string tileSet, int layer)
        {
            var loader = Loader.getInstance();
            // u is the type of tile
            var tileSrc = loader.Map["Tile"][tileSet][src["u"].GetString()][src["no"].GetInt().ToString()];
            var texture = new MapleTexture(tileSrc);
            var pos = new Vector2(src["x"].GetInt(), -src["y"].GetInt()) / Constant.PixelsPerUnit;
            var z = tileSrc["z"]?.GetInt() ?? 0;
            if (z == 0)
                z = tileSrc["zM"]?.GetInt() ?? 0;
            var mapleObject = MapleObject.Create(texture, "Tile" + layer);
            mapleObject.SpriteRenderer.sortingOrder = z;
            mapleObject.transform.position = pos;
            var mapleTile = mapleObject.AddComponent<MapleTile>();
            return mapleTile;
        }
    }
}