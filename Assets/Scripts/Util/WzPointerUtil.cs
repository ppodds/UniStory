using System;
using UnityEngine;
using WzComparerR2.WzLib;

namespace Util
{
    public static class WzPointerUtil
    {
        public static Wz_Node OutPointerToNode(string wzPointer)
        {
            var split = wzPointer.Split("/");
            var node = (Wz_Node)typeof(Loader).GetField(split[0])?.GetValue(Loader.getInstance());
            for (var i = 1; i < split.Length && node != null; i++)
            {
                node = node.Nodes[split[i]];
                if (!split[i].Contains(".img")) continue;
                var wzImage = node.GetValue<Wz_Image>();
                if (!wzImage.TryExtract())
                    throw new Exception();
                node = wzImage.Node;
            }
            return node;
        }

        public static Wz_Node InPointerToNode(string wzPointer, Wz_Node currentNode)
        {
            var imageNode = currentNode.ParentNode;
            while (imageNode.GetValue<Wz_Image>() == null)
            {
                imageNode = imageNode.ParentNode;
            }
            var split = wzPointer.Split("/");
            var node = imageNode.Nodes[split[0]];
            for (var i = 1; i < split.Length && node != null; i++)
                node = node.Nodes[split[i]];
            
            return node;
        }
    }
}