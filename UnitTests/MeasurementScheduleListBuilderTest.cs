namespace UnitTests
{
	using System.Collections.Generic;
	using System.Linq;
	using MariaTask.Entities;
	using MariaTask.Models;
	using Moq;
	using Xunit;

	public class MeasurementScheduleListBuilderTest
	{
		[Fact]
		public void BuildingCorrectness()
		{
			var totalDays    = 5;
			var fakeTownList = new List<Town>()
			{
				new Town("1"),
				new Town("2"),
				new Town("3"),
			};
			var manager = new Mock<IMeasurementOrderManager>();
			var builder = new TwoHoursMeasurementScheduleListBuilder();
			var result  = builder.WithTownList(fakeTownList)
				.WithTotalDays(totalDays)
				.WithOrderManager(manager.Object)
				.EachIntervalMeasurmentsCount(() => { return 2; })
				.TimeIntervalsCount(() => { return 2; })
				.Build();

			Assert.True(result.Count > 0);
			Assert.True(fakeTownList.All(t => result.Any(s => s.Town == t)));
			Assert.True(result.GroupBy(s => s.Date).Count() == totalDays);
		}	
	}
}
