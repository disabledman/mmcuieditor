
// https://docs.coronalabs.com/api/library/display/newImage.html

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using System.IO;

namespace MMUIEditor
{
    [Serializable]
    class ImageStruct : ObjectStruct
    {
        public enum BaseDirEnum
        {
            ResourceDirectory,
            DocumentsDirectory,
            TemporaryDirectory,
            CachesDirectory,
        };

        //
        private string m_strParent;
        private string m_strFilename;
        private BaseDirEnum m_numBaseDir;

        //
        public ImageStruct()
        {
            Name = "";
        }

        //
        [Description("Parent Object")]
        public string Parent
        {
            get
            {
                return m_strParent;
            }
            set
            {
                m_strParent = value;
            }
        }
        //
        [Category("Image")]
        [Description("Image Filename")]
        [EditorAttribute(typeof(ImageFileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Filename
        {
            get
            {
                return m_strFilename;
            }
            set
            {
                m_strFilename = value;
                NotifyPropertyChanged("Filename");
            }
        }
        //
        [Category("Image")]
        [Description("Storaged Directory")]
        [EditorAttribute(typeof(System.Drawing.Design.UITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public BaseDirEnum BaseDir
        {
            get
            {
                return m_numBaseDir;
            }
            set
            {
                m_numBaseDir = value;
            }
        }

        //
        public class ImageFileNameEditor : System.Windows.Forms.Design.FileNameEditor
        {
            protected override void InitializeDialog(System.Windows.Forms.OpenFileDialog openFileDialog)
            {
                base.InitializeDialog(openFileDialog);
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "Image Files|*.BMP;*.JPG;*.GIF;*.PNG;*.JPEG|All|*.*";
            }
        }
    }
}
