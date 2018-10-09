using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualInterpreterV2
{
    public interface IPseudoLanguage
    {
        void Reset();
        void Load(string[] input);
        int GetIp();
        Instruction IncrementIp();
        void p0(int op1, int op2, int op3);
        void p1(int op1, int op2, int op3);
        void p2(int op1, int op2, int op3);
        void p3(int op1, int op2, int op3);
        void p4(int op1, int op2, int op3);
        void p5(int op1, int op2, int op3);
        void p6(int op1, int op2, int op3);
        void p7(int op1, int op2, int op3);
        void p8(int op1, int op2, int op3);
        void p9(int op1, int op2, int op3);
        void n0(int op1, int op2, int op3);
        void n1(int op1, int op2, int op3);
        void n2(int op1, int op2, int op3);
        void n3(int op1, int op2, int op3);
        void n4(int op1, int op2, int op3);
        void n5(int op1, int op2, int op3);
        void n6(int op1, int op2, int op3);
        void n7(int op1, int op2, int op3);
        void n8(int op1, int op2, int op3);
        void n9(int op1, int op2, int op3);
    }
}
