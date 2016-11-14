using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return String.Format("{0}{1}, {2}{3}",'{', Type, Value, '}');
        }

        public enum TokenType {
            Int,
            Symbol,
            Add,
            Sub,
            Mult,
            Div,
            LParen,
            RParen,
            Eof
        }
    }
}
