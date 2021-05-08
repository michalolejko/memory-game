using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace memory_game.Connection.Messages
{
    [Serializable]
    public class Card
    {
        public int Id { get; set; }
        [XmlIgnore]
        public Image Image { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [XmlElement("Image")]
        public byte[] ImageSerialized
        {
            // serialize
            get
            { 
                if (Image == null) return null;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }
            // deserialize
            set
            { 
                if (value == null)
                    Image = null;
                else
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(value))
                        Image = new Bitmap(ms);
            }
        }
    }
}
