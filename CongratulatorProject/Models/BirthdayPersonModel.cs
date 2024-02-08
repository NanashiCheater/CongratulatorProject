using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace CongratulatorProject.Models
{
    public class BirthdayPersonModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Middlename { get; set; }
        public DateTime Birthday { get; set; }
        public String image { get; set; }
        public String imageType { get; set; }
    }
}
