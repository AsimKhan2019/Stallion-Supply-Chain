using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StallionSuppyChain
{
    public class SqlComboBox
    {
        public string SqlString { get; set; }
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }
        public ComboBox ComboBox { get; set; }
    }
}
