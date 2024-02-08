using CongratulatorProject.db;
using CongratulatorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CongratulatorProject.Services.Interfaces
{
    public interface ICongratulatorService
    {
        BirthdayPersonModel CreateBirthdayPerson(BirthdayPersonModel model);
        BirthdayPersonModel GetBirthdayPersonByID(int id);
        BirthdayPersonModel UpdateBirthdayPerson(BirthdayPersonModel model);
        IEnumerable<BirthdayPersonModel> GetAllBirthdayPersons();
        IEnumerable<BirthdayPersonModel> UpcomingBirthdayPersons(int days);
        void DeleteBirthdayPersonByID(int id);
    }
}
