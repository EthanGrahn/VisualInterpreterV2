using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualInterpreterV2
{
    public class ComputerSim
    {
        public static readonly string NEW_LINE = System.Environment.NewLine;

        //==========  SIM SUPPORT ===============

        // --- COMPUTER MEMORY STRUCTURES

        protected static int[] data = new int[1000]; // actual data memory

        protected static int[] LABEL_TABLE = new int[100];   // LABEL TABLE structure
        protected static int[] SYMBOL_TABLE = new int[1000];  // SYMBOL TABLE structure

        protected static Instruction[] instrmem = new Instruction[1000]; // INSTRUCTION MEMORY SPACE

        // --- CARD READER SUPPORT
        protected static int[] card = new int[1000];
        protected static int next_card = 0;

        // --- INTERPRETER SUPPORT
        protected static int ip = 0;

        private static MainForm mainForm;

        //======================================

        public ComputerSim()
        {

        }

        public ComputerSim(MainForm form)
        {
            mainForm = form;
        }

        protected static Instruction ParseInstruction(string input)
        {
            Instruction result = new Instruction();

            result.instr = input.Substring(0, 2);
            result.op1 = int.Parse(input.Substring(2, 3));
            result.op2 = int.Parse(input.Substring(5, 3));
            result.op3 = int.Parse(input.Substring(8, 3));

            return result;
        }

        protected static bool PrintOutput(string message, bool isVerbose)
        {
            if (mainForm == null)
                return false;

            mainForm.PrintOutput(message, isVerbose);
            return true;
        }

        protected static bool AddToDataGrid(int lineNumber, int value)
        {
            if (mainForm == null)
                return false;

            mainForm.AddToDataGrid(lineNumber, value);
            return true;
        }

        protected static bool AddToProgramGrid(string instruction, string op1, string op2, string op3)
        {
            if (mainForm == null)
                return false;

            mainForm.AddToProgramGrid(instruction, op1, op2, op3);
            return true;
        }

        protected static bool AddToInputGrid(string value)
        {
            if (mainForm == null)
                return false;

            mainForm.AddToInputGrid(value);
            return true;
        }
    }
}
