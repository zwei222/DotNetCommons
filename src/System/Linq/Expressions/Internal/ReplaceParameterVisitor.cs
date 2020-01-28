// ---------------------------------------------------------------------
// <copyright file="ReplaceParameterVisitor.cs" company="zwei222">
// Copyright (c) zwei222. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------

using System.Linq.Expressions;

namespace DotNetCommons.System.Linq.Expressions.Internal
{
    /// <summary>
    /// Replaces named parameter expressions in the expression tree.
    /// </summary>
    internal class ReplaceParameterVisitor : ExpressionVisitor
    {
        /// <summary>
        /// Old named parameter expression.
        /// </summary>
        private readonly ParameterExpression _oldParameterExpression;

        /// <summary>
        /// New named parameter expression.
        /// </summary>
        private readonly ParameterExpression _newParameterExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceParameterVisitor"/> class.
        /// </summary>
        /// <param name="oldParameterExpression">Old named parameter expression.</param>
        /// <param name="newParameterExpression">New named parameter expression.</param>
        public ReplaceParameterVisitor(
            ParameterExpression oldParameterExpression,
            ParameterExpression newParameterExpression)
        {
            this._oldParameterExpression = oldParameterExpression;
            this._newParameterExpression = newParameterExpression;
        }

        /// <summary>
        /// Visit the ParameterExpression. If the ParameterExpression is the same as the old one, return the new one.
        /// </summary>
        /// <param name="node">Parameter expression.</param>
        /// <returns>The parameter expression that was visited.</returns>
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
