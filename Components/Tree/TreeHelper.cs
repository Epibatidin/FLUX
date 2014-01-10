using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tree
{
    public static class TreeHelper
    {
        /// <summary>
        /// Converts from SubTypes To BaseTypes 
        /// </summary>
        /// <typeparam name="TBase"></typeparam>
        /// <typeparam name="TSub"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        //public static TreeItem<TBase> Convert<TBase, TSub>(TreeItem<TSub> root) where TSub : TBase
        //{
        //    TreeItem<TBase> sub = new TreeItem<TBase>();
        //    sub.Value = root.Value;
        //    sub.Level = root.Level;

        //    if (root.HasChildren)
        //    {
        //        foreach (var item in root.Children)
        //        {
        //            var child = Convert<TBase, TSub>(item);
        //            sub.SaveAdd(child);
        //        }
        //    }
        //    return sub;
        //} 

    }
}
