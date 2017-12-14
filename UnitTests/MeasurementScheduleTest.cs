namespace UnitTests
{
	using System;
	using System.Collections.Generic;
	using MariaTask.Entities;
	using MariaTask.Models;
	using Moq;
	using Xunit;

	public class MeasurementScheduleTest
	{
		[Fact]
		public void MeasurementScheduleStatusCheck()
		{
			var fakeScheduleList = new List<MeasurementOrder>();
			var fakeTown = new Town("Fake");
			var fakeOrder = new MeasurementOrder(100000, new Client());

			var manager = new Mock<IMeasurementOrderManager>();
			manager.Setup(m => m.Storage).Returns(fakeScheduleList);
			manager.Setup(m => m.AddOrder(It.IsAny<MeasurementOrder>())).Callback<MeasurementOrder>((s) => fakeScheduleList.Add(s));

			var schedule = new TwoHoursMeasurementSchedule(manager.Object, fakeTown, DateTime.Now);

			Assert.True(schedule.OrderStatus == MeasurmentScheduleStatus.Unassigned);

			schedule.OrderManager.AddOrder(fakeOrder);
			schedule.Order = fakeOrder;

			Assert.True(schedule.OrderStatus == MeasurmentScheduleStatus.Assigned);

			schedule.Order = null;

			Assert.True(schedule.OrderStatus == MeasurmentScheduleStatus.Canceled);
		}
	}
}
