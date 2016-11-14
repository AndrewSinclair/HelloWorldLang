using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Token.TokenType;

namespace HelloWorld
{
    class Parser
    {
        private List<Token> tokens;
        private int pointer;

        private readonly Token CurrToken {
            get
            {
                return tokens[pointer]; 
            }
        }

        private void Term(AST parent)
        {
            if (CurrToken.Type == TokenType.LParen)
            {
                parent.children.add(Eat(TokenType.LParen));

                Term(parent);
            }
            Token operandA = Eat(TokenType.Int);
            Token op;

            switch (CurrToken.Type)
            {
                case TokenType.Add:
                    op = Eat(TokenType.Add);
                    break;

                case TokenType.Sub:
                    op = Eat(TokenType.Sub);
                    break;

                case TokenType.Mult:
                    op = Eat(TokenType.Mult);
                    break;

                case TokenType.Div:
                    op = Eat(TokenType.Div);
                    break;
            }

            Token operandB = Eat(TokenType.Int);

            parent.Children.Add(operandA);
            parent.Children.Add(op);
            parent.Children.Add(operandB);
        }

        public string Parse(List<Token> tokens)
        {
            this.tokens = tokens;
            this.pointer = 0;

            Token currToken = this.tokens[pointer];

            AST program = new AST();

            while (currToken.Type != TokenType.Eof)
            {

                if (currToken.Type == TokenType.Int)
                {
                    Term(program);
                }
            }

            return String.Join(",", tokens.ToArray().Select(x => x.ToString()));
        }
    }
}
