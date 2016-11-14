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
        public List<AST> Children { get; set; }

        public AST()
        {
            this.Children = new List<AST>();
        }
    }
}
