using System;
using System.IO;

using Newtonsoft.Json;
using System.Collections.Generic;

namespace DeployManager
{
    internal class Program
    {
        static void Main()
        {
            short numberOfReleasesToKeep = 1;

            dynamic projects = JsonConvert.DeserializeObject(File.ReadAllText("Projects.json"));
            dynamic environments = JsonConvert.DeserializeObject(File.ReadAllText("Environments.json"));

            dynamic releases = JsonConvert.DeserializeObject(File.ReadAllText("Releases.json"));
            dynamic deployments = JsonConvert.DeserializeObject(File.ReadAllText("Deployments.json"));

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
                    Deployment.FindLatestDeploy(environment.Id, projectDeploys);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
