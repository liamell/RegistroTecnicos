using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class Ciudades
{
    [Key]
    public int CiudadId { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Solo se permiten letras")]
    public string Nombres { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [ForeignKey("Clientes")]
    public int ClienteId { get; set; }
    public Clientes Clientes { get; set; }


}
