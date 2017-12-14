namespace MariaTask.Models
{
	using System;
	using System.Diagnostics.Contracts;

	/// <summary>
	/// Class presented sheduled measurement order.
	/// </summary>
	public class MeasurementOrder
	{
		private string comment;
		private int measureNumber;
		private Client client;
		private Town town;

		/// <summary>
		/// Mesurement identificator. Can be only in format XX-XX-XX.
		/// </summary>
		public int Id
		{
			get { return measureNumber; }
			private set
			{
				Contract.Requires<ArgumentOutOfRangeException>(value > 99999 && value < 1000000, "Measure number can be only in format XX-XX-XX");
				measureNumber = value;
			}
		}

		/// <summary>
		/// Comment to order.
		/// </summary>
		public string Comment
		{
			get { return comment; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null, "Order comment can't be null");
				comment = value;
			}
		}

		public Client Client
		{
			get { return client; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null, "Param can't be null");
				client = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MeasurementOrder"/> class.
		/// </summary>
		/// <param name="id">Measurement order id</param>
		/// <param name="client">Measurement order id</param>
		public MeasurementOrder(int id, Client client)
		{
			this.Id = id;
			this.Client = client;
			this.Comment = "";
		}

		public override string ToString()
		{
			return $"ID:{this.Id} Клиент:{this.Client.FullName}";
		}
	}
}
