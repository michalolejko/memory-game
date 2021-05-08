using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace memory_game.Connection.Messages
{
    [Serializable]
    public class Deck
    {
        public string name;
        public List<Card> cards;
        [XmlIgnore] private readonly string deckPaths = @"../../Resources/Decks/";

        public Deck()
        {
            this.cards = new List<Card>();
            this.name = "";
        }
        public Deck[] LoadDecks()
        {
            string[] paths = Directory.GetFiles(deckPaths);
            Deck[] decks = new Deck[paths.Length];
            for (int i = 0; i < paths.Length; i++)
                decks[i] = MySerialization.DeSerializeObject<Deck>(paths[i]);
            return decks;
        }
        public Deck FindDeckByName(string name)
        {
            Deck[] decks = LoadDecks();
            foreach (Deck deck in decks)
                if (deck.name == name)
                    return deck;
            return null;
        }
        public void UploadNewDeck()
        {
            this.name = SetNamePopupWindowForm();
            if (TryAddCardsFromImages(LoadImages()))
                OkOrNotOk_PopupWindow(TrySaveDeck());
            else
                OkOrNotOk_PopupWindow(false);

        }
        private void OkOrNotOk_PopupWindow(bool okOrNotOk)
        {
            Button okButton = new Button() { DialogResult = DialogResult.OK };
            okButton.Text = okOrNotOk ? "Zapisano talie" : "Niepowodzenie";
            Form popupForm = new Form() { AcceptButton = okButton, Size = new Size(200, 100) };
            popupForm.Controls.Add(okButton);
            popupForm.ShowDialog();
        }

        private bool TryAddCardsFromImages(List<Image> images)
        {
            if (images == null) return false;
            int count = 0;
            foreach (Image img in images)
                cards.Add(new Card()
                {
                    Image = img,
                    Id = count++
                });
            if (count > 0)
                return true;
            return false;
        }

        private bool IsNameExists(string name)
        {
            foreach (Deck deck in LoadDecks())
                if (deck.name == name)
                    return true;
            return false;
        }
        private string SetNamePopupWindowForm()
        {
            TextBox textBox = new TextBox()
            {
                Text = "Podaj nazwe talii",
                Location = new Point(10, 10),
                Multiline = false
            };
            Button okButton = new Button()
            {
                Text = "Ok",
                Location = new Point(textBox.Left, textBox.Height + textBox.Top + 10),
                DialogResult = DialogResult.OK
            };
            Form popupForm = new Form()
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                AcceptButton = okButton,
                ClientSize = new Size(200, 100),
                Text = "Memory Game - ustaw nazwe nowej talii",
            };
            popupForm.Controls.Add(textBox);
            popupForm.Controls.Add(okButton);
            if (popupForm.ShowDialog() == DialogResult.OK)
                return textBox.Text;
            else return "";
        }

        private List<Image> LoadImages()
        {
            List<Image> images = new List<Image>();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.png";
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string[] paths = dialog.FileNames;
                for (int i = 0; i < paths.Length; i++)
                    images.Add(Image.FromFile(paths[i]));
            }
            if (dialog.FileNames.Length > 90)
            {
                MessageBox.Show("You can upload up to 90 images :(");
                return null;
            }
            return images;
        }

        public bool TrySaveDeck()
        {
            if (!IsNameExists(this.name))
                return MySerialization.TrySerializeObject(this, deckPaths + name) ? true : false;
            return false;
        }
    }
}
