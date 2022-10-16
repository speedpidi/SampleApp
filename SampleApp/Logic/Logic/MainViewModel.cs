namespace SampleApp.Logic
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using CommunityToolkit.Mvvm.Messaging;
    using Interfaces;
    using Messages;
    using Model.EventArgs;

    /// <summary>
    /// ViewModel for the MainWindow
    /// </summary>
    public class MainViewModel : ObservableObject
    {
        private readonly ITimeService _timeService;
        private string _caption = string.Empty;
        private DateTime _dateTime = DateTime.Now;
        private RelayCommand _closeApplicationCommand;
        private RelayCommand _showMainControlCommand;
        private RelayCommand _showProgressControlCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="timeService">A valid Timeservice.</param>
        public MainViewModel(ITimeService timeService)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                Caption = "SampleApp Design";
            }
            else
            {
                Caption = "SampleApp";
            }
            this._timeService = timeService;
            this._timeService.CurrentDateTime += (sender, e) =>
            {
                if (e is DateTimeEventArgs args)
                {
                    DateTime = args.CurrentDateTime;
                }
            };
        }

        /// <summary>
        /// Gets or sets the Caption property.
        /// </summary>
        /// <value>Holds the Caption of the View.</value>
        public string Caption
        {
            get => _caption;
            set => SetProperty(ref _caption, value);
        }

        /// <summary>
        /// Gets or sets the DateTime property
        /// </summary>
        /// <value>Holds the data from the TimeService</value>
        public DateTime DateTime
        {
            get => _dateTime;
            set => SetProperty(ref _dateTime, value);
        }

        /// <summary>
        /// Gets the CloseApplicationCommmand property.
        /// </summary>
        /// <value>Holds the command to close the application.</value>
        public RelayCommand CloseApplicationCommand => this._closeApplicationCommand ??= new RelayCommand(
            () =>
            {
                WeakReferenceMessenger.Default.Send(new CloseApplicationMessage());
            });
        
        /// <summary>
        /// Gets the ShowMainControlCommand property.
        /// </summary>
        /// <value>Holds the command to show the main content control.</value>
        public RelayCommand ShowMainControlCommand => this._showMainControlCommand ??= new RelayCommand(
            () =>
            {
                WeakReferenceMessenger.Default.Send(new ShowHomeControlMessage());
            });

        /// <summary>
        /// Gets the ShowProgressControlCommand property.
        /// </summary>
        /// <value>Holds the command to show the progress content control.</value>
        public RelayCommand ShowProgressControlCommand => this._showProgressControlCommand ??= new RelayCommand(() =>
        {
            WeakReferenceMessenger.Default.Send(new ShowProgressControlMessage());
        });
    }
}