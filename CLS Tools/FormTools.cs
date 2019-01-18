using System;
using System.Threading;
using System.Windows.Forms;

namespace CLSTools
{
    /// <summary>
    /// Tools for using Windows Forms
    /// </summary>
    public static class FormTools
    {
        private static void ApplicationRunProc(object state)
        {
            Application.Run(state as Form);
        }

        /// <summary>
        /// Runs a form object in a seperate thread
        /// </summary>
        /// <param name="form">The form to run</param>
        /// <param name="isBackground">Whether to focus on the form</param>
        public static void RunInNewThread(this Form form, bool isBackground)
        {
            if (form == null)
                throw new ArgumentNullException("form");
            if (form.IsHandleCreated)
                throw new InvalidOperationException("Form is already running.");
            Thread thread = new Thread(ApplicationRunProc);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = isBackground;
            thread.Start(form);
        }
    }
}
