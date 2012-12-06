namespace Thoughtology.GameOfLife.Tests.Web.Models.CellTests
{
    using Foundation;
    using GameOfLife.Web.Models;
    using Machine.Specifications;

    [Subject(typeof(Cell))]
    public class When_killing_a_live_cell : WithSubject<Cell>
    {
        Establish context = () =>
        {
            Subject = new Cell(alive: true);
        };

        Because of = () =>
            Subject.Kill();

        It should_mark_the_cell_as_dead = () =>
            Subject.Alive.ShouldBeFalse();
    }
}
