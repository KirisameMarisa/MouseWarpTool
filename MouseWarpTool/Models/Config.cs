using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MouseWarpTool.Models
{
    [Serializable]
    public class Config
    {   
        public double IgnitionTime { get; set; }

        public Config()
        {

        }

        public Config(double ignitionTime)
        {
            IgnitionTime = ignitionTime;
        }

        public static Config DefaultConfig()
        {
            return new Config { IgnitionTime = 30 };
        }
    }
}
