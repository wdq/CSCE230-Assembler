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
        static void Main(string[] args)
        {

            List<string> memoryOutput = new List<string>();

            memoryOutput.Add("0 : 000000;");

            foreach (string line in File.ReadLines("code.s"))
            {
                string currentLine = line;

                currentLine = currentLine.Replace(",", "");

                string[] currentLineParts = currentLine.Split(' ');


                if(currentLineParts[0] == "noop")
                {
                    memoryOutput.Add(memoryOutput.Count + " : 000000; % " + line + " %");
                }

                if(currentLineParts[0] == "add" || currentLineParts[0] == "sub" || currentLineParts[0] == "and" || currentLineParts[0] == "or" || currentLineParts[0] == "xor" || currentLineParts[0] == "cmp" || currentLineParts[0] == "jr") // R-type
                {

                    if(currentLineParts[0] == "add")
                    {
                        string prefix = "000000000100";
                        string RegD = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + RegD + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0] == "sub")
                    {
                        string prefix = "000000000011";
                        string RegD = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + RegD + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0] == "and")
                    {
                        string prefix = "000000000111";
                        string RegD = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + RegD + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0] == "or")
                    {
                        string prefix = "000000000110";
                        string RegD = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + RegD + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0] == "xor")
                    {
                        string prefix = "000000000101";
                        string RegD = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + RegD + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0] == "cmp")
                    {
                        string prefix = "0010000000000000";
                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0] == "jr")
                    {
                        string prefix = "0001000000000000";
                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + RegS + "0000", 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count + " : " + instruction + "; % " + line + " %");
                    }


                }










                if(currentLineParts[0] == "addi" || currentLineParts[0] == "lw" || currentLineParts[0] == "sw") // D-type
                {
                    if (currentLineParts[0] == "addi")
                    {
                        string prefix = "011000000";
                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string Immediate = Convert.ToString(Convert.ToInt32(currentLineParts[3].Replace("#", ""), 10), 2).PadLeft(7, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + Immediate + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0] == "lw")
                    {
                        string prefix = "010000000";
                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Split('(', ')')[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string Immediate = Convert.ToString(Convert.ToInt32(currentLineParts[2].Split('(')[0], 10), 2).PadLeft(7, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + Immediate + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0] == "sw")
                    {
                        string prefix = "010100000";
                        string RegS = Convert.ToString(Convert.ToInt32(currentLineParts[2].Split('(', ')')[1].Replace("r", ""), 10), 2).PadLeft(4, '0');
                        string Immediate = Convert.ToString(Convert.ToInt32(currentLineParts[2].Split('(')[0], 10), 2).PadLeft(7, '0');
                        string RegT = Convert.ToString(Convert.ToInt32(currentLineParts[1].Replace("r", ""), 10), 2).PadLeft(4, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + Immediate + RegS + RegT, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count + " : " + instruction + "; % " + line + " %");
                    }

                }















                if (currentLineParts[0] == "b" || currentLineParts[0] == "bal") // B-type
                {

                    if (currentLineParts[0] == "b")
                    {
                        string prefix = "10000000";
                        string label = Convert.ToString(Convert.ToInt32(currentLineParts[1], 10), 2).PadLeft(16, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + label, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count + " : " + instruction + "; % " + line + " %");
                    }

                    if (currentLineParts[0] == "bal")
                    {
                        string prefix = "10010000";
                        string label = Convert.ToString(Convert.ToInt32(currentLineParts[1], 10), 2).PadLeft(16, '0');

                        string instruction = Convert.ToString(Convert.ToInt32(prefix + label, 2), 16).PadLeft(6, '0');
                        memoryOutput.Add(memoryOutput.Count + " : " + instruction + "; % " + line + " %");
                    }

                }

                //Console.WriteLine(currentLine);
            }


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
