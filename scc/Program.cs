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
    //          2   3 Number node                            1   2
    // 
    //episode 5 1:02:30

        // by Lars Meske

    internal static class Program
    {
        //Test
      private  static void Main()
        {
            var showTree = false;
            var variables = new Dictionary<VariableSymbol, object>();

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
                var compilation = new CodeAnalysis.Compilation(syntaxTree);
                var result = compilation.Evaluate(variables);

                var diagnostics = result.Diagnostics;

                if (showTree)
                {
           
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    syntaxTree.Root.WriteTo(Console.Out);
                 
                    Console.ResetColor();
                }




                
                if (!diagnostics.Any())
                {
                 
                    Console.WriteLine(result.Value);
                }
                else
                {

                    var text = syntaxTree.Text;

                    foreach (var diagnostic in diagnostics)
                    {
                        var lineIndex = text.GetLineIndex(diagnostic.Span.Start);
                        var lineNumber = lineIndex + 1;
                        var character = diagnostic.Span.Start - text.Lines[lineIndex].Start + 1;

                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write($"({lineNumber}, {character}): ");
                        Console.WriteLine(diagnostic);
                        Console.ResetColor();

                        var prefix = line.Substring(0, diagnostic.Span.Start);
                        var error = line.Substring(diagnostic.Span.Start, diagnostic.Span.Length);
                        var suffix = line.Substring(diagnostic.Span.End);

                        Console.Write("    ");
                        Console.Write(prefix);

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(error);
                        Console.ResetColor();

                        Console.Write(suffix);
                        Console.WriteLine();


                    }
                    Console.WriteLine();
                }
            }
        }

     
    }
}
