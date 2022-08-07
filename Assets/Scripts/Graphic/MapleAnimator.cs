using System;
using System.Collections.Generic;
using System.Drawing;
using Gameplay;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Util;

namespace Graphic
{
    public class MapleAnimator : MonoBehaviour
    {
        public List<MapleFrame> Frames { get; } = new List<MapleFrame>();
        private Animator _animator;
        private bool _animated;
        private bool _zigzag;

        public static MapleAnimator Create(MapleObject obj, WzObject src)
        {
            var mapleAnimator = obj.AddComponent<MapleAnimator>();
            if (src is WzCanvasProperty)
            {
                mapleAnimator.Frames.Add(new MapleFrame(src));
            }
            else
            {
                foreach (var sub in new WzObjectEnumerable(src))
                {
                    mapleAnimator.Frames.Add(new MapleFrame(sub));
                }
            }

            mapleAnimator._animated = mapleAnimator.Frames.Count > 1;
            mapleAnimator._zigzag = src["zigzag"]?.GetInt() == 1;

            var animator = mapleAnimator.GetComponent<Animator>();
            if (animator != null)
                mapleAnimator._animator = animator;
            mapleAnimator._animator = mapleAnimator.AddComponent<Animator>();

            if (mapleAnimator._animated)
            {
                var clip = new AnimationClip
                {
                    name = "Default"
                };
                var spriteBinding = new EditorCurveBinding
                {
                    type = typeof(SpriteRenderer),
                    path = "",
                    propertyName = "m_Sprite"
                };
                var spriteKeyFrames = new ObjectReferenceKeyframe[mapleAnimator.Frames.Count];
                var delay = 0;
                for (var i = 0; i < mapleAnimator.Frames.Count; i++)
                {
                    spriteKeyFrames[i] = new ObjectReferenceKeyframe
                    {
                        time = delay / Constant.TimeStep,
                        value = mapleAnimator.Frames[i].MapleTexture.Sprite
                    };
                    delay += mapleAnimator.Frames[i].Delay;
                }
                AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, spriteKeyFrames);
                var setting = AnimationUtility.GetAnimationClipSettings(clip);
                setting.loopTime = true;
                AnimationUtility.SetAnimationClipSettings(clip, setting);
                var controller = new AnimatorController();
                controller.AddLayer("Base Layer");
                var state = controller.layers[0].stateMachine.AddState("Default");
                state.motion = clip;
                mapleAnimator._animator.runtimeAnimatorController = controller;
            }
            else obj.SpriteRenderer.sprite = mapleAnimator.Frames[0].MapleTexture.Sprite;

            return mapleAnimator;
        }
    }
}