using GodInterpreter.Cypher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GodInterpreter
{
    public partial class Interface : Form
    {
        private void Form1_Load (object sender, EventArgs e) { }

        public Interface()
        {
            InitializeComponent();
        }

        private void RunInterpreter(string code)
        {
            try
            {
                Interpreter interpreter = new Interpreter(code);
                interpreter.Execute();
            }
            catch (Exception exception)
            {
                Clipboard.SetText(exception.StackTrace);
                MessageBox.Show(exception.StackTrace);
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunButton_MouseClick(object sender, MouseEventArgs e)
        {
            string code = CodeText.Text;
            RunInterpreter(code);
        }
    }
}
