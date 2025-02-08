using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Sistemas
{
    [Key]
    public int SistemaId { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Solo se permiten letras")]
    public string Descripcion{ get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [Range(1, 100)]
    public double Complejidad { get; set; }
}
