namespace Common.MvvmBase.Dialogs
{
    public interface IWindow
    {
        void Exit();
    }

    public interface IDialog : IWindow
    {
        bool? ShowDialog();
    }    
}
