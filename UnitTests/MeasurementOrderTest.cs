namespace UnitTests
{
	using System;
	using System.Collections.Generic;
	using MariaTask.Models;
	using Xunit;

	public class MeasurementOrderTest
	{
		public static IEnumerable<object[]> WrongIdList
			= new List<object[]>
				{
					 new object[] { 5 },
					 new object[] { 99999 },
					 new object[] { 1000000 },
				};

		[Theory, MemberData(nameof(WrongIdList))]
		public void MeasurementOrderIdBoundsCheck(int id)
		{
			var client = new Client();

			Assert.Throws<ArgumentOutOfRangeException>(() => new MeasurementOrder(id, client));
		}
    }
}
