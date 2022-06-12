namespace SampleApp.Logic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Windows;
    using System.Windows.Data;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Model;

    public class HomeViewModel : ObservableObject
    {
        private bool _isBusy = false;
        private AsyncRelayCommand _loadDataCommand;
        private RelayCommand _clearCommand;
        private ListCollectionView _dataView;

        public HomeViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                DataView = new ListCollectionView(new List<PublicHoliday>()
                    { new PublicHoliday() { Bundesland = "Hessen", Date = DateTime.Now, Name = "Blubb", Hint = "Hinweis1" } });
            }
        }

        public bool IsBusy
        {
            get => this._isBusy;
            set
            {
                if (SetProperty(ref this._isBusy, value))
                {
                    LoadDataCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public ListCollectionView DataView
        {
            get => this._dataView;
            set => SetProperty(ref this._dataView, value);
        }

        public AsyncRelayCommand LoadDataCommand => this._loadDataCommand ??= new AsyncRelayCommand(async () =>
            {
                this.IsBusy = true;
                try
                {
                    var responseFromServer = string.Empty;
                    var publicHolidays = new List<PublicHoliday>();
                    using (var client = new HttpClient())
                    {
                        var response = await client.GetAsync($"https://feiertage-api.de/api/?jahr={DateTime.Now.Year}");
                        responseFromServer = await new StreamReader(await response.Content.ReadAsStreamAsync()).ReadToEndAsync();
                    }

                    if (!string.IsNullOrWhiteSpace(responseFromServer))
                    {
                        var germanSpecialDaysDict =
                            JsonSerializer
                                .Deserialize<Dictionary<string, Dictionary<string, Dictionary<string, string>>>>(
                                    responseFromServer);
                        publicHolidays.AddRange(
                            from bundesland in germanSpecialDaysDict
                            let blvalue = bundesland.Key
                            from bl in bundesland.Value
                            select new PublicHoliday()
                            {
                                Bundesland = blvalue,
                                Name = bl.Key,
                                Hint = bl.Value.Values.ElementAt(1),
                                Date = DateTime.Parse(bl.Value.Values.ElementAt(0))
                            });
                        DataView = new ListCollectionView(publicHolidays);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    IsBusy = false;
                }
            },
            () => !IsBusy);

        public RelayCommand ClearCommand => this._clearCommand ??= new RelayCommand(() =>
        {
            DataView = new ListCollectionView(new List<PublicHoliday>());
        });
    }
}