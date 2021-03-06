﻿/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using JetBrains.Annotations;

namespace OpenIddict.Validation
{
    /// <summary>
    /// Exposes extensions simplifying the integration with the OpenIddict validation services.
    /// </summary>
    public static class OpenIddictValidationHelpers
    {
        /// <summary>
        /// Retrieves a property value from the validation transaction using the specified name.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="transaction">The validation transaction.</param>
        /// <param name="name">The property name.</param>
        /// <returns>The property value or <c>null</c> if it couldn't be found.</returns>
        public static TProperty GetProperty<TProperty>(
            [NotNull] this OpenIddictValidationTransaction transaction, [NotNull] string name) where TProperty : class
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The property name cannot be null or empty.", nameof(name));
            }

            if (transaction.Properties.TryGetValue(name, out var property) && property is TProperty result)
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Sets a property in the validation transaction using the specified name and value.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="transaction">The validation transaction.</param>
        /// <param name="name">The property name.</param>
        /// <param name="value">The property value.</param>
        /// <returns>The validation transaction, so that calls can be easily chained.</returns>
        public static OpenIddictValidationTransaction SetProperty<TProperty>(
            [NotNull] this OpenIddictValidationTransaction transaction,
            [NotNull] string name, [CanBeNull] TProperty value) where TProperty : class
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The property name cannot be null or empty.", nameof(name));
            }

            if (value == null)
            {
                transaction.Properties.Remove(name);
            }

            else
            {
                transaction.Properties[name] = value;
            }

            return transaction;
        }
    }
}
