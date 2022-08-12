using Graphic;
using UnityEngine;

namespace Gameplay
{
    public class MapleObject : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer { get; private set; }

        public static MapleObject Create(string name)
        {
            var gameObject = new GameObject(name);
            var mapleObject = gameObject.AddComponent<MapleObject>();
            mapleObject.SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            return mapleObject;
        }

        public static MapleObject Create(string name, string sortingLayer)
        {
            var mapleObject = Create(name);
            mapleObject.SpriteRenderer.sortingLayerID = SortingLayer.NameToID(sortingLayer);
            return mapleObject;
        }

        public static MapleObject Create(string name, MapleTexture mapleTexture)
        {
            var mapleObject = Create(name);
            mapleObject.SpriteRenderer.sprite = mapleTexture.Sprite;
            return mapleObject;
        }

        public static MapleObject Create(string name, MapleTexture mapleTexture, string sortingLayer)
        {
            var mapleObject = Create(name, mapleTexture);
            mapleObject.SpriteRenderer.sortingLayerID = SortingLayer.NameToID(sortingLayer);
            return mapleObject;
        }
    }
}