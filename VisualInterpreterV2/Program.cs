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
        public static ComputerSim compSim;
        public static bool isRunning = false;
        public static bool isLoaded = false;
        public static char langVersion = 'a';
        public static System.Threading.EventWaitHandle waitHandle = new System.Threading.AutoResetEvent(false);

        private static IPseudoLanguage language;

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
            language.Reset();
        }

        public static void LoadData(string input)
        {
            mainForm.Reset();
            mainForm.ResetDataGrid();

            if (langVersion == 'a')
            {
                language = new LanguageA();
            }
            else
            {
                language = new LanguageB();
            }

            input = Regex.Replace(input, @"%.*", ""); // remove comments
            string[] splitInput = Regex.Split(input, @"\s+"); // split string into array of instructions

            language.Load(splitInput);

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
            Instruction currInstr = new Instruction();
            while (isRunning)
            {
                currInstr = language.IncrementIp();
                switch (currInstr.instr)
                {
                    case "+0":
                        language.p0(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+1":
                        language.p1(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+2":
                        language.p2(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+3":
                        language.p3(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+4":
                        language.p4(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+5":
                        language.p5(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+6":
                        language.p6(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+7":
                        language.p7(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+8":
                        language.p8(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "+9":
                        language.p9(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-0":
                        language.n0(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-1":
                        language.n1(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-2":
                        language.n2(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-3":
                        language.n3(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-4":
                        language.n4(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-5":
                        language.n5(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-6":
                        language.n6(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-7":
                        language.n7(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-8":
                        language.n8(currInstr.op1, currInstr.op2, currInstr.op3);
                        break;
                    case "-9":
                        language.n9(currInstr.op1, currInstr.op2, currInstr.op3);
                        isRunning = false;
                        break;
                    default:
                        mainForm.PrintOutput("Missing end of program card (+9000000000)", false);
                        isRunning = false;
                        break;
                }

                if (isStep && isRunning)
                {
                    mainForm.UpdateIP(language.GetIp());
                    waitHandle.WaitOne();
                }
            }

            mainForm.UpdateIP(-1);
            isLoaded = false;
            mainForm.SetMenuItem("F6", false);
        }
        
    }
}
