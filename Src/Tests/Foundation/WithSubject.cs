namespace Thoughtology.GameOfLife.Tests.Foundation
{
    using Machine.Fakes.Adapters.Moq;

    public abstract class WithSubject<TSubject> : WithSubjectAndFakeEngine<TSubject, MoqFakeEngine>
        where TSubject : class
    {
    }
}
