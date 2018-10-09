using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VisualInterpreterV2
{
    class LanguageA : ComputerSim, IPseudoLanguage
    {
        Instruction IPseudoLanguage.IncrementIp()
        {
            Instruction value = instrmem[ip];
            ip++;
            return value;
        }

        void IPseudoLanguage.Reset()
        {
            data = new int[1000];
            instrmem = new Instruction[1000];
            card = new int[1000];
            next_card = 0;
            ip = 0;
        }

        void IPseudoLanguage.Load(string[] input)
        {
            int index = 0;
            int scanSector = 0;
            PrintOutput("Reading Input File:" + NEW_LINE, true);
            PrintOutput(NEW_LINE + "Initial Data Value --" + NEW_LINE, true);

            foreach (string s in input)
            {
                if (Regex.IsMatch(s, @"\s+") || s == String.Empty)
                    continue;

                if (s == "+9999999999")
                {
                    if (scanSector == 0)
                        PrintOutput(NEW_LINE + "Program Instructions --" + NEW_LINE, true);
                    else if (scanSector == 1)
                        PrintOutput(NEW_LINE + "Input Data --" + NEW_LINE, true);

                    index = 0;
                    scanSector++;
                    continue;
                }

                if (s.Length != 11)
                {
                    PrintOutput("Invalid input length of " + s.Length + " on line " + index + NEW_LINE,
                        false);
                    PrintOutput("Ending parse of input file" + NEW_LINE, false);
                    return;
                }

                if (scanSector == 0)
                {
                    int d = int.Parse(s.Substring(1));
                    data[index] = d;
                    AddToDataGrid(index, d);
                }
                else if (scanSector == 1)
                {
                    Instruction instr = ParseInstruction(s);
                    instrmem[index] = instr;
                    AddToProgramGrid(instr.instr, instr.op1.ToString("D3"), instr.op2.ToString("D3"),
                        instr.op3.ToString("D3"));
                }
                else if (scanSector == 2)
                {
                    int d = int.Parse(s.Substring(1));
                    card[index] = d;
                    AddToInputGrid(s);
                }

                PrintOutput(s + NEW_LINE, true);
                index++;
            }
        }

        int IPseudoLanguage.GetIp()
        {
            return ip;
        }

        void IPseudoLanguage.p0(int op1, int op2, int op3)
        {
            data[op3] = data[op1];
            AddToDataGrid(op3, data[op1]);
            PrintOutput("Moving " + data[op1] + " into location " + op3 + NEW_LINE, true);
        }

        void IPseudoLanguage.p1(int op1, int op2, int op3)
        {
            data[op3] = data[op1] + data[op2];
            AddToDataGrid(op3, data[op3]);
            PrintOutput("Adding " + data[op1] + " and " + data[op2] + ", storing in location " + op3 + NEW_LINE, true);
        }

        void IPseudoLanguage.p2(int op1, int op2, int op3)
        {
            data[op3] = data[op1] * data[op2];
            AddToDataGrid(op3, data[op1] * data[op2]);
            PrintOutput(
                "Multiplying " + data[op1] + " by " + data[op2] + ", storing in location " + op3 + NEW_LINE, true);
        }

        void IPseudoLanguage.p3(int op1, int op2, int op3)
        {
            data[op3] = data[op1] * data[op1];
            AddToDataGrid(op3, data[op1] * data[op1]);
            PrintOutput("Squaring " + data[op1] + ", storing in location " + op3 + NEW_LINE, true);
        }

        void IPseudoLanguage.p4(int op1, int op2, int op3)
        {
            PrintOutput("Checking if " + data[op1] + " = " + data[op2] + NEW_LINE, true);
            if (data[op1] == data[op2])
            {
                ip = op3;
                PrintOutput("    result: TRUE (" + data[op1] + " = " + data[op2] + ")" + NEW_LINE, true);
            }
            else
            {
                PrintOutput("    result: FALSE (" + data[op1] + " != " + data[op2] + ")" + NEW_LINE, true);
            }
        }

        void IPseudoLanguage.p5(int op1, int op2, int op3)
        {
            PrintOutput("Checking if " + data[op1] + " >= " + data[op2] + NEW_LINE, true);
            if (data[op1] >= data[op2])
            {
                ip = op3;
                PrintOutput("    result: TRUE (" + data[op1] + " >= " + data[op2] + ")" + NEW_LINE, true);
            }
            else
            {
                PrintOutput("    result: FALSE (" + data[op1] + " < " + data[op2] + ")" + NEW_LINE, true);
            }
        }

        void IPseudoLanguage.p6(int op1, int op2, int op3)
        {
            data[op3] = data[op1 + data[op2]];
            AddToDataGrid(op3, data[op1 + data[op2]]);
            PrintOutput(
                "Inserting value from array location " + op1 + " + " + data[op2] + " into location " + op3 +
                NEW_LINE, true);
        }

        void IPseudoLanguage.p7(int op1, int op2, int op3)
        {
            data[op1] = data[op1] + 1;
            PrintOutput("Incrementing location " + op1 + NEW_LINE, true);
            AddToDataGrid(op1, data[op1]);
            PrintOutput("Checking if " + data[op1] + " < " + data[op2] + NEW_LINE, true);
            if (data[op1] < data[op2])
            {
                ip = op3;
                PrintOutput("    result: TRUE (" + data[op1] + " < " + data[op2] + ")" + NEW_LINE, true);
            }
            else
            {
                PrintOutput("    result: FALSE (" + data[op1] + " >= " + data[op2] + ")" + NEW_LINE, true);
            }
        }

        void IPseudoLanguage.p8(int op1, int op2, int op3)
        {
            data[op3] = card[next_card++];
            AddToDataGrid(op3, card[next_card - 1]);
            PrintOutput("Reading next input card and inserting into location " + op3 + NEW_LINE, true);
        }

        void IPseudoLanguage.p9(int op1, int op2, int op3)
        {
            // same command in versions a and b
            PrintOutput("Stopping program" + NEW_LINE, true);
        }

        void IPseudoLanguage.n0(int op1, int op2, int op3)
        {
            // neither version a nor b contains an operator
            PrintOutput("Unused operator -0" + NEW_LINE, true);
        }

        void IPseudoLanguage.n1(int op1, int op2, int op3)
        {
            data[op3] = data[op1] - data[op2];
            AddToDataGrid(op3, data[op1] - data[op2]);
            PrintOutput(
                "Subtracting " + data[op2] + " from " + data[op1] + ", storing in location " + op3 + NEW_LINE,
                true);
        }

        void IPseudoLanguage.n2(int op1, int op2, int op3)
        {
            data[op3] = data[op1] / data[op2];
            AddToDataGrid(op3, data[op1] / data[op2]);
            PrintOutput(
                "Dividing " + data[op1] + " by " + data[op2] + ", storing in location " + op3 + NEW_LINE, true);
        }

        void IPseudoLanguage.n3(int op1, int op2, int op3)
        {
            // square root not implemented in version a or b
            PrintOutput("Attempted use of unimplemented square root" + NEW_LINE, true);
        }

        void IPseudoLanguage.n4(int op1, int op2, int op3)
        {
            PrintOutput("Checking if " + data[op1] + " != " + data[op2] + NEW_LINE, true);
            if (data[op1] != data[op2])
            {
                ip = op3;
                PrintOutput("    result: TRUE (" + data[op1] + " != " + data[op2] + ")" + NEW_LINE, true);
            }
            else
            {
                PrintOutput("    result: FALSE (" + data[op1] + " = " + data[op2] + ")" + NEW_LINE, true);
            }
        }

        void IPseudoLanguage.n5(int op1, int op2, int op3)
        {
            PrintOutput("Checking if " + data[op1] + " < " + data[op2] + NEW_LINE, true);
            if (data[op1] < data[op2])
            {
                ip = op3;
                PrintOutput("    result: TRUE (" + data[op1] + " < " + data[op2] + ")" + NEW_LINE, true);
            }
            else
            {
                PrintOutput("    result: FALSE (" + data[op1] + " >= " + data[op2] + ")" + NEW_LINE, true);
            }
        }

        void IPseudoLanguage.n6(int op1, int op2, int op3)
        {
            data[op2 + data[op3]] = data[op1];
            AddToDataGrid(op2 + data[op3], data[op1]);
            PrintOutput(
                "Inserting value from location " + op3 + " into array location " + op1 + " + " + data[op2] +
                NEW_LINE, true);
        }

        void IPseudoLanguage.n7(int op1, int op2, int op3)
        {
            PrintOutput("Unused operator -7" + NEW_LINE, true);
        }

        void IPseudoLanguage.n8(int op1, int op2, int op3)
        {
            PrintOutput("Printing: ", true);
            PrintOutput(data[op1].ToString() + NEW_LINE, false);
        }

        void IPseudoLanguage.n9(int op1, int op2, int op3)
        {
            // unused operator in version a and b
            PrintOutput("Unused operator -9" + NEW_LINE, true);
        }

    }
}
