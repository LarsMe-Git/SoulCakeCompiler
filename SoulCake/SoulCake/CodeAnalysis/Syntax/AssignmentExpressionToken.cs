using System.Collections.Generic;

namespace SoulCake.CodeAnalysis.Syntax
{
    public sealed class AssignmentExpressionSyntax : ExpresssionSyntax
    {
        public AssignmentExpressionSyntax(SyntaxToken identifierToken, SyntaxToken equalsToken, ExpresssionSyntax expresssion)
        {
            IdentifierToken = identifierToken;
            EqualsToken = equalsToken;
            Expresssion = expresssion;
        }

        public override SyntaxKind Kind => SyntaxKind.AssignmentExpression;
        public SyntaxToken IdentifierToken { get; }
        public SyntaxToken EqualsToken { get; }
        public ExpresssionSyntax Expresssion { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return IdentifierToken;
            yield return EqualsToken;
            yield return Expresssion;

        }
    }

}