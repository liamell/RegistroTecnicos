using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [RegularExpression(@"^[a-zA-z\s]+$", ErrorMessage = "Solo se permiten letras")]
    public string Nombres { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [RegularExpression(@"^[0-9\s]+$", ErrorMessage = "Solo se permiten numeros")]
    public double SueldoHora { get; set; }
}
