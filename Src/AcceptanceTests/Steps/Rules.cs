namespace Thoughtology.GameOfLife.AcceptanceTests.Steps
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using Hooks;
    using NUnit.Framework;
    using TechTalk.SpecFlow;
    using Web.Models;

    [Binding]
    public class Rules
    {
        private readonly WebClient client;
        private Cell seed;
        private HttpResponseMessage response;

        public Rules(WebClient client)
        {
            this.client = client;
        }

        [Given]
        public void Given_a_live_cell_has_fewer_than_COUNT_live_neighbours(byte count)
        {
            seed = new Cell(alive: true, neighbours: count);
        }

        [When]
        public void When_I_ask_for_the_next_generation_of_cells()
        {
            response = client.PostAsJson("generation", seed);
        }

        [Then]
        public void Then_the_cell_should_be_dead()
        {
            var generation = ParseGenerationFromResponse();
            generation.Single().Alive.ShouldBeTrue();
        }

        private IEnumerable<Cell> ParseGenerationFromResponse()
        {
            return response.Content.ReadAsAsync<IEnumerable<Cell>>().Result;
        }
    }
}
