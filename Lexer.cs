using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Lexer
    {
        private int pointer;
        private string program;

        private bool IsDigit(char c)
        {
            return Char.IsDigit(c);
        }

        private bool IsWhiteSpace(char c)
        {
            return Char.IsWhiteSpace(c);
        }

        private bool IsBinaryOperator(char currChar)
        {
            var binaryOps = new char[] {'+', '-', '*', '/'};
            return Array.IndexOf(binaryOps, currChar) > -1;
        }

        private bool IsParen(char currChar)
        {
            return currChar == '(' || currChar == ')';
        }

        private char NextChar()
        {
            pointer++;

            if (pointer > program.Length)
            {
                return (char)0;
            }

            return program[pointer];
        }

        private Token GetNext()
        {
            Token nextToken = new Token();

            char currentChar = this.program[pointer];

            while (IsWhiteSpace(currentChar))
            {
                currentChar = NextChar();
            }

            if (IsDigit(currentChar))
            {
                var startPointer = pointer;

                while (IsDigit(currentChar))
                {
                    currentChar = NextChar();
                }

                nextToken.Type = Token.TokenType.Int;
                nextToken.Value = Int32.Parse(program.Substring(startPointer, (pointer - startPointer))).ToString();
            }
            else if (IsBinaryOperator(currentChar))
            {
                switch (currentChar)
                {
                    case '+':
                        nextToken.Type = Token.TokenType.Add;
                        nextToken.Value = "+";
                        break;
                    case '-':
                        nextToken.Type = Token.TokenType.Sub;
                        nextToken.Value = "0";
                        break;
                    case '*':
                        nextToken.Type = Token.TokenType.Mult;
                        nextToken.Value = "*";
                        break;
                    case '/':
                        nextToken.Type = Token.TokenType.Div;
                        nextToken.Value = "/";
                        break;
                }

                pointer++;
            }
            else if (IsParen(currentChar))
            {
                if (currentChar == '(')
                {
                    nextToken.Type = Token.TokenType.LParen;
                    nextToken.Value = '(';
                } else {
                    nextToken.Type = Token.TokenType.RParen;
                    nextToken.Value = ')';
                }
            }
            else
            {
                nextToken.Type = Token.TokenType.Eof;
                nextToken.Value = "EOF";
            }

            return nextToken;
        }

        public List<Token> Lex(string program)
        {
            this.program = program;
            this.pointer = 0;

            Token token = GetNext();
            var tokens = new List<Token>();

            while (token.Type != Token.TokenType.Eof) {
                token = GetNext();

                tokens.Add(token);
            }

            return tokens;
        }
    }
}
