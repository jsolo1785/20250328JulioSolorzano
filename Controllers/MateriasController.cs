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


        [HttpGet("RegistroAlumnos-Materias")]

        public async Task<IActionResult> GetAllInscripciones()
        {
            try
            {



                // Obtén el cliente por ID
                var estudianteById = _dbContext.materias
                    .FromSqlRaw("spCRUDMaterias @bandera,@codestudiante,@id,@codigo,@materia,@instructor,@horario,@ubicacion,@usercreate,@userupdate",
                        new SqlParameter("@bandera", "1"),
                         new SqlParameter("@codestudiante", "0"),
                         new SqlParameter("@id", "0"),
                          new SqlParameter("@codigo", "0"),
                          new SqlParameter("@materia", "0"),
                        new SqlParameter("@instructor", "0"),
                        new SqlParameter("@@horario", "0"),
                        new SqlParameter("@ubicacion", "0"),
                        new SqlParameter("@usercreate", "0"),
                        new SqlParameter("@userupdate", "0"))
                    .ToList();

                if (estudianteById != null)
                {


                    return Ok(estudianteById);
                }
                else
                {

                    return StatusCode(404, $"No tiene Materias Registradas");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(400, $"Error al obtener Informacion Materias: {ex.Message}");

            }
        }


        [HttpPost("InscribirMateria")]
        public IActionResult InscribirMateria([FromBody] materias materias)
        {
            var connectionString = _dbContext.Database.GetConnectionString();


            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("spCRUDMaterias", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // encabezado pedido
                        cmd.Parameters.Add("@bandera", SqlDbType.Int).Value = 3;
                        cmd.Parameters.Add("@codestudiante", SqlDbType.VarChar).Value = materias.id;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = materias.codigo;
                        cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value = materias.codigo;
                        cmd.Parameters.Add("@materia", SqlDbType.VarChar).Value = materias.materia;
                        cmd.Parameters.Add("@instructor", SqlDbType.VarChar).Value = materias.instructor;
                        cmd.Parameters.Add("@horario", SqlDbType.VarChar).Value = materias.horario;
                        cmd.Parameters.Add("@ubicacion", SqlDbType.VarChar).Value = materias.ubicacion;
                        cmd.Parameters.Add("@usercreate", SqlDbType.Int).Value = materias.usercreate;
                        cmd.Parameters.Add("@userupdate", SqlDbType.Int).Value = materias.userupdate;




                        cmd.ExecuteNonQuery();
                        connection.Close();





                    }



                }

                return StatusCode(200, $"Inscripcion Materia - Estudiante Registrado");

            }
            catch (Exception ex)
            {

                return StatusCode(400, $"Error al procesar Insripción: {ex.Message}");
            }


        }



        [HttpDelete("BorrarInscripcionEstudiante/{codigo}")]

        public async Task<IActionResult> DelInscripcion(string codigo)
        {
            try
            {
                var connectionString = _dbContext.Database.GetConnectionString();


                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("spCRUDMaterias", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                       
                        cmd.Parameters.Add("@bandera", SqlDbType.Int).Value = 5;
                        cmd.Parameters.Add("@codestudiante", SqlDbType.VarChar).Value = codigo;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                        cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@materia", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@instructor", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@horario", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@ubicacion", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@usercreate", SqlDbType.Int).Value = 0;
                        cmd.Parameters.Add("@userupdate", SqlDbType.Int).Value = 0;





                        cmd.ExecuteNonQuery();
                        connection.Close();





                    }



                }



                return StatusCode(200, $"Estudiante Eliminado con Exito");


            }
            catch (Exception ex)
            {

                return StatusCode(400, $"Error al obtener Informacion Estudiante: {ex.Message}");

            }
        }

    }
}
