namespace Thoughtology.GameOfLife.Web.Models
{
    public class Cell
    {
        private byte neighbours;

        public Cell(bool alive = false, byte neighbours = 0)
        {
            this.Alive = alive;
            this.neighbours = neighbours;
        }

        public bool Alive { get; private set; }
    }
}
