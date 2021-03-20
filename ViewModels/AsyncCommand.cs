using System;
using System.Threading.Tasks;

namespace Energetic.Clients.ViewModels
{
    public class AsyncCommand : IAsyncCommand
    {
        public event EventHandler? CanExecuteChanged;

        private bool _isExecuting;
        private readonly Func<Task> _execute;
        private readonly Func<bool>? _canExecute;
        private readonly Action<Exception>? _errorHandler;

        public AsyncCommand(
            Func<Task> execute,
            Func<bool>? canExecute = null,
            Action<Exception>? errorHandler = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        public bool CanExecute()
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    await _execute();
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        public void Execute(object parameter)
        {
            ExecuteAsync().SafeFireAndForget(_errorHandler);
        }
    }
}