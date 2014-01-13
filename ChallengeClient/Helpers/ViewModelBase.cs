using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ChallengeClient.Helpers
{
    public abstract class ViewModelBase : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsDesignTime
        {
            get
            {
                return DesignerProperties.IsInDesignTool;
            }
        }
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
