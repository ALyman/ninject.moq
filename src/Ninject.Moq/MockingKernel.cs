using System;
using System.Collections.Generic;
using Moq;
using Ninject.Activation.Caching;
using Ninject.Planning.Bindings;

namespace Ninject.Moq
{
	/// <summary>
	/// A kernel that will create mocked instances (via Moq) for any service that is
	/// requested for which no binding is registered.
	/// </summary>
	public class MockingKernel : StandardKernel
	{
		List<MockProvider> mockProviders = new List<MockProvider>();
		HashSet<Mock> mocks = new HashSet<Mock>();

		/// <summary>
		/// Clears the kernel's cache, immediately deactivating all activated instances regardless of scope.
		/// This does not remove any modules, extensions, or bindings.
		/// </summary>
		public void Reset()
		{
			Components.Get<ICache>().Clear();
			mocks.Clear();
			foreach (var provider in mockProviders)
			{
				provider.MockCreated -= new MockCreatedEventHandler(provider_MockCreated);
			}
			mockProviders.Clear();
		}

		/// <summary>
		/// Attempts to handle a missing binding for a service.
		/// </summary>
		/// <param name="service">The service.</param>
		/// <returns><c>True</c> if the missing binding can be handled; otherwise <c>false</c>.</returns>
		protected override bool HandleMissingBinding(Type service)
		{
			var binding = new Binding(service)
			{
				ProviderCallback = MockProvider.GetCreationCallback(this),
				ScopeCallback = ctx => null,
				IsImplicit = true
			};

			AddBinding(binding);

			return true;
		}

		internal void AddProvider(MockProvider provider)
		{
			mockProviders.Add(provider);
			provider.MockCreated += new MockCreatedEventHandler(provider_MockCreated);
		}

		void provider_MockCreated(object sender, MockCreatedEventArgs e)
		{
			mocks.Add(e.Mock);
		}

		/// <summary>
		/// Verifies all of the instantiated mocks.
		/// </summary>
		public void VerifyAll()
		{
			foreach (var mock in mocks)
				mock.VerifyAll();
		}
	}
}
