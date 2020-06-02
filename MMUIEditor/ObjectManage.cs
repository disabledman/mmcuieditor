using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

namespace MMUIEditor
{
    class ObjectManage
    {
        BaseStruct m_ObjectList;
        int m_iObjectCounter;
        List<string> m_ObjectNameList = new List<string>();

        //
        public ObjectManage()
        {
            m_ObjectList = new BaseStruct();
            m_iObjectCounter = 1;
        }

        //
        public bool IsNameAvailable(string strName)
        {
            if(m_ObjectNameList.Contains(strName) == false)
            {
                m_ObjectNameList.Add(strName);
                return true;
            }

            Util.Log(string.Format("Object name is duplicated : {0}", strName));

            return false;
        }

        //
        public String AddNewGroup(String ParentNodeString, PropertyChangedEventHandler evtPropertyChange)
        {
            String new_label = "";
            BaseStruct  parent_node = m_ObjectList;

            try
            {
                //
                if (IsNameAvailable(m_iObjectCounter.ToString()) == false) return "";

                // Find the selected node to be the Parent 
                if (ParentNodeString != "ROOT")
                {
                    // Find the selected node to be a parent
                    BaseStruct obj = m_ObjectList.FindChildNode(m_ObjectList, ParentNodeString);
                    if (obj != null)
                    {
                        parent_node = obj;
                    }
                }

                // Add new node into the root
                using (GroupStruct obj = new GroupStruct())
                {
                    obj.Name = m_iObjectCounter.ToString();

                    //
                    parent_node.AddChildNode(obj);

                    //
                    new_label = "group_" + obj.Name;
                    obj.PropertyChanged += evtPropertyChange;
                }

                //
                m_iObjectCounter++;
            }
            catch (Exception)
            {
                new_label = "";
            }

            return new_label;
        }

        //
        public String AddNewImage(String ParentNodeString, PropertyChangedEventHandler evtPropertyChange)
        {
            String new_label = "";
            BaseStruct parent_node = m_ObjectList;


            try
            {
                //
                if (IsNameAvailable(m_iObjectCounter.ToString()) == false) return "";

                // Find the selected node to be the Parent 
                if (ParentNodeString != "ROOT")
                {
                    // Find the selected node to be a parent
                    BaseStruct obj = m_ObjectList.FindChildNode(m_ObjectList, ParentNodeString);
                    if (obj != null)
                    {
                        parent_node = obj;
                    }
                }

                // Add new node into the root
                using (ImageStruct obj = new ImageStruct())
                {
                    obj.Name = m_iObjectCounter.ToString();

                    //
                    parent_node.AddChildNode(obj);

                    //
                    new_label = "image_" + obj.Name;
                    obj.PropertyChanged += evtPropertyChange;
                }

                //
                m_iObjectCounter++;
            }
            catch (Exception)
            {
                new_label = "";
            }

            return new_label;
        }

        //
        public String AddNewTest(String ParentNodeString, PropertyChangedEventHandler evtPropertyChange)
        {
            String new_label = "";
            BaseStruct parent_node = m_ObjectList;

            try
            {
                //
                if (IsNameAvailable(m_iObjectCounter.ToString()) == false) return "";

                // Find the selected node to be the Parent 
                if (ParentNodeString != "ROOT")
                {
                    // Find the selected node to be a parent
                    BaseStruct obj = m_ObjectList.FindChildNode(m_ObjectList, ParentNodeString);
                    if (obj != null)
                    {
                        parent_node = obj;
                    }
                }

                // Add new node into the root
                using (TestStruct obj = new TestStruct())
                {
                    obj.Name = m_iObjectCounter.ToString();

                    //
                    parent_node.AddChildNode(obj);

                    //
                    new_label = "test_" + obj.Name;
                    obj.PropertyChanged += evtPropertyChange;
                }

                //
                m_iObjectCounter++;
            }
            catch (Exception)
            {
                new_label = "";
            }

            return new_label;
        }

        //
        public object GetObject(String strName)
        {
            BaseStruct obj  = null;


            try
            {
                string[] piece = strName.Split('_');
                if (piece.Length >= 2)
                {
                    obj = m_ObjectList.FindChildNode(m_ObjectList, piece[1]);
                }
            }
            catch (Exception)
            {
            }

            return obj;
        }

        //
        public void Serialization()
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(BaseStruct));
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:/123.xml");

            writer.Serialize(file, m_ObjectList);
            file.Close();
        }
    }
}
