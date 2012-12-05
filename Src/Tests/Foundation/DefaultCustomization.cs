namespace Thoughtology.GameOfLife.Tests.Foundation
{
    using System.Collections.Generic;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    public class DefaultCustomization : ICustomization
    {
        private IEnumerable<ICustomization> DefaultCustomizations
        {
            get
            {
                yield return new MultipleCustomization();
                yield return new StableFiniteSequenceCustomization();
            }
        }

        public virtual void Customize(IFixture fixture)
        {
            fixture.Customize(new CompositeCustomization(DefaultCustomizations));
            fixture.Customize(new CompositeCustomization(AdditionalCustomizations()));
            fixture.Customizations.Add(new CompositeSpecimenBuilder(AdditionalBuilders()));
        }

        protected virtual IEnumerable<ICustomization> AdditionalCustomizations()
        {
            yield return new CompositeCustomization();
        }

        protected virtual IEnumerable<ISpecimenBuilder> AdditionalBuilders()
        {
            yield return new CompositeSpecimenBuilder();
        }
    }
}
