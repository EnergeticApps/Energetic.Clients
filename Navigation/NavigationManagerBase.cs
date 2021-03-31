using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Energetic.Clients.Navigation
{
    public abstract class NavigationManagerBase : INavigationManager
    {
        public NavigationManagerBase(IOptions<OidcProviderOptions> optionsAccessor)
        {
            if (optionsAccessor is null)
                throw new ArgumentNullException(nameof(optionsAccessor));

            OidcOptions = optionsAccessor.Value;

            if (string.IsNullOrEmpty(OidcOptions.Authority))
                throw new InvalidOperationException($"There's something wrong with the {typeof(OidcProviderOptions)} passed. " +
                    "Maybe you don't have an appSettings.json? " +
                    "Maybe you didn't put the OIDC configuration section in the JSON file? " +
                    "Maybe you didn't register the section in the dependency injection container?");
        }

        public abstract string Location { get; }

        protected OidcProviderOptions OidcOptions { get; }

        public abstract Task<bool> IsLoggedInAsync();

        public abstract Task NavigateExternalAsync(string url);

        public abstract Task NavigateToLogInAsync();

        public abstract Task NavigateToLogOutAsync();

        public abstract Task NavigateToManageAccountAsync();

        public abstract Task NavigateToPageAsync(string url);

        public abstract Task NavigateToPageAsync(string url, string parameterKey, string parameterValue);

        public abstract Task NavigateToRegisterAsync();

        protected virtual string MakeAuthUrl(string pattern, string parameter)
        {
            return string.Format(pattern, OidcOptions.Authority, parameter);
        }

        protected virtual string MakeAuthUrl(string pattern)
        {
            return string.Format(pattern, OidcOptions.Authority);
        }
    }
}