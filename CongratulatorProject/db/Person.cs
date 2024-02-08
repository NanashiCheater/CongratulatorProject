using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CongratulatorProject.db
{
    public partial class Person
    {
        public Person()
        {
            PersonsImages = new HashSet<PersonImage>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("surname")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Surname { get; set; }
        [Column("name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("middlename")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Middlename { get; set; }
        [Column("birthday", TypeName = "datetime")]
        public DateTime Birthday { get; set; }

        [InverseProperty(nameof(PersonImage.PersonModel))]
        public virtual ICollection<PersonImage> PersonsImages { get; set; }
    }
}
