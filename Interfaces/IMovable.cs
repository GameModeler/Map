using System;
using System.Windows;

namespace Map.Interfaces
{
    public interface IMovable
    {
        Point Position { get; set; }
        double X { get; set; }
        double Y { get; set; }
    }
}
