namespace SampleApp.Model
{
    using System;
    using CommunityToolkit.Mvvm.ComponentModel;

    public class PublicHoliday : ObservableObject
    {
        private string _bundesland = String.Empty;
        private DateTime _date;
        private string _name = String.Empty;
        private string _hint = string.Empty;

        public string Bundesland
        {
            get => this._bundesland;
            set => SetProperty(ref this._bundesland, value);
        }

        public DateTime Date
        {
            get => this._date;
            set => SetProperty(ref this._date, value);
        }

        public string Name
        {
            get => this._name;
            set => SetProperty(ref this._name, value);
        }

        public string Hint
        {
            get => _hint;
            set => SetProperty(ref this._hint, value);
        }
    }
}