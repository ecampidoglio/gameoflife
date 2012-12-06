namespace Thoughtology.GameOfLife.Tests.Foundation
{
    using System;
    using System.Collections.Generic;
    using Machine.Fakes;
    using Machine.Fakes.Sdk;
    using Machine.Specifications;
    using Machine.Specifications.Factories;

    public abstract class WithSubjectAndFakeEngine<TSubject, TFakeEngine> : WithAutoDataFor<DefaultCustomization>
        where TSubject : class
        where TFakeEngine : IFakeEngine, new()
    {
        private static SpecificationController<TSubject, TFakeEngine> specificationController;

        protected WithSubjectAndFakeEngine()
        {
            specificationController = new SpecificationController<TSubject, TFakeEngine>();
            ContextFactory.ChangeAllowedNumberOfBecauseBlocksTo(2);
        }

        protected static TSubject Subject
        {
            get { return specificationController.Subject; }
            set { specificationController.Subject = value; }
        }

        protected static TInterfaceType The<TInterfaceType>() where TInterfaceType : class
        {
            return specificationController.The<TInterfaceType>();
        }

        protected static TInterfaceType An<TInterfaceType>(params object[] args) where TInterfaceType : class
        {
            return specificationController.An<TInterfaceType>(args);
        }

        protected static IList<TInterfaceType> Some<TInterfaceType>() where TInterfaceType : class
        {
            return specificationController.Some<TInterfaceType>();
        }

        protected static IList<TInterfaceType> Some<TInterfaceType>(int amount) where TInterfaceType : class
        {
            return specificationController.Some<TInterfaceType>(amount);
        }

        protected static void Configure<TInterfaceType>(TInterfaceType instance)
        {
            specificationController.Configure(instance);
        }

        protected static void Configure<TInterfaceType, TImplementationType>() where TImplementationType : TInterfaceType
        {
            specificationController.Configure<TInterfaceType, TImplementationType>();
        }

        protected static void Configure(Action<Registrar> registrarExpression)
        {
            Guard.AgainstArgumentNull(registrarExpression, "registar");

            specificationController.Configure(registrarExpression);
        }

        protected static TBehaviorConfig With<TBehaviorConfig>() where TBehaviorConfig : new()
        {
            return specificationController.With<TBehaviorConfig>();
        }

        protected static void With(object behaviorConfig)
        {
            specificationController.With(behaviorConfig);
        }

        // ReSharper disable UnusedMember.Local
        Because of = () =>
            specificationController.EnsureSubjectCreated();

        Cleanup after = () =>
        {
            ContextFactory.ChangeAllowedNumberOfBecauseBlocksTo(1);
            specificationController.Dispose();
        };
    }
}
