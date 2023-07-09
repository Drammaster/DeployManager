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

            string projectsJson = File.ReadAllText("Projects.json");
            var projects = JsonConvert.DeserializeObject<List<Project>>(projectsJson);

            string environmentsJson = File.ReadAllText("Environments.json");
            var environments = JsonConvert.DeserializeObject<List<Environment>>(environmentsJson);

            string releasesJson = File.ReadAllText("Releases.json");
            var releases = JsonConvert.DeserializeObject<List<Release>>(releasesJson);

            string deploymentsJson = File.ReadAllText("Deployments.json");
            var deployments = JsonConvert.DeserializeObject<List<Deployment>>(deploymentsJson);

            List<Deployment> FindProjectDeploys(List<string> projectReleases)
            {
                List<Deployment> projectDeploys = new List<Deployment>();
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
                    List<Deployment> projectDeploys = FindProjectDeploys(projectReleases);
                    Deployment.FindLatestDeploy(environment.Id, projectDeploys);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
