namespace MariaTask
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using Entities;
	using Models;
	using ViewModels;

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainViewModel model;	 

		public MainWindow()
		{
			InitializeComponent();
			model = new MainViewModel();
			this.DataContext = model;

			InitTownList(model);
			model.SelectedTown = model.TownList.First();

			InitMeasurementsSchedule(model);
		}

		private void InitTownList(MainViewModel model)
		{
			var townList = new List<string>() { "Саратов", "Москва", "Санкт-Петербург"};

			townList.ForEach(
				t => 
				{
					model.TownList.Add(new TownViewModel(new Town(t)));
				});
		}

		private void InitMeasurementsSchedule(MainViewModel model)
		{
			var randGen = new Random();
			var orderManager = new MeasurementOrderManager();
			var builder = new TwoHoursMeasurementScheduleListBuilder();
			var scheduleList = builder
				.WithTownList(model.TownList.Select(m => m.Model))
				.WithTotalDays(randGen.Next(1, 10))
				.WithOrderManager(orderManager)
				.WithDateStartPoint(DateTime.Now)
				.EachIntervalMeasurmentsCount(() => { return randGen.Next(0, 5); })
				.TimeIntervalsCount(() => { return randGen.Next(0, 4); })
				.Build();

			scheduleList.ForEach(
				s =>
				{
					model.MeasurementScheduleList.Add(new MeasurementScheduleViewModel(s));
				});
		}
	}
}
