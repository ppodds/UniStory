using Gameplay.Physics;
using MapleLib.WzLib;
using UnityEngine;

namespace Gameplay.Map
{
    public class Layer : MonoBehaviour
    {
        public static Layer Create(WzObject src, int layer)
        {
            var obj = new GameObject("Layer" + layer);
            var layerObj = obj.AddComponent<Layer>();
            obj.transform.position = new Vector3(0, 0, layer);
            var objs = Objs.Create(src[layer.ToString()], layer);
            objs.transform.SetParent(obj.transform);
            var tiles = Tiles.Create(src[layer.ToString()], layer);
            tiles.transform.SetParent(obj.transform);
            return layerObj;
        }
    }
}