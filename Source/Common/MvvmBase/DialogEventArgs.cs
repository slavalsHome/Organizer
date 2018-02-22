using System;


namespace Common.MvvmBase
{
    public class DialogEventArgs : EventArgs
    {
        public ViewModelBase VModel { get; private set; }

        public DialogEventArgs(ViewModelBase vmodel)
        {
            VModel = vmodel;
        }
    }
}
