using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DotNetCommons.System.Linq.Expressions.Internal
{
    internal class ParameterReplaceVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParameterExpression;

        private readonly ParameterExpression _newParameterExpression;

        public ParameterReplaceVisitor(ParameterExpression oldParameterExpression,
            ParameterExpression newParameterExpression)
        {
            this._oldParameterExpression = oldParameterExpression;
            this._newParameterExpression = newParameterExpression;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node == this._oldParameterExpression)
            {
                return this._newParameterExpression;
            }

            return base.VisitParameter(node);
        }
    }
}
