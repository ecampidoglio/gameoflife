namespace Thoughtology.GameOfLife.Tests.Foundation
{
    using Machine.Fakes.Adapters.Moq;

    public abstract class WithSubject<TSubject> : WithSubjectAndFakes<TSubject, MoqFakeEngine>
        where TSubject : class
    {
    }
}
