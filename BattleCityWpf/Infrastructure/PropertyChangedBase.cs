using System.ComponentModel;
using PropertyChanged;

namespace BattleCityWpf.Infrastructure
{
    [ImplementPropertyChanged]
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
         
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
