using System.Collections;
using System.Collections.Generic;
using MapleLib.WzLib;
using UnityEngine;

namespace Util
{
    public class WzObjectEnumerator : IEnumerator<WzObject>
    {
        private readonly WzObject _src;
        private int _index = -1;

        public WzObjectEnumerator(WzObject src)
        {
            _src = src;
        }

        public bool MoveNext()
        {
            if (_src[(++_index).ToString()] == null)
                return false;

            Current = _src[_index.ToString()];
            return true;
        }

        public void Reset()
        {
            _index = -1;
        }

        public WzObject Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}