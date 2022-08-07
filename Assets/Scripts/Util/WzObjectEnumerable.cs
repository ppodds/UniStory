using System.Collections;
using System.Collections.Generic;
using MapleLib.WzLib;

namespace Util
{
    public class WzObjectEnumerable : IEnumerable<WzObject>
    {
        private WzObject _src;

        public WzObjectEnumerable(WzObject src)
        {
            _src = src;
        }
        
        public IEnumerator<WzObject> GetEnumerator()
        {
            return new WzObjectEnumerator(_src);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}