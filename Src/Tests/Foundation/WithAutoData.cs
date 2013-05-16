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

        protected static T AnAnonymous<T>()
        {
            return fixture.Create<T>();
        }

        protected static T AnAnonymousWith<T>(params Action<T>[] propertySetters)
        {
            var value = fixture.Create<T>();

            foreach (var setter in propertySetters)
            {
                setter(value);
            }

            return value;
        }

        protected static T AnAnonymousWithOnly<T>(params Expression<Func<T, object>>[] properties)
        {
            var builder = fixture
                .Build<T>()
                .OmitAutoProperties();

            foreach (var p in properties)
            {
                builder = builder.With(p);
            }

            return builder.Create();
        }

        protected static IEnumerable<T> ManyAnonymous<T>()
        {
            return fixture.CreateMany<T>();
        }

        protected static IEnumerable<T> ManyAnonymous<T>(int count)
        {
            return fixture.CreateMany<T>(count);
        }

        protected static IEnumerable<T> ManyAnonymousWith<T>(params Action<T>[] propertySetters)
        {
            var values = fixture.CreateMany<T>();

            foreach (var value in values)
            {
                Array.ForEach(propertySetters, setter => setter(value));
            }

            return values;
        }

        protected static IEnumerable<T> ManyAnonymousIncluding<T>(params T[] elements)
        {
            return ManyAnonymousIncluding((IEnumerable<T>)elements);
        }

        protected static IEnumerable<T> ManyAnonymousIncluding<T>(IEnumerable<T> elements)
        {
            return ManyAnonymous<T>().Union(elements);
        }
    }
}
