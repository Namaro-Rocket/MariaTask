namespace UnitTests
{
	using System;
	using System.Collections.Generic;
	using MariaTask.Models;
	using Xunit;

	public class MeasurementOrderManagerTest
	{
		public static IEnumerable<object[]> FakeMeasureOrdersData 
			= new List<object[]>
				{
					 new object[] { new MeasurementOrder(100000, new Client()) },
					 new object[] { new MeasurementOrder(200000, new Client()) },
				};

		[Theory, MemberData(nameof(FakeMeasureOrdersData))]
		public void MeasurementOrderManagerAddRepeatingId(MeasurementOrder order)
		{
			var manager = new MeasurementOrderManager();
			Assert.Throws<ArgumentException>(() => { manager.AddOrder(order); manager.AddOrder(order); });
		}

		[Fact]
		public void MeasurementOrderManagerAddNullId()
		{
			var manager = new MeasurementOrderManager();
			Assert.Throws<NullReferenceException>(() => manager.AddOrder(null));
		}
	}
}
