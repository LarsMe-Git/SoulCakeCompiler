using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using SoulCake.CodeAnalysis;
using SoulCake.CodeAnalysis.Syntax;
using SoulCake.CodeAnalysis.Binding;

namespace SoulCake
{
    // 1 + 2 * 3                                            1 + 2 + 3
    //
    //          +                                               +
    //         / \                                             / \
    //        1   * binary operator node                      +   3
    //           / \                                         / \     
    //          2   3 Numbernode                            1   2
    // 51:22 Episode 1


    internal static class Program
    {
        //Test
      private  static void Main()
        {
            var showTree = false;
            while (true)
            {
                Console.WriteLine("> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    return;
                }

                if (line == "#showTree")
                {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing parse trees." : "Not showing parse trees");
                    continue;
                }
                else if (line == "#cls")
                {
                    Console.Clear();
                    continue;
                }
                var syntaxTree = CodeAnalysis.Syntax.SyntaxTree.Parse(line);
                var binder = new Binder();
                var boundExpression = binder.BindExpression(syntaxTree.Root);


                var diagnostics = syntaxTree.Diagnostics.Concat(binder.Diagnostics).ToArray();

                if (showTree)
                {
           
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }




                
                if (!diagnostics.Any())
                {
                    var e = new Evaluator(boundExpression);
                    var result = e.Evaluate();
                    Console.WriteLine(result);
                }
                else
                {
                  
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    foreach (var diagnostic in diagnostics)
                    {
                        Console.WriteLine(diagnostic);
                    }

                    Console.ResetColor();
                }
            }
        }

        static void PrettyPrint(CodeAnalysis.Syntax.SyntaxNode node, string indent = "", bool isLast = true)
        {

            var marker = isLast ? "└─" : "├─";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is CodeAnalysis.Syntax.SyntaxToken t && t.Value != null)
            {

                Console.Write(" ");
                Console.Write(t.Value);

            }

            Console.WriteLine();

            // indent += "    ";
            indent += isLast ? "   " : "│   ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
            {
                PrettyPrint(child, indent, child == lastChild);
            }
        }
    }
}
