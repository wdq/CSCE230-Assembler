﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembler
{
    public class DType : Instruction
    {
        public string Cond { get; set; }
        public string S { get; set; }
        public string Immediate { get; set; }
        public string RegS { get; set; }
        public string RegT { get; set; }
    }
}
