namespace MariaTask.Models
{
	using System;
	using System.Diagnostics.Contracts;

	/// <summary>
	/// Class presented client data.
	/// </summary>
	public class Client
	{
		private string fullName;
		private string phone;

		/// <summary>
		/// Client full name.
		/// </summary>
		public string FullName
		{
			get { return fullName; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null, "Client full name can't be null.");
				fullName = value;
			}
		}

		/// <summary>
		/// Client phone number.
		/// </summary>
		public string Phone
		{
			get { return phone; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null, "Phone param can't be null");
				phone = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Client"/> class.
		/// </summary>
		public Client()
		{
			this.FullName = "";
			this.Phone = "";
		}
	}
}
