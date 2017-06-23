using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace R3MUS.Devpack.Expressions.Tests.BinderTests
{
    [TestClass]
    public class OrTests
    {
        public Fixture Fixture;

        [TestInitialize]
        public void TestInitialise()
        {
            Fixture = new Fixture();
        }

        [TestMethod]
        public void Two_FunctionMerge_Returns_Expected()
        {
            //  Arrange
            var id = Fixture.Create<int>();
            var name = Fixture.Create<string>();
            Expression<Func<TestModel, bool>> idExpr = (c) => c.Id == id;
            Expression<Func<TestModel, bool>> nameExpr = (c) => c.Name == name;

            var models = Fixture.CreateMany<TestModel>().ToList();
            var expected1 = Fixture.Build<TestModel>()
                .With(w => w.Id, id)
                .Create();
            var expected2 = Fixture.Build<TestModel>()
                .With(w => w.Name, name)
                .Create();
            models.Add(expected1);
            models.Add(expected2);
            var combinedExpr = idExpr.Or(nameExpr).Compile();

            //  Act
            var result = models.Where(combinedExpr);

            //  Assert
            result.Count().Should().BeGreaterOrEqualTo(2);
            result.Where(w => w.Id == id).Should().Contain(expected1);
            result.Where(w => w.Name == name).Should().Contain(expected2);
        }

        [TestMethod]
        public void Three_FunctionMerge_Returns_Expected()
        {
            //  Arrange
            var id = Fixture.Create<int>();
            var name = Fixture.Create<string>();
            var enabled = Fixture.Create<bool>();
            Expression<Func<TestModel, bool>> idExpr = (c) => c.Id == id;
            Expression<Func<TestModel, bool>> nameExpr = (c) => c.Name == name;
            Expression<Func<TestModel, bool>> enExpr = (c) => c.Enabled == enabled;

            var models = Fixture.CreateMany<TestModel>().ToList();
            var expected1 = Fixture.Build<TestModel>()
                .With(w => w.Id, id)
                .With(w => w.Name, name)
                .With(w => w.Enabled, enabled)
                .Create();
            var expected2 = Fixture.Build<TestModel>()
                .With(w => w.Id, id)
                .With(w => w.Name, name)
                .With(w => w.Enabled, enabled)
                .Create();
            var expected3 = Fixture.Build<TestModel>()
                .With(w => w.Id, id)
                .With(w => w.Name, name)
                .With(w => w.Enabled, enabled)
                .Create();
            models.Add(expected1);
            models.Add(expected2);
            models.Add(expected3);
            var combinedExpr = idExpr.Or(nameExpr).Or(enExpr).Compile();

            //  Act
            var result = models.Where(combinedExpr);

            //  Assert
            result.Count().Should().BeGreaterOrEqualTo(3);
            result.Where(w => w.Id == id).Should().Contain(expected1);
            result.Where(w => w.Name == name).Should().Contain(expected2);
            result.Where(w => w.Enabled == enabled).Should().Contain(expected3);
        }
    }
}
