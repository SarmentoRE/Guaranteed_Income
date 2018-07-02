using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models
{
    public class Concerns
    {
        public static List<IRider> ConcernsToRiders(List<string> concerns)
        {
            List<IRider> riders = new List<IRider>();

            if (concerns.Contains("SMC"))
            {
                
            }
            if (concerns.Contains("LTL"))
            {

            }
            if (concerns.Contains("DIE"))
            {

            }

            return riders;
        }
    }
}