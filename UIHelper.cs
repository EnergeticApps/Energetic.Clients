using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Energetic.Clients
{
    public static class UIHelper
    {
        /// <summary>
        /// Returns the display name of the member passed in <paramref name="expression"/>, as determined by a
        /// <see cref="DisplayAttribute"/> decorating the member.
        /// </summary>
        /// <typeparam name="T">The type on which the member in the <paramref name="expression"/> will be found.</typeparam>
        /// <param name="expression">A lambda expression that must point to a member that is decorated by a <see cref="DisplayAttribute"/>.</param>
        /// <returns>The value of the <see cref="DisplayAttribute.Name"/> property, or an empty string if none was found.</returns>
        public static string DisplayNameFor<T>(Expression<Func<T>> expression)
        {
            var member = expression.Body as MemberExpression;

            if (member == null)
                throw new ArgumentException("Lambda must resolve to a member");

            var attribute = member.Member.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            return attribute?.Name ?? member.Member.Name ?? string.Empty;
        }
    }
}
