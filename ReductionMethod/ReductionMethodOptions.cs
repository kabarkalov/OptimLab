using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OptimLab
{
    public class ReductionMethodOptions : MethodOptions
    {
        public ReductionMethodOptions()
        {
            SetDescription("R", "Параметр метода");
            SetDescription("MaxIters", "Макс. кол-во итераций");
            SetDescription("Epsilon", "Требуемая точность");

            values["R"] = GetDefaultValue("R");
            values["MaxIters"] = GetDefaultValue("MaxIters");
            values["Epsilon"] = GetDefaultValue("Epsilon");
        }

        public override object GetDefaultValue(string name)
        {
            switch (name)
            {
                case "R":
                    return 2.0;
                case "MaxIters":
                    return 2000;
                case "Epsilon":
                    return 0.01;
                default:
                    return "";
            }
        }

        public override void SetValue(string name, string value)
        {
            switch (name)
            {
                case "R":
                    try { values[name] = Convert.ToDouble(value, CultureInfo.InvariantCulture); }
                    catch { values[name] = (double)GetDefaultValue(name); }
                    break;
                case "MaxIters":
                    try { values[name] = Convert.ToInt32(value); }
                    catch { values[name] = (int)GetDefaultValue(name); }
                    break;
                case "Epsilon":
                    try { values[name] = Convert.ToDouble(value, CultureInfo.InvariantCulture); }
                    catch { values[name] = (double)GetDefaultValue(name); }
                    break;
            }
        }
    }
}