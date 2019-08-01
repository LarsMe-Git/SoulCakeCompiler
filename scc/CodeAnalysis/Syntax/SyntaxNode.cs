using System.Collections.Generic;

namespace SoulCake.CodeAnalysis.Syntax
{
  public  abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }



}