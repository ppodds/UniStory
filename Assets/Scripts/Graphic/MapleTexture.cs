using System;
using Gameplay;
using MapleLib.WzLib;
using UnityEngine;
using Util;

namespace Graphic
{
    public class MapleTexture
    {
        public Sprite Sprite { get; private set; }
        public Vector2 Origin { get; private set; }

        public MapleTexture(WzObject src)
        {
            // TODO: source link
            var link = src["source"];
            if (link != null)
            {
                throw new NotImplementedException();
            }

            var texture = new TextureTransformer(src.GetBitmap()).Transform();
            var origin = src["origin"].GetPoint();
            Origin = new Vector2((float)origin.X / texture.width,
                (float)(texture.height - origin.Y) / texture.height);
            Sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Origin, Constant.PixelsPerUnit);
        }
    }
}