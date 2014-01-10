using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace VirtualFileProvider.Config
{
    public class DebugSection : ConfigurationSection
    {
        private string[] _rootNames;
        public string[] RootNames
        {
            get
            {
                if (_rootNames == null)
                {
                    var value = rootnames ?? "";
                    _rootNames = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                return _rootNames;
            }
        }


        [ConfigurationProperty("RootNames", IsRequired = false, DefaultValue = null)]
        private string rootnames
        {
            get
            {
                return this["RootNames"] as string;
            }
        }

        [ConfigurationProperty("SubRootPos", IsRequired = false, DefaultValue = null)]
        private string subrootpos
        {
            get
            {
                return this["SubRootPos"] as string;
            }
        }

        private int[] _subRoots;
        [ConfigurationProperty("SubRootPos", IsRequired = false, DefaultValue = null)]
        public int[] SubRootPos
        {
            get
            {
                if (_subRoots == null)
                {
                    var value = subrootpos ?? "";

                    List<int> ints = new List<int>();
                    int dummy = 0;
                    foreach (var intString in value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (!Int32.TryParse(intString, out dummy)) continue;

                        ints.Add(dummy);
                    }
                    _subRoots = ints.ToArray();
                }
                return _subRoots;
            }
        }
    }
}
