/*
 * https://msdn.microsoft.com/en-us/library/aa302326.aspx
 * https://msdn.microsoft.com/library/system.componentmodel.itypedescriptorcontext.aspx
 * https://msdn.microsoft.com/en-us/library/system.drawing.design.uitypeeditor.aspx
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Drawing;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Diagnostics;

namespace MMUIEditor
{
    //
    public class ObjectNameConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            TestStruct myKeywordProps = context.Instance as TestStruct;

            if (context.PropertyDescriptor.Name == "TestSelectionCountry")
            {
                List<string> listOfCountries = new List<string>();
                listOfCountries.Add("Country1");
                listOfCountries.Add("Country2");

                return new StandardValuesCollection(listOfCountries);
            }

            List<string> listOfCities = new List<string>();
            if (myKeywordProps.m_MyCountry == "Country1")
            {
                listOfCities.Add("City11");
                listOfCities.Add("City12");
                listOfCities.Add("City13");
            }
            else
            {
                listOfCities.Add("City21");
                listOfCities.Add("City22");
                listOfCities.Add("City23");
            }

            return new StandardValuesCollection(listOfCities);
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

    //
    public class DropdownListEditor : UITypeEditor
    {
        private IWindowsFormsEditorService _editorService;

        //
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        //
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            _editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            
            ListBox lb = new ListBox();
            lb.SelectionMode = SelectionMode.One;
            lb.SelectedValueChanged += OnListBoxSelectedValueChanged;

            lb.Items.Add("1111");
            lb.Items.Add("2222");

            _editorService.DropDownControl(lb);
            if (lb.SelectedItem == null)
                return base.EditValue(context, provider, value); // no selection, no change

            return lb.SelectedItem;
        }

        //
        private void OnListBoxSelectedValueChanged(object sender, EventArgs e)
        {
            _editorService.CloseDropDown();
        }
    }

    //
    public class ImageTriggerEditor : UITypeEditor
    {
        private IWindowsFormsEditorService _editorService;

        //
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        //
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            string text = "";

            TestStruct obj = context.Instance as TestStruct;

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"d:\svn\tools\trunk\CScripterEditorImageFeature\CScripterEditorImageFeature\bin\CScripterEditorImageFeature.exe",
                    Arguments = @"d:\test_img\casino_ag.png,400,52,450,101,471,249,521,299,261,161,380,267",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();

            // Waiting for extern tool closed
            while (!proc.StandardOutput.EndOfStream)
            {
            }

            text = Clipboard.GetText();

            return text;
        }

        //
        //private void OnListBoxSelectedValueChanged(object sender, EventArgs e)
        //{
        //    _editorService.CloseDropDown();
        //}
    }

    [Serializable]
    class TestStruct : BaseStruct
    {
        public enum TestEnum
        {
            ResourceDirectory,
            DocumentsDirectory,
            TemporaryDirectory,
            CachesDirectory,
        };

        //
        private TestEnum m_numTestEnum;
        private List<string> m_TestList = new List<string>();
        private string[] m_TestArray = new string[10];
        private bool m_bToggle = false;
        private int m_iValue = 0;
        private double m_dValue = 0.0f;
        private Color m_cColor = new Color();
        private Point m_pPoint = new Point(1, 1);
        private Rectangle m_rRectangle = new Rectangle(1, 1, 2, 2);
        private Size m_sSize = new Size(1, 1);
        private Font m_oFont = new Font("Arial", 10);
        public string m_MyCountry = "";
        private string m_MyCity = "";
        public string m_strImgFilename = "";
        private string m_strDropdownListItem;
        private string m_strImageTriggerPos = "";

        //
        public TestStruct()
        {
        }

        //
        public void AddChild(BaseStruct Node)
        {
            AddChildNode(Node);
        }

        //
        [Category("Test")]
        [Description("Test Image trigger modal")]
        [EditorAttribute(typeof(ImageTriggerEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string TestImageModal
        {
            get { return m_strImageTriggerPos; }
            set { m_strImageTriggerPos = value; }
        }

        //
        [Category("Test")]
        [Description("Test Dropdown list")]
        [EditorAttribute(typeof(DropdownListEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string TestDropdownList
        {
            get { return m_strDropdownListItem; }
            set { m_strDropdownListItem = value; }
        }

        //
        [Category("Test")]
        [Description("Test String Selection.")]
        [TypeConverter(typeof(ObjectNameConverter))]
        public string TestSelectionCountry
        {
            get { return m_MyCountry; }
            set { m_MyCountry = value; }
        }

        [Category("Test")]
        [Description("Test String Selection.")]
        [TypeConverter(typeof(ObjectNameConverter))]
        public string TestSelectionCity
        {
            get { return m_MyCity; }
            set { m_MyCity = value; }
        }

        //
        [Category("Test")]
        [Description("Image Filename")]
        [EditorAttribute(typeof(ImageFileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string TestImageFilename
        {
            get
            {
                return m_strImgFilename;
            }
            set
            {
                m_strImgFilename = value;
            }
        }

        //
        [Category("Test")]
        [Description("Test Font.")]
        public Font TestFont
        {
            get
            {
                return m_oFont;
            }
            set
            {
                m_oFont = value;
            }
        }

        //
        [Category("Test")]
        [Description("Test Size.")]
        public Size TestSize
        {
            get
            {
                return m_sSize;
            }
            set
            {
                m_sSize = value;
            }
        }

        //
        [Category("Test")]
        [Description("Test Rectangle.")]
        public Rectangle TestRectangle
        {
            get
            {
                return m_rRectangle;
            }
            set
            {
                m_rRectangle = value;
            }
        }

        //
        [Category("Test")]
        [Description("Test Enum.")]
        public TestEnum Browser
        {
            get
            {
                return m_numTestEnum;
            }
            set
            {
                m_numTestEnum = value;
            }
        }

        //
        [Category("Test")]
        [Description("Test Color.")]
        public Color TestColor
        {
            get
            {
                return m_cColor;
            }
            set
            {
                m_cColor = value;
            }
        }

        //
        [Category("Test")]
        [Description("Test Point.")]
        public Point TestPoint
        {
            get
            {
                return m_pPoint;
            }
            set
            {
                m_pPoint = value;
            }
        }

        //
        [Category("Test")]
        [Description("Test bool.")]
        public bool TestBool
        {
            get
            {
                return m_bToggle;
            }
            set
            {
                m_bToggle = value;
            }
        }

        //
        [Category("Test")]
        [Description("Test Int.")]
        public int TestInt
        {
            get
            {
                return m_iValue;
            }
            set
            {
                m_iValue = value;
            }
        }

        //
        [Category("Test")]
        [Description("Test Double.")]
        public double TestDouble
        {
            get
            {
                return m_dValue;
            }
            set
            {
                m_dValue = value;
            }
        }
        //
        [Category("Test")]
        [Description("Test List.")]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public List<string> TestList
        {
            get
            {
                return m_TestList;
            }
            set
            {
                m_TestList = value;
            }
        }

        //
        [Category("Test")]
        [Description("Test Array.")]
        //[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public string[] TestArray
        {
            get
            {
                return m_TestArray;
            }
            set
            {
                m_TestArray = value;
            }
        }
    }
}
