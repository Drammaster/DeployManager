using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeployManager
{
    internal class Deployment
    {
        public string Id { get; set; }
        public string ReleaseId { get; set; }
        public string EnvironmentId { get; set; }
        public string DeployedAt { get; set; }
        public Deployment(string id, string releaseId, string environmentId, string deployedAt)
        {
            Id = id;
            ReleaseId = releaseId;
            EnvironmentId = environmentId;
            DeployedAt = deployedAt;
        }

        // Finds the latest deployment to the provided environmentId.
        public static void FindLatestDeploy(string environmentId, List<Deployment> deployments)
        {
            try
            {
                Deployment latestDeployment = new Deployment(
                    String.Empty,
                    String.Empty,
                    String.Empty,
                    String.Empty
                    );

                // Find each deployment which was made to the environment.
                foreach (var deployment in deployments)
                if (deployment.EnvironmentId.ToString() == environmentId)
                {
                    // If we haven't saved a deployment yet, then save it as the latest deployment.
                    if (latestDeployment.DeployedAt == String.Empty)
                    {
                        latestDeployment.Id = deployment.Id;
                        latestDeployment.ReleaseId = deployment.ReleaseId;
                        latestDeployment.EnvironmentId = deployment.EnvironmentId;
                        latestDeployment.DeployedAt = deployment.DeployedAt;
                    }
                    // If we have a deployment saved, then compare our current deployment with the new one to determine which was deployed later.
                    else if (DateTime.Parse(latestDeployment.DeployedAt) < DateTime.Parse(deployment.DeployedAt.ToString()))
                    {
                        latestDeployment.Id = deployment.Id;
                        latestDeployment.ReleaseId = deployment.ReleaseId;
                        latestDeployment.EnvironmentId = deployment.EnvironmentId;
                        latestDeployment.DeployedAt = deployment.DeployedAt;
                    }
                }
                Console.WriteLine($"- `{latestDeployment.ReleaseId}` kept because it was the most recently deployed to `{latestDeployment.EnvironmentId}`");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // Finds all the deployments related to a project based on the list of releases made to that project.
        public static List<Deployment> FindProjectDeploys(List<string> projectReleases, List<Deployment> deployments)
        {
            List<Deployment> projectDeploys = new List<Deployment>();

            // Find each deployment that relates to a release in the list of releases.
            foreach (var deployment in deployments)
            if (projectReleases.Contains(deployment.ReleaseId.ToString()))
            {
                // Add deployment to the list of deployments
                projectDeploys.Add(deployment);
            }
            return projectDeploys;
        }
    }
}
