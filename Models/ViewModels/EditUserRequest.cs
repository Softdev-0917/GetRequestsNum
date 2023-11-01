using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyProject.Models.ViewModels
{
    public class EditUserRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Team { get; set; }
        public string Position { get; set; }

        //Display Location
        public IEnumerable<SelectListItem> Locations { get; set; }

        //Collect Location
        public string[] SelectedLocations { get; set; } = Array.Empty<string>();
    }
}
