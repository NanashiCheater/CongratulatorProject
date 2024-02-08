using CongratulatorProject.Data;
using CongratulatorProject.db;
using CongratulatorProject.Models;
using CongratulatorProject.Services.Interfaces;

namespace CongratulatorProject.Services
{
    public class CongratulatorService : ICongratulatorService               
    {
        private CongratulatorDataContext _congratulatorDataContext;
        public CongratulatorService(CongratulatorDataContext congratulatorDataContext)
        {
            _congratulatorDataContext = congratulatorDataContext;
        }
        public BirthdayPersonModel CreateBirthdayPerson(BirthdayPersonModel model)
        {
            _congratulatorDataContext.CreateBirthdayPerson(model);
            return model;
        }
        public BirthdayPersonModel UpdateBirthdayPerson(BirthdayPersonModel model)
        {
            var modelToUpdate = _congratulatorDataContext.UpdateBirthdayPerson(model);
            return modelToUpdate;
            
        }

        public IEnumerable<BirthdayPersonModel> GetAllBirthdayPersons()
        {
            return _congratulatorDataContext.GetAllBirthdayPersons();
        }
        
        public IEnumerable<BirthdayPersonModel> UpcomingBirthdayPersons(int days)
        {
            return _congratulatorDataContext.UpcomingBirthdayPersons(days);
        }
        public void DeleteBirthdayPersonByID(int id)
        {
            _congratulatorDataContext.DeleteBirthdayPersonByID(id);
        }
        public BirthdayPersonModel GetBirthdayPersonByID(int id)
        {
            string f = "";
            return _congratulatorDataContext.GetBirthdayPersonByID(id);
        }



    }
}
