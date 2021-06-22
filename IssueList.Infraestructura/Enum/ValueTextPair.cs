using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.Infraestructura.Enum
{
    public class ValueTextPair
    {
        public int value { get; set; }
        public string text { get; set; }

        public ValueTextPair(int value, string text)
        {
            this.value = value;
            this.text = text;
        }
    }
}
