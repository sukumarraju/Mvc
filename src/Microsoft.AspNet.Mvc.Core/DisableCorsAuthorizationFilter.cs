// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNet.Cors.Core;
using Microsoft.AspNet.WebUtilities;
using Microsoft.Framework.Internal;

namespace Microsoft.AspNet.Mvc
{
    /// <summary>
    /// An <see cref="ICorsAuthorizationFilter"/> which ensures that an action does not run for a pre-flight request.
    /// </summary>
    public class DisableCorsAuthorizationFilter : ICorsAuthorizationFilter
    {
        /// <inheritdoc />
        public Task OnAuthorizationAsync([NotNull] AuthorizationContext context)
        {
            if (context.HttpContext.Request.IsPreflight())
            {
                // Short circuit if the request is preflight as that should not result in action execution.
                context.Result = new HttpStatusCodeResult(StatusCodes.Status200OK);
            }

            // Let the action be executed.
            return Task.FromResult(true);
        }
    }
}
