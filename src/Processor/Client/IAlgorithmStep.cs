namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents a single major step within an <see cref="Algorithm"/>.
    /// </summary>
    public interface IAlgorithmStep
    {
        /// <summary>
        /// Executes this step of the algorithm with the information available within the
        /// <see cref="JobState"/>.
        /// </summary>
        /// <param name="stateOfJob">An instance of the <see cref="JobState"/> class used
        /// to provide this step with execution information.</param>
        void Run( JobState stateOfJob );
    }
}
