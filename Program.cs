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
            if(condition == "al")
            {
                return "0000";
            }
            else if(condition == "nv")
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

            return "NoCond";
        }

        static void convertOpCodeCondSX(string instructionPart, ref string OpCode, ref string Cond, ref string OpX, ref string S)
        {
            Cond = "NoCond";
            if (instructionPart.Length > 2)
            {
                Cond = conditionToCond(instructionPart.Substring(instructionPart.Length - 2));
            }

            if(Cond == "NoCond")
            {
                Cond = "0000";
            } else
            {
                instructionPart = instructionPart.Remove(instructionPart.Length - 2);
            }

            if(instructionPart == "add")
            {
                OpCode = "0000";
                OpX = "100";
                S = "0";
            }
            else if(instructionPart == "sub")
            {
                OpCode = "0000";
                OpX = "011";
                S = "0";
            }
            else if (instructionPart == "and")
            {
                OpCode = "0000";
                OpX = "111";
                S = "0";
            }
            else if (instructionPart == "or")
            {
                OpCode = "0000";
                OpX = "110";
                S = "0";
            }
            else if (instructionPart == "xor")
            {
                OpCode = "0000";
                OpX = "101";
                S = "0";
            }
            else if (instructionPart == "cmp")
            {
                OpCode = "0010";
                OpX = "000";
                S = "1";
            }
            else if (instructionPart == "jr")
            {
                OpCode = "0001";
                OpX = "000";
                S = "0";
            }
            else if (instructionPart == "lw")
            {
                OpCode = "0100";
                OpX = "NoOpX";
                S = "0";
            }
            else if (instructionPart == "sw")
            {
                OpCode = "0101";
                OpX = "NoOpX";
                S = "0";
            }
            else if (instructionPart == "addi")
            {
                OpCode = "0110";
                OpX = "NoOpX";
                S = "0";
            }
            else if (instructionPart == "b")
            {
                OpCode = "1000";
                OpX = "NoOpX";
                S = "NoS";
            }
            else if (instructionPart == "bal")
            {
                OpCode = "1001";
                OpX = "NoOpX";
                S = "NoS";
            }
        }

        static void Main(string[] args)
        {

            List<string> memoryOutput = new List<string>();
            List<Label> labels = new List<Label>();
            int addressCounter = 1;
            List<Instruction> Instructions = new List<Instruction>();

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


                if(currentLineParts[0].Contains(":"))
                {
                    Label label = new Label();
                    label.Value = currentLineParts[0].Substring(0, currentLineParts[0].IndexOf(":"));
                    label.Address = addressCounter; // might need some logic here based on if the label is supposed to be before or after the current address
                    labels.Add(label);
                    Array.Copy(currentLineParts, 1, currentLineParts, 0, currentLineParts.Length - 1);
                }

                bool RType = false;
                bool DType = false;
                bool BType = false;

                if(currentLineParts[0].Length == 3 || currentLineParts[0].Length == 5)
                {
                    if(currentLineParts[0].Substring(0, 3) == "add" || currentLineParts[0].Substring(0, 3) == "sub" || currentLineParts[0].Substring(0, 3) == "and" || currentLineParts[0].Substring(0, 3) == "xor" || currentLineParts[0].Substring(0, 3) == "cmp")
                    {
                        RType = true;

                    }
                    if (currentLineParts[0].Substring(0, 3) == "bal")
                    {
                        BType = true;
                    }
                }

                if(currentLineParts[0].Length == 2 || currentLineParts[0].Length == 4)
                {
                    if(currentLineParts[0].Substring(0, 2) == "or" || currentLineParts[0].Substring(0, 2) == "jr")
                    {
                        RType = true;
                    }
                    if(currentLineParts[0].Substring(0, 2) == "lw" || currentLineParts[0].Substring(0, 2) == "sw")
                    {
                        DType = true;

                    }
                }

                if(currentLineParts[0].Length == 4 || currentLineParts[0].Length == 6)
                {
                    if(currentLineParts[0].Substring(0, 4) == "addi")
                    {
                        DType = true;
                    }
                }

                if(currentLineParts[0].Length == 1 || currentLineParts[0].Length == 3)
                {
                    if(currentLineParts[0].Substring(0, 1) == "b")
                    {
                        BType = true;
                    }
                }

                if(RType)
                {
                    Instruction instruction = new Instruction();  // R-Type
                    instruction.Line = line;
                    string OpCode = "";
                    string Cond = "";
                    string OpX = "";
                    string S = "";

                    convertOpCodeCondSX(currentLineParts[0], ref OpCode, ref Cond, ref OpX, ref S);

                    instruction.Cond = Cond;
                    instruction.OpCode = OpCode;
                    instruction.Opx = OpX;
                    instruction.S = S;
                    if (instruction.OpCode == "0010" && instruction.Opx == "000") // cmp
                    {
                        instruction.RegD = "0000";
                        instruction.RegS = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        instruction.RegT = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                    } else if(instruction.OpCode == "0001" && instruction.Opx == "000") // jr
                    {
                        instruction.RegD = "0000";
                        instruction.RegS = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        instruction.RegT = "0000";
                    } else
                    {
                        instruction.RegD = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        instruction.RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        instruction.RegT = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("r", ""), 10), 2).PadLeft(4, '0');
                    }

                    instruction.Address = addressCounter;
                    instruction.Type = "R";

                    addressCounter++;
                    instruction.Complete = true;
                    Instructions.Add(instruction);
                }

                if(DType)
                {
                    Instruction instruction = new Instruction(); // D-Type
                    instruction.Line = line;
                    string OpCode = "";
                    string Cond = "";
                    string OpX = ""; // not used
                    string S = "";

                    convertOpCodeCondSX(currentLineParts[0], ref OpCode, ref Cond, ref OpX, ref S);

                    instruction.Cond = Cond;
                    instruction.OpCode = OpCode;
                    instruction.S = S;
                    string Immediate = "0000000";

                    if (instruction.OpCode == "0100" || instruction.OpCode == "0101") // lw + sw
                    {
                        instruction.RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Split('(', ')')[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        Immediate = Convert.ToString(Convert.ToInt32(currentLineParts[2].Split('(')[0], 10), 2).PadLeft(7, '0');
                    }
                    else
                    {
                        if (Convert.ToInt32(currentLineParts[3]) >= 0)
                        {
                            Immediate = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("#", ""), 10), 2).PadLeft(7, '0');
                            Immediate = Immediate.Substring(Immediate.Length - 7);
                        }
                        else
                        {
                            Immediate = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("#", ""), 10), 2).PadLeft(7, '1');
                            Immediate = Immediate.Substring(Immediate.Length - 7);
                        }
                        instruction.RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                    }
                    instruction.Immediate = Immediate;
                    instruction.RegT = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                    instruction.Address = addressCounter;
                    instruction.Type = "D";

                    addressCounter++;
                    instruction.Complete = true;
                    Instructions.Add(instruction);
                }

                if(BType)
                {
                    Instruction instruction = new Instruction(); // B-Type
                    instruction.Line = line;
                    string OpCode = "";
                    string Cond = "";
                    string OpX = "";
                    string S = "";
                    instruction.Complete = false;


                    convertOpCodeCondSX(currentLineParts[0], ref OpCode, ref Cond, ref OpX, ref S);

                    instruction.Cond = Cond;
                    instruction.OpCode = OpCode;
                    instruction.TextLabel = currentLineParts[1];
                    instruction.Address = addressCounter;
                    instruction.Type = "B";

                    int intlabel;
                    if(int.TryParse(instruction.TextLabel, out intlabel))
                    {
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
                        instruction.Label = label;
                        instruction.Complete = true;
                    }

                    if(!instruction.Complete)
                    {
                        Label labelSearch = labels.Find(x => x.Value == instruction.TextLabel);
                        if(labelSearch != null)
                        {
                            int difference = labelSearch.Address - instruction.Address - 1;
                            if(difference >= 0)
                            {
                                instruction.Label = Convert.ToString(Convert.ToInt32(difference.ToString(), 10), 2).PadLeft(16, '0');
                                instruction.Label = instruction.Label.Substring(instruction.Label.Length - 16);
                            } else
                            {
                                instruction.Label = Convert.ToString(Convert.ToInt32(difference.ToString(), 10), 2).PadLeft(16, '1');
                                instruction.Label = instruction.Label.Substring(instruction.Label.Length - 16);
                            }
                            instruction.Complete = true;
                        }
                    }

                    addressCounter++;
                    Instructions.Add(instruction);
                }


           
            }

            foreach(Instruction currentInstruction in Instructions)
            {
                if (!currentInstruction.Complete)
                {
                    Label label = labels.Find(x => x.Value == currentInstruction.TextLabel);
                    int difference = label.Address - currentInstruction.Address - 1;
                    if (difference >= 0)
                    {
                        currentInstruction.Label = Convert.ToString(Convert.ToInt32(difference.ToString(), 10), 2).PadLeft(16, '0');
                        currentInstruction.Label = currentInstruction.Label.Substring(currentInstruction.Label.Length - 16);
                    } else
                    {
                        currentInstruction.Label = Convert.ToString(Convert.ToInt32(difference.ToString(), 10), 2).PadLeft(16, '1');
                        currentInstruction.Label = currentInstruction.Label.Substring(currentInstruction.Label.Length - 16);
                    }

                    currentInstruction.Complete = true;
                }
            }

            foreach(Instruction currentInstruction in Instructions)
            {
                string binaryInstruction = "0";
                if(currentInstruction.Type == "R")
                {
                    binaryInstruction = currentInstruction.OpCode + currentInstruction.Cond + currentInstruction.S + currentInstruction.Opx + currentInstruction.RegD + currentInstruction.RegS + currentInstruction.RegT;
                } else if(currentInstruction.Type == "D")
                {
                    binaryInstruction = currentInstruction.OpCode + currentInstruction.Cond + currentInstruction.S + currentInstruction.Immediate + currentInstruction.RegS + currentInstruction.RegT;
                } else if (currentInstruction.Type == "B")
                {
                    binaryInstruction = currentInstruction.OpCode + currentInstruction.Cond + currentInstruction.Label;
                }
                string hexInstruction = Convert.ToString(Convert.ToInt32(binaryInstruction, 2), 16).PadLeft(6, '0');
                memoryOutput.Add(currentInstruction.Address + " : " + hexInstruction + ";    % " + currentInstruction.Line + " %");
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
