using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Repo;
using EFCore.Domain;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //decorator q já faz as validações contidas nos objetos/models
    public class BatalhaController : ControllerBase
    {
        //criamos a propriedade de contexto (vamos comentar, pois agora não usaremos mais o contexto 'chumbado' pois ele virá pela injeção da Startup.cs)
        //private readonly HeroiContext _context;

        //criamos o construtor recebendo o contexto (não usaremos mais assim, pois vamos receber a interface do repositório)
        //public BatalhaController(HeroiContext context)
        //{
        //    _context = context;
        //}
        private readonly IEFCoreRepository _repo;
        public BatalhaController(IEFCoreRepository repo)
        {
            _repo = repo;
        }     


        // GET: api/<BatalhaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //foi preciso add uma referencia ao newtonsoft json lá no startup.cs, pra evitar erros de "loop infinito"
                var batalhas = await _repo.GetAllBatalhas();

                return Ok(batalhas);
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            
        }

        // GET api/<BatalhaController>/5
        [HttpGet("{id}", Name = "GetBatalha")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var batalha = await _repo.GetBatalhaById(id, true);

                return Ok(batalha);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpPost]
        //declaração antiga
        //public ActionResult Post(Batalha model)
        
        //agora iremos trabalhar com método async, pois o SaveChangesAsync exige isso
        public async Task<IActionResult> Post(Batalha model)
        {
            try
            {
                //não trabalharemos mais direto com o contexto
                //_context.Batalhas.Add(model);
                //_context.SaveChanges();

                _repo.Add(model);
                if (await _repo.SaveChangeAsync())
                {
                    return Ok("beleza batalha!");
                }
                else
                {
                    return Ok("não salvou....");
                }

                
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Batalha model)
        {
            try
            {
                var batalha = await _repo.GetBatalhaById(id);
                if (batalha != null)
                {
                    _repo.Update(model);

                    if (await _repo.SaveChangeAsync())
                    {
                        return Ok("batalha alterada!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            return BadRequest($"Não alterado!");
        }

        // DELETE api/<HeroiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var batalha = await _repo.GetBatalhaById(id);
                if (batalha != null)
                {
                    _repo.Delete(batalha);

                    if (await _repo.SaveChangeAsync())
                    {
                        return Ok("batalha deletada!");
                    }
                }
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            return BadRequest($"Não deletado!");
        }
    }
}
