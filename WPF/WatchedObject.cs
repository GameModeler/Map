using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Map.Annotations;
using Map.WPF.Graphics;
using Point = System.Drawing.Point;

namespace Map.WPF
{
    /// <summary>
    /// Define the base components of a game object using WPF
    /// </summary>
    [Serializable]
    public abstract class WatchedObject : DependencyObject, INotifyPropertyChanged
    {
        #region Attributes
        #endregion

        #region Properties

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors
        #endregion

        #region Methods

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyChange([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
