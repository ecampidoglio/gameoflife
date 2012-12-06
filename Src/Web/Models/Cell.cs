namespace Thoughtology.GameOfLife.Web.Models
{
    public class Cell
    {
        public Cell(bool alive = false, byte neighbours = 0)
        {
            this.Alive = alive;
            this.Neighbours = neighbours;
        }

        public bool Alive { get; set; }

        public byte Neighbours { get; private set; }
    }
}
