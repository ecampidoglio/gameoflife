namespace Thoughtology.GameOfLife.AcceptanceTests.Steps
{
    using NUnit.Framework;
    using Pages;
    using TechTalk.SpecFlow;

    [Binding]
    public class Game
    {
        private readonly UniversePage page;

        public Game(UniversePage page)
        {
            this.page = page;
        }

        [Given]
        public void Given_a_live_cell_in_the_grid_does_not_have_any_live_neighbours()
        {
            page.ResurrectCell(0, 0);
        }

        [When]
        public void When_I_trigger_the_next_generation_of_cells()
        {
            page.DisplayNextGeneration();
        }

        [Then]
        public void Then_the_cell_in_the_grid_should_be_dead()
        {
            page.IsCellAlive(0, 0).ShouldBeFalse();
        }
    }
}
