using System.Collections.Generic;

namespace DeployerDomain
{
    public class Release
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string Version { get; set; }
        public string Created { get; set; }
        public Release(string id = "", string projectId = "", string version = "", string created = "")
        {
            Id = id;
            ProjectId = projectId;
            Version = version;
            Created = created;
        }

        // Finds and returns all releases related to the provided projectId as a list of strings.
        public static List<string> AllProjectReleases(string projectId, List<Release> releases)
        {
            List<string> allReleases = new List<string>();

            foreach (var release in releases)
            if (projectId == release.ProjectId.ToString())
            {
                allReleases.Add(release.Id.ToString());
            }
            return allReleases;
        }
    }
}
