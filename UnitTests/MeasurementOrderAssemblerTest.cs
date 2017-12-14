namespace UnitTests
{
	using System.Linq;
	using System.Collections.Generic;
	using MariaTask.Entities;
	using MariaTask.Models;
	using Moq;
	using Xunit;

	public class MeasurementOrderAssemblerTest
	{
		[Fact]
		public void AssemblerCorrectness()
		{
			var fakeScheduleList = new List<MeasurementOrder>();
			var manager          = new Mock<IMeasurementOrderManager>();
			manager.Setup(m => m.Storage).Returns(fakeScheduleList);
			manager.Setup(m => m.AddOrder(It.IsAny<MeasurementOrder>())).Callback<MeasurementOrder>((s) => fakeScheduleList.Add(s));

			var orderAssembler = new MeasurementOrderAssembler(manager.Object);

			foreach(var i in Enumerable.Range(1,10))
			{
				orderAssembler.AssembleMeasurementOrder();
				Assert.True(manager.Object.Storage.Count() == i);
			}
		}
	}
}
