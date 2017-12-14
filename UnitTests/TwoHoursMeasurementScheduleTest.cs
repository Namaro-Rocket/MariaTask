namespace UnitTests
{
	using System;
	using MariaTask.Entities;
	using MariaTask.Models;
	using Moq;
	using Xunit;

	public class TwoHoursMeasurementScheduleTest
	{
		[Fact]
		public void TwoHoursDifference()
		{
			var fakeTown = new Town("Fake");
			var manager  = new Mock<IMeasurementOrderManager>();
			var schedule = new TwoHoursMeasurementSchedule(manager.Object, fakeTown, DateTime.Now);

			Assert.True((schedule.DateTimeUntil - schedule.DateTimeFrom) == TimeSpan.FromHours(2));
		}
	}
}
