namespace Thoughtology.GameOfLife.Tests.Foundation
{
    using Machine.Specifications;
    using Ploeh.AutoFixture;

    public abstract class WithAutoDataFor<TCustomization> : WithAutoData
            where TCustomization : ICustomization, new()
    {
        Establish context = () =>
            ApplyCustomization();

        private static void ApplyCustomization()
        {
            fixture.Customize(new TCustomization());
        }
    }
}
