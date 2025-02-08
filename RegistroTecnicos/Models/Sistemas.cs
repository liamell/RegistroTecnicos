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
    public string Complejidad { get; set; }
}
