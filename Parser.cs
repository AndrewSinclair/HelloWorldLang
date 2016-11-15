using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Parser
    {
        private List<Token> tokens;
        private int pointer;


        private Token Peek()
        {
            if (pointer < tokens.Count)
            {
                return tokens[pointer];
            }
            else
            {
                return new Token { Type = Token.TokenType.Eof };
            }
        }

        private Token GetToken()
        {
            Token currToken;
            if (pointer < tokens.Count)
            {
                currToken = tokens[pointer];
                pointer++;
            }
            else
            {
                currToken = new Token { Type = Token.TokenType.Eof };
            }

            return currToken;
        }

        private void Expr(AST parent)
        {
            /*
             * expr := (term (PLUS|MINUS term)*)
             * */

            /*
             * 
             * AST ast = new AST();
             * 
             * ast.left = term()
             * 
             * while peek in (+ , -)
             *   ast.value = (+, -)
             *   ast.right = term()
             */

            Token currToken;

            Term(parent);

            currToken = Peek();

            while (currToken.Type == Token.TokenType.Add || currToken.Type == Token.TokenType.Sub)
            {
                // get add or sub
                currToken = GetToken();
                parent.Value = currToken;

                Term(parent);

                currToken = Peek();
            }
        }

        private void Term(AST parent)
        {
            /*
             * term := (factor (MULT|DIV factor)*)
             * */

            Token currToken;

            Factor(parent);

            currToken = Peek();

            while (currToken.Type == Token.TokenType.Mult || currToken.Type == Token.TokenType.Div)
            {
                //Get Mult or Div
                currToken = GetToken();
                parent.Value = currToken;

                Factor(parent);

                currToken = Peek();
            };
        }

        private void Factor(AST parent)
        {
            /*
             * factor := LPAREN expr RPAREN | INT
             * */

            Token currToken = Peek();

            AST exp = new AST();

            if (currToken.Type == Token.TokenType.LParen)
            {
                GetToken();
                Expr(exp);
                GetToken(); //DeQueue RParen

                parent.Children.Add(exp);
            }
            else
            {
                currToken = GetToken();
                exp.Value = currToken;

                parent.Children.Add(exp);
            }
        }

        public AST Parse(List<Token> tokens)
        {
            this.tokens = tokens;
            this.pointer = 0;

            Token currToken = this.tokens[pointer];
            AST program = new AST();

            Expr(program);

            //return String.Join(",", tokens.ToArray().Select(x => x.ToString()));
            return program;
        }
    }
}
