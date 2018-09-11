using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace VisualInterpreterV2
{
    static class Program
    {
        public static MainForm mainForm;
        public static bool isRunning = false;
        public static bool isLoaded = false;
        public static char langVersion = 'a';
        public static System.Threading.EventWaitHandle waitHandle = new System.Threading.AutoResetEvent(false);

        private static readonly string NEW_LINE = System.Environment.NewLine;


        //==========  SIM SUPPORT ===============

        struct INSTRUCTION
        {
            public string instr;
            public int op1;
            public int op2;
            public int op3;
        };

        // --- COMPUTER MEMORY STRUCTURES

        static int[] data = new int[1000]; // actual data memory

        static int[] LABEL_TABLE = new int[100];   // LABEL TABLE structure
        static int[] SYMBOL_TABLE = new int[1000];  // SYMBOL TABLE structure

        static INSTRUCTION[] instrmem = new INSTRUCTION[1000]; // INSTRUCTION MEMORY SPACE

        // --- CARD READER SUPPORT
        static int[] card = new int[1000];
        static int next_card = 0;

        // --- INTERPRETER SUPPORT
        private static int ip = 0;
        //======================================

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Set variables to their initialized values
        /// </summary>
        public static void Reset()
        {
            data = new int[1000];
            LABEL_TABLE = new int[100];
            SYMBOL_TABLE = new int[1000];
            instrmem = new INSTRUCTION[1000];
            card = new int[1000];
            next_card = 0;
            ip = 0;
        }

        public static void LoadData(string input)
        {
            mainForm.Reset();
            mainForm.ResetDataGrid();

            int index = 0;
            int scanSector = 0;
            string[] splitInput = Regex.Split(input, @"\s+");
            mainForm.PrintOutput("Reading Input File:" + NEW_LINE, true);
            mainForm.PrintOutput(NEW_LINE + "Initial Data Value --" + NEW_LINE, true);

            if (langVersion == 'a')
            {
                foreach (string s in splitInput)
                {
                    if (Regex.IsMatch(s, @"\s+") || s == String.Empty)
                        continue;

                    if (s == "+9999999999")
                    {
                        if (scanSector == 0)
                            mainForm.PrintOutput(NEW_LINE + "Program Instructions --" + NEW_LINE, true);
                        else if (scanSector == 1)
                            mainForm.PrintOutput(NEW_LINE + "Input Data --" + NEW_LINE, true);

                        index = 0;
                        scanSector++;
                        continue;
                    }

                    if (s.Length != 11)
                    {
                        mainForm.PrintOutput("Invalid input length of " + s.Length + " on line " + index + NEW_LINE,
                            false);
                        mainForm.PrintOutput("Ending parse of input file" + NEW_LINE, false);
                        return;
                    }

                    if (scanSector == 0)
                    {
                        int d = int.Parse(s.Substring(1));
                        data[index] = d;
                        mainForm.AddToDataGrid(index, d);
                    }
                    else if (scanSector == 1)
                    {
                        INSTRUCTION instr = ParseInstruction(s);
                        instrmem[index] = instr;
                        mainForm.AddToProgramGrid(instr.instr, instr.op1.ToString("D3"), instr.op2.ToString("D3"),
                            instr.op3.ToString("D3"));
                    }
                    else if (scanSector == 2)
                    {
                        int d = int.Parse(s.Substring(1));
                        card[index] = d;
                        mainForm.AddToInputGrid(s);
                    }

                    mainForm.PrintOutput(s + NEW_LINE, true);
                    index++;
                }
            }
            else if (langVersion == 'b')
            {
                Stack<string> newInput = new Stack<string>(splitInput);
                while (newInput.Count > 0)
                {
                    string s = newInput.Pop();

                    if (Regex.IsMatch(s, @"\s+") || s == String.Empty)
                        continue;

                    if (s == "+9999999999")
                    {
                        if (scanSector == 0)
                            mainForm.PrintOutput(NEW_LINE + "Program Instructions --" + NEW_LINE, true);
                        else if (scanSector == 1)
                            mainForm.PrintOutput(NEW_LINE + "Input Data --" + NEW_LINE, true);

                        index = 0;
                        scanSector++;
                        continue;
                    }

                    if (s.Length != 11)
                    {
                        mainForm.PrintOutput("Invalid input length of " + s.Length + " on line " + index + NEW_LINE,
                            false);
                        mainForm.PrintOutput("Ending parse of input file" + NEW_LINE, false);
                        return;
                    }

                    if (scanSector == 0)
                    {
                        int c = int.Parse(s.Substring(2, 3)); // op1
                        int n = int.Parse(s.Substring(5, 3)); // op2
                        for (int i = 0; i < n; i++)
                        {
                            string s2 = newInput.Pop();
                            int d = int.Parse(s2.Substring(1));
                            SYMBOL_TABLE[d] = index;
                            data[i] = d;
                            mainForm.AddToDataGrid(index, d);
                        }
                    }
                    else if (scanSector == 1)
                    {
                        INSTRUCTION instr = ParseInstruction(s);
                        instrmem[index] = instr;
                        mainForm.AddToProgramGrid(instr.instr, instr.op1.ToString("D3"), instr.op2.ToString("D3"),
                            instr.op3.ToString("D3"));
                    }
                    else if (scanSector == 2)
                    {
                        int d = int.Parse(s.Substring(1));
                        card[index] = d;
                        mainForm.AddToInputGrid(s);
                    }

                    mainForm.PrintOutput(s + NEW_LINE, true);
                    index++;
                }
            }

            isLoaded = true;
            mainForm.ProgramLoaded();
        }

        public static void BeginProgram(bool isStep)
        {
            if (isRunning && !isStep)
            {
                isRunning = false;
                waitHandle.Set();
            }
            var t = new Thread(() => ExecuteProgram(isStep));
            t.IsBackground = true;
            t.Start();
        }

        private static void ExecuteProgram(bool isStep)
        {
            isRunning = true;
            INSTRUCTION currInstr;
            while (isRunning)
            {
                currInstr = instrmem[ip];
                ip++;
                switch (currInstr.instr)
                {
                    case "+0":
                        p0(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+1":
                        p1(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+2":
                        p2(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+3":
                        p3(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+4":
                        p4(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+5":
                        p5(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+6":
                        p6(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+7":
                        p7(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+8":
                        p8(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+9":
                        p9(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-0":
                        n0(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-1":
                        n1(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-2":
                        n2(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-3":
                        n3(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-4":
                        n4(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-5":
                        n5(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-6":
                        n6(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-7":
                        n7(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-8":
                        n8(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-9":
                        n9(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    default:
                        mainForm.PrintOutput("Missing end of program card (+9000000000)", false);
                        isRunning = false;
                        break;
                }

                if (isStep && isRunning)
                {
                    mainForm.UpdateIP(ip);
                    waitHandle.WaitOne();
                }
            }

            mainForm.UpdateIP(-1);
            isLoaded = false;
            mainForm.SetMenuItem("F6", false);
        }

        static INSTRUCTION ParseInstruction(string input)
        {
            INSTRUCTION result;

            result.instr = input.Substring(0, 2);
            result.op1 = int.Parse(input.Substring(2, 3));
            result.op2 = int.Parse(input.Substring(5, 3));
            result.op3 = int.Parse(input.Substring(8, 3));

            return result;
        }

        #region INSTRUCTION OPERATORS

        /// <summary>
        /// Move
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p0(int op1, int op2, int op3)
        {
            if (langVersion == 'a')
            {
                data[op3] = data[op1];
                mainForm.AddToDataGrid(op3, data[op1]);
                mainForm.PrintOutput("Moving " + data[op1] + " into location " + op3 + NEW_LINE, true);
            }
            else if (langVersion == 'b')
            {
                data[SYMBOL_TABLE[op3]] = data[SYMBOL_TABLE[op1]];
                mainForm.AddToDataGrid(SYMBOL_TABLE[op3], data[SYMBOL_TABLE[op1]]);
                mainForm.PrintOutput("Moving " + data[SYMBOL_TABLE[op1]] + " into location " + SYMBOL_TABLE[op3] + NEW_LINE, true);
            }
        }

        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p1(int op1, int op2, int op3)
        {
            if (langVersion == 'a')
            {
                data[op3] = data[op1] + data[op2];
                mainForm.AddToDataGrid(op3, data[op1] + data[op2]);
                mainForm.PrintOutput("Adding " + data[op1] + " and " + data[op2] + ", storing in location " + op3 + NEW_LINE, true);
            }
            else if (langVersion == 'b')
            {
                data[SYMBOL_TABLE[op3]] = data[SYMBOL_TABLE[op1]] + data[SYMBOL_TABLE[op2]];
                mainForm.AddToDataGrid(SYMBOL_TABLE[op3], data[SYMBOL_TABLE[op1]] + data[SYMBOL_TABLE[op2]]);
                mainForm.PrintOutput("Adding " + data[SYMBOL_TABLE[op1]] + " and " + data[SYMBOL_TABLE[op2]] + 
                                     ", storing in location " + SYMBOL_TABLE[op3] + NEW_LINE, true);

            }
        }

        /// <summary>
        /// Multiplication
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p2(int op1, int op2, int op3)
        {
            if (langVersion == 'a')
            {
                data[op3] = data[op1] * data[op2];
                mainForm.AddToDataGrid(op3, data[op1] * data[op2]);
                mainForm.PrintOutput(
                    "Multiplying " + data[op1] + " by " + data[op2] + ", storing in location " + op3 + NEW_LINE, true);
            }
            else if (langVersion == 'b')
            {
                data[SYMBOL_TABLE[op3]] = data[SYMBOL_TABLE[op1]] * data[SYMBOL_TABLE[op2]];
                mainForm.AddToDataGrid(SYMBOL_TABLE[op3], data[SYMBOL_TABLE[op1]] * data[SYMBOL_TABLE[op2]]);
                mainForm.PrintOutput(
                    "Multiplying " + data[SYMBOL_TABLE[op1]] + " by " + data[SYMBOL_TABLE[op2]] + ", storing in location " + 
                    SYMBOL_TABLE[op3] + NEW_LINE, true);
            }
        }

        /// <summary>
        /// Square
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p3(int op1, int op2, int op3)
        {
            if (langVersion == 'a')
            {
                data[op3] = data[op1] * data[op1];
                mainForm.AddToDataGrid(op3, data[op1] * data[op1]);
                mainForm.PrintOutput("Squaring " + data[op1] + ", storing in location " + op3 + NEW_LINE, true);
            }
            else if (langVersion == 'b')
            {
                data[SYMBOL_TABLE[op3]] = data[SYMBOL_TABLE[op1]] * data[SYMBOL_TABLE[op1]];
                mainForm.AddToDataGrid(SYMBOL_TABLE[op3], data[SYMBOL_TABLE[op1]] * data[SYMBOL_TABLE[op1]]);
                mainForm.PrintOutput("Squaring " + data[SYMBOL_TABLE[op1]] + ", storing in location " + SYMBOL_TABLE[op3] + NEW_LINE, true);

            }
        }

        /// <summary>
        /// If equal to
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p4(int op1, int op2, int op3)
        {
            if (langVersion == 'a')
            {
                mainForm.PrintOutput("Checking if " + data[op1] + " = " + data[op2] + NEW_LINE, true);
                if (data[op1] == data[op2])
                {
                    ip = op3;
                    mainForm.PrintOutput("    result: TRUE (" + data[op1] + " = " + data[op2] + ")" + NEW_LINE, true);
                }
                else
                {
                    mainForm.PrintOutput("    result: FALSE (" + data[op1] + " != " + data[op2] + ")" + NEW_LINE, true);
                }
            }
            else if (langVersion == 'b')
            {
                mainForm.PrintOutput("Checking if " + data[SYMBOL_TABLE[op1]] + " = " + data[SYMBOL_TABLE[op2]] + NEW_LINE, true);
                if (data[SYMBOL_TABLE[op1]] == data[SYMBOL_TABLE[op2]])
                {
                    ip = LABEL_TABLE[op3];
                    mainForm.PrintOutput("    result: TRUE (" + data[SYMBOL_TABLE[op1]] + " = " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
                }
                else
                {
                    mainForm.PrintOutput("    result: FALSE (" + data[SYMBOL_TABLE[op1]] + " != " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
                }
            }
        }

        /// <summary>
        /// If greater than or equal to
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p5(int op1, int op2, int op3)
        {
            if (langVersion == 'a')
            {
                mainForm.PrintOutput("Checking if " + data[op1] + " >= " + data[op2] + NEW_LINE, true);
                if (data[op1] >= data[op2])
                {
                    ip = op3;
                    mainForm.PrintOutput("    result: TRUE (" + data[op1] + " >= " + data[op2] + ")" + NEW_LINE, true);
                }
                else
                {
                    mainForm.PrintOutput("    result: FALSE (" + data[op1] + " < " + data[op2] + ")" + NEW_LINE, true);
                }
            }
            else if (langVersion == 'b')
            {
                mainForm.PrintOutput("Checking if " + data[SYMBOL_TABLE[op1]] + " >= " + data[SYMBOL_TABLE[op2]] + NEW_LINE, true);
                if (data[SYMBOL_TABLE[op1]] >= data[SYMBOL_TABLE[op2]])
                {
                    ip = LABEL_TABLE[op3];
                    mainForm.PrintOutput("    result: TRUE (" + data[SYMBOL_TABLE[op1]] + " >= " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
                }
                else
                {
                    mainForm.PrintOutput("    result: FALSE (" + data[SYMBOL_TABLE[op1]] + " < " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
                }
            }
        }

        /// <summary>
        /// X[Y] -> Z
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p6(int op1, int op2, int op3)
        {
            if (langVersion == 'a')
            {
                data[op3] = data[op1 + data[op2]];
                mainForm.AddToDataGrid(op3, data[op1 + data[op2]]);
                mainForm.PrintOutput(
                    "Inserting value from array location " + op1 + " + " + data[op2] + " into location " + op3 +
                    NEW_LINE, true);
            }
            else if (langVersion == 'b')
            {
                data[SYMBOL_TABLE[op3]] = data[SYMBOL_TABLE[op1] + data[SYMBOL_TABLE[op2]]];
                mainForm.AddToDataGrid(SYMBOL_TABLE[op3], data[SYMBOL_TABLE[op1] + data[SYMBOL_TABLE[op2]]]);
                mainForm.PrintOutput(
                    "Inserting value from array location " + SYMBOL_TABLE[op1] + " + " + data[SYMBOL_TABLE[op2]] + 
                    " into location " + SYMBOL_TABLE[op3] + NEW_LINE, true);
            }
        }

        /// <summary>
        /// Increment and test
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p7(int op1, int op2, int op3)
        {
            if (langVersion == 'a')
            {
                data[op1] = data[op1] + 1;
                mainForm.PrintOutput("Incrementing location " + op1 + NEW_LINE, true);
                mainForm.AddToDataGrid(op1, data[op1]);
                mainForm.PrintOutput("Checking if " + data[op1] + " < " + data[op2] + NEW_LINE, true);
                if (data[op1] < data[op2])
                {
                    ip = op3;
                    mainForm.PrintOutput("    result: TRUE (" + data[op1] + " < " + data[op2] + ")" + NEW_LINE, true);
                }
                else
                {
                    mainForm.PrintOutput("    result: FALSE (" + data[op1] + " >= " + data[op2] + ")" + NEW_LINE, true);
                }
            }
            else if (langVersion == 'b')
            {
                data[SYMBOL_TABLE[op1]] = data[SYMBOL_TABLE[op1]] + 1;
                mainForm.PrintOutput("Incrementing location " + SYMBOL_TABLE[op1] + NEW_LINE, true);
                mainForm.AddToDataGrid(SYMBOL_TABLE[op1], data[SYMBOL_TABLE[op1]]);
                mainForm.PrintOutput("Checking if " + data[SYMBOL_TABLE[op1]] + " < " + data[SYMBOL_TABLE[op2]] + NEW_LINE, true);
                if (data[SYMBOL_TABLE[op1]] < data[SYMBOL_TABLE[op2]])
                {
                    ip = LABEL_TABLE[op3];
                    mainForm.PrintOutput("    result: TRUE (" + data[SYMBOL_TABLE[op1]] + " < " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
                }
                else
                {
                    mainForm.PrintOutput("    result: FALSE (" + data[SYMBOL_TABLE[op1]] + " >= " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
                }

            }
        }

        /// <summary>
        /// Read input
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p8(int op1, int op2, int op3)
        {
            if (langVersion == 'a')
            {
                data[op3] = card[next_card++];
                mainForm.AddToDataGrid(op3, card[next_card - 1]);
                mainForm.PrintOutput("Reading next input card and inserting into location " + op3 + NEW_LINE, true);
            }
            else if (langVersion == 'b')
            {
                data[SYMBOL_TABLE[op3]] = card[next_card++];
                mainForm.AddToDataGrid(SYMBOL_TABLE[op3], card[next_card - 1]);
                mainForm.PrintOutput("Reading next input card and inserting into location " + SYMBOL_TABLE[op3] + NEW_LINE, true);
            }
        }

        /// <summary>
        /// Stop program
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p9(int op1, int op2, int op3)
        {
            // same command in versions a and b
            mainForm.PrintOutput("Stopping program" + NEW_LINE, true);
            isRunning = false;
        }

        /// <summary>
        /// Unused
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n0(int op1, int op2, int op3)
        {
            // neither version a nor b contains an operator
            mainForm.PrintOutput("Unused operator -0" + NEW_LINE, true);
        }

        /// <summary>
        /// Subtraction
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n1(int op1, int op2, int op3)
        {
            if (langVersion == 'a')
            {
                data[op3] = data[op1] - data[op2];
                mainForm.AddToDataGrid(op3, data[op1] - data[op2]);
                mainForm.PrintOutput(
                    "Subtracting " + data[op2] + " from " + data[op1] + ", storing in location " + op3 + NEW_LINE,
                    true);
            }
            else if (langVersion == 'b')
            {
                data[SYMBOL_TABLE[op3]] = data[SYMBOL_TABLE[op1]] - data[SYMBOL_TABLE[op2]];
                mainForm.AddToDataGrid(SYMBOL_TABLE[op3], data[SYMBOL_TABLE[op1]] - data[SYMBOL_TABLE[op2]]);
                mainForm.PrintOutput(
                    "Subtracting " + data[SYMBOL_TABLE[op2]] + " from " + data[SYMBOL_TABLE[op1]] + ", storing in location " + SYMBOL_TABLE[op3] + NEW_LINE,
                    true);
            }
        }

        /// <summary>
        /// Division
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n2(int op1, int op2, int op3)
        {
            if (langVersion == 'a')
            {
                data[op3] = data[op1] / data[op2];
                mainForm.AddToDataGrid(op3, data[op1] / data[op2]);
                mainForm.PrintOutput(
                    "Dividing " + data[op1] + " by " + data[op2] + ", storing in location " + op3 + NEW_LINE, true);
            }
            else if (langVersion == 'b')
            {
                data[SYMBOL_TABLE[op3]] = data[SYMBOL_TABLE[op1]] / data[SYMBOL_TABLE[op2]];
                mainForm.AddToDataGrid(SYMBOL_TABLE[op3], data[SYMBOL_TABLE[op1]] / data[SYMBOL_TABLE[op2]]);
                mainForm.PrintOutput(
                    "Dividing " + data[SYMBOL_TABLE[op1]] + " by " + data[SYMBOL_TABLE[op2]] + ", storing in location " + SYMBOL_TABLE[op3] + NEW_LINE, true);
            }
        }

        /// <summary>
        /// Square root
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n3(int op1, int op2, int op3)
        {
            // square root not implemented in version a or b
            mainForm.PrintOutput("Attempted use of unimplemented square root" + NEW_LINE, true);
        }

        /// <summary>
        /// If not equal to
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n4(int op1, int op2, int op3)
        {
            if (langVersion == 'a')
            {
                mainForm.PrintOutput("Checking if " + data[op1] + " != " + data[op2] + NEW_LINE, true);
                if (data[op1] != data[op2])
                {
                    ip = op3;
                    mainForm.PrintOutput("    result: TRUE (" + data[op1] + " != " + data[op2] + ")" + NEW_LINE, true);
                }
                else
                {
                    mainForm.PrintOutput("    result: FALSE (" + data[op1] + " = " + data[op2] + ")" + NEW_LINE, true);
                }
            }
            else if (langVersion == 'b')
            {
                mainForm.PrintOutput("Checking if " + data[SYMBOL_TABLE[op1]] + " != " + data[SYMBOL_TABLE[op2]] + NEW_LINE, true);
                if (data[SYMBOL_TABLE[op1]] != data[SYMBOL_TABLE[op2]])
                {
                    ip = LABEL_TABLE[op3];
                    mainForm.PrintOutput("    result: TRUE (" + data[SYMBOL_TABLE[op1]] + " != " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
                }
                else
                {
                    mainForm.PrintOutput("    result: FALSE (" + data[SYMBOL_TABLE[op1]] + " = " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
                }
            }
        }

        /// <summary>
        /// If less than
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n5(int op1, int op2, int op3)
        {
            if (langVersion == 'a')
            {
                mainForm.PrintOutput("Checking if " + data[op1] + " < " + data[op2] + NEW_LINE, true);
                if (data[op1] < data[op2])
                {
                    ip = op3;
                    mainForm.PrintOutput("    result: TRUE (" + data[op1] + " < " + data[op2] + ")" + NEW_LINE, true);
                }
                else
                {
                    mainForm.PrintOutput("    result: FALSE (" + data[op1] + " >= " + data[op2] + ")" + NEW_LINE, true);
                }
            }
            else if (langVersion == 'b')
            {
                mainForm.PrintOutput("Checking if " + data[SYMBOL_TABLE[op1]] + " < " + data[SYMBOL_TABLE[op2]] + NEW_LINE, true);
                if (data[SYMBOL_TABLE[op1]] < data[SYMBOL_TABLE[op2]])
                {
                    ip = LABEL_TABLE[op3];
                    mainForm.PrintOutput("    result: TRUE (" + data[SYMBOL_TABLE[op1]] + " < " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
                }
                else
                {
                    mainForm.PrintOutput("    result: FALSE (" + data[SYMBOL_TABLE[op1]] + " >= " + data[SYMBOL_TABLE[op2]] + ")" + NEW_LINE, true);
                }
            }
        }

        /// <summary>
        /// X -> Y[Z]
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n6(int op1, int op2, int op3)
        {
            if (langVersion == 'a')
            {
                data[op2 + data[op3]] = data[op1];
                mainForm.AddToDataGrid(op2 + data[op3], data[op1]);
                mainForm.PrintOutput(
                    "Inserting value from location " + op3 + " into array location " + op1 + " + " + data[op2] +
                    NEW_LINE, true);
            }
            else if (langVersion == 'b')
            {
                data[SYMBOL_TABLE[op2] + data[SYMBOL_TABLE[op3]]] = data[SYMBOL_TABLE[op1]];
                mainForm.AddToDataGrid(SYMBOL_TABLE[op2] + data[SYMBOL_TABLE[op3]], data[SYMBOL_TABLE[op1]]);
                mainForm.PrintOutput(
                    "Inserting value from location " + SYMBOL_TABLE[op3] + " into array location " + SYMBOL_TABLE[op1] + 
                    " + " + data[SYMBOL_TABLE[op2]] + NEW_LINE, true);
            }
        }

        /// <summary>
        /// Unused
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n7(int op1, int op2, int op3)
        {
            // unused operator in versions a and b
            mainForm.PrintOutput("Unused operator -7" + NEW_LINE, true);
        }

        /// <summary>
        /// Print
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n8(int op1, int op2, int op3)
        {
            if (langVersion == 'a')
            {
                mainForm.PrintOutput("Printing: ", true);
                mainForm.PrintOutput(data[op1].ToString() + NEW_LINE, false);
            }
            else if (langVersion == 'b')
            {
                mainForm.PrintOutput("Printing: ", true);
                mainForm.PrintOutput(data[SYMBOL_TABLE[op1]].ToString() + NEW_LINE, false);
            }
        }

        /// <summary>
        /// Unused
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n9(int op1, int op2, int op3)
        {
            // unused operator in version a and b
            mainForm.PrintOutput("Unused operator -9" + NEW_LINE, true);
        }
        #endregion INSTRUCTION OPERATORS
    }
}
