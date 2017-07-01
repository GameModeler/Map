using Map.Graphics.Structs;

namespace Map.Graphics.Interfaces
{
    /// <summary>
    /// Interface for every object that can be drawn to a rendered window
    /// </summary>
    public interface IDrawable
    {
        /// <summmary>
        /// Draw the object to a render target
        ///
        /// This is a function that has to be implemented by the
        /// derived class to define how the drawable should be drawn.
        /// </summmary>
        /// <param name="target">Render target to draw to</param>
        /// <param name="states">Current render states</param>
        void Draw(IRenderTarget target, RenderStates states);
    }
}
