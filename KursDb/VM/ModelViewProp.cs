using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursDb.VM
{
   public class ModelViewProp
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public string Sex { get; set; }
        public string Season { get; set; }
        public string UpMat { get; set; }
        public string DownMat { get; set; }
        public int Price { get; set; }
        public int LaborCosts { get; set; }
    }
}
