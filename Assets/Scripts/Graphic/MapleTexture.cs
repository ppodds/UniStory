using System;
using UnityEngine;
using Util;
using WzComparerR2.WzLib;

namespace Graphic
{
    public class MapleTexture
    {
        public Sprite Sprite { get; private set; }
        public Vector2 Origin { get; private set; }

        public MapleTexture(Wz_Node src)
        {
            if (src.Value is Wz_Uol)
            {
                src = src.GetValue<Wz_Uol>().HandleUol(src);
            }
            // this is a pointer to a texture asset
            var outLink = src.Nodes["_outlink"]?.GetValue<string>();
            // this is a pointer which point to a texture asset in the same wz image
            var inLink = src.Nodes["_inlink"]?.GetValue<string>();
            // texture hash, the same texture has the same hash
            // I think can use this to implement texture repository to save memory
            // hash would be null sometime
            var hash = src.Nodes["_hash"]?.GetValue<string>();
            Wz_Node node = null;
            if (outLink != null)
            {
                node = WzPointerUtil.OutPointerToNode(outLink);
            }
            else if (inLink != null)
            {
                node = WzPointerUtil.InPointerToNode(inLink, src);
            }
            else
            {
                node = src;
            }
            
            var texture = new TextureTransformer(node.GetValue<Wz_Png>().ExtractPng()).Transform();
            var origin = src.FindNodeByPath("origin").GetValue<Wz_Vector>();
            Origin = new Vector2((float)origin.X / texture.width,
                (float)(texture.height - origin.Y) / texture.height);
            Sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Origin,
                Constant.PixelsPerUnit);
        }
    }
}