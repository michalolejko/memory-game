using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memory_game.Forms
{
    public static class FormFunctions
    {
        public delegate void AddColouredText(RichTextBox rtb, string text, Color color);
        public static void AddColouredTextFunction(RichTextBox rtb, string text, Color color)
        {
            var StartIndex = rtb.TextLength;
            rtb.AppendText(text);
            var EndIndex = rtb.TextLength;
            rtb.Select(StartIndex, EndIndex - StartIndex);
            rtb.SelectionColor = color;
        }
        public static void AppendColoredText(RichTextBox rtb, string text, Color colour)
        {
            if (rtb.InvokeRequired)
                rtb.Invoke(new AddColouredText(AddColouredTextFunction), rtb, text, colour);
            else
                AddColouredTextFunction(rtb, text, colour);
        }
    }
}
