namespace SampleApp.Interfaces
{
    using System;

    /// <summary>
    /// Interface definition for TimeService
    /// </summary>
    public interface ITimeService
    {  
        /// <summary>
        /// Event to transport data.
        /// </summary>
        event EventHandler CurrentDateTime;
    }
}