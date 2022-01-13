using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Models
{
    public class Heroi
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        //relacionamento de heroi x batalha (listas sempre tem nome no plural)
        public List<HeroiBatalha> HeroisBatalhas { get; set; }

        public List<Arma> Armas { get; set; }

        public IdentidadeSecreta Identidade { get; set; }
    }
}
