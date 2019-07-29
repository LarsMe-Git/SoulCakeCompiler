using System;

namespace SoulCake.CodeAnalysis
{

    class Evaluator
    {
        private readonly ExpresssionSyntax _root;

        public Evaluator(ExpresssionSyntax root)
        {
            this._root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private int EvaluateExpression(ExpresssionSyntax node)
        {
            // binary expression
            // numberExpression

            if (node is NumberExpressionSyntax n)
            {
                return (int)n.NumberToken.Value;
            }

            if (node is BinaryExpressionSyntax b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                if (b.OperatorToken.Kind == SyntaxKind.PlusToken)
                {
                    return left + right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.MinusToken)
                {
                    return left - right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.StarToken)
                {
                    return left * right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.SlashToken)
                {
                    return left / right;
                }
                else
                {
                    throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}");
                }

            }

            if (node is ParenthesizedExpressionSyntax p)
            {
                return EvaluateExpression(p.Expresssion);
            }

            throw new Exception($"Unexpected node {node.Kind}");
        }
    }



}