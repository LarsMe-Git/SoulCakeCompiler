﻿using System.Collections.Generic;

namespace SoulCake.CodeAnalysis.Syntax
{
  public  sealed class LiteralExpressionSyntax : ExpresssionSyntax
    {
        public LiteralExpressionSyntax(SyntaxToken literalToken)
            : this(literalToken, literalToken.Value)
        {
      
        }

        public LiteralExpressionSyntax(SyntaxToken literalToken, object value)
        {
            LiteralToken = literalToken;
            Value = value;
        }
        public override SyntaxKind Kind => SyntaxKind.LiteralExpression;
        public SyntaxToken LiteralToken { get; }
        public object Value { get; }

   
    }



}