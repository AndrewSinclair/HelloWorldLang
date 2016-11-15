using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelloWorld.Resources;

namespace HelloWorld
{
    class Program
    {
        private static Lexer _lexer { get; set; }
        private static Parser _parser { get; set; }
        private static Evaler _evaler { get; set; }
        private static Printer _printer { get; set; }

        static void InitPipelineComponents()
        {
            _lexer = new Lexer();
            _parser = new Parser();
            _evaler = new Evaler();
            _printer = new Printer();
        }

        static bool IsExitDetected(string inputLine)
        {
            if (inputLine == null || inputLine == "exit" || inputLine == "quit") return true;
            else return false;
        }

        static string Prompt(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                Console.Write("user>");
            }
            else
            {
                Console.Write(message + ">");
            }
            return Console.ReadLine();
        }

        static string REP(string inputLine)
        {
            var tokens = _lexer.Lex(inputLine);
            var ast = _parser.Parse(tokens);
            var result = _evaler.Eval(ast);

            return _printer.Print(result);
        }


        static void Main(string[] args)
        {
            InitPipelineComponents();

            var inputLine = Prompt(null);

            while (!IsExitDetected(inputLine))
            {
                try
                {
                    var output = REP(inputLine);
                    Console.WriteLine(output);
                }
                catch (Exception e)
                {
                    
                }

                inputLine = Prompt(null);
            }
        }
    }
}
