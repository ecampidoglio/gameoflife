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

        protected static T A<T>(params Action<T>[] with)
        {
            var value = fixture.Create<T>();

            foreach (var propertySetter in with)
            {
                propertySetter(value);
            }

            return value;
        }

        protected static T A<T>(params Expression<Func<T, object>>[] withOnly)
        {
            var builder = fixture
                .Build<T>()
                .OmitAutoProperties();

            foreach (var property in withOnly)
            {
                builder = builder.With(property);
            }

            return builder.Create();
        }

        protected static IEnumerable<T> Many<T>(int count = 3, params Action<T>[] with)
        {
            var values = fixture.CreateMany<T>(count);

            foreach (var value in values)
            {
                Array.ForEach(with, setter => setter(value));
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
