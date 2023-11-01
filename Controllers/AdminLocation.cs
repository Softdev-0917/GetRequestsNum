using Microsoft.AspNetCore.Mvc;
using MyProject.Data;
using MyProject.Models.Domain;
using MyProject.Models.ViewModels;
using MyProject.Repositories;

namespace MyProject.Controllers
{
    public class AdminLocation : Controller
    {
        private readonly ILocationRepository locationRepository;

        public AdminLocation(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLocationRequest addLocationRequest)
        {
            if (ModelState.IsValid)
            {
                // Mapping AddLocationRequest to Location domain model
                var location = new Location
                {
                    LocationName = addLocationRequest.LocationName,
                    LocationRoom = addLocationRequest.LocationRoom,
                };

                await locationRepository.AddAsync(location);
                TempData["successMessage"] = "Location Added Successfully";
                return RedirectToAction("List");
            }

            TempData["errorMessage"] = "Location NOT Added";

            // If there are validation errors, return to the Add view with the model
            return View("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //Use dbContext to read the locations
            var locations = await locationRepository.GetAllAsync();

            return View(locations);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) 
        {
            var location = await locationRepository.GetAsync(id);

            if (location != null)
            {
                var editLocationRequest = new EditLocationRequest
                {
                    Id = location.Id,
                    LocationName = location.LocationName,
                    LocationRoom = location.LocationRoom,
                };
                return View(editLocationRequest);
            }
            return View(null);
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditLocationRequest editLocationRequest)
        {
            var location = new Location
            {
                Id = editLocationRequest.Id,
                LocationName = editLocationRequest.LocationName,
                LocationRoom = editLocationRequest.LocationRoom,
            };
           
            var updatedLocation = await locationRepository.UpdateAsync(location);

            if (updatedLocation != null)
            {
                //Show success notification
                TempData["successMessage"] = "Location Updated Successfully";
                return RedirectToAction("List");
            }
            else
            {
                //Show failure notification
                TempData["errorMessage"] = "Location NOT Updated Successfully";
            }

            return RedirectToAction("Edit", new { id=editLocationRequest.Id});
        }

        public async Task<IActionResult> Delete(EditLocationRequest editLocationRequest)
        {
           var deletedLocation =  await locationRepository.DeleteAsync(editLocationRequest.Id);

            if (deletedLocation != null)
            {
                //Show success notification
                TempData["successMessage"] = "Location Delete Successfully";
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new {id=editLocationRequest.Id});
        }
    }
}
