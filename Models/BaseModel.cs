using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Map.Annotations;

namespace Map.Models
{
    /// <summary>
    /// Define the base properties and behaviors of any game model
    /// </summary>
    [Serializable]
    public abstract class BaseModel : INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Converter to convert a brush to a string representation
        /// </summary>
        [NotMapped]
        public BrushConverter BrushConverter { get; set; }

        #endregion

        #region Events

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected BaseModel()
        {
            BrushConverter = new BrushConverter();
        }
        
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
