using Energetic.Text;
using Energetic.ValueObjects;
using System;

namespace Energetic.Clients
{
    public class Alert
    {
        private readonly AlertType _type;
        private readonly Link? _link;

        public Alert(string text, AlertType type, AlertBehaviour behaviour, Link? link = null)
        {
            Text = string.IsNullOrWhiteSpace(text) ? throw new StringArgumentNullOrWhiteSpaceException(nameof(text)) : text;
            _type = type;
            Behaviour = behaviour;
            _link = link;
        }

        public string Text;
        public string Type
        {
            get
            {
                return Enum.GetName(typeof(AlertType), _type).ToLowerInvariant();
            }
        }

        public AlertBehaviour Behaviour;

        public Link? Link
        {
            get
            {
                return _link;
            }
        }
    }
}
