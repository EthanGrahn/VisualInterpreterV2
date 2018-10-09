using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VisualInterpreterV2
{
    class LanguageB : ComputerSim, IPseudoLanguage
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
            LABEL_TABLE = new int[100];
            SYMBOL_TABLE = new int[1000];
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

            Stack<string> newInput = new Stack<string>(input.Reverse());
            while (newInput.Count > 0)
            {
                string s = newInput.Pop();

                if (Regex.IsMatch(s, @"\s+") || s == String.Empty)
                    continue; // skip empty lines

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

                PrintOutput(s + NEW_LINE, true);
                if (scanSector == 0) // data cards
                {
                    int c = int.Parse(s.Substring(2, 3)); // op1
                    int n = int.Parse(s.Substring(5, 3)); // op2
                    SYMBOL_TABLE[c] = index;
                    string s2 = newInput.Pop();
                    int d = int.Parse(s2.Substring(1));
                    for (int i = 0; i < n; i++)
                    {
                        data[index + i] = d;
                        AddToDataGrid(c + i, d);
                        PrintOutput(s2 + NEW_LINE, true);
                    }

                    index = index + n;
                }
                else if (scanSector == 1) // instruction cards
                {
                    Instruction instr = ParseInstruction(s);
                    if (instr.instr == "-7")
                    {
                        if (LABEL_TABLE[instr.op1] > 0)
                        {
                            PrintOutput("ERROR: Duplicate Label " + instr.op1 + NEW_LINE, true);
                            return;
                        }

                        LABEL_TABLE[instr.op1] = index;
                    }
                    else if ((instr.instr[0] == '+') &&
                             ((instr.instr[1] == '4') || (instr.instr[1] == '5') || (instr.instr[1] == '7')))
                    {
                        if (LABEL_TABLE[instr.op3] < 0)
                            LABEL_TABLE[instr.op3] = -2; // referenced, but not defined
                    }
                    else if ((instr.instr[0] == '-') && ((instr.instr[1] == '4') || (instr.instr[1] == '5')))
                    {
                        if (LABEL_TABLE[instr.op3] < 0)
                            LABEL_TABLE[instr.op3] = -2; // referenced, but not defined
                    }

                    instrmem[index] = instr;
                    AddToProgramGrid(instr.instr, instr.op1.ToString("D3"), instr.op2.ToString("D3"),
                        instr.op3.ToString("D3"));
                    index++;
                }
                else if (scanSector == 2) // input cards
                {
                    int d = int.Parse(s.Substring(1));
                    card[index] = d;
                    AddToInputGrid(s);
                    index++;
                }
            }

            foreach (int i in LABEL_TABLE)
            {
                if (i == -2)
                {
                    PrintOutput("ERROR: Use of undefined label " + i + NEW_LINE, true);
                    return;
                }
            }
        }

        int IPseudoLanguage.GetIp()
        {
            return ip;
        }

        void IPseudoLanguage.p0(int op1, int op2, int op3)
        {
            data[SYMBOL_TABLE[op3]] = data[SYMBOL_TABLE[op1]];
            AddToDataGrid(SYMBOL_TABLE[op3], data[SYMBOL_TABLE[op1]]);
            PrintOutput("Moving " + data[SYMBOL_TABLE[op1]] + " into location " + SYMBOL_TABLE[op3] + NEW_LINE, true);
        }

        void IPseudoLanguage.p1(int op1, int op2, int op3)
        {
            data[SYMBOL_TABLE[op3]] = data[SYMBOL_TABLE[op1]] + data[SYMBOL_TABLE[op2]];
            AddToDataGrid(SYMBOL_TABLE[op3], data[SYMBOL_TABLE[op3]]);
            PrintOutput("Adding " + data[SYMBOL_TABLE[op1]] + " and " + data[SYMBOL_TABLE[op2]] +
                                 ", storing in location " + SYMBOL_TABLE[op3] + NEW_LINE, true);
        }

        void IPseudoLanguage.p2(int op1, int op2, int op3)
        {
            data[SYMBOL_TABLE[op3]] = data[SYMBOL_TABLE[op1]] * data[SYMBOL_TABLE[op2]];
            AddToDataGrid(SYMBOL_TABLE[op3], data[SYMBOL_TABLE[op1]] * data[SYMBOL_TABLE[op2]]);
            PrintOutput(
                "Multiplying " + data[SYMBOL_TABLE[op1]] + " by " + data[SYMBOL_TABLE[op2]] + ", storing in location " +
                SYMBOL_TABLE[op3] + NEW_LINE, true);
        }

        void IPseudoLanguage.p3(int op1, int op2, int op3)
        {
            data[SYMBOL_TABLE[op3]] = data[SYMBOL_TABLE[op1]] * data[SYMBOL_TABLE[op1]];
            AddToDataGrid(SYMBOL_TABLE[op3], data[SYMBOL_TABLE[op1]] * data[SYMBOL_TABLE[op1]]);
            PrintOutput("Squaring " + data[SYMBOL_TABLE[op1]] + ", storing in location " + SYMBOL_TABLE[op3] + NEW_LINE, true);
        }

        void IPseudoLanguage.p4(int op1, int op2, int op3)
        {
            PrintOutput("Checking if " + data[SYMBOL_TABLE[op1]] + " = " + data[SYMBOL_TABLE[op2]] + NEW_LINE, true);
            if (data[SYMBOL_TABLE[op1]] == data[SYMBOL_TABLE[op2]])
            {
                ip = LABEL_TABLE[op3];
                PrintOutput("    result: TRUE (" + data[SYMBOL_TABLE[op1]] + " = " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
            }
            else
            {
                PrintOutput("    result: FALSE (" + data[SYMBOL_TABLE[op1]] + " != " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
            }
        }

        void IPseudoLanguage.p5(int op1, int op2, int op3)
        {
            PrintOutput("Checking if " + data[SYMBOL_TABLE[op1]] + " >= " + data[SYMBOL_TABLE[op2]] + NEW_LINE, true);
            if (data[SYMBOL_TABLE[op1]] >= data[SYMBOL_TABLE[op2]])
            {
                ip = LABEL_TABLE[op3];
                PrintOutput("    result: TRUE (" + data[SYMBOL_TABLE[op1]] + " >= " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
            }
            else
            {
                PrintOutput("    result: FALSE (" + data[SYMBOL_TABLE[op1]] + " < " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
            }
        }

        void IPseudoLanguage.p6(int op1, int op2, int op3)
        {
            data[SYMBOL_TABLE[op3]] = data[SYMBOL_TABLE[op1] + data[SYMBOL_TABLE[op2]]];
            AddToDataGrid(SYMBOL_TABLE[op3], data[SYMBOL_TABLE[op1] + data[SYMBOL_TABLE[op2]]]);
            PrintOutput(
                "Inserting value from array location " + SYMBOL_TABLE[op1] + " + " + data[SYMBOL_TABLE[op2]] +
                " into location " + SYMBOL_TABLE[op3] + NEW_LINE, true);
        }

        void IPseudoLanguage.p7(int op1, int op2, int op3)
        {
            data[SYMBOL_TABLE[op1]] = data[SYMBOL_TABLE[op1]] + 1;
            PrintOutput("Incrementing location " + SYMBOL_TABLE[op1] + NEW_LINE, true);
            AddToDataGrid(SYMBOL_TABLE[op1], data[SYMBOL_TABLE[op1]]);
            PrintOutput("Checking if " + data[SYMBOL_TABLE[op1]] + " < " + data[SYMBOL_TABLE[op2]] + NEW_LINE, true);
            if (data[SYMBOL_TABLE[op1]] < data[SYMBOL_TABLE[op2]])
            {
                ip = LABEL_TABLE[op3];
                PrintOutput("    result: TRUE (" + data[SYMBOL_TABLE[op1]] + " < " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
            }
            else
            {
                PrintOutput("    result: FALSE (" + data[SYMBOL_TABLE[op1]] + " >= " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
            }
        }

        void IPseudoLanguage.p8(int op1, int op2, int op3)
        {
            data[SYMBOL_TABLE[op3]] = card[next_card++];
            AddToDataGrid(SYMBOL_TABLE[op3], card[next_card - 1]);
            PrintOutput("Reading next input card and inserting into location " + SYMBOL_TABLE[op3] + NEW_LINE, true);
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
            data[SYMBOL_TABLE[op3]] = data[SYMBOL_TABLE[op1]] - data[SYMBOL_TABLE[op2]];
            AddToDataGrid(SYMBOL_TABLE[op3], data[SYMBOL_TABLE[op1]] - data[SYMBOL_TABLE[op2]]);
            PrintOutput(
                "Subtracting " + data[SYMBOL_TABLE[op2]] + " from " + data[SYMBOL_TABLE[op1]] + ", storing in location " + SYMBOL_TABLE[op3] + NEW_LINE,
                true);
        }

        void IPseudoLanguage.n2(int op1, int op2, int op3)
        {
            data[SYMBOL_TABLE[op3]] = data[SYMBOL_TABLE[op1]] / data[SYMBOL_TABLE[op2]];
            AddToDataGrid(SYMBOL_TABLE[op3], data[SYMBOL_TABLE[op1]] / data[SYMBOL_TABLE[op2]]);
            PrintOutput(
                "Dividing " + data[SYMBOL_TABLE[op1]] + " by " + data[SYMBOL_TABLE[op2]] + ", storing in location " + SYMBOL_TABLE[op3] + NEW_LINE, true);
        }

        void IPseudoLanguage.n3(int op1, int op2, int op3)
        {
            // square root not implemented in version a or b
            PrintOutput("Attempted use of unimplemented square root" + NEW_LINE, true);
        }

        void IPseudoLanguage.n4(int op1, int op2, int op3)
        {
            PrintOutput("Checking if " + data[SYMBOL_TABLE[op1]] + " != " + data[SYMBOL_TABLE[op2]] + NEW_LINE, true);
            if (data[SYMBOL_TABLE[op1]] != data[SYMBOL_TABLE[op2]])
            {
                ip = LABEL_TABLE[op3];
                PrintOutput("    result: TRUE (" + data[SYMBOL_TABLE[op1]] + " != " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
            }
            else
            {
                PrintOutput("    result: FALSE (" + data[SYMBOL_TABLE[op1]] + " = " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
            }
        }

        void IPseudoLanguage.n5(int op1, int op2, int op3)
        {
            PrintOutput("Checking if " + data[SYMBOL_TABLE[op1]] + " < " + data[SYMBOL_TABLE[op2]] + NEW_LINE, true);
            if (data[SYMBOL_TABLE[op1]] < data[SYMBOL_TABLE[op2]])
            {
                ip = LABEL_TABLE[op3];
                PrintOutput("    result: TRUE (" + data[SYMBOL_TABLE[op1]] + " < " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
            }
            else
            {
                PrintOutput("    result: FALSE (" + data[SYMBOL_TABLE[op1]] + " >= " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
            }
        }

        void IPseudoLanguage.n6(int op1, int op2, int op3)
        {
            data[SYMBOL_TABLE[op2] + data[SYMBOL_TABLE[op3]]] = data[SYMBOL_TABLE[op1]];
            AddToDataGrid(SYMBOL_TABLE[op2] + data[SYMBOL_TABLE[op3]], data[SYMBOL_TABLE[op1]]);
            PrintOutput(
                "Inserting value from location " + SYMBOL_TABLE[op3] + " into array location " + SYMBOL_TABLE[op1] +
                " + " + data[SYMBOL_TABLE[op2]] + NEW_LINE, true);
        }

        void IPseudoLanguage.n7(int op1, int op2, int op3)
        {
            PrintOutput("Label instruction; no operation performed" + NEW_LINE, true);
        }

        void IPseudoLanguage.n8(int op1, int op2, int op3)
        {
            PrintOutput("Printing: ", true);
            PrintOutput(data[SYMBOL_TABLE[op1]].ToString() + NEW_LINE, false);
        }

        void IPseudoLanguage.n9(int op1, int op2, int op3)
        {
            // unused operator in version a and b
            PrintOutput("Unused operator -9" + NEW_LINE, true);
        }
    }
}
