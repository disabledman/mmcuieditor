using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMUIEditor
{
    [Serializable]
    class GroupStruct : BaseStruct
    {
        //
        public GroupStruct()
        {
        }

        //
        public void AddChild(BaseStruct Node)
        {
            AddChildNode(Node);
        }
    }
}
