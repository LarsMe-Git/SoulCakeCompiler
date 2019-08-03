using SoulCake.CodeAnalysis.Binding;
using SoulCake.CodeAnalysis.Syntax;
using System;
using System.Linq;

namespace SoulCake.CodeAnalysis
{
    // Lesson 3

    public sealed class Compilation
    {
        public Compilation(SyntaxTree syntax)
            {
            Syntax = syntax;
            }

        public SyntaxTree Syntax { get; }

        public EvaluationResult Evaluate()
            {
            var binder = new Binder();
            var boundExpression = binder.BindExpression(Syntax.Root);

            var diagnostics = Syntax.Diagnostics.Concat(binder.Diagnostics).ToArray();
            if (diagnostics.Any())
            {
                return new EvaluationResult(diagnostics, null);
            }

            var evaluator = new Evaluator(boundExpression);
            var value = evaluator.Evaluate();
            return new EvaluationResult(Array.Empty<Diagnostic>(), value);
            }
    }

}