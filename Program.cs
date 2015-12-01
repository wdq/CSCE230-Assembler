using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace assembler
{
    class Program
    {

        static string conditionToCond(string condition)
        {
            if(condition == "nv")
            {
                return "0001";
            } else if(condition == "eq")
            {
                return "0010";
            }
            else if (condition == "ne")
            {
                return "0011";
            }
            else if (condition == "vs")
            {
                return "0100";
            }
            else if (condition == "vc")
            {
                return "0101";
            }
            else if (condition == "mi")
            {
                return "0110";
            }
            else if (condition == "pl")
            {
                return "0111";
            }
            else if (condition == "cs")
            {
                return "1000";
            }
            else if (condition == "cc")
            {
                return "1001";
            }
            else if (condition == "hi")
            {
                return "1010";
            }
            else if (condition == "ls")
            {
                return "1011";
            }
            else if (condition == "gt")
            {
                return "1100";
            }
            else if (condition == "lt")
            {
                return "1101";
            }
            else if (condition == "ge")
            {
                return "1110";
            }
            else if (condition == "le")
            {
                return "1111";
            }

            return "0000";
        }

        static void Main(string[] args)
        {

            List<string> memoryOutput = new List<string>();

            memoryOutput.Add("WIDTH=24;");
            memoryOutput.Add("DEPTH=1024;");
            memoryOutput.Add("ADDRESS_RADIX=UNS;");
            memoryOutput.Add("DATA_RADIX=HEX;");
            memoryOutput.Add("CONTENT BEGIN");
            memoryOutput.Add("0 : 000000;");

            foreach (string line in File.ReadLines("code.s"))
            {
                string currentLine = line;

                currentLine = currentLine.Replace(",", "");

                string[] currentLineParts = currentLine.Split(' ');


                if(currentLineParts[0].Contains("noop"))
                {
                    memoryOutput.Add(memoryOutput.Count - 5 + " : 000000; % " + line + " %");
                }

                if((currentLineParts[0].Contains("add") && !currentLineParts[0].Contains("addi")) || currentLineParts[0].Contains("sub") || currentLineParts[0] == "and" || currentLineParts[0].Contains("or") || currentLineParts[0].Contains("xor") || currentLineParts[0].Contains("cmp") || currentLineParts[0].Contains("jr")) // R-type
                {

                    if(currentLineParts[0].Contains("add"))
                    {
                        string opcode = "0000";
                        string cond = "0000";
                        if(currentLineParts[0] != "add")
                        {
                            string condition = currentLineParts[0].Substring(currentLineParts[0].Length - 2);
                            cond = conditionToCond(condition);
                        }
                        string s = "0";
                        string opx = "100";
                        string prefix = opcode + cond + s + opx;

                        string RegD = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + RegD + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count - 5 + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0].Contains("sub"))
                    {
                        string opcode = "0000";
                        string cond = "0000";
                        if (currentLineParts[0] != "sub")
                        {
                            string condition = currentLineParts[0].Substring(currentLineParts[0].Length - 2);
                            cond = conditionToCond(condition);
                        }
                        string s = "0";
                        string opx = "011";
                        string prefix = opcode + cond + s + opx;

                        string RegD = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + RegD + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count - 5 + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0].Contains("and"))
                    {
                        string opcode = "0000";
                        string cond = "0000";
                        if (currentLineParts[0] != "and")
                        {
                            string condition = currentLineParts[0].Substring(currentLineParts[0].Length - 2);
                            cond = conditionToCond(condition);
                        }
                        string s = "0";
                        string opx = "111";
                        string prefix = opcode + cond + s + opx;

                        string RegD = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + RegD + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count - 5 + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0].Contains("or"))
                    {
                        string opcode = "0000";
                        string cond = "0000";
                        if (currentLineParts[0] != "or")
                        {
                            string condition = currentLineParts[0].Substring(currentLineParts[0].Length - 2);
                            cond = conditionToCond(condition);
                        }
                        string s = "0";
                        string opx = "110";
                        string prefix = opcode + cond + s + opx;

                        string RegD = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + RegD + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count - 5 + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0].Contains("xor"))
                    {
                        string opcode = "0000";
                        string cond = "0000";
                        if (currentLineParts[0] != "xor")
                        {
                            string condition = currentLineParts[0].Substring(currentLineParts[0].Length - 2);
                            cond = conditionToCond(condition);
                        }
                        string s = "0";
                        string opx = "101";
                        string prefix = opcode + cond + s + opx;

                        string RegD = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + RegD + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count - 5 + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0].Contains("cmp"))
                    {
                        string opcode = "0010";
                        string cond = "0000";
                        if (currentLineParts[0] != "cmp")
                        {
                            string condition = currentLineParts[0].Substring(currentLineParts[0].Length - 2);
                            cond = conditionToCond(condition);
                        }
                        string s = "1";
                        string opx = "000";
                        string prefix = opcode + cond + s + opx + "0000";

                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count - 5 + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0].Contains("jr"))
                    {
                        string opcode = "0001";
                        string cond = "0000";
                        if (currentLineParts[0] != "or")
                        {
                            string condition = currentLineParts[0].Substring(currentLineParts[0].Length - 2);
                            cond = conditionToCond(condition);
                        }
                        string s = "0";
                        string opx = "000";
                        string prefix = opcode + cond + s + opx + "0000";

                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + RegS + "0000", 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count - 5 + " : " + instruction + "; % " + line + " %");
                    }


                }










                if(currentLineParts[0].Contains("addi") || currentLineParts[0].Contains("lw") || currentLineParts[0].Contains("sw")) // D-type
                {
                    if (currentLineParts[0].Contains("addi"))
                    {
                        string opcode = "0110";
                        string cond = "0000";
                        if (currentLineParts[0] != "addi")
                        {
                            string condition = currentLineParts[0].Substring(currentLineParts[0].Length - 2);
                            cond = conditionToCond(condition);
                        }
                        string s = "0";
                        string prefix = opcode + cond + s;

                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string Immediate; 

                        if (Convert.ToInt32(currentLineParts[3]) >= 0)
                        {
                            Immediate = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("#", ""), 10), 2).PadLeft(7, '0');
                            Immediate = Immediate.Substring(Immediate.Length - 7);
                        } else
                        {
                            Immediate = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("#", ""), 10), 2).PadLeft(7, '1');
                            Immediate = Immediate.Substring(Immediate.Length - 7);
                        }

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + Immediate + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count - 5 + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0].Contains("lw"))
                    {
                        string opcode = "0100";
                        string cond = "0000";
                        if (currentLineParts[0] != "lw")
                        {
                            string condition = currentLineParts[0].Substring(currentLineParts[0].Length - 2);
                            cond = conditionToCond(condition);
                        }
                        string s = "0";
                        string prefix = opcode + cond + s;

                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Split('(', ')')[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string Immediate = Convert.ToString(Convert.ToInt32(currentLineParts[2].Split('(')[0], 10), 2).PadLeft(7, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + Immediate + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count - 5 + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0].Contains("sw"))
                    {
                        string opcode = "0101";
                        string cond = "0000";
                        if (currentLineParts[0] != "sw")
                        {
                            string condition = currentLineParts[0].Substring(currentLineParts[0].Length - 2);
                            cond = conditionToCond(condition);
                        }
                        string s = "0";
                        string prefix = opcode + cond + s;

                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Split('(', ')')[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string Immediate = Convert.ToString(Convert.ToInt32(currentLineParts[2].Split('(')[0], 10), 2).PadLeft(7, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + Immediate + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count - 5 + " : " + instruction + "; % " + line + " %");
                    }

                }















                if (currentLineParts[0].Contains("b")) // B-type
                {

                    if (currentLineParts[0].Contains("b") && !currentLineParts[0].Contains("bal"))
                    {
                        string opcode = "1000";
                        string cond = "0000";
                        if (currentLineParts[0] != "b")
                        {
                            string condition = currentLineParts[0].Substring(currentLineParts[0].Length - 2);
                            cond = conditionToCond(condition);
                        }
                        string prefix = opcode + cond;

                        string label;

                        if (Convert.ToInt32(currentLineParts[1]) >= 0)
                        {
                            label = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("#", ""), 10), 2).PadLeft(16, '0');
                            label = label.Substring(label.Length - 7);
                        }
                        else
                        {
                            label = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("#", ""), 10), 2).PadLeft(16, '1');
                            label = label.Substring(label.Length - 16);
                        }


                        string instruction = Convert.ToString(Convert.ToInt32(prefix + label, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count - 5 + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0].Contains("bal"))
                    {
                        string opcode = "1001";
                        string cond = "0000";
                        if (currentLineParts[0] != "b")
                        {
                            string condition = currentLineParts[0].Substring(currentLineParts[0].Length - 2);
                            cond = conditionToCond(condition);
                        }
                        string prefix = opcode + cond;

                        string label = Convert.ToString(Convert.ToInt32(currentLineParts[1], 10), 2).PadLeft(16, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + label, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count - 5 + " : " + instruction + "; % " + line + " %");
                    }

                }

                //Console.WriteLine(currentLine);
            }


            memoryOutput.Add("END;");

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\output.mif"))
            {
                foreach (var line in memoryOutput)
                {
                    file.WriteLine(line);

                }
            }

            Console.WriteLine("Done");

            Console.ReadKey();

        }
    }
}
