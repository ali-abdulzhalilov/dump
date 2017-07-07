using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vm
{
    public partial class Form1 : Form
    {
        VM vm;
        Dictionary<string, int> codes = new Dictionary<string, int>()
        {
            {"ADD",   10},
            {"SUB",   11},
            {"MUL",   12},
            {"DIV",   13},

            {"MOV",   20},
            {"LOAD",  21},
            {"STORE", 22},
            {"SET",   23},

            {"JMP",   30},
            {"CMP",   31},
            {"JOP",   32},
            {"JOM",   33},
            {"JOE",   34},

            {"HALT",  40},
            {"SLEEP", 41},
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            vm = new VM();
        }

        private void run_Click(object sender, EventArgs e)
        {
            string[] text = textBox.Text.Split(' ', '\n');
            int[] code = interpret(text);

            vm.run(-1, code);

            regBox.Text = "";
            int[] regs = vm.getReg();
            for (int i = 0; i < regs.Length; i++)
                regBox.Text += regs[i] + " ";
        }

        private int[] interpret(string[] words)
        {
            int[] res = new int[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                if (codes.ContainsKey(words[i]))
                    res[i] = codes[words[i]];
                else if (int.TryParse(words[i], out res[i]))
                    res[i] = int.Parse(words[i]);
            }

            return res;
        }

    }

    class VM
    {
        int pc; //program counter
        int[] reg = new int[6];
        int[] code;
        int cmp;
        bool ex;

        public VM()
        {
            for (int i = 0; i < this.reg.Length; i++)
                this.reg[i] = 0;
            this.cmp = 0;
        }

        public int[] getReg()
        {
            return reg;
        }

        public void run(int pc, int[] code)
        {
            this.pc = pc;
            this.code = code;
            for (int i = 0; i < this.reg.Length; i++)
                this.reg[i] = 0;
            ex = true;

            while (ex)
            {
                execute(ncode());
            }
        }

        int ncode()
        {
            pc++;
            if (pc >= code.Length) return 40;
            return code[pc];
        }

        void execute(int word)
        {
            int regA, regB, line, num;

            switch (word)
            {
                case 10: //add
                    regA = ncode();
                    regB = ncode();
                    reg[regA] = reg[regA] + reg[regB];
                    break;
                case 11: //sub
                    regA = ncode();
                    regB = ncode();
                    reg[regA] = reg[regA] - reg[regB];
                    break;
                case 12: //mul
                    regA = ncode();
                    regB = ncode();
                    reg[regA] = reg[regA] * reg[regB];
                    break;
                case 13: //div
                    regA = ncode();
                    regB = ncode();
                    reg[regA] = reg[regA] / reg[regB];
                    break;

                case 20: //mov
                    regA = ncode();
                    regB = ncode();
                    reg[regB] = reg[regA];
                    break;
                case 21: //load
                    line = ncode();
                    regA = ncode();
                    reg[regA] = code[line];
                    break;
                case 22: //store
                    regA = ncode();
                    line = ncode();
                    code[line] = reg[regA];
                    break;
                case 23: //set
                    regA = ncode();
                    num = ncode();
                    reg[regA] = num;
                    break;

                case 30: //jmp
                    line = ncode();
                    pc = line - 1;
                    break;
                case 31: //cmp
                    regA = ncode();
                    regB = ncode();
                    if (reg[regA] > reg[regB]) cmp = 1;
                    else if (reg[regA] == reg[regB]) cmp = 0;
                    else cmp = -1;
                    break;
                case 32: //jop
                    line = ncode();
                    if (cmp == 1)
                        pc = line - 1;
                    break;
                case 33: //jom
                    line = ncode();
                    if (cmp == -1)
                        pc = line - 1;
                    break;
                case 34: //joe
                    line = ncode();
                    if (cmp == 0)
                        pc = line - 1;
                    break;

                case 40: //halt
                    ex = false;
                    break;
                case 41: //sleep
                    regA = ncode();
                    System.Threading.Thread.Sleep(reg[regA] * 1000);
                    break;
            }
        }
    }
}
