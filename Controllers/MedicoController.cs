using API_Consultas_Agendadas.Interfaces;
using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_Consultas_Agendadas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoRepository repositorio;

        public MedicoController(IMedicoRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        [HttpPost]
        public IActionResult Cadastrar(Medico medico)
        {
            try
            {
                var retorno = repositorio.Insert(medico);
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
                    return NotFound(new { Message = "Não foi encontrado um medico com esse Id." });
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
        public IActionResult Alterar(int id, Medico medico)
        {
            try
            {
                var retorno = repositorio.GetById(id);

                if (id != medico.Id)
                {
                    return BadRequest(new { Message = "O id indicado deve ser o mesmo id do medico" });
                }

                if (retorno is null)
                {
                    return NotFound(new { Message = "Não foi encontrado um medico com esse Id." });
                }

                repositorio.Update(medico);

                return Ok(medico);
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

                var medico = repositorio.GetById(id);

                if (medico is null)
                {
                    return NotFound(new { Message = "Não foi encontrado um medico com esse Id." });
                }

                repositorio.UpdateParcial(patch, medico);

                return Ok(medico);
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
                    return NotFound(new { Message = "Não foi encontrado um medico com esse Id." });
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
