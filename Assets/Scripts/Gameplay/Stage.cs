using System;
using Gameplay.Map;
using UnityEngine;

namespace Gameplay
{
    public class Stage : MonoBehaviour
    {
        #region properties

        public static Stage Instance { get; private set; }

        public int MapId { get; private set; }

        private State _state;

        public Backgrounds Backgrounds { get; private set; }
        public Layers Layers { get; private set; }
        private Physics.Physics _physics; 

        #endregion

        #region unity hooks

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        #endregion

        private enum State
        {
            Inactive,
            Transition,
            Active
        }

        private Stage()
        {
            _state = State.Inactive;
        }

        public void Load(int mapId, int portalId)
        {
            switch (_state)
            {
                case State.Inactive:
                    LoadMap(mapId);
                    Respawn(portalId);
                    break;
                case State.Transition:
                    Respawn(portalId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _state = State.Active;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void LoadPlayer()
        {
            throw new NotImplementedException();
        }

        private void LoadMap(int mapId)
        {
            MapId = mapId;

            var padding = "";
            for (var i = 0; i < 9 - mapId.ToString().Length; i++)
                padding += "0";
            var strId = padding + mapId;
            var prefix = (mapId / 100000000).ToString();
            var loader = Loader.getInstance();
            var src = MapId == -1
                ? loader.UI["CashShopPreview.img"]
                : loader.Map["Map"]["Map" + prefix][strId + ".img"];
            Backgrounds = Backgrounds.Create(src["back"]);
            Backgrounds.transform.SetParent(gameObject.transform);
            Layers = Layers.Create(src);
            Layers.transform.SetParent(gameObject.transform);
            _physics = new Physics.Physics(src["foothold"]);
        }

        private void Respawn(int portalId)
        {
        }
    }
}