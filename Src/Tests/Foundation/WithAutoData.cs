namespace Thoughtology.GameOfLife.Tests.Foundation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Machine.Specifications;
    using Ploeh.AutoFixture;

    public abstract class WithAutoData
    {
        protected static Fixture fixture;

        Establish context = () =>
        {
            fixture = new Fixture();
        };

        protected static T A<T>()
        {
            return fixture.Create<T>();
        }

        protected static T A<T>(Action<T> with)
        {
            var value = fixture.Create<T>();
            with(value);
            return value;
        }

        protected static T APartial<T>(Expression<Func<T, object>> withOnly)
        {
            return fixture
                .Build<T>()
                .OmitAutoProperties()
                .With(withOnly)
                .Create();
        }

        protected static T AnEmpty<T>()
        {
            return fixture
                .Build<T>()
                .OmitAutoProperties()
                .Create();
        }

        protected static T AFrozen<T>()
        {
            return fixture.Freeze<T>();
        }

        protected static IEnumerable<T> Many<T>(int count = 3)
        {
            return fixture.CreateMany<T>(count);
        }

        protected static IEnumerable<T> Many<T>(Action<T> with, int count = 3)
        {
            var values = fixture.CreateMany<T>(count);

            foreach (var value in values)
            {
                with(value);
            }

            return values;
        }

        protected static IEnumerable<T> ManyIncluding<T>(params T[] elements)
        {
            return ManyIncluding((IEnumerable<T>)elements);
        }

        protected static IEnumerable<T> ManyIncluding<T>(IEnumerable<T> elements)
        {
            return Many<T>().Union(elements);
        }
    }
}
