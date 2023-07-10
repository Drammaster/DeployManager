namespace Domain
{
    public class Environment
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Environment(string id = "", string name = "")
        { 
            Id = id;
            Name = name;
        }
    }
}
