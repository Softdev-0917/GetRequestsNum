namespace MyProject.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Team { get; set; }
        public string Position { get; set; }
        public ICollection<Location> Locations { get; set; }
    }
}
