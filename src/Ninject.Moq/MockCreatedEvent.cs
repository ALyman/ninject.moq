using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;

namespace Ninject.Moq
{
	/// <summary>
	/// 
	/// </summary>
	[CLSCompliant(false)]
	public delegate void MockCreatedEventHandler(object sender, MockCreatedEventArgs e);

	/// <summary>
	/// 
	/// </summary>
	[CLSCompliant(false)]
	public class MockCreatedEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MockCreatedEventArgs"/> class.
		/// </summary>
		/// <param name="mock">The mock.</param>
		public MockCreatedEventArgs(Mock mock)
		{
			this.Mock = mock;
		}

		/// <summary>
		/// Gets or sets the mock.
		/// </summary>
		/// <value>The mock.</value>
		public Mock Mock { get; private set; }
	}
}
