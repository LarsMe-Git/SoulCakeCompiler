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

      
    }

}