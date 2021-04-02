using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseClicker
{
    public class Test
    {
        public void Test1()
        {
            var x = new BB();
        }
    }

    public class BB:AbstractTest
    {
        public BB()
        {

        }
    }
    public abstract class AbstractTest
    {
        public AbstractTest()
        {
            //do some job
        }
    }
}
