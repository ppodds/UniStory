using System;
using Gameplay.Physics;
using Graphic;
using MapleLib.WzLib;
using Unity.VisualScripting;
using UnityEngine;

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
        private bool _animated;
        private int _cx;
        private int _cy;
        private double _rx;
        private double _ry;
        private int _hTile;
        private int _vTile;
        private float _opacity;
        private bool _flipped;

        private MovingObject _movingObject;

        #endregion

        public static Background Create(WzObject src)
        {
            var loader = Loader.getInstance();
            var backSrc = loader.Map["Back"];
            var animated = src["ani"].GetInt() == 1;

            var opacity = src["a"].GetFloat();
            var flipped = src["f"].GetInt() == 1;
            var cx = src["cx"].GetInt();
            var cy = src["cy"].GetInt();
            var rx = src["rx"].GetInt();
            var ry = src["ry"].GetInt();

            var movingObject = new MovingObject();
            // _movingObject.set_x(src["x"]);
            // _movingObject.set_y(src["y"]);

            var type = TypeById(src["type"].GetInt());

            var obj = MapleObject.Create(src["front"].GetInt() == 1 ? "Foreground" : "Background");
            var mapleAnimation = MapleAnimator.Create(obj,
                backSrc[src["bS"] + ".img"][animated ? "ani" : "back"][src["no"].ToString()]);
            var background = obj.AddComponent<Background>();
            obj.transform.position = new Vector2(src["x"].GetInt() / Constant.PixelsPerUnit,
                -src["y"].GetInt() / Constant.PixelsPerUnit);

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