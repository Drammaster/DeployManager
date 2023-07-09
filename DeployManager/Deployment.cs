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

        public static void FindLatestDeploy(string environmentId, List<Deployment> deploys)
        {
            try
            {
                Deployment latestDeploy = new Deployment(
                    String.Empty,
                    String.Empty,
                    String.Empty,
                    String.Empty
                    );

                foreach (var deploy in deploys)
                if (deploy.EnvironmentId.ToString() == environmentId)
                {
                    if (latestDeploy.DeployedAt == String.Empty)
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
                Console.WriteLine($"- `{latestDeploy.ReleaseId}` kept because it was the most recently deployed to `{latestDeploy.EnvironmentId}`");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static List<Deployment> FindProjectDeploys(List<string> projectReleases, List<Deployment> deployments)
        {
            List<Deployment> projectDeploys = new List<Deployment>();

            foreach (var deployment in deployments)
            if (projectReleases.Contains(deployment.ReleaseId.ToString()))
            {
                projectDeploys.Add(deployment);
            }
            return projectDeploys;
        }
    }
}
