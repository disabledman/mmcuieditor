using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.CompilerServices;

namespace MMUIEditor
{
    public partial class Form1 : Form
    {
        //
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_ObjManager.Serialization();
        }
    }
}
