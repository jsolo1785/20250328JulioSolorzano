
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public partial class estudiantes
{


    [Key]
    public int? id { get; set; }

    public string? codigo { get; set; }

    public string? nombre { get; set; }

    public string? apllidos { get; set; }

    public string? fechaNacimiento { get; set; }

    public int? edad { get; set; }

    public string? correo { get; set; }

    public int? usercreate { get; set; }

    public DateTime? datecreate { get; set; }

    public int? userupdate { get; set; }

    public DateTime? dateupdate { get; set; }
}