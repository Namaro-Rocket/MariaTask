namespace MariaTask.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Runtime.CompilerServices;
	using System.Windows.Data;
	using System.Windows.Input;
	using Entities;

	/// <summary>
	///  Class represent main window view model.
	/// </summary>
	public class MainViewModel : INotifyPropertyChanged
	{
		private DateTime selectedDate;
		private TownViewModel selectedTown;
		private ObservableCollection<TownViewModel> townList = new ObservableCollection<TownViewModel>();
		private ObservableCollection<MeasurementScheduleViewModel> measurementScheduleList = new ObservableCollection<MeasurementScheduleViewModel>();
		private CollectionViewSource measureScheduleSourceList = new CollectionViewSource();
		private ICommand unRegistrateRelayCommand;
		private ICommand registrateRelayCommand;

		#region Properties
		/// <summary>
		/// Currently selected town.
		/// </summary>
		public TownViewModel SelectedTown
		{
			get { return selectedTown; }
			set
			{
				SetField(ref selectedTown, value);
				UpdateMeasureScheduleCollection();
			}
		}

		/// <summary>
		/// Currently selected day.
		/// </summary>
		public DateTime SelectedDay
		{
			get { return selectedDate; }
			set
			{
				SetField(ref selectedDate, value);
				UpdateMeasureScheduleCollection();
			}
		}

		/// <summary>
		/// Town collection.
		/// </summary>
		public ObservableCollection<TownViewModel> TownList
		{
			get { return townList; }
			set { townList = value; }
		}

		/// <summary>
		/// Measurement scedule collection.
		/// </summary>
		public ObservableCollection<MeasurementScheduleViewModel> MeasurementScheduleList
		{
			get { return measurementScheduleList; }
			set { measurementScheduleList = value; }
		}

		/// <summary>
		/// Measurement scedule collection ViewSource.
		/// </summary>
		public ICollectionView MeasureScheduleCollectionViewSource
		{
			get { return measureScheduleSourceList.View; }
		}

		/// <summary>
		/// Registrate new order relay command.
		/// </summary>
		public ICommand RegistrateRelayCommand
		{
			get
			{
				if (registrateRelayCommand == null)
					registrateRelayCommand = new RelayCommand(RegistrationButtonClick, (object sender) => { return true; });

				return registrateRelayCommand;
			}
		}

		/// <summary>
		/// Delete current order relay command.
		/// </summary>
		public ICommand UnRegistrateRelayCommand
		{
			get
			{
				if (unRegistrateRelayCommand == null)
					unRegistrateRelayCommand = new RelayCommand(UnRegistrationButtonClick, (object sender) => { return true; });

				return unRegistrateRelayCommand;
			}
		}
		#endregion /Properties

		#region Private methods
		/// <summary>
		/// Delete registration button click handler.
		/// </summary>
		private void UnRegistrationButtonClick(object sender)
		{
			var schedule = sender as MeasurementScheduleViewModel;

			if (schedule == null)
				throw new FormatException("Data context not MeasurementScheduleViewModel.");

			schedule.Order = null;
			RaisePropertyChanged("SelectedTown");
		}

		/// <summary>
		/// Registration button click handler.
		/// </summary>
		private void RegistrationButtonClick(object sender)
		{
			var schedule = sender as MeasurementScheduleViewModel;

			if (schedule == null)
				throw new FormatException("Data context not MeasurementScheduleViewModel.");

			schedule.Order = new MeasurementOrderAssembler(schedule.Model.OrderManager).AssembleMeasurementOrder();
			RaisePropertyChanged("SelectedTown");
		}

		private void UpdateMeasureScheduleCollection()
		{
		    MeasureScheduleCollectionViewSource?.Refresh();
		}

		private void SetScheduleViewSourceFilter()
		{
			var currentDayTownfilter = new Predicate<object>(item
				=> ((((MeasurementScheduleViewModel)item).Date == SelectedDay.Date)
				&& (((MeasurementScheduleViewModel)item).Town == SelectedTown?.Model)));

			MeasureScheduleCollectionViewSource.Filter = currentDayTownfilter;
		}
		#endregion /Private methods

		/// <summary>
		/// 
		/// </summary>
		public MainViewModel()
		{
			SelectedDay = DateTime.Now;
			measureScheduleSourceList.Source = measurementScheduleList;
			SetScheduleViewSourceFilter();
		}

		#region INotifyPropertyChanged realization

		protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(field, value)) return false;
			field = value;
			RaisePropertyChanged(propertyName);
			return true;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
