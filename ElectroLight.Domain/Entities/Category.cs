using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElectroLight.Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        [StringLength(100,MinimumLength =3)]
        public string Name{ get; set; } =string.Empty;

        //public List<Product> products { get; set; }
    }
}
