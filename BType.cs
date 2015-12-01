using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembler
{
    public class BType : Instruction
    {
        public string Cond { get; set; }
        public string Label { get; set; }
    }
}
