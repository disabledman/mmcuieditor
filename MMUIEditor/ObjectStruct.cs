using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MMUIEditor
{
    [Serializable]
    class ObjectStruct : BaseStruct
    {
        private int m_iWidth;
        private int m_iHeight;
        private double m_dWidthScale;
        private double m_dHeightScale;
        private bool m_bIsVisible;
        private int m_iX;
        private int m_iY;
        private int m_iAnchorX;
        private int m_iAnchorY;
        private bool m_bIsHitTestable;
        private int m_iRotate;

        //
        public ObjectStruct()
        {
            m_iWidth = 0;
            m_iHeight = 0;
            m_dWidthScale = 1.0f;
            m_dHeightScale = 1.0f;
            m_bIsVisible = true;
            m_iX = 0;
            m_iY = 0;
            m_iAnchorX = 0;
            m_iAnchorY = 0;
            m_bIsHitTestable = true;
            m_iRotate = 0;
        }

        //
        [Category("Object")]
        public int Width
        {
            get
            {
                return m_iWidth;
            }
            set
            {
                m_iWidth = value;
                NotifyPropertyChanged("Width");
            }
        }
        //
        [Category("Object")]
        public int Height
        {
            get
            {
                return m_iHeight;
            }
            set
            {
                m_iHeight = value;
                NotifyPropertyChanged("Height");
            }
        }
        //
        [Category("Object")]
        public double WidthScale
        {
            get
            {
                return m_dWidthScale;
            }
            set
            {
                m_dWidthScale = value;
                NotifyPropertyChanged("WidthScale");
            }
        }
        //
        [Category("Object")]
        public double HeightScale
        {
            get
            {
                return m_dHeightScale;
            }
            set
            {
                m_dHeightScale = value;
                NotifyPropertyChanged("HeightScale");
            }
        }
        //
        [Category("Object")]
        [EditorAttribute(typeof(System.Drawing.Design.UITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public bool IsVisible
        {
            get
            {
                return m_bIsVisible;
            }
            set
            {
                m_bIsVisible = value;
                NotifyPropertyChanged("IsVisible");
            }
        }
        //
        [Category("Object")]
        public int X
        {
            get
            {
                return m_iX;
            }
            set
            {
                m_iX = value;
                NotifyPropertyChanged("X");
            }
        }
        //
        [Category("Object")]
        public int Y
        {
            get
            {
                return m_iY;
            }
            set
            {
                m_iY = value;
                NotifyPropertyChanged("Y");
            }
        }
        //
        [Category("Object")]
        public int AnchorX
        {
            get
            {
                return m_iAnchorX;
            }
            set
            {
                m_iAnchorX = value;
                NotifyPropertyChanged("AnchorX");
            }
        }
        //
        [Category("Object")]
        public int AnchorY
        {
            get
            {
                return m_iAnchorY;
            }
            set
            {
                m_iAnchorY = value;
                NotifyPropertyChanged("AnchorY");
            }
        }
        //
        [Category("Object")]
        [EditorAttribute(typeof(System.Drawing.Design.UITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public bool IsHitTestable
        {
            get
            {
                return m_bIsHitTestable;
            }
            set
            {
                m_bIsHitTestable = value;
                NotifyPropertyChanged("IsHitTestable");
            }
        }
        //
        [Category("Object")]
        [Description("Roate angle")]
        public int Rotate
        {
            get
            {
                return m_iRotate;
            }
            set
            {
                m_iRotate = value;
                NotifyPropertyChanged("Rotate");
            }
        }

        
        
        //
        //public class EnumGridComboBox : System.Windows.Forms.Design.FileNameEditor
        //{
        //    protected override void InitializeDialog(System.Windows.Forms.OpenFileDialog openFileDialog)
        //    {
        //    }
        //}
    }
}
