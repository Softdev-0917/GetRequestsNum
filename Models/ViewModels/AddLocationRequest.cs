using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models.ViewModels
{
    public class AddLocationRequest
    {
        public string LocationName { get; set; }
        public string LocationRoom { get; set; }
    }
}
