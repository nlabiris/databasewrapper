using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseWrapper.Mapping.Attributes {
    [AttributeUsage(AttributeTargets.Property)]
    public class DataNamesAttribute : Attribute {
        public List<string> ValueNames = null;

        public DataNamesAttribute() {
            ValueNames = new List<string>();
        }

        public DataNamesAttribute(params string[] valueNames) {
            ValueNames = valueNames.ToList();
        }
    }
}
