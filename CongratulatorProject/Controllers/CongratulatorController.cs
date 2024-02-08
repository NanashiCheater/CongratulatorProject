using CongratulatorProject.db;
using CongratulatorProject.Models;
using CongratulatorProject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace CongratulatorProject.Controllers
{
    public class ImageDTO
    {
        public string FileName { get; set; }

        public IFormFile Image { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class CongratulatorController : ControllerBase
    {
        private ICongratulatorService _congratulatorService;
        public CongratulatorController(ICongratulatorService congratulatorService)
        {
            _congratulatorService = congratulatorService;
        }

        [HttpPost]
        public async Task<BirthdayPersonModel> CreateBirthdayPerson(BirthdayPersonModel model)
        {
            return _congratulatorService.CreateBirthdayPerson(model);
        }
        [HttpPatch]
        public async Task<BirthdayPersonModel> UpdateBirthdayPerson(BirthdayPersonModel model)
        {
            return _congratulatorService.UpdateBirthdayPerson(model);
        }
        [Route("getall")]
        [HttpGet]
        public async Task<IEnumerable<BirthdayPersonModel>> GetAllBirthdayPersons()
        {
            return _congratulatorService.GetAllBirthdayPersons();
        }
        [Route("getone/{id:int}")]
        [HttpGet]
        public async Task<BirthdayPersonModel> GetBirthdayPersonByID(int id)
        {
            return _congratulatorService.GetBirthdayPersonByID(id);
        }
        [Route("getmany/{days:int}")]
        [HttpGet]
        public async Task<IEnumerable<BirthdayPersonModel>> UpcomingBirthdayPersons(int days)
        {
            return _congratulatorService.UpcomingBirthdayPersons(days);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBirthdayPersonByID(int id)
        {
            _congratulatorService.DeleteBirthdayPersonByID(id);
            return Ok();
        }
    }
}
