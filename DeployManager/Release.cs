using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeployManager
{
    internal class Release
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string Version { get; set; }
        public string Created { get; set; }

        public static List<string> AllProjectReleases(string projectId, List<Release> releases)
        {
            List<string> allReleases = new List<string>();
            foreach (var release in releases)
            {
                if (projectId == release.ProjectId.ToString())
                {
                    allReleases.Add(release.Id.ToString());
                }
            }
            return allReleases;
        }
    }
}
