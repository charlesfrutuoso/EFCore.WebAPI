using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Domain
{
    //isso aqui seria uma "tabela de relacionamento" (classe, nesse caso)
    public class HeroiBatalha
    {
        //Os campos de Id devem estar presentes nessa classe
        //e tem que seguir essa nomenclatura "HeroiId" e "BatalhaId"
        public int HeroiId { get; set; }
        public int BatalhaId { get; set; }
        public Heroi Heroi { get; set; }
        public Batalha Batalha { get; set; }
    }
}
