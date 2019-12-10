using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDataGridRandomColumnsBinding
{
    public class TestClass
    {
        public string Property1 { get; }
        public string Property2 { get; }
        public string Property3 { get; }

        public TestClass(string property1, string property2, string property3)
        {
            Property1 = property1;
            Property2 = property2;
            Property3 = property3;
        }
    }
}
