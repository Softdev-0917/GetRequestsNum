using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyProject.Models.Domain;
using MyProject.Models.ViewModels;
using MyProject.Repositories;

namespace MyProject.Controllers
{
    public class AdminUser : Controller
    {
        private readonly ILocationRepository locationRepository;
        private readonly IUserRepository userRepository;

        //Constructor of two parameter and these two parameters dependencies that are injected into the controller.
        public AdminUser(ILocationRepository locationRepository, IUserRepository userRepository)
        {
            this.locationRepository = locationRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //Get location from repository
            var locations = await locationRepository.GetAllAsync();
            var model = new AddUserRequest
            {
                Locations = locations.Select(x => new SelectListItem { Text = x.LocationRoom, Value = x.Id.ToString(), })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserRequest addUserRequest)
        {
                //Map view model to domain model
                var user = new User
                {
                    FirstName = addUserRequest.FirstName,
                    LastName = addUserRequest.LastName,
                    Username = addUserRequest.Username,
                    Team = addUserRequest.Team,
                    Position = addUserRequest.Position,
                };

                //map Locations from selected location
                var selectedLocations = new List<Location>();
                foreach (var selectedLocationId in addUserRequest.SelectedLocations)
                {
                    var selectedLocationIdAsGuid = Guid.Parse(selectedLocationId);
                    var exisitingLocation = await locationRepository.GetAsync(selectedLocationIdAsGuid);

                    if (exisitingLocation != null)
                    {
                        selectedLocations.Add(exisitingLocation);
                    }
                }

                //Mapping location to domain model
                user.Locations = selectedLocations;
                await userRepository.AddAsync(user);
                TempData["successMessage"] = "User Added Successfully";
                return RedirectToAction("List");           
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //Call the repository
            var user = await userRepository.GetAllAsync();

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //Retrieve the result from the repository
            var user = await userRepository.GetAsync(id);
            var locationsDomainModel = await locationRepository.GetAllAsync();

            if (user != null)
            {
                //map the domain model into the view model
                var model = new EditUserRequest
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Team = user.Team,
                    Position = user.Position,
                    Locations = locationsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.LocationRoom,
                        Value = x.Id.ToString()
                    }),
                    SelectedLocations = user.Locations.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);
            }

            //Pass data to view
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserRequest editUserRequest)
        {
            //map view model back to domain model
            var userDomainModel = new User
            {
                Id = editUserRequest.Id,
                FirstName = editUserRequest.FirstName,
                LastName = editUserRequest.LastName,
                Username = editUserRequest.Username,
                Team = editUserRequest.Team,
                Position = editUserRequest.Position,
            };

            //Map location into the domain model
            var selectedLocations = new List<Location>();
            foreach (var selectedlocation in editUserRequest.SelectedLocations)
            {
                if (Guid.TryParse(selectedlocation, out var location))
                {
                    var foundLocation = await locationRepository.GetAsync(location);

                    if (foundLocation != null)
                    {
                        selectedLocations.Add(foundLocation);
                    }
                }
            }

            userDomainModel.Locations = selectedLocations;

            //submit info to repository to update
            var updatedUser = await userRepository.UpdateAsync(userDomainModel);

            if (updatedUser != null)
            {
                //Show success notification
                TempData["successMessage"] = "User Updated Successfully";
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditUserRequest editUserRequest)
        {
            //communicate with repository to delete the database personnel and location
            var deletedUser = await userRepository.DeleteAsync(editUserRequest.Id);

            if (deletedUser != null)
            {
                //Show success notification
                TempData["successMessage"] = "User Delete Successfully";
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editUserRequest?.Id });
        }
    }
}
