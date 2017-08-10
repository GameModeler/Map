using System.Drawing;

namespace Map.WPF.Interfaces
{
    public interface IMovable
    {
        bool IsActive { get; set; }

        Point Position { get; set; }

        bool CanMove(Point point);
    }
}
