using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class AST
    {
        public Token Value { get; set; }
        public AST Left { get; set; }
        public AST Right{ get; set; }
    }
}
