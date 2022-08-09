using UnityEngine;
using UnityEngine.Playables;

namespace Graphic
{
    public class MaplePlayableBehavior : PlayableBehaviour
    {
        public SpriteRenderer SpriteRenderer;


        private MapleAnimation _mapleAnimation;
        public MapleAnimation MapleAnimation
        {
            get => _mapleAnimation;
            set
            {
                _mapleAnimation = value;
                _delay = MapleAnimation.Frames[0].Delay;
            }
        }

        private int _currentFrame = 0;
        private float _delay = 0;
        private int _frameStep = 1;
        
        public override void PrepareFrame(Playable playable, FrameData info)
        {
            if (info.deltaTime >= _delay)
            {
                var lastFrame = MapleAnimation.Frames.Count - 1;
                int nextFrame;
                if (MapleAnimation.Zigzag && lastFrame > 0)
                {
                    if (_frameStep == 1 && _currentFrame == lastFrame)
                    {
                        _frameStep = -_frameStep;
                    }
                    else if (_frameStep == -1 && _currentFrame == 0)
                    {
                        _frameStep = -_frameStep;
                    }
                    
                    nextFrame = _currentFrame + _frameStep;
                }
                else
                {
                    if (_currentFrame == lastFrame)
                    {
                        nextFrame = 0;
                    }
                    else
                    {
                        nextFrame = _currentFrame + 1;
                    }
                }

                var delta = info.deltaTime - _delay;
                _currentFrame = nextFrame;
                _delay = CurrentFrame().Delay;

                if (_delay >= delta)
                    _delay -= delta;
                
                SpriteRenderer.sprite = CurrentFrame().MapleTexture.Sprite;
            }
            else
            {
                _delay -= info.deltaTime;
            }
            base.PrepareFrame(playable, info);
        }

        private MapleFrame CurrentFrame()
        {
            return MapleAnimation.Frames[_currentFrame];
        }
        
    }
}