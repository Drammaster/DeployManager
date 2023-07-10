using System;
using System.IO;

using Newtonsoft.Json;
using System.Collections.Generic;
using DeployerDomain;

namespace DeployManager
{
    internal class Program
    {
        static void Main()
        {
            //short numberOfReleasesToKeep = 1;

            // All JSON file imports.
            string projectsJson = File.ReadAllText("Projects.json");
            var projects = JsonConvert.DeserializeObject<List<Project>>(projectsJson);

            string environmentsJson = File.ReadAllText("Environments.json");
            var environments = JsonConvert.DeserializeObject<List<DeployerDomain.Environment>>(environmentsJson);

            string releasesJson = File.ReadAllText("Releases.json");
            var releases = JsonConvert.DeserializeObject<List<Release>>(releasesJson);

            string deploymentsJson = File.ReadAllText("Deployments.json");
            var deployments = JsonConvert.DeserializeObject<List<Deployment>>(deploymentsJson);

            foreach (var project in projects)
            {
                // Finding all releases for the project.
                List<string> projectReleases = Release.AllProjectReleases(project.Id, releases);

                // Find all deployments made to the project
                var projectDeploys = Deployment.FindProjectDeploys(projectReleases, deployments);

                Console.WriteLine($"Project {project.Name}");
                Console.WriteLine(); // Space for readability.

                foreach (var environment in environments)
                {
                    Console.WriteLine($"Environment {environment.Name}");

                    // Find and show the latest deployment to the environment.
                    Deployment.FindLatestDeploy(environment.Id, projectDeploys);
                }
                Console.WriteLine(); // Space for readability.
            }

            // Wait for key press to allow user to inspect the results.
            Console.ReadKey();
        }
    }
}
