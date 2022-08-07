using Graphic;
using UnityEngine;

namespace Gameplay
{
    public class MapleObject : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer { get; private set; }

        public static MapleObject Create()
        {
            var gameObject = new GameObject();
            var mapleObject = gameObject.AddComponent<MapleObject>();
            mapleObject.SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            return mapleObject;
        }

        public static MapleObject Create(string sortingLayer)
        {
            var mapleObject = Create();
            mapleObject.SpriteRenderer.sortingLayerID = SortingLayer.NameToID(sortingLayer);
            return mapleObject;
        }

        public static MapleObject Create(MapleTexture mapleTexture)
        {
            var mapleObject = Create();
            mapleObject.SpriteRenderer.sprite = mapleTexture.Sprite;
            return mapleObject;
        }

        public static MapleObject Create(MapleTexture mapleTexture, string sortingLayer)
        {
            var mapleObject = Create(mapleTexture);
            mapleObject.SpriteRenderer.sortingLayerID = SortingLayer.NameToID(sortingLayer);
            return mapleObject;
        }
    }
}