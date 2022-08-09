using System;
using Gameplay;
using MapleLib.WzLib;
using UnityEngine;
using UnityEngine.Playables;

namespace Graphic
{
    public class MapleAnimator : MonoBehaviour
    {
        private Animator _animator;
        private PlayableGraph _playableGraph;

        public static MapleAnimator Create(MapleObject obj, WzObject src)
        {
            var mapleAnimator = obj.gameObject.AddComponent<MapleAnimator>();

            var animator = mapleAnimator.GetComponent<Animator>();
            if (animator != null)
                mapleAnimator._animator = animator;
            mapleAnimator._animator = mapleAnimator.gameObject.AddComponent<Animator>();

            var mapleAnimation = new MapleAnimation(src);
            if (!mapleAnimation.IsAnimated) {
                obj.SpriteRenderer.sprite = mapleAnimation.Frames[0].MapleTexture.Sprite;
                return mapleAnimator;
            }

            var playableGraph = PlayableGraph.Create("MapleAnimation");
            var scriptPlayable = ScriptPlayable<MaplePlayableBehavior>.Create(playableGraph);
            scriptPlayable.GetBehaviour().MapleAnimation = mapleAnimation;
            scriptPlayable.GetBehaviour().SpriteRenderer = obj.SpriteRenderer;
            var scriptPlayableOutput = ScriptPlayableOutput.Create(playableGraph, "out");
            scriptPlayableOutput.SetSourcePlayable(scriptPlayable);
            playableGraph.Play();
            mapleAnimator._playableGraph = playableGraph;

            return mapleAnimator;
        }
    }
}