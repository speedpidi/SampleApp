namespace SampleApp.Logic
{
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;

    public class ProgressViewModel : ObservableObject
    {
        private int _progress = 0;
        private bool _isBusy = false;
        private AsyncRelayCommand _startCommand;
        private RelayCommand _resetCommand;


        public int Progress
        {
            get => this._progress;
            set => SetProperty(ref this._progress, value);
        }

        public bool IsBusy
        {
            get => this._isBusy;
            set
            {
                if (this.SetProperty(ref this._isBusy, value))
                {
                    ResetCommand.NotifyCanExecuteChanged();
                    StartCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public RelayCommand ResetCommand => this._resetCommand ??= new RelayCommand(
            () =>
            {
                IsBusy = true;
                Progress = 0;
                IsBusy = false;
            },
            () => !IsBusy);

        public AsyncRelayCommand StartCommand => this._startCommand ??= new AsyncRelayCommand(
            async () =>
            {
                IsBusy = true;
                Progress = 0;
                int counter = 0;
                while (counter != 100)
                {
                    counter++;
                    Progress++;
                    await Task.Delay(10);
                }
                IsBusy = false;
            },
            () => !IsBusy);
    }
}