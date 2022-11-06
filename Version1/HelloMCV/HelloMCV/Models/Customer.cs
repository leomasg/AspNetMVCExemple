using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HelloMCV.Models
{
    public class Customer
    {
        public string Id { get; set; }
        [Required]
        [StringLength(50 , ErrorMessage ="Maximo de 50 caracteres!")]
        [DisplayName("Digite o seu nome completo!")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("(00)00000-0000")]
        public string Telephone { get; set; }

    }
}