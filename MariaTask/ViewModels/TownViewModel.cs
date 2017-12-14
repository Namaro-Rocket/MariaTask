namespace MariaTask.ViewModels
{
	using System;
	using System.Diagnostics.Contracts;
	using Models;

	public class TownViewModel
	{
		private Town model;

		/// <summary>
		/// Returns name of town.
		/// </summary>
		public string Name
		{
			get { return this.Model.Name; }
		}

		/// <summary>
		/// Returns name of town.
		/// </summary>
		public Town Model
		{
			get { return model; }
			private set
			{
				Contract.Requires<ArgumentNullException>(value != null, "Town model can't be null.");
				model = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TownViewModel"/> class.
		/// </summary>
		/// <param name="town">name of town</param>
		public TownViewModel(Town town)
		{
			this.Model = town;
		}
	}
}
