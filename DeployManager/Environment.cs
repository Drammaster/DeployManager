using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeployManager
{
    internal class Environment
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public dynamic LatestRelease { get; set; }

        public Environment(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
