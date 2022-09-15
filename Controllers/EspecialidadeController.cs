using API_Consultas_Agendadas.Interfaces;
using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_Consultas_Agendadas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadeController : ControllerBase
    {
        private readonly IEspecialidadeRepository repositorio;

        public EspecialidadeController(IEspecialidadeRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        [HttpPost]
        public IActionResult Cadastrar(Especialidade especialidade)
        {
            try
            {
                var retorno = repositorio.Insert(especialidade);
                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message
                });

            }
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var retorno = repositorio.GetAll();
                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message
                });

            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                var retorno = repositorio.GetById(id);

                if (retorno is null)
                {
                    return NotFound(new { Message = "Não foi encontrada uma especialidade com esse Id." });
                }

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message
                });

            }
        }

        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Especialidade especialidade)
        {
            try
            {
                var retorno = repositorio.GetById(id);

                if (id != especialidade.Id)
                {
                    return BadRequest(new { Message = "O id indicado deve ser o mesmo id da especialidade" });
                }

                if (retorno is null)
                {
                    return NotFound(new { Message = "Não foi encontrada uma especialidade com esse Id." });
                }

                repositorio.Update(especialidade);

                return Ok(especialidade);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message
                });

            }
        }

        [HttpPatch("{id}")]
        public IActionResult AlterarParcialmente(int id, [FromBody] JsonPatchDocument patch)
        {
            try
            {
                if (patch is null)
                {
                    return BadRequest(new { Message = "Não houve alterações no objeto" });
                }

                var especialidade = repositorio.GetById(id);

                if (especialidade is null)
                {
                    return NotFound(new { Message = "Não foi encontrada uma especialidade com esse Id." });
                }

                repositorio.UpdateParcial(patch, especialidade);

                return Ok(especialidade);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message
                });

            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var busca = repositorio.GetById(id);

                if (busca is null)
                {
                    return NotFound(new { Message = "Não foi encontrada uma especialidade com esse Id." });
                }

                repositorio.Delete(busca);

                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message
                });

            }
        }
    }
}
