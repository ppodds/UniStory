using Graphic;
using MapleLib.WzLib;
using Unity.VisualScripting;
using UnityEngine;
using Util;

namespace Gameplay.Map
{
    public class MapleObj : MonoBehaviour
    {
        public static MapleObj Create(WzObject src, int layer)
        {
            var loader = Loader.getInstance();

            var pos = new Vector2(src["x"].GetInt(), -src["y"].GetInt()) / Constant.PixelsPerUnit;
            var flip = src["f"].GetInt() == 1;
            var z = src["z"].GetInt();

            var mapleObject = MapleObject.Create("Obj" + layer);
            mapleObject.SpriteRenderer.sortingOrder = z;
            mapleObject.transform.position = pos;
            var mapleObj = mapleObject.AddComponent<MapleObj>();
            MapleAnimation.Create(mapleObject,
                loader.Map["Obj"][src["oS"] + ".img"][src["l0"].GetString()][src["l1"].GetString()][
                    src["l2"].GetString()]);
            return mapleObj;
        }
    }
}