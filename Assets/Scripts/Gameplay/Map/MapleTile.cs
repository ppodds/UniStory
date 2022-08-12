using System;
using Graphic;
using Unity.VisualScripting;
using UnityEngine;
using WzComparerR2.WzLib;

namespace Gameplay.Map
{
    public class MapleTile : MonoBehaviour
    {
        public static MapleTile Create(Wz_Node src, string tileSet, int layer)
        {
            var loader = Loader.getInstance();
            // u is the type of tile
            var wzImage = loader.Map.Nodes["Tile"].Nodes[tileSet].GetValue<Wz_Image>();
            if (!wzImage.TryExtract())
                throw new Exception();
            var tileSrc = wzImage.Node.Nodes[src.Nodes["u"].GetValue<string>()].Nodes[src.Nodes["no"].GetValue<int>()];
            var texture = new MapleTexture(tileSrc);
            var pos = new Vector2(src.FindNodeByPath("x").GetValue<int>(), -src.FindNodeByPath("y").GetValue<int>()) /
                      Constant.PixelsPerUnit;
            var z = tileSrc.FindNodeByPath("z")?.GetValue<int>() ?? 0;
            if (z == 0)
                z = tileSrc.FindNodeByPath("zM")?.GetValue<int>() ?? 0;
            var mapleObject = MapleObject.Create(texture, "Tile" + layer);
            mapleObject.SpriteRenderer.sortingOrder = z;
            mapleObject.transform.position = pos;
            var mapleTile = mapleObject.AddComponent<MapleTile>();
            return mapleTile;
        }
    }
}