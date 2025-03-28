
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public partial class materias
{

    [Key]
    public int? id { get; set; }

    public string? codestudiante { get; set; }

    public string? codigo { get; set; }

    public string? materia { get; set; }

    public string? instructor { get; set; }

    public string? horario { get; set; }

    public string? ubicacion { get; set; }

    public int? usercreate { get; set; }

    public DateTime? datecreate { get; set; }

    public int? userupdate { get; set; }

    public DateTime? dateupdate { get; set; }
}