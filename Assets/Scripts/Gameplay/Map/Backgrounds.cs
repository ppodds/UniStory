using UnityEngine;
using WzComparerR2.WzLib;

namespace Gameplay.Map
{
    public class Backgrounds : MonoBehaviour
    {
        private bool _black;

        public static Backgrounds Create(Wz_Node src)
        {
            var obj = new GameObject("Backgrounds");
            var backgrounds = obj.AddComponent<Backgrounds>();
            var no = 0;
            var back = src.Nodes[no.ToString()];
            while (back != null)
            {
                var child = Background.Create(back);
                child.transform.SetParent(obj.transform);
                no++;
                back = src.FindNodeByPath(no.ToString());
            }

            backgrounds._black = src.FindNodeByPath("0\\bS").GetValue<string>() == "";
            return backgrounds;
        }
    }
}