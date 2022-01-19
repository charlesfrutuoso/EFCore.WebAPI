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
    }
}
