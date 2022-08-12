using System;
using Gameplay.Physics;
using Graphic;
using Unity.VisualScripting;
using UnityEngine;
using WzComparerR2.WzLib;

namespace Gameplay.Map
{
    [Serializable]
    public class Background : MonoBehaviour
    {
        #region properties

        private int _vWidth => Screen.width;
        private int _vHeight => Screen.height;
        private int _wOffset => Screen.width / 2;
        private int _hOffset => Screen.height / 2;

        private MapleAnimator _mapleAnimator;
        [SerializeField] private bool animated;
        [SerializeField] private int cx;
        [SerializeField] private int cy;
        [SerializeField] private double rx;
        [SerializeField] private double ry;
        [SerializeField] private int hTile;
        [SerializeField] private int vTile;
        [SerializeField] private float opacity;
        [SerializeField] private bool flipped;

        private MovingObject _movingObject;

        #endregion

        public static Background Create(Wz_Node src)
        {
            var loader = Loader.getInstance();
            var backSrc = loader.Map.Nodes["Back"];
            var animated = src.Nodes["ani"].GetValue<bool>();

            var opacity = src.Nodes["a"].GetValue<float>();
            var flipped = src.Nodes["f"].GetValue<bool>();
            var cx = src.Nodes["cx"].GetValue<int>();
            var cy = src.Nodes["cy"].GetValue<int>();
            var rx = src.Nodes["rx"].GetValue<int>();
            var ry = src.Nodes["ry"].GetValue<int>();

            var movingObject = new MovingObject();
            // _movingObject.set_x(src["x"]);
            // _movingObject.set_y(src["y"]);

            var type = TypeById(src.Nodes["type"].GetValue<int>());

            var obj = MapleObject.Create(src.Nodes["front"].GetValue<bool>() ? "Foreground" : "Background");
            var value = src.Nodes["bS"].GetValue<string>();
            var wzImage = backSrc.Nodes[value + ".img"].GetValue<Wz_Image>();
            if (!wzImage.TryExtract())
                throw new Exception();

            var mapleAnimation = MapleAnimator.Create(obj,
                wzImage.Node.Nodes[animated ? "ani" : "back"].Nodes[src.Nodes["no"].GetValue<int>().ToString()]);
            var background = obj.AddComponent<Background>();
            obj.transform.position = new Vector2(src.Nodes["x"].GetValue<int>() / Constant.PixelsPerUnit,
                -src.Nodes["y"].GetValue<int>() / Constant.PixelsPerUnit);
            background.animated = animated;
            background.cx = cx;
            background.cy = cy;
            background.rx = rx;
            background.ry = ry;
            background.flipped = flipped;
            background.opacity = opacity;
            // background.SetType(type);
            return background;
        }

        private enum Type
        {
            Normal,
            HTiled,
            VTiled,
            Tiled,
            HMoveA,
            VMoveA,
            HMoveB,
            VMoveB
        };

        private static Type TypeById(int id)
        {
            if (id is >= (int)Type.Normal and <= (int)Type.VMoveB)
                return (Type)id;

            Debug.Log("Unknown Background::Type id: [" + id + "]");

            return Type.Normal;
        }

        //     private void SetType(Type type)
        //     {
        //         var dimX = _mapleAnimation.Dimension.x;
        //         var dimY = _mapleAnimation.Dimension.y;
        //
        //         // TODO: Double check for zero. Is this a WZ reading issue?
        //         if (_cx == 0)
        //             _cx = (int)(dimX > 0 ? dimX : 1);
        //
        //         if (_cy == 0)
        //             _cy = (int)(dimY > 0 ? dimY : 1);
        //
        //         _hTile = 1;
        //         _vTile = 1;
        //
        //         switch (type)
        //         {
        //             case Type.HTiled:
        //             case Type.HMoveA:
        //                 _hTile = _vWidth / _cx + 3;
        //                 break;
        //             case Type.VTiled:
        //             case Type.VMoveA:
        //                 _hTile = _vHeight / _cy + 3;
        //                 break;
        //             case Type.Tiled:
        //             case Type.HMoveB:
        //             case Type.VMoveB:
        //                 _hTile = _vWidth / _cx + 3;
        //                 _vTile = _vHeight / _cy + 3;
        //                 break;
        //         }
        //
        //         // switch (type)
        //         // {
        //         //     case Type.HMoveA:
        //         //     case Type.HMoveB:
        //         //         moveobj.hspeed = rx / 16;
        //         //         break;
        //         //     case Type.VMoveA:
        //         //     case Type.VMoveB:
        //         //         moveobj.vspeed = ry / 16;
        //         //         break;
        //         // }
        //         Debug.Log(_hTile);
        //         Debug.Log(_vTile);
        //     }
    }
}