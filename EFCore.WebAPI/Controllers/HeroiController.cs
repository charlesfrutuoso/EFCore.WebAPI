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
    public class HeroiController : ControllerBase
    {

        //criamos a propriedade de contexto
        private readonly HeroiContext _context;
        
        //criamos o construtor recebendo o contexto
        public HeroiController(HeroiContext context)
        {
            _context = context;
        }        
        
        // GET: api/<HeroiController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(new Heroi());
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            
        }

        // GET api/<HeroiController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok("value");
        }

        // POST api/<HeroiController> (comentado apenas pra ficar de exemplo do que já foi feito)
        //[HttpPost]
        //public ActionResult Post()
        //{
        //    try
        //    {
        //        var heroi = new Heroi
        //        {
        //            Nome = "Homem de Ferro",
        //            Armas = new List<Arma>
        //            {
        //                new Arma { Nome = "Mac 3"},
        //                new Arma { Nome = "Mac 5"},
        //            }
        //        };

        //        _context.Herois.Add(heroi);
        //        _context.SaveChanges();

        //        return Ok("beleza!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Erro: {ex}");
        //    }
        //}


        [HttpPost]
        public ActionResult Post(Heroi model)
        {
            try
            {
                
                _context.Herois.Add(model);
                _context.SaveChanges();

                return Ok("beleza!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // PUT api/<HeroiController>/5
        //[HttpPut("{id}")]
        //public ActionResult Put(int id)
        //{
        //    try
        //    {
        //        //o método put faz update do objeto
        //        var heroi = new Heroi
        //        {
        //            Id = id,
        //            Nome = "Ironman",
        //            Armas = new List<Arma>
        //            {
        //                new Arma { Id = 1, Nome = "Mark III"},
        //                new Arma { Id = 2, Nome = "Mark V"},
        //            }
        //        };

        //        //atualiza no banco
        //        _context.Herois.Update(heroi);
        //        _context.SaveChanges();

        //        return Ok("beleza!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Erro: {ex}");
        //    }
        //}

        [HttpPut("{id}")]
        public ActionResult Put(int id, Heroi model)
        {
            try
            {
                //atenção pra trabalhar com objetos "trackeados" no projeto (travados)
                
                //if(_context.Herois.Find(id) != null) //Find é um metodo que "trava" o objeto encontrado, então temos que usar o "AsNoTracking"
                if(_context.Herois.AsNoTracking().FirstOrDefault(h => h.Id == id) != null)
                {
                    //atualiza no banco
                    _context.Herois.Update(model);
                    _context.SaveChanges();

                    return Ok("beleza!");
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
