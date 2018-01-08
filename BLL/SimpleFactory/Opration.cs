using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SimpleFactory
{
  public  class Opration
    {
        
        public double NumberA { get; set; }


        public double NumberB { get; set; }

        public virtual double GetResult()
        {
            return 0; 
        }
    }
}
