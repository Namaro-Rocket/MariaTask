namespace MariaTask.Entities
{
	using Models;
	using System.Linq;

	public sealed class MeasurementOrderAssembler
	{
		private const int defaultId = 100000;

		public IMeasurementOrderManager Manager { get; private set; }

		public MeasurementOrder AssembleMeasurementOrder()
		{
			MeasurementOrder order;
			var client    = new Client();
			var lastOrder = Manager.Storage.LastOrDefault();

			if (lastOrder == null)
			{
				order = new MeasurementOrder(defaultId, client);
			}
			else
			{
				var newId = lastOrder.Id + 1;
				order     = new MeasurementOrder(newId, client);
			}

			this.Manager.AddOrder(order);
			return order;
		}

		public MeasurementOrderAssembler(IMeasurementOrderManager manager)
		{
			this.Manager = manager;
		}
	}
}
