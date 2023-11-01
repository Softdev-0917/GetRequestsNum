namespace MyProject.Models.Domain
{
    public class Location
    {
        public Guid Id { get; set; }
        public string LocationName { get; set; }
        public string LocationRoom { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
