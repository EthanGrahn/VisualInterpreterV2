using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace VisualInterpreterV2
{
    static class Program
    {
        public static Form1 mainForm;
        public static bool isRunning = false;
        public static System.Threading.EventWaitHandle waitHandle = new System.Threading.AutoResetEvent(false);
        private static string newLine = System.Environment.NewLine;

        public static bool isLoaded = false;

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
            Application.Run(new Form1());
        }

        public static void Reset()
        {
            data = new int[1000];
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
            mainForm.PrintOutput("Reading Input File:" + newLine, true);
            mainForm.PrintOutput(newLine + "Initial Data Value --" + newLine, true);

            foreach (string s in splitInput)
            {
                if (Regex.IsMatch(s, @"\s+") || s == String.Empty)
                    continue;

                if (s == "+9999999999")
                {
                    if (scanSector == 0)
                        mainForm.PrintOutput(newLine + "Program Instructions --" + newLine, true);
                    else if (scanSector == 1)
                        mainForm.PrintOutput(newLine + "Input Data --" + newLine, true);

                    index = 0;
                    scanSector++;
                    continue;
                }
                
                if (s.Length != 11)
                {
                    mainForm.PrintOutput("Invalid input length of " + s.Length + " on line " + index + newLine, false);
                    mainForm.PrintOutput("Ending parse of input file" + newLine, false);
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
                    mainForm.AddToProgramGrid(instr.instr, instr.op1.ToString("D3"), instr.op2.ToString("D3"), instr.op3.ToString("D3"));
                }
                else if (scanSector == 2)
                {
                    int d = int.Parse(s.Substring(1));
                    card[index] = d;
                    mainForm.AddToInputGrid(s);
                }

                mainForm.PrintOutput(s + newLine, true);
                index++;
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
            data[op3] = data[op1];
            mainForm.AddToDataGrid(op3, data[op1]);
            mainForm.PrintOutput("Moving " + data[op1] + " into location " + op3 + newLine, true);
        }

        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p1(int op1, int op2, int op3)
        {
            data[op3] = data[op1] + data[op2];
            mainForm.AddToDataGrid(op3, data[op1] + data[op2]);
            mainForm.PrintOutput("Adding " + data[op1] + " and " + data[op2] + ", storing in location " + op3 + newLine, true);
        }

        /// <summary>
        /// Multiplication
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p2(int op1, int op2, int op3)
        {
            data[op3] = data[op1] * data[op2];
            mainForm.AddToDataGrid(op3, data[op1] * data[op2]);
            mainForm.PrintOutput("Multiplying " + data[op1] + " by " + data[op2] + ", storing in location " + op3 + newLine, true);
        }

        /// <summary>
        /// Square
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p3(int op1, int op2, int op3)
        {
            data[op3] = data[op1] * data[op1];
            mainForm.AddToDataGrid(op3, data[op1] * data[op1]);
            mainForm.PrintOutput("Squaring " + data[op1] + ", storing in location " + op3 + newLine, true);
        }

        /// <summary>
        /// If equal to
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p4(int op1, int op2, int op3)
        {
            mainForm.PrintOutput("Checking if " + data[op1] + " = " + data[op2] + newLine, true);
            if (data[op1] == data[op2])
            {
                ip = op3;
                mainForm.PrintOutput("    result: TRUE (" + data[op1] + " = " + data[op2] + ")" + newLine, true);
            }
            else
            {
                mainForm.PrintOutput("    result: FALSE (" + data[op1] + " != " + data[op2] + ")" + newLine, true);
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
            mainForm.PrintOutput("Checking if " + data[op1] + " >= " + data[op2] + newLine, true);
            if (data[op1] >= data[op2])
            {
                ip = op3;
                mainForm.PrintOutput("    result: TRUE (" + data[op1] + " >= " + data[op2] + ")" + newLine, true);
            }
            else
            {
                mainForm.PrintOutput("    result: FALSE (" + data[op1] + " < " + data[op2] + ")" + newLine, true);
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
            data[op3] = data[op1 + data[op2]];
            mainForm.AddToDataGrid(op3, data[op1 + data[op2]]);
            mainForm.PrintOutput("Inserting value from array location " + op1 + " + " + data[op2] + " into location " + op3 + newLine, true);
        }

        /// <summary>
        /// Increment and test
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p7(int op1, int op2, int op3)
        {
            data[op1] = data[op1] + 1;
            mainForm.PrintOutput("Incrementing location " + op1 + newLine, true);
            mainForm.AddToDataGrid(op1, data[op1]);
            mainForm.PrintOutput("Checking if " + data[op1] + " < " + data[op2] + newLine, true);
            if (data[op1] < data[op2])
            {
                ip = op3;
                mainForm.PrintOutput("    result: TRUE (" + data[op1] + " < " + data[op2] + ")" + newLine, true);
            }
            else
            {
                mainForm.PrintOutput("    result: FALSE (" + data[op1] + " >= " + data[op2] + ")" + newLine, true);
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
            data[op3] = card[next_card++];
            mainForm.AddToDataGrid(op3, card[next_card - 1]);
            mainForm.PrintOutput("Reading next input card and inserting into location " + op3 + newLine, true);
        }

        /// <summary>
        /// Stop program
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void p9(int op1, int op2, int op3)
        {
            mainForm.PrintOutput("Stopping program" + newLine, true);
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
            mainForm.PrintOutput("Unused operator -0" + newLine, true);
        }

        /// <summary>
        /// Subtraction
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n1(int op1, int op2, int op3)
        {
            data[op3] = data[op1] - data[op2];
            mainForm.AddToDataGrid(op3, data[op1] - data[op2]);
            mainForm.PrintOutput("Subtracting " + data[op2] + " from " + data[op1] + ", storing in location " + op3 + newLine, true);
        }

        /// <summary>
        /// Division
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n2(int op1, int op2, int op3)
        {
            data[op3] = data[op1] / data[op2];
            mainForm.AddToDataGrid(op3, data[op1] / data[op2]);
            mainForm.PrintOutput("Dividing " + data[op1] + " by " + data[op2] + ", storing in location " + op3 + newLine, true);
        }

        /// <summary>
        /// Square root
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n3(int op1, int op2, int op3)
        {
            mainForm.PrintOutput("Attempted use of unimplemented square root" + newLine, true);
        }

        /// <summary>
        /// If not equal to
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n4(int op1, int op2, int op3)
        {
            mainForm.PrintOutput("Checking if " + data[op1] + " != " + data[op2] + newLine, true);
            if (data[op1] != data[op2])
            {
                ip = op3;
                mainForm.PrintOutput("    result: TRUE (" + data[op1] + " != " + data[op2] + ")" + newLine, true);
            }
            else
            {
                mainForm.PrintOutput("    result: FALSE (" + data[op1] + " = " + data[op2] + ")" + newLine, true);
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
            mainForm.PrintOutput("Checking if " + data[op1] + " < " + data[op2] + newLine, true);
            if (data[op1] < data[op2])
            {
                ip = op3;
                mainForm.PrintOutput("    result: TRUE (" + data[op1] + " < " + data[op2] + ")" + newLine, true);
            }
            else
            {
                mainForm.PrintOutput("    result: FALSE (" + data[op1] + " >= " + data[op2] + ")" + newLine, true);
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
            data[op2 + data[op3]] = data[op1];
            mainForm.AddToDataGrid(op2 + data[op3], data[op1]);
            mainForm.PrintOutput("Inserting value from location " + op3 + " into array location " + op1 + " + " + data[op2] + newLine, true);
        }

        /// <summary>
        /// Unused
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n7(int op1, int op2, int op3)
        {
            mainForm.PrintOutput("Unused operator -7" + newLine, true);
        }

        /// <summary>
        /// Print
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n8(int op1, int op2, int op3)
        {
            mainForm.PrintOutput("Printing: ", true);
            mainForm.PrintOutput(data[op1].ToString() + newLine, false);
        }

        /// <summary>
        /// Unused
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="op3"></param>
        static void n9(int op1, int op2, int op3)
        {
            mainForm.PrintOutput("Unused operator -9" + newLine, true);
        }
#endregion INSTRUCTION OPERATORS
    }
}
