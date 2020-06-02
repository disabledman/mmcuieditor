using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MMUIEditor
{
    [Serializable]
    class BaseStruct : INotifyPropertyChanged, IDisposable
    {
        List<BaseStruct> m_ChildNodes;
        private String m_sName;
        //
        public BaseStruct()
        {
            m_sName = "";
            m_ChildNodes = new List<BaseStruct>();
        }

        //
        [Category("Basic")]
        [DefaultValue("")]
        public string Name
        {
            get
            {
                return m_sName;
            }
            set
            {
                m_sName = value;
                NotifyPropertyChanged(string.Format("Name_{0}", value));
            }
        }

        //
        public void AddChildNode(BaseStruct Obj)
        {
            m_ChildNodes.Add(Obj);
        }

        //
        public BaseStruct FindChildNode(BaseStruct Obj, String NodeName)
        {
            BaseStruct ret = null;

            foreach (BaseStruct bs in Obj.m_ChildNodes)
            {
                if (bs.m_ChildNodes.Count > 0)
                {
                    ret = FindChildNode(bs, NodeName);
                    if (ret != null) return ret;
                }
                //
                if (bs.Name == NodeName)
                {
                    ret = bs;
                    break;
                }
            }

            return ret;
        }
    
        //
        public event PropertyChangedEventHandler PropertyChanged;

        //
        public void NotifyPropertyChanged(String info = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        //
        public void Dispose()
        {
            m_ChildNodes.Clear();
            //
            GC.SuppressFinalize(this);
        }
    }
}
