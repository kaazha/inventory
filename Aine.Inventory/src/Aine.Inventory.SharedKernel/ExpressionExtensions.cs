﻿
using System.Linq.Expressions;

namespace Aine.Inventory.SharedKernel;
public static  class ExpressionExtensions
{
  public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
  {
    if (left == null) return right;
    if (right == null) return left;

    var parameter = Expression.Parameter(typeof(T));

    var leftVisitor = new ReplaceExpressionVisitor(left.Parameters[0], parameter);
    var leftExpr = leftVisitor.Visit(left.Body);

    var rightVisitor = new ReplaceExpressionVisitor(right.Parameters[0], parameter);
    var rightExpr = rightVisitor.Visit(right.Body);

    return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(leftExpr!, rightExpr!), parameter);
  }

  private class ReplaceExpressionVisitor : ExpressionVisitor
  {
    private readonly Expression _oldValue;
    private readonly Expression _newValue;

    public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
    {
      _oldValue = oldValue;
      _newValue = newValue;
    }

    public override Expression? Visit(Expression? node)
    {
      return node == _oldValue ? _newValue : base.Visit(node);
    }
  }
}
