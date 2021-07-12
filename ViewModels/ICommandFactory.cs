using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Energetic.Clients.ViewModels
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(Action<object> execute);
        ICommand CreateCommand(Action execute);
        ICommand CreateCommand(Action<object> execute, Func<object, bool> canExecute);
        ICommand CreateCommand(Action execute, Func<bool> canExecute);
        ICommand CreateCommand<T>(Action<T> execute);
        ICommand CreateCommand<T>(Action<T> execute, Func<T, bool> canExecute);

        /* TODO: Calling code can do:
         * CommandFactory.CreateCommand(async () => await InitializeAsync())
         * But also 
         * CommandFactory.CreateCommand(InitializeAsync)
         * Both compile with no errors or warnings, but they're not the same (at least, I don't think they are).
         * If there are important differences and a "right" and "wrong" way to do it, let's refactor this so that nobody can
         * do it the wrong way. */

        IAsyncCommand CreateCommand(Func<Task> execute, Func<bool>? canExecute = null, Action<Exception>? errorHandler = null);
        //IAsyncCommand CreateCommand<T>(Func<Task<T>> execute, Func<bool>? canExecute = null, Action<Exception> errorHandler = null);
    }
}