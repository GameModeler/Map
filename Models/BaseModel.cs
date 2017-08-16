using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Map.Annotations;

namespace Map.Models
{
    /// <summary>
    /// Define the base properties and behaviors of any game model
    /// </summary>
    [Serializable]
    public abstract class BaseModel : INotifyPropertyChanged
    {
        #region Attributes

        private int _id;

        #endregion

        #region Properties

        /// <summary>
        /// Model's id
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Events

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Update the UI when a model's property changes
        /// </summary>
        /// <param name="propertyName">Name of the changed property</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
