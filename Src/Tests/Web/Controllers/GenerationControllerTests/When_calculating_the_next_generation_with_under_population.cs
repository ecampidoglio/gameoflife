namespace Thoughtology.GameOfLife.Tests.Web.Controllers.GenerationControllerTests
{
    using System.Collections.Generic;
    using Foundation;
    using GameOfLife.Web.Controllers;
    using GameOfLife.Web.Models;
    using Machine.Specifications;

    [Subject(typeof(GenerationController))]
    public class When_calculating_the_next_generation_with_under_population : WithSubject<GenerationController>
    {
        private static Cell solitaryCell;
        private static IEnumerable<Cell> seed;
        private static IEnumerable<Cell> nextGen;

        Establish context = () =>
        {
            solitaryCell = new Cell(alive: true, neighbours: 1);
            seed = ManyAnonymousIncluding(solitaryCell);
        };

        Because of = () =>
            nextGen = Subject.Post(seed);

        It should_include_the_solitary_cell_in_the_next_generation = () =>
            nextGen.ShouldContain(solitaryCell);

        It should_only_include_the_original_cells_in_the_next_generation = () =>
            nextGen.ShouldContainOnly(seed);

        It should_mark_the_solitary_cell_as_dead = () =>
            solitaryCell.Alive.ShouldBeFalse();
    }
}
