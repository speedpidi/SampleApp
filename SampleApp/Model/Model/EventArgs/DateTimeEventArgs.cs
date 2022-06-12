namespace SampleApp.Model.EventArgs
{
    using System;

    /// <summary>
    /// Eventargs to transport the time data from the TimeService.
    /// </summary>
    public class DateTimeEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the CurrentTime property.
        /// </summary>
        /// <value>Hold the current <see cref="DateTime"></see> value.</value>
        public DateTime CurrentDateTime { get; set; }
    }
}