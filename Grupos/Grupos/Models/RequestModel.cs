using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Grupos.Models
{
    public class RequestModel
    {
       [DisplayName("Origen")]
       [Required(ErrorMessage ="Este campo es obligatorio")]
        public string  source { get; set; }
        [DisplayName("Destino")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string destination { get; set; }
        [DisplayName("Fecha de Salida")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime arrivedDate { get; set; }
        [DisplayName("Fecha de Regreso")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime returnDate { get; set; }
        [DisplayName("Jornada de Salida")]
        public List<string> arrivedDayTime  { get; set; }
        [DisplayName("Jornada de Regreso")]
        public List<string>  returnDayTime { get; set; }
        [DisplayName("Número de adultos")]
        [Required(ErrorMessage = "Este campo es obligatorio")]        
        public int adultsNumber { get; set; }
        [DisplayName("Número de niños")]        
        public int childsNumber { get; set; }
        [DisplayName("Tipo de grupo")]
        public List<string>  groupType { get; set; }
        [DisplayName("Eres agencia?")]
        public bool isAgency { get; set; }
        [DisplayName("Nombre de la agencia")]
        public string  agencyName { get; set; }
        [DisplayName("Obervaciones")]
        public string Observations { get; set; }
        [DisplayName("Nombres y Apellidos")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string fullName { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Correo electrónico incorrecto.")]
        public string Email { get; set; }
        [DisplayName("Número de contacto")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.PhoneNumber, ErrorMessage ="Número de teléfono incorrecto.")]
        [RegularExpression(@"^(\d{10})$",ErrorMessage = "Número de teléfono incorrecto.")]
        public string phoneNumber { get; set; }

        

    }
}