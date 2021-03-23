using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Energetic.Clients.ViewModels
{
    public abstract class ValidatableViewModel<TSelf> : ViewModelBase, INotifyDataErrorInfo
        where TSelf : ValidatableViewModel<TSelf>
    {
        private IValidator<TSelf> _validator;
        private Dictionary<string, IEnumerable<string>> _errors = new Dictionary<string, IEnumerable<string>>();

        public ValidatableViewModel(ICommandFactory commandFactory, IValidator<TSelf> validator) : base(commandFactory)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public bool HasErrors
        {
            get
            {
                Validate();
                return _errors.Any();
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };

        public IEnumerable GetErrors(string propertyName)
        {
            Validate();
            return _errors[propertyName];
        }

        protected virtual void SetPropertyValue<T>(ref T backingField, T value, bool validate = true, [CallerMemberName] string propertyName = "", Action? onChanged = null)
        {
            bool changed = base.SetPropertyValue(ref backingField, value, propertyName, onChanged);

            if (changed && validate)
            {
                ValidateProperty(propertyName);
            }
        }

        private void ValidateProperty(string propertyName)
        {
            var result = _validator.Validate(GetMyselfToValidate(), options => options.IncludeProperties(propertyName));
            var errorMessagesForProperty = result.Errors.Select(c => c.ErrorMessage);
            ReplacePreviousErrorMessagesWithLatest(errorMessagesForProperty, propertyName);
        }

        private void Validate()
        {
            var validationResult = _validator.Validate(GetMyselfToValidate());
            UpdateCurrentErrorMessages(ValidationResultToDictionary(validationResult));
        }

        private void ReplacePreviousErrorMessagesWithLatest(
            IEnumerable<string>? latestErrorMessagesForProperty,
            string propertyName)
        {
            IEnumerable<string> previousErrorMessagesForProperty = _errors.ContainsKey(propertyName) ? 
                _errors[propertyName] :
                new List<string>();

            if (latestErrorMessagesForProperty.IsNullOrEmpty())
            {
                if (_errors.Remove(propertyName))
                {
                    ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
                }

                return;
            }

            if (latestErrorMessagesForProperty.AreNotTheSameAs(previousErrorMessagesForProperty))
            {
                _errors[propertyName] = latestErrorMessagesForProperty!;
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        private TSelf GetMyselfToValidate()
        {
            TSelf myself = this as TSelf ??
                throw new InvalidOperationException($"This should never happen. It is assumed that {typeof(TSelf)} is derived from {GetType()}.");

            return myself;
        }

        private void UpdateCurrentErrorMessages(IDictionary<string, IEnumerable<string>> latestErrorMessages)
        {
            var allPropertyNamesBeingValidated = GetPropertyNamesToValidate(_errors, latestErrorMessages);

            foreach (var propertyName in allPropertyNamesBeingValidated)
            {
                var latestErrorMessagesForProperty = latestErrorMessages[propertyName];
                ReplacePreviousErrorMessagesWithLatest(latestErrorMessagesForProperty, propertyName);
            }
        }

        private static IEnumerable<string> GetPropertyNamesToValidate(
            Dictionary<string, IEnumerable<string>> previousErrorMessages,
            IDictionary<string, IEnumerable<string>> latestErrorMessages)
        {
            return previousErrorMessages.Keys.Union(latestErrorMessages.Keys).Distinct();
        }

        private static IDictionary<string, IEnumerable<string>> ValidationResultToDictionary(ValidationResult result)
        {
            return result.Errors.ToLookup(error => error.PropertyName, error => error.ErrorMessage)
                .ToDictionary(error => error.Key, error => (IEnumerable<string>)error);
        }
    }
}