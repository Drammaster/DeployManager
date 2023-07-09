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

        public static void FindLatestDeploy(string environmentId, Deployment[] deploys)
        {
            Deployment latestDeploy = new Deployment("", "", "", "");
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
    }
}
