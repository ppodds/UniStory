using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using UnityEngine;

namespace Util
{
    public class TextureTransformer : Transformer<Bitmap, Texture2D>
    {
        public TextureTransformer(Bitmap bitmap) : base(bitmap)
        {
        }

        public override Texture2D Transform()
        {
            var texture2D = new Texture2D(BeTransformed.Width, BeTransformed.Height);
            var ms = new MemoryStream();
            BeTransformed.Save(ms, ImageFormat.Png);
            var buffer = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(buffer, 0, buffer.Length);
            texture2D.LoadImage(buffer);
            return texture2D;
        }
    }
}