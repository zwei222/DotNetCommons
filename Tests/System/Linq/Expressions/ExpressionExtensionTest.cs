using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DotNetCommons.System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCommons.Tests.System.Linq.Expressions
{
    [TestClass]
    public class ExpressionExtensionTest
    {
        [TestMethod]
        public void AndTest_001()
        {
            var param1 = 1;
            var param2 = 2;
            var param3 = 3;
            var param4 = 4;
            Expression<Func<int, bool>> param1Expression = param => param != param1;
            Expression<Func<int, bool>> param2Expression = param => param != param2;
            Expression<Func<int, bool>> param3Expression = param => param != param3;
            var expression = param1Expression.And(param2Expression).And(param3Expression);

            if (expression.Compile().Invoke(param1))
            {
                Assert.Fail();
            }

            if (expression.Compile().Invoke(param2))
            {
                Assert.Fail();
            }

            if (expression.Compile().Invoke(param3))
            {
                Assert.Fail();
            }

            if (!expression.Compile().Invoke(param4))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void OrTest_001()
        {
            var param1 = 1;
            var param2 = 2;
            var param3 = 3;
            var param4 = 4;
            Expression<Func<int, bool>> param1Expression = param => param == param1;
            Expression<Func<int, bool>> param2Expression = param => param == param2;
            Expression<Func<int, bool>> param3Expression = param => param == param3;
            var expression = param1Expression.Or(param2Expression).Or(param3Expression);

            if (!expression.Compile().Invoke(param1) ||
                !expression.Compile().Invoke(param2) ||
                !expression.Compile().Invoke(param3))
            {
                Assert.Fail();
            }

            if (expression.Compile().Invoke(param4))
            {
                Assert.Fail();
            }
        }
    }
}
