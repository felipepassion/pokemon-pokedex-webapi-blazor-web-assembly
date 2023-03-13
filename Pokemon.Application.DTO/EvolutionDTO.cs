using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Application.DTO
{
    public class EvolutionDTO
    {
        public string Name { get; set; }
        public int? Level { get; set; }
        public string Trigger { get; set; }
        public string SpriteUrl { get; set; }
    }

}
