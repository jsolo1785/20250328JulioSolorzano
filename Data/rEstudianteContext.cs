using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class rEstudianteContext : DbContext
{
    public rEstudianteContext(DbContextOptions<rEstudianteContext> options)
        : base(options)
    {
    }

    public  DbSet<estudiantes> estudiantes { get; set; }

    public  DbSet<materias> materias { get; set; }

}