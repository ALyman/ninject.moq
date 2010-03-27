using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Syntax;
using Ninject.Parameters;
using Moq;
using Ninject.Planning.Bindings;

namespace Ninject.Moq
{
    /// <summary>
    /// Extensions for the fluent resolution API
    /// </summary>
    public static class ExtensionsForResolution
    {
        /// <summary>
        /// Gets the mock of an instance of the specified service.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <param name="root">The resolution root.</param>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>A mock for the instance of the service.</returns>
        [CLSCompliant(false)]
        public static Mock<T> GetMock<T>(this IResolutionRoot root, params IParameter[] parameters)
            where T : class
        {
            return Mock.Get<T>(root.Get<T>(parameters));
        }

        /// <summary>
        /// Gets the mock of an instance of the specified service by using the first binding that matches the specified constraint.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <param name="root">The resolution root.</param>
        /// <param name="constraint">The constraint to apply to the binding.</param>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>A mock for the instance of the service.</returns>
        [CLSCompliant(false)]
        public static Mock<T> GetMock<T>(this IResolutionRoot root, Func<IBindingMetadata, bool> constraint, params IParameter[] parameters)
            where T : class
        {
            return Mock.Get<T>(root.Get<T>(constraint, parameters));
        }

        /// <summary>
        /// Gets the mock of an instance of the specified service by using the first binding with the specified name.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <param name="root">The resolution root.</param>
        /// <param name="name">The name of the binding.</param>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>A mock for the instance of the service.</returns>
        [CLSCompliant(false)]
        public static Mock<T> GetMock<T>(this IResolutionRoot root, string name, params IParameter[] parameters)
            where T : class
        {
            return Mock.Get<T>(root.Get<T>(name, parameters));
        }

        /// <summary>
        /// Gets the mock of an instance of the specified service.
        /// </summary>
        /// <param name="service">The service to resolve.</param>
        /// <param name="root">The resolution root.</param>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>A mock for the instance of the service.</returns>
        [CLSCompliant(false)]
        public static Mock GetMock(this IResolutionRoot root, Type service, params IParameter[] parameters)
        {
            var mocked = root.Get(service, parameters) as IMocked;
            return mocked == null ? null : mocked.Mock;
        }

        /// <summary>
        /// Gets the mock of an instance of the specified service by using the first binding that matches the specified constraint.
        /// </summary>
        /// <param name="service">The service to resolve.</param>
        /// <param name="root">The resolution root.</param>
        /// <param name="constraint">The constraint to apply to the binding.</param>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>A mock for the instance of the service.</returns>
        [CLSCompliant(false)]
        public static Mock GetMock(this IResolutionRoot root, Type service, Func<IBindingMetadata, bool> constraint, params IParameter[] parameters)
        {
            var mocked = root.Get(service, constraint, parameters) as IMocked;
            return mocked == null ? null : mocked.Mock;
        }

        /// <summary>
        /// Gets the mock of an instance of the specified service by using the first binding with the specified name.
        /// </summary>
        /// <param name="service">The service to resolve.</param>
        /// <param name="root">The resolution root.</param>
        /// <param name="name">The name of the binding.</param>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>A mock for the instance of the service.</returns>
        [CLSCompliant(false)]
        public static Mock GetMock(this IResolutionRoot root, Type service, string name, params IParameter[] parameters)
        {
            var mocked = root.Get(service, name, parameters) as IMocked;
            return mocked == null ? null : mocked.Mock;
        }
    }
}
