using System;
using System.IO;

using Newtonsoft.Json;
using System.Collections.Generic;

namespace DeployManager
{
    public class LatestDeploy
    {
        public string Id { get; set; }
        public string ReleaseId { get; set; }
        public string EnvironmentId { get; set; }
        public string DeployedAt { get; set; }
        public LatestDeploy(string id, string releaseId, string environmentId, string deployedAt)
        {
            Id = id;
            ReleaseId = releaseId;
            EnvironmentId = environmentId;
            DeployedAt = deployedAt;
        }
    }

    internal class Program
    {
        static void Main()
        {
            short numberOfReleasesToKeep = 1;

            dynamic projects = JsonConvert.DeserializeObject(File.ReadAllText("Projects.json"));
            dynamic environments = JsonConvert.DeserializeObject(File.ReadAllText("Environments.json"));

            dynamic releases = JsonConvert.DeserializeObject(File.ReadAllText("Releases.json"));
            dynamic deployments = JsonConvert.DeserializeObject(File.ReadAllText("Deployments.json"));
            
            void FindLatestDeploy(string environmentId, dynamic deploys)
            {
                LatestDeploy latestDeploy = new LatestDeploy("", "", "", "");
                foreach (var deploy in deploys)
                {
                    if (deploy.EnvironmentId.ToString() == environmentId)
                    {
                        if (latestDeploy.DeployedAt == "")
                        {
                            latestDeploy.Id = deploy.Id;
                            latestDeploy.ReleaseId = deploy.ReleaseId;
                            latestDeploy.EnvironmentId = deploy.EnvironmentId;
                            latestDeploy.DeployedAt = deploy.DeployedAt;
                        }
                        else if (DateTime.Parse(latestDeploy.DeployedAt) < DateTime.Parse(deploy.DeployedAt.ToString()))
                        {
                            latestDeploy.Id = deploy.Id;
                            latestDeploy.ReleaseId = deploy.ReleaseId;
                            latestDeploy.EnvironmentId = deploy.EnvironmentId;
                            latestDeploy.DeployedAt = deploy.DeployedAt;
                        }
                    }
                }
                Console.WriteLine($"- `{latestDeploy.ReleaseId}` kept because it was the most recently deployed to `{latestDeploy.EnvironmentId}`");
            }

            dynamic FindProjectDeploys(List<string> projectReleases)
            {
                List<dynamic> projectDeploys = new List<dynamic>();
                foreach (var deployment in deployments)
                {
                    if (projectReleases.Contains(deployment.ReleaseId.ToString()))
                    {
                        projectDeploys.Add(deployment);
                    }
                }
                return projectDeploys;
            }

            List<string> FindAllReleases(string projectId)
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

            foreach (var project in projects)
            {
                List<string> projectReleases = FindAllReleases(project.Id);
                Console.WriteLine($"Project {project.Name}");
                Console.WriteLine();
                foreach (var environment in environments)
                {
                    Console.WriteLine($"Environment {environment.Name}");
                    dynamic projectDeploys = FindProjectDeploys(projectReleases);
                    FindLatestDeploy(environment.Id, projectDeploys);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
