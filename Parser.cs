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

        private AST Expr()
        {
            /*
             * expr := (term (PLUS|MINUS term)*)
             * */

            Token currToken;

            AST termTree = Term();

            currToken = Peek();

            while (currToken.Type == Token.TokenType.Add || currToken.Type == Token.TokenType.Sub)
            {
                // get add or sub
                currToken = GetToken();
                termTree = new AST { Value = currToken, Left = termTree, Right = Term() };
                
                currToken = Peek();
            }

            return termTree;
        }

        private AST Term()
        {
            /*
             * term := (factor (MULT|DIV factor)*)
             * */

            Token currToken;
            AST factorTree = Factor();

            currToken = Peek();

            while (currToken.Type == Token.TokenType.Mult || currToken.Type == Token.TokenType.Div)
            {
                //Get Mult or Div
                currToken = GetToken();
                factorTree = new AST { Value = currToken, Left = factorTree, Right = Factor() };

                currToken = Peek();
            };

            return factorTree;
        }

        private AST Factor()
        {
            /*
             * factor := LPAREN expr RPAREN | INT
             * */

            Token currToken = Peek();

            AST exp = new AST();

            if (currToken.Type == Token.TokenType.LParen)
            {
                GetToken(); //DeQueue LParen
                exp = Expr();
                GetToken(); //DeQueue RParen
            }
            else
            {
                currToken = GetToken();
                exp.Value = currToken;
            }

            return exp;
        }

        public AST Parse(List<Token> tokens)
        {
            this.tokens = tokens;
            this.pointer = 0;

            Token currToken = this.tokens[pointer];
            AST program = Expr();

            //return String.Join(",", tokens.ToArray().Select(x => x.ToString()));
            return program;
        }
    }
}
