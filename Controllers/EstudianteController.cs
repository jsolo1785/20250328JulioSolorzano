using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {



        private readonly rEstudianteContext _dbContext;
        //private readonly IWebHostEnvironment _env;
        //private readonly HttpClient _httpClient;
        //private readonly IHttpClientFactory _httpClientFactory;

        //public EstudianteController(rEstudianteContext context, IWebHostEnvironment env, IHttpClientFactory httpClientFactory)
        public EstudianteController(rEstudianteContext context)
        {
            _dbContext = context;
            //_env = env ?? throw new ArgumentNullException(nameof(env));
            //_httpClient = new HttpClient();
            //_httpClientFactory = httpClientFactory;


        }


        [HttpGet("Estudiante/{codigo}")]

        public async Task<IActionResult> GetEstudiante(string codigo)
        {
            try
            {

             

                // Obtén el cliente por ID
                var estudianteById = _dbContext.estudiantes
                    .FromSqlRaw("spCRUDEstudiante @bandera,@id,@codigo,@nombre,@apllidos,@fechaNacimiento,@edad,@correo,@usercreate,@userupdate",
                        new SqlParameter("@bandera", "2"),
                         new SqlParameter("@id", "0"),
                         new SqlParameter("@codigo", codigo),
                          new SqlParameter("@nombre", "0"),
                          new SqlParameter("@apllidos", "0"),
                        new SqlParameter("@fechaNacimiento", SqlDbType.VarChar) { Value = DateTime.Parse("2000-01-01") },
                        new SqlParameter("@edad", "0"),
                        new SqlParameter("@correo", "0"),
                        new SqlParameter("@usercreate", "0"),
                        new SqlParameter("@userupdate", "0"))
                    .AsEnumerable()
                    .FirstOrDefault();

                if (estudianteById != null)
                {


                    return Ok(estudianteById);  
                }
                else
                {
                   
                    return StatusCode(404, $"Estudiante: {codigo} , No esta Activo, Esta Bloqueado o No Existe");
                }
            }
            catch (Exception ex)
            {
             
                return StatusCode(400, $"Error al obtener Informacion Estudiante: {ex.Message}");
          
            }
        }



        [HttpGet("TodosEstudiante")]

        public async Task<IActionResult> GetAllEstudiante()
        {
            try
            {



                // Obtén el cliente por ID
                var estudianteById = _dbContext.estudiantes
                    .FromSqlRaw("spCRUDEstudiante @bandera,@id,@codigo,@nombre,@apllidos,@fechaNacimiento,@edad,@correo,@usercreate,@userupdate",
                        new SqlParameter("@bandera", "1"),
                         new SqlParameter("@id", "0"),
                         new SqlParameter("@codigo", "0"),
                          new SqlParameter("@nombre", "0"),
                          new SqlParameter("@apllidos", "0"),
                        new SqlParameter("@fechaNacimiento", SqlDbType.VarChar) { Value = DateTime.Parse("2000-01-01") },
                        new SqlParameter("@edad", "0"),
                        new SqlParameter("@correo", "0"),
                        new SqlParameter("@usercreate", "0"),
                        new SqlParameter("@userupdate", "0"))
                    .ToList();

                if (estudianteById != null)
                {


                    return Ok(estudianteById);
                }
                else
                {

                    return StatusCode(404, $"No tiene estudiantes registrados");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(400, $"Error al obtener Informacion Estudiante: {ex.Message}");

            }
        }


        [HttpPost("NuevoEstudiante")]
        public IActionResult NuevoEstudiante( [FromBody] estudiantes estudiante)
        {
            var connectionString = _dbContext.Database.GetConnectionString();


            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("spCRUDEstudiante", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // encabezado pedido
                        cmd.Parameters.Add("@bandera", SqlDbType.Int).Value = 3;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = estudiante.id;
                        cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value = estudiante.codigo;
                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = estudiante.nombre;
                        cmd.Parameters.Add("@apllidos", SqlDbType.VarChar).Value = estudiante.apllidos;
                        cmd.Parameters.Add("@fechaNacimiento", SqlDbType.VarChar).Value = estudiante.fechaNacimiento;
                        cmd.Parameters.Add("@edad", SqlDbType.Int).Value = estudiante.edad;
                        cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = estudiante.correo;
                        cmd.Parameters.Add("@usercreate", SqlDbType.Int).Value = estudiante.usercreate;
                        cmd.Parameters.Add("@userupdate", SqlDbType.Int).Value = estudiante.userupdate;





                        cmd.ExecuteNonQuery();
                        connection.Close();





                    }



                }

                return StatusCode(200, $"Nuevo Estudiante Registrado");

            }
            catch (Exception ex)
            {

                return StatusCode(400, $"Error al procesar el registro: {ex.Message}");
            }


        }



        [HttpDelete("BorrarEstudiante/{codigo}")]     

        public async Task<IActionResult> DelEstudiante(string codigo)
        {
            try
            {
                var connectionString = _dbContext.Database.GetConnectionString();


                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("spCRUDEstudiante", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // encabezado pedido
                        cmd.Parameters.Add("@bandera", SqlDbType.Int).Value = 5;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                        cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value = 0;
                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = 0;
                        cmd.Parameters.Add("@apllidos", SqlDbType.VarChar).Value = 0;
                        cmd.Parameters.Add("@fechaNacimiento", SqlDbType.VarChar).Value = 0;
                        cmd.Parameters.Add("@edad", SqlDbType.Int).Value = 0;
                        cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = 0;
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
