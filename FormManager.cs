using Memory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memory_game
{
    public static class FormManager
    {
        //do usuniecia
        /*private static Dictionary<string, Form> forms = new Dictionary<string, Form>();


        public static void AddForm(string keyInString, Form newForm)
        {
            forms.Add(keyInString, newForm);
        }

        public static Form GetForm(string keyInString)
        {
            return forms[keyInString];
        }*/

        public static Form NewWindow(Form OldWindow, Form NewForm)
        {
            OldWindow.Hide();
            NewForm.ShowDialog();
            OldWindow.Close();
            return NewForm;
        }

        public static Form NewPopupWindow(Form NewForm)
        {
            NewForm.ShowDialog();
            return NewForm;
        }

        public static Form InitGameForm()
        {
            return new WelcomeWindowForm();
        }
    }
}
