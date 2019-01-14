using System;
using System.Collections.Generic;
using System.Text;

namespace OptimLab
{
    public abstract class MethodOptions
    {
        protected Dictionary<string, string> descriptions;
        protected Dictionary<string, object> values;

        public MethodOptions()
        {
            descriptions = new Dictionary<string, string>();
            values = new Dictionary<string, object>();
        }

        public List<string> GetNames()
        {
            List<string> result = new List<string>();
            foreach (string name in values.Keys) result.Add(name);
            return result;
        }

        public string GetDescription(string name)
        {
            return descriptions[name];
        }

        public void SetDescription(string name, string description)
        {
            descriptions[name] = description;
        }

        public object GetValue(string name)
        {
            return values[name];
        }

        public abstract object GetDefaultValue(string name);

        public abstract void SetValue(string name, string value);

        public void CopyTo(MethodOptions destination)
        {
            destination.descriptions = new Dictionary<string, string>(descriptions);
            destination.values = new Dictionary<string, object>(values);
        }
    }
}