﻿using System;
using Audio;
using Gameplay.Map;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay
{
    public class Stage : MonoBehaviour
    {
        #region properties

        public static Stage Instance { get; private set; }

        [field: SerializeField] public int MapId { get; private set; }

        private State _state;

        [field: SerializeField] public Backgrounds Backgrounds { get; private set; }
        [field: SerializeField] public Layers Layers { get; private set; }
        [field: SerializeField] private Physics.Physics _physics;
        [field: SerializeField] private MapInfo _mapInfo;
        [field: SerializeField] private AudioManager _audioManager;
        [field: SerializeField] private Ladders _ladders;
        [field: SerializeField] private Seats _seats;

        #endregion

        #region unity hooks

        private void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);

            Instance = this;
            _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
            _mapInfo = new MapInfo(src["info"]);
            _ladders = new Ladders(src["ladderRope"]);
            _seats = new Seats(src["seat"]);
            _audioManager.PlayBGM(_mapInfo.BGM);
        }

        private void Respawn(int portalId)
        {
        }
    }
}