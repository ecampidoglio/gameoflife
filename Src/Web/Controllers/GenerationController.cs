namespace Thoughtology.GameOfLife.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Models;

    public class GenerationController : ApiController
    {
        public IEnumerable<Cell> Post(IEnumerable<Cell> seed)
        {
            return KillUnderPopulatedCells(seed);
        }

        private static IEnumerable<Cell> KillUnderPopulatedCells(IEnumerable<Cell> seed)
        {
            return Kill(GetUnderPopulatedCells(seed)).Union(seed);
        }

        private static IEnumerable<Cell> GetUnderPopulatedCells(IEnumerable<Cell> cells)
        {
            return cells.Where(cell => cell.Alive && cell.Neighbours < 2);
        }

        private static IEnumerable<Cell> Kill(IEnumerable<Cell> cells)
        {
            return cells.Select(cell =>
            {
                cell.Kill();
                return cell;
            });
        }
    }
}
