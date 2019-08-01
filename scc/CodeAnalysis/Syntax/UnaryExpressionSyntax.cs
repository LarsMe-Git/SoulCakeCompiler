using System.Collections.Generic;

namespace SoulCake.CodeAnalysis.Syntax
{
    public sealed class UnaryExpressionSyntax : ExpresssionSyntax
    {
        public UnaryExpressionSyntax(SyntaxToken operatorToken, ExpresssionSyntax operand)
        {
            
            OperatorToken = operatorToken;
            Operand = operand;
        }

        public override SyntaxKind Kind => SyntaxKind.UnaryExpression;
       
        public SyntaxToken OperatorToken { get; }
        public ExpresssionSyntax Operand { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            //yield produce stateful iterators
     
            yield return OperatorToken;
            yield return Operand;
        }
    }

}