namespace Thoughtology.GameOfLife.AcceptanceTests.Steps
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using Foundation;
    using Hooks;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public class Rules
    {
        private readonly WebClient client;
        private IEnumerable<dynamic> seed;
        private HttpResponseMessage response;
        private IEnumerable<dynamic> nextGeneration;

        public Rules(WebClient client)
        {
            this.client = client;
        }

        [Given]
        public void Given_a_live_cell_has_fewer_than_COUNT_live_neighbours(byte count)
        {
            seed = new[]
                   {
                       new { Alive = true, Neighbours = --count }
                   };
        }

        [When]
        public void When_I_ask_for_the_next_generation_of_cells()
        {
            response = client.PostAsJson("api/generation", seed);
        }

        [Then]
        public void Then_I_should_get_back_a_new_generation()
        {
            response.ShouldBeSuccessful();
            nextGeneration = ParseGenerationFromResponse();
            nextGeneration.ShouldNot(Be.Null);
        }

        [Then]
        public void Then_it_should_have_the_same_number_of_cells()
        {
            nextGeneration.Should(Have.Count.EqualTo(1));
        }

        [Then]
        public void Then_the_cell_should_be_dead()
        {
            var isAlive = (bool)nextGeneration.Single().Alive;
            isAlive.ShouldBeFalse();
        }

        private IEnumerable<dynamic> ParseGenerationFromResponse()
        {
            return response.ReadContentAs<IEnumerable<dynamic>>();
        }
    }
}
