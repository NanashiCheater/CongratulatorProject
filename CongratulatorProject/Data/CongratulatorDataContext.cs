using CongratulatorProject.db;
using CongratulatorProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace CongratulatorProject.Data
{
    public class CongratulatorDataContext
    {
        private Congratulator congratulatorDB { get; set; }

        public CongratulatorDataContext()
        {
            congratulatorDB = new Congratulator();
        }
        public void CreateBirthdayPerson(BirthdayPersonModel model)
        {
            congratulatorDB.Persons.Add(new Person { 
                Surname=model.Surname,
                Birthday=model.Birthday,
                Name=model.Name,
                Middlename=model.Middlename,
                PersonsImages=new PersonImage[] { new PersonImage {ContentType=model.imageType,FileContent=model.image } }
            });

            congratulatorDB.SaveChanges();
        }
        
        public BirthdayPersonModel UpdateBirthdayPerson(BirthdayPersonModel model)
        {
            Person personToChange = congratulatorDB.Persons.Single(x => x.Id == model.Id);
            PersonImage imageToChange = congratulatorDB.PersonsImages.Single(x => x.PersonId == model.Id);
            personToChange.Surname = model.Surname;
            personToChange.Name = model.Name;
            personToChange.Middlename = model.Middlename;
            personToChange.Birthday = model.Birthday;
            imageToChange.FileContent = model.image;
            imageToChange.ContentType = model.imageType;
            congratulatorDB.SaveChanges();
            return model;

        }
        public IEnumerable<BirthdayPersonModel> GetAllBirthdayPersons()
        {
            List<Person> persons = new List<Person>(congratulatorDB.Persons);
            List<BirthdayPersonModel> modelList = new List<BirthdayPersonModel>();
            foreach (Person person in persons)
            {
                PersonImage personImage = congratulatorDB.PersonsImages.Single(x => x.PersonId == person.Id);
                if (personImage != null)
                {
                    modelList.Add(new BirthdayPersonModel
                    {
                        Id = person.Id,
                        Surname = person.Surname,
                        Name = person.Name,
                        Middlename = person.Middlename,
                        Birthday = person.Birthday,
                        image = personImage.FileContent,
                        imageType = personImage.ContentType
                    });
                }
            }
            return modelList;
        }
        public IEnumerable<BirthdayPersonModel> UpcomingBirthdayPersons(int days)
        {
            DateTime date = DateTime.Now;
            List<Person> persons = new List<Person>(congratulatorDB.Persons.Where(x => ((x.Birthday.DayOfYear - date.DayOfYear) >= 0 && (x.Birthday.DayOfYear - date.DayOfYear) <= days) || ((x.Birthday.Day == date.Day) && (x.Birthday.Month == date.Month))));
            List<BirthdayPersonModel> modelList = new List<BirthdayPersonModel>();
            foreach (Person person in persons)
            {
                PersonImage personImage = congratulatorDB.PersonsImages.Single(x => x.PersonId == person.Id);
                if (personImage != null)
                {
                    modelList.Add(new BirthdayPersonModel
                    {
                        Id = person.Id,
                        Surname = person.Surname,
                        Name = person.Name,
                        Middlename = person.Middlename,
                        Birthday = person.Birthday,
                        image = personImage.FileContent,
                        imageType = personImage.ContentType
                    });
                }
            }
            return modelList;
           
        }
        public void DeleteBirthdayPersonByID(int id)
        {
            Person personToFind = congratulatorDB.Persons.Single(x => x.Id == id);
            PersonImage imageToFind = congratulatorDB.PersonsImages.Single(x => x.PersonId == id);
            congratulatorDB.PersonsImages.Remove(imageToFind);
            congratulatorDB.Persons.Remove(personToFind);
            congratulatorDB.SaveChanges();
        }
        public BirthdayPersonModel GetBirthdayPersonByID(int id)
        {
            Person personToFind = congratulatorDB.Persons.Single(x => x.Id == id);
            PersonImage imageToFind = congratulatorDB.PersonsImages.Single(x => x.PersonId == id);
            if (personToFind != null && imageToFind != null)
            {
                return new BirthdayPersonModel
                {
                    Id = personToFind.Id,
                    Surname = personToFind.Surname,
                    Name = personToFind.Name,
                    Middlename = personToFind.Middlename,
                    Birthday = personToFind.Birthday,
                    image = imageToFind.FileContent,
                    imageType = imageToFind.ContentType
                };
            }
            return null;
        }
    }
}
