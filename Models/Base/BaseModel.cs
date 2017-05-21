using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Map.Annotations;

namespace Map.Models.Base
{
    [Serializable]
    public abstract class BaseModel : INotifyPropertyChanged
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
