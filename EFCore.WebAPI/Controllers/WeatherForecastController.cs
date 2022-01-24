using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //A maneira mais certa de trabalhar é que a controller receba o contexto (HeroiContext)
        public readonly HeroiContext _context;

        public WeatherForecastController(HeroiContext context)
        {
            //criamos o construtor da controller já recebendo o contexto pra ser usado em toda a classe (ver o 2º teste de insert)
            _context = context;   
        }

        //1º) teste de insert do heroi pelo EFCore (usando o construtor vazio do HeroiContext)
        //[HttpGet("{id}")]
        //public ActionResult Get(int id)
        //{
        //    //cria o heroi (quando tem atributo "id" ele entende como UPDATE, e sem o "id", o EFCore entende como Insert automaticamente)
        //    var heroi = new Heroi { Nome = "Batman"};
        //    //cria o contexto da HeroApp
        //    using(var contexto = new HeroiContext())
        //    {
        //        //forma explicita de add o heroi
        //        contexto.Herois.Add(heroi);
        //        contexto.SaveChanges();
        //    }
        //    return Ok();
        //}

        //2º) teste de insert do heroi pelo EFCore (usando o HeroiContext que já foi injetado no construtor da controller)
        [HttpGet("{nameHero}")]
        public ActionResult Get(string nameHero)
        {
            //cria o heroi (quando tem atributo "id" ele entende como UPDATE, e sem o "id", o EFCore entende como Insert automaticamente)
            var heroi = new Heroi { Nome = nameHero };
            
            //forma explicita de add o heroi
            _context.Herois.Add(heroi);
            _context.SaveChanges();
            return Ok();
        }

        //3º) teste de select do heroi pelo EFCore 
        [HttpGet]
        public ActionResult Get()
        {

            //usando padrão "LINQ Methods"
            //var listHeroi = _context.Herois.ToList();

            //usando padrão "LINQ Query"
            var listHeroi = (from heroi in _context.Herois select heroi).ToList();

            return Ok(listHeroi);
        }

        //4º) teste de select com where do heroi pelo EFCore com rota específica
        [HttpGet("filtro/{nome}")]
        public ActionResult GetNome(string nome)
        {

            //usando padrão "LINQ Methods"
            var listHeroi = _context.Herois.Where(x => x.Nome.Contains(nome)).ToList();

            //usando padrão "LINQ Query"
            //var listHeroi = (from heroi in _context.Herois where heroi.Nome.Contains(nome) select heroi).ToList();

            return Ok(listHeroi);
        }

        //5º) teste de update do heroi pelo EFCore 
        [HttpGet("atualizar/{id}/{novonome}")]
        public ActionResult Get(int id, string novonome)
        {
            //pega o herói pelo id recebido no parametro
            var heroi = _context.Herois.Where(x => x.Id == id).FirstOrDefault();

            //altrera nome pelo nome recebido no parametro
            heroi.Nome = novonome;

            _context.SaveChanges();
            return Ok();
        }

        //6º) inserindo vários heróis por AddRange pra testar a exclusão depois
        [HttpGet("addRange")]
        public ActionResult GetAddRange()
        {
            _context.Herois.AddRange(
                new Heroi { Nome = "Capitão América" },
                new Heroi { Nome = "Deadpool" },
                new Heroi { Nome = "Hulk" },
                new Heroi { Nome = "Mulher Maravilha" },
                new Heroi { Nome = "Viúva Negra" },
                new Heroi { Nome = "Pantera Negra" }
            );
            
            _context.SaveChanges();
            return Ok();
        }

        //7º) teste de delete do heroi pelo EFCore 
        [HttpGet("delete/{id}")]
        public ActionResult Get(int id)
        {
            //pega o herói pelo id recebido no parametro
            var heroi = _context.Herois.Where(x => x.Id == id).Single();

            _context.Herois.Remove(heroi);
            _context.SaveChanges();
            return Ok();
        }


    }
}
