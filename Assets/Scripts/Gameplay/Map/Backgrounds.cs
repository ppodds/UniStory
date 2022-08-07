using System;
using System.Collections;
using System.Collections.Generic;
using MapleLib.WzLib;
using UnityEngine;

namespace Gameplay.Map
{
    public class Backgrounds : MonoBehaviour
    {
        private bool _black;

        public static Backgrounds Create(WzObject src)
        {
            var obj = new GameObject("Backgrounds");
            var backgrounds = obj.AddComponent<Backgrounds>();
            var no = 0;
            var back = src[no.ToString()];
            while (back != null)
            {
                var child = Background.Create(back);
                child.transform.SetParent(obj.transform);
                no++;
                back = src[no.ToString()];
            }

            backgrounds._black = src["0"]["bS"].GetString() == "";
            return backgrounds;
        }
    }
}