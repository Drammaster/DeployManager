namespace DeployerDomain
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Project(string id = "", string name = "")
        {
            Id = id;
            Name = name;
        }
    }
}
