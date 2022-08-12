using System;
using Graphic;
using Unity.VisualScripting;
using UnityEngine;
using WzComparerR2.WzLib;

namespace Gameplay.Map
{
    public class MapleObj : MonoBehaviour
    {
        public static MapleObj Create(Wz_Node src, int layer)
        {
            var loader = Loader.getInstance();

            var pos = new Vector2(src.Nodes["x"].GetValue<int>(), -src.Nodes["y"].GetValue<int>()) /
                      Constant.PixelsPerUnit;
            var flip = src.Nodes["f"].GetValue<bool>();
            var z = src.Nodes["z"].GetValue<int>();

            var mapleObject = MapleObject.Create(src.FullPathToFile, "Obj" + layer);
            mapleObject.SpriteRenderer.sortingOrder = z;
            mapleObject.transform.position = pos;
            var mapleObj = mapleObject.AddComponent<MapleObj>();
            var wzImage = loader.Map.Nodes["Obj"].Nodes[src.Nodes["oS"].GetValue<string>() + ".img"]
                .GetValue<Wz_Image>();
            if (!wzImage.TryExtract())
                throw new Exception();

            var l0 = src.Nodes["l0"].GetValue<string>();
            var l1 = src.Nodes["l1"].GetValue<string>();
            var l2 = src.Nodes["l2"].GetValue<string>();
            // It may contains other nodes
            var objAnimation = wzImage.Node.Nodes[l0].Nodes[l1].Nodes[l2];
            MapleAnimator.Create(mapleObject, objAnimation.Nodes["seat"] != null ? objAnimation.Nodes["0"] : objAnimation);
            return mapleObj;
        }
    }
}