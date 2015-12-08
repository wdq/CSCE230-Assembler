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
            Cond = conditionToCond(instructionPart.Substring(instructionPart.Length - 2));

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

                if((currentLineParts[0].Substring(0, 3) == "add" && currentLineParts[0].Substring(0, 4) != "addi") || currentLineParts[0].Substring(0, 3) == "sub" || currentLineParts[0].Substring(0, 3) == "and" || currentLineParts[0].Substring(0, 2) == "or" || currentLineParts[0].Substring(0, 3) == "xor" || currentLineParts[0].Substring(0, 3) == "cmp" || currentLineParts[0].Substring(0, 2) == "jr")
                {
                    Instruction instruction = new Instruction();  // R-Type
                    string OpCode = "";
                    string Cond = "";
                    string OpX = "";
                    string S = "";

                    convertOpCodeCondSX(currentLineParts[0], ref OpCode, ref Cond, ref OpX, ref S);

                    instruction.Cond = Cond;
                    instruction.OpCode = OpCode;
                    instruction.Opx = OpX;
                    instruction.S = S;
                    instruction.RegD = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                    instruction.RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                    instruction.RegT = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("r", ""), 10), 2).PadLeft(4, '0');
                    instruction.Address = addressCounter;
                    instruction.Type = "R";

                    addressCounter++;
                    instruction.Complete = true;
                    Instructions.Add(instruction);
                }

                if(currentLineParts[0].Substring(0, 2) == "lw" || currentLineParts[0].Substring(0, 2) == "sw" || currentLineParts[0].Substring(0, 4) == "addi")
                {
                    Instruction instruction = new Instruction(); // D-Type
                    string OpCode = "";
                    string Cond = "";
                    string OpX = ""; // not used
                    string S = "";

                    convertOpCodeCondSX(currentLineParts[0], ref OpCode, ref Cond, ref OpX, ref S);

                    instruction.Cond = Cond;
                    instruction.OpCode = OpCode;
                    instruction.S = S;
                    string Immediate;

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
                    instruction.Immediate = Immediate;
                    instruction.RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                    instruction.RegT = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("r", ""), 10), 2).PadLeft(4, '0');
                    instruction.Address = addressCounter;
                    instruction.Type = "D";

                    addressCounter++;
                    instruction.Complete = true;
                    Instructions.Add(instruction);
                }

                if(currentLineParts[0].Substring(0, 1) == "b" || currentLineParts[0].Substring(0, 3) == "bal")
                {
                    Instruction instruction = new Instruction(); // B-Type
                    string OpCode = "";
                    string Cond = "";
                    string OpX = "";
                    string S = "";

                    convertOpCodeCondSX(currentLineParts[0], ref OpCode, ref Cond, ref OpX, ref S);

                    instruction.Cond = Cond;
                    instruction.OpCode = OpCode;
                    instruction.TextLabel = currentLineParts[1];
                    instruction.Address = addressCounter;
                    instruction.Type = "B";

                    addressCounter++;
                    instruction.Complete = false;
                    Instructions.Add(instruction);
                }


           
            }

            foreach(Instruction currentInstruction in Instructions)
            {
                if (!currentInstruction.Complete)
                {
                    Label label = labels.Find(x => x.Value == currentInstruction.TextLabel);
                    currentInstruction.Label = Convert.ToString(Convert.ToInt32(label.Address.ToString(), 10), 2).PadLeft(16, '0');
                }
            }

            Console.WriteLine("Done");

            Console.ReadKey();

        }
    }
}
