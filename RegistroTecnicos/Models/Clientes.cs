using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class Clientes
{
    [Key]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public DateOnly FechaIngreso { get; set; }

    [Required(ErrorMessage ="Campo Obligatorio")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage ="Solo se permiten letras")]
    public string Nombres { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public string Direccion { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public string RNC { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public int LimiteCredito { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [ForeignKey("Tecnicos")]
    public int TecnicoId { get; set; }
    public Tecnicos Tecnicos { get; set; }
}
