namespace MariaTask.Models
{
	using System;
	using System.Diagnostics.Contracts;

	/// <summary>
	/// Class presentation town
	/// </summary>
	public class Town
	{
		private string name;

		/// <summary>
		/// Name of town.
		/// </summary>
		public string Name
		{
			get { return name; }
			private set
			{
				Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(value), "Town name can't be empty or null");
				name = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Town"/> class.
		/// </summary>
		/// <param name="name">name of town</param>
		public Town(string name)
		{
			this.Name = name;
		}

		public override string ToString()
		{
			return $"{this.Name}";
		}
	}
}
