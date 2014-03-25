using DIPS.ViewModel.Commands;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DIPS.ViewModel.UserInterfaceVM
{
    /// <summary>
    /// Represents the <see cref="PostProcessingOptions"/> used to
    /// specify one handler.
    /// </summary>
    public class SingleHandlerOptions : PostProcessingOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleHandlerOptions"/>
        /// class.
        /// </summary>
        public SingleHandlerOptions()
        {

        }


        /// <summary>
        /// In a dervied class, gets an identifier for this kind of
        /// <see cref="PostProcessingOptions"/>.
        /// </summary>
        public override string Identifier
        {
            get
            {
                return "Single";
            }
        }

        /// <summary>
        /// In a dervied class, creates the <see cref="IJobResultsHandler"/>
        /// represented by the settings within this <see cref="PostProcessingOptions"/>.
        /// </summary>
        /// <returns>A <see cref="IJobResultsHandler"/> represented by the properties
        /// within this <see cref="PostProcessingOptions"/></returns>
        public override IJobResultsHandler CreateHandler()
        {
            if( SelectedHandler != null )
            {
                return (IJobResultsHandler)SelectedHandler.Handler.Clone();
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Occurs when the selected handler is changed.
        /// </summary>
        protected override void OnSelectedHandlerChanged()
        {
            base.OnSelectedHandlerChanged();

            IsValid = SelectedHandler != null;
        }
    }
}
