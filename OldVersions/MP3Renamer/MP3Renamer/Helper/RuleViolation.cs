using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MP3Renamer.Helper
{
    public class RuleViolation
    {
        public string Key { get; private set; }
        public string Value { get; private set; }


        public RuleViolation(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }

    }
}