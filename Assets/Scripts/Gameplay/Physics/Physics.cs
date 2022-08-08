using MapleLib.WzLib;

namespace Gameplay.Physics
{
    public class Physics
    {
        private FootholdTree _footholdTree;

        public Physics(WzObject src)
        {
            _footholdTree = new FootholdTree(src);
        }
    }
}