using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashFront.Models.Stages
{
    public abstract class BaseStage
    {
        public String Name { get; set; }
        public Boolean IsLegalSingles { get; set; }
        public Boolean IsLegalDoubles { get; set; }
    }
}
