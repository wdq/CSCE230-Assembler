using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembler
{
    public class Instruction
    {
        public int Address { get; set; }
        public string Label { get; set; }
        public string TextLabel { get; set; }
        public string OpCode { get; set; }
        public string Type { get; set; }
        public bool Complete { get; set; }

        public string Cond { get; set; }
        public string S { get; set; }
        public string Opx { get; set; }
        public string RegD { get; set; }
        public string RegS { get; set; }
        public string RegT { get; set; }

        public string Immediate { get; set; }
        public string Line { get; set; }




    }
}
