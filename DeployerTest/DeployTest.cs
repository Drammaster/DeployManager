using DeployerDomain;
using System.Collections.Generic;

namespace DeployTest
{
    public class DeployTests
    {
        public static IEnumerable<object[]> goodData()
        {
            yield return new object[] {
                new List<string> { "Release-1" },
                new List<Deployment>
                    {
                        new Deployment("Project-1", "Release-1", "Environment-1", "2000-01-01T08:00:00"),
                        new Deployment("Project-2", "Release-2", "Environment-1", "2000-01-01T09:00:00")
                    },
                new List<Deployment>
                    {
                        new Deployment("Project-1", "Release-1", "Environment-1", "2000-01-01T08:00:00")
                    }
            };
        }

        public static IEnumerable<object[]> badData()
        {
            yield return new object[] {
                new List<string> { "Release-2" },
                new List<Deployment>
                    {
                        new Deployment("Project-1", "Release-1", "Environment-1", "2000-01-01T08:00:00"),
                        new Deployment("Project-2", "Release-2", "Environment-1", "2000-01-01T09:00:00")
                    },
                new List<Deployment>
                    {
                        new Deployment("Project-1", "Release-1", "Environment-1", "2000-01-01T08:00:00")
                    }
            };
        }

        [Theory]
        [MemberData(nameof(goodData))]
        public void FindProjectDeploys_correct_test(List<string> releases, List<Deployment> deployments, List<Deployment> answer)
        {
            Assert.Equal(answer[0].Id, Deployment.FindProjectDeploys(releases, deployments)[0].Id);
        }

        [Theory]
        [MemberData(nameof(badData))]
        public void FindProjectDeploys_incorrect_test(List<string> releases, List<Deployment> deployments, List<Deployment> answer)
        {
            Assert.NotEqual(answer[0].Id, Deployment.FindProjectDeploys(releases, deployments)[0].Id);
        }
    }
}