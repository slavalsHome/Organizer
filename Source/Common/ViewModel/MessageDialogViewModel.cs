using Common.MvvmBase;

namespace Common.ViewModel
{
    public class MessageDialogViewModel : BindableObject
    {
        public string Header { get; set; }

        public string Message { get; set; }
    }
}
