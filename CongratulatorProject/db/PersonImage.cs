using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CongratulatorProject.db
{
    public partial class PersonImage
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("personId")]
        public int PersonId { get; set; }
        [Column("contentType")]
        [StringLength(50)]
        [Unicode(false)]
        public string ContentType { get; set; } = null!;
        [Column("fileContent")]
        public string FileContent { get; set; } = null!;

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("PersonsImages")]
        public virtual Person PersonModel { get; set; } = null!;
    }
}
