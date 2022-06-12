namespace SampleApp.Services
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using Interfaces;
    using Model.EventArgs;

    public class TimeService : ITimeService
    {
        private readonly  BackgroundWorker _worker;

        #region event

        public event EventHandler CurrentDateTime;

        #endregion

        #region ctor
        
        public TimeService()
        {
            this._worker = new BackgroundWorker()
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = false
            };

            this._worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            this._worker.RunWorkerAsync();
        }

        #endregion

        #region event handlers

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                CurrentDateTime?.Invoke(this, new DateTimeEventArgs
                {
                    CurrentDateTime = DateTime.Now
                });
                Thread.Sleep(TimeSpan.FromMilliseconds(1000));
            }
        }

        #endregion
    }
}