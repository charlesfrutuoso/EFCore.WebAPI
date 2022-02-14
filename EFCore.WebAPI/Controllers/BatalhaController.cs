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

        //criamos a propriedade de contexto
        private readonly HeroiContext _context;
        
        //criamos o construtor recebendo o contexto
        public BatalhaController(HeroiContext context)
        {
            _context = context;
        }        
        
        // GET: api/<BatalhaController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(new Batalha());
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            
        }

        // GET api/<BatalhaController>/5
        [HttpGet("{id}", Name = "GetBatalha")]
        public ActionResult Get(int id)
        {
            return Ok("value");
        }

        [HttpPost]
        public ActionResult Post(Batalha model)
        {
            try
            {
                
                _context.Batalhas.Add(model);
                _context.SaveChanges();

                return Ok("beleza batalha!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Batalha model)
        {
            try
            {
                //atenção pra trabalhar com objetos "trackeados" no projeto (travados)
                
                 if(_context.Batalhas.AsNoTracking().FirstOrDefault(h => h.Id == id) != null)
                {
                    //atualiza no banco
                    _context.Batalhas.Update(model);
                    _context.SaveChanges();

                    return Ok("beleza batalha!");
                }
                return Ok("não encontrado!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // DELETE api/<HeroiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
