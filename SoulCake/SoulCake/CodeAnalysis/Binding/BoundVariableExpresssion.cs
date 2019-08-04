using System;

namespace SoulCake.CodeAnalysis.Binding
{
    internal sealed class BoundVariableExpresssion : BoundExpression
    {
        public BoundVariableExpresssion(VariableSymbol variable)
        {
           
            Variable = variable;
        }

        

        public override BoundNodeKind Kind => BoundNodeKind.VariableExpression;

      
        public override Type Type => Variable.Type;
        public VariableSymbol Variable { get; }
    }
}


