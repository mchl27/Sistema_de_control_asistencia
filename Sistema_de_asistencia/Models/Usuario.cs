using System;
using System.Collections.Generic;

namespace Sistema_de_asistencia.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string? Rol { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Estudiante? Estudiante { get; set; }

    public virtual Profesor? Profesor { get; set; }
}
