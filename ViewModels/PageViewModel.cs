using Energetic.Text;

namespace Energetic.Clients.ViewModels
{
    public abstract class PageViewModel : ViewModelBase
    {
        protected PageViewModel(ICommandFactory commandFactory, string title) : base(commandFactory)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new StringArgumentNullOrWhiteSpaceException(nameof(title));

            Title = title;
        }

        public string Title { get; } = string.Empty;
    }
}