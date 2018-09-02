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
        static int ip = 0;
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

        public static void LoadData(string input)
        {
            mainForm.Reset();
            mainForm.ResetDataGrid();

            int index = 0;
            int scanSector = 0;
            string newLine = System.Environment.NewLine;
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
                    mainForm.AddToDataGrid(index, d.ToString());
                }
                else if (scanSector == 1)
                {
                    INSTRUCTION instr = ParseInstruction(s);
                    instrmem[index] = instr;
                    mainForm.AddToProgramGrid(instr.instr, instr.op1, instr.op2, instr.op3);
                }
                else if (scanSector == 2)
                {
                    int d = int.Parse(s.Substring(1));
                    card[index] = d;
                    mainForm.AddToInputGrid(d.ToString());
                }

                mainForm.PrintOutput(s + newLine, true);
                index++;
            }

            
        }

        public static void BeginProgram()
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
                        isRunning = false;
                        break;
                }

            }
            Console.WriteLine("finished");
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

        // ===========  INTER OPERATIONS ============================
        // =========
        static void p0(int op1, int op2, int op3)
        {
            data[op3] = data[op1];
        }
        // =========
        static void p1(int op1, int op2, int op3)
        {
            data[op3] = data[op1] + data[op2];
        }
        // =========
        static void p2(int op1, int op2, int op3)
        {
            data[op3] = data[op1] * data[op2];
        }
        // =========
        static void p3(int op1, int op2, int op3)
        {
            data[op3] = data[op1] * data[op1];
        }
        // =========
        static void p4(int op1, int op2, int op3)
        {
            if (data[op1] == data[op2])
                ip = op3;
        }
        // =========
        static void p5(int op1, int op2, int op3)
        {
            if (data[op1] >= data[op2])
                ip = op3;
        }
        // =========
        static void p6(int op1, int op2, int op3)
        {
            data[op3] = data[op1 + data[op2]];
        }
        // =========
        static void p7(int op1, int op2, int op3)
        {
            data[op1] = data[op1] + 1;
            if (data[op1] < data[op2])
                ip = op3;
        }
        // =========
        static void p8(int op1, int op2, int op3)
        {
            data[op3] = card[next_card++];
        }
        // =========
        static void p9(int op1, int op2, int op3)
        {
            //cout << "stop " << endl;  // prints stop for testing only !!!!!
            isRunning = false;
        }
        // =========
        static void n0(int op1, int op2, int op3)
        {
            //cout << " no op " << endl;
        }
        // =========
        static void n1(int op1, int op2, int op3)
        {
            data[op3] = data[op1] - data[op2];
        }
        // =========
        static void n2(int op1, int op2, int op3)
        {
            data[op3] = data[op1] / data[op2];
        }
        // =========
        static void n3(int op1, int op2, int op3)
        {
            //cout << "square root " << endl;  // do not implement
        }
        // =========
        static void n4(int op1, int op2, int op3)
        {
            if (data[op1] != data[op2])
                ip = op3;
        }
        // =========
        static void n5(int op1, int op2, int op3)
        {
            if (data[op1] < data[op2])
                ip = op3;
        }
        // =========
        static void n6(int op1, int op2, int op3)
        {
            data[op2 + data[op3]] = data[op1];
        }
        // =========
        static void n7(int op1, int op2, int op3)
        {
            //cout << "no op " << endl;
        }
        // =========
        static void n8(int op1, int op2, int op3)
        {
            //cout << data[op1] << endl;
            Console.WriteLine("print: " + data[op1].ToString());
            mainForm.PrintOutput(data[op1].ToString() + System.Environment.NewLine, false);
        }
        // =========
        static void n9(int op1, int op2, int op3)
        {
            //cout << "no op " << endl;
        }
    }
}
