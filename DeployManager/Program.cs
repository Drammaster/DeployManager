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

            // All JSON file imports.
            string projectsJson = File.ReadAllText("Projects.json");
            var projects = JsonConvert.DeserializeObject<List<Project>>(projectsJson);

            string environmentsJson = File.ReadAllText("Environments.json");
            var environments = JsonConvert.DeserializeObject<List<Environment>>(environmentsJson);

            string releasesJson = File.ReadAllText("Releases.json");
            var releases = JsonConvert.DeserializeObject<List<Release>>(releasesJson);

            string deploymentsJson = File.ReadAllText("Deployments.json");
            var deployments = JsonConvert.DeserializeObject<List<Deployment>>(deploymentsJson);

            foreach (var project in projects)
            {
                // Finding all releases for a project.
                List<string> projectReleases = Release.AllProjectReleases(project.Id, releases);

                Console.WriteLine($"Project {project.Name}");
                Console.WriteLine();

                foreach (var environment in environments)
                {
                    Console.WriteLine($"Environment {environment.Name}");
                    var projectDeploys = Deployment.FindProjectDeploys(projectReleases, deployments);
                    Deployment.FindLatestDeploy(environment.Id, projectDeploys);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
