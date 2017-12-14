namespace MariaTask.Entities
{
	public enum MeasurmentScheduleStatus
	{
		/// <summary>
		/// None order was assigned to current schedule.
		/// </summary>
		Unassigned,

		/// <summary>
		/// Currently some order assigned to this schedule.
		/// </summary>
		Assigned,

		/// <summary>
		/// Order was deleted from this schedule.
		/// </summary>
		Canceled,
	}
}
