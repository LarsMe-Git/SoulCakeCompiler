using System.Collections.Generic;

namespace SoulCake.CodeAnalysis.Syntax
{
   public sealed class ParenthesizedExpressionSyntax : ExpresssionSyntax
    {
        public ParenthesizedExpressionSyntax(SyntaxToken OpenParenthesisToken, ExpresssionSyntax expresssion, SyntaxToken CloseParenthesisToken)
        {
            this.OpenParenthesisToken = OpenParenthesisToken;
            Expresssion = expresssion;
            this.CloseParenthesisToken = CloseParenthesisToken;
        }

        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;
        public SyntaxToken OpenParenthesisToken { get; }
        public ExpresssionSyntax Expresssion { get; }
        public SyntaxToken CloseParenthesisToken { get; }



        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenParenthesisToken;
            yield return Expresssion;
            yield return CloseParenthesisToken;
        }
    }



}