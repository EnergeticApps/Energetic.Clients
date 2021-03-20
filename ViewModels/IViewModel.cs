using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Energetic.Clients.ViewModels
{
    public interface IViewModel : INotifyPropertyChanged, IDisposable
    {
        bool IsBusy { get; set; }
        ICommand InitializeCommand { get; }
    }
}
