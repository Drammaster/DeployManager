using DeployerDomain;

namespace ReleaseTest
{
    public class ReleaseTests
    {
        private readonly List<Release> releases = new List<Release>
        {
            new Release("Release-1", "Project-1", "1.0.0", "2000-01-01T08:00:00"),
            new Release("Release-2", "Project-2", "1.0.0", "2000-01-01T09:00:00")
        };

        [Theory]
        [InlineData("Project-1", new string[] { "Release-1" })]
        [InlineData("Project-2", new string[] { "Release-2" })]
        public void AllProjectsReleases_function_correct_tests(string project, string[] answer)
        {
            Assert.Equal(answer, Release.AllProjectReleases(project, releases));
        }

        [Theory]
        [InlineData("Project-2", new string[] { "Release-1" })]
        [InlineData("Project-1", new string[] { "Release-2" })]
        [InlineData("Project-3", new string[] { "Release-1" })]
        public void AllProjectsReleases_function_incorrect_tests(string project, string[] answer)
        {
            Assert.NotEqual(answer, Release.AllProjectReleases(project, releases));
        }
    }
}