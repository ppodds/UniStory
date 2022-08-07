using System;
using System.Collections.Generic;
using System.Drawing;
using Gameplay;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Util;

namespace Graphic
{
    public class MapleAnimation : MonoBehaviour
    {
        public List<MapleFrame> Frames { get; } = new List<MapleFrame>();
        private Animation _animation;
        private bool _animated;
        private bool _zigzag;

        public static MapleAnimation Create(MapleObject obj, WzObject src)
        {
            var mapleAnimation = obj.AddComponent<MapleAnimation>();
            if (src is WzCanvasProperty)
            {
                mapleAnimation.Frames.Add(new MapleFrame(src));
            }
            else
            {
                foreach (var sub in new WzObjectEnumerable(src))
                {
                    mapleAnimation.Frames.Add(new MapleFrame(sub));
                }
            }

            mapleAnimation._animated = mapleAnimation.Frames.Count > 1;
            mapleAnimation._zigzag = src["zigzag"]?.GetInt() == 1;

            var animation = mapleAnimation.GetComponent<Animation>();
            if (animation != null)
                mapleAnimation._animation = animation;
            mapleAnimation._animation = mapleAnimation.AddComponent<Animation>();

            if (mapleAnimation._animated)
            {
                var clip = new AnimationClip
                {
                    legacy = true,
                    name = "Default",
                    wrapMode = WrapMode.Loop
                };
                var spriteBinding = new EditorCurveBinding
                {
                    type = typeof(SpriteRenderer),
                    path = "",
                    propertyName = "m_Sprite"
                };
                var spriteKeyFrames = new ObjectReferenceKeyframe[mapleAnimation.Frames.Count];
                for (var i = 0; i < mapleAnimation.Frames.Count; i++)
                {
                    spriteKeyFrames[i] = new ObjectReferenceKeyframe
                    {
                        time = i,
                        value = mapleAnimation.Frames[i].MapleTexture.Sprite
                    };
                }

                AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, spriteKeyFrames);
                mapleAnimation._animation.AddClip(clip, clip.name);
                Debug.Log(mapleAnimation._animation.Play(clip.name));
            }
            else obj.SpriteRenderer.sprite = mapleAnimation.Frames[0].MapleTexture.Sprite;

            return mapleAnimation;
        }
    }
}