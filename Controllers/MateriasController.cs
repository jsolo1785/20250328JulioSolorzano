using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasController : ControllerBase
    {

        private readonly rEstudianteContext _dbContext;

        public MateriasController(rEstudianteContext context)
        {
            _dbContext = context;
        }

        [HttpGet("EstudianteMaterias/{codigo}")]

        public async Task<IActionResult> GetMateriasEstudiante(string codigo)
        {
            try
            {



                // Obtén el cliente por ID
                var materiasById = _dbContext.materias
                    .FromSqlRaw("spCRUDMaterias @bandera,@codestudiante,@id,@codigo,@materia,@instructor,@horario,@ubicacion,@usercreate,@userupdate",
                        new SqlParameter("@bandera", "2"),                        
                         new SqlParameter("@codestudiante", codigo),
                          new SqlParameter("@id", "0"),
                         new SqlParameter("@codigo", "0"),
                          new SqlParameter("@materia", "0"),
                          new SqlParameter("@instructor", "0"),                      
                        new SqlParameter("@horario", "0"),
                        new SqlParameter("@ubicacion", "0"),
                        new SqlParameter("@usercreate", "0"),
                        new SqlParameter("@userupdate", "0"))
                    .ToList();

                if (materiasById != null)
                {


                    return Ok(materiasById);
                }
                else
                {

                    return StatusCode(404, $"Estudiante: {codigo} , No Tiene Materias Inscritas");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(400, $"Error al obtener Informacion Estudiante - Materias: {ex.Message}");

            }
        }

    }
}
