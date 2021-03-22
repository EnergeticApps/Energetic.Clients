using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Energetic.Clients.ViewModels
{
    public abstract class ViewModelBase : ObservableObject, IViewModel, IDisposable, IAsyncDisposable
    {
        protected ViewModelBase(ICommandFactory commandFactory)
        {
            CommandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
            InitializeCommand = commandFactory.CreateCommand(async () => await InitializeAsync());
        }

        protected ICommandFactory CommandFactory { get; private set; }

        public ICommand InitializeCommand { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //Free unmanaged resources

            if (disposing)
            {
                //Free managed resources
            }
        }

        public async ValueTask DisposeAsync()
        {
            // Perform async cleanup.
            await DisposeAsyncCore();

            // Dispose of unmanaged resources.
            Dispose(false);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected async virtual ValueTask DisposeAsyncCore()
        {
            if (CommandFactory is { })
            {
                //TODO: Learn about IDisposable and IAsyncDisposable and make sure we're using them properly wherever we should be
                //await CommandFactory.DisposeAsync();
            }

            CommandFactory = null!; //TODO: is this the right way to do it? We're setting a non-nullable thing to a null reference.
            await Task.CompletedTask;
        }

        ~ViewModelBase()
        {
            Dispose(false);
        }

        protected virtual async Task InitializeAsync()
        {
            //TODO: take this out. For now we leave it in because the Blazor debugger takes ages to attach.
            //await Task.Delay(10000);
            await Task.CompletedTask;
        }

        //public virtual void HandleAfterRender(bool firstRender) { }

        //public virtual async Task HandleAfterRenderAsync(bool firstRender)
        //{
        //    await Task.CompletedTask;
        //}

        //public virtual void HandleInitialized() { }

        //public virtual async Task HandleInitializedAsync()
        //{
        //    await Task.CompletedTask;
        //}

        //public virtual void HandleParametersSet() { }

        //public virtual async Task HandleParametersSetAsync()
        //{
        //    await Task.CompletedTask;
        //}
    }
}