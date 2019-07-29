using System.Collections.Generic;

namespace SoulCake.CodeAnalysis
{
    sealed class BinaryExpressionSyntax : ExpresssionSyntax
    {
        public BinaryExpressionSyntax(ExpresssionSyntax left, SyntaxToken operatorToken, ExpresssionSyntax right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;
        public ExpresssionSyntax Left { get; }
        public SyntaxToken OperatorToken { get; }
        public ExpresssionSyntax Right { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            //yield produce stateful iterators
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }



}