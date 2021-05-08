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
        public static bool autoScroll = true;
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
            text += "\n";
            if (rtb.InvokeRequired)
                rtb.Invoke(new AddColouredText(AddColouredTextFunction), rtb, text, colour);
            else
                AddColouredTextFunction(rtb, text, colour);
            if(autoScroll)
                rtb.ScrollToCaret();
        }

        public static void AppendColoredTextWithTime(RichTextBox rtb, string text, Color colour)
        {
            AppendColoredText(rtb, DateTime.Now.ToString("HH:mm:ss") + ": ", Color.Black);
            AppendColoredText(rtb, text, colour);
        }
    }
}
