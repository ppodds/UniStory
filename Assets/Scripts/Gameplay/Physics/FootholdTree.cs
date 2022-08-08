using System.Collections.Generic;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;

namespace Gameplay.Physics
{
    public class FootholdTree
    {
        private Dictionary<int, Foothold> _footholds= new Dictionary<int, Foothold>(); 
        public FootholdTree(WzObject src)
        {
            foreach (var layer in ((WzSubProperty)src).WzProperties)
            {
                foreach (var a in layer.WzProperties)
                {
                    foreach (var b in a.WzProperties)
                    {
                        var id = int.Parse(b.Name);
                        var foothold = new Foothold(b, id, int.Parse(layer.Name));
                        _footholds.Add(id, foothold);
                    }
                }
            }
        }
    }
}