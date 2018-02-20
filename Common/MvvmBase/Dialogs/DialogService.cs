using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Interop;

namespace Common.MvvmBase.Dialogs
{
	/// <summary>
	/// Class responsible for abstracting ViewModels from Views.
	/// </summary>
	class DialogService : IDialogService
	{
		private readonly HashSet<FrameworkElement> views;
		private readonly IWindowViewModelMappings windowViewModelMappings;


		/// <summary>
		/// Initializes a new instance of the <see cref="DialogService"/> class.
		/// </summary>
		/// <param name="windowViewModelMappings">
		/// The window ViewModel mappings. Default value is null.
		/// </param>
		public DialogService(IWindowViewModelMappings windowViewModelMappings = null)
		{
			this.windowViewModelMappings = windowViewModelMappings;

			views = new HashSet<FrameworkElement>();
		}


		#region IDialogService Members

		/// <summary>
		/// Gets the registered views.
		/// </summary>
		public ReadOnlyCollection<FrameworkElement> Views
		{
			get { return new ReadOnlyCollection<FrameworkElement>(views.ToList()); }
		}      

		/// <summary>
		/// Registers a View.
		/// </summary>
		/// <param name="view">The registered View.</param>
		public void Register(FrameworkElement view)
		{
			// Get owner window
			Window owner = GetOwner(view);
			if (owner == null)
			{
				// Perform a late register when the View hasn't been loaded yet.
				// This will happen if e.g. the View is contained in a Frame.
				view.Loaded += LateRegister;
				return;
			}

			// Register for owner window closing, since we then should unregister View reference,
			// preventing memory leaks
			owner.Closed += OwnerClosed;

			views.Add(view);
		}                                

		/// <summary>
		/// Unregisters a View.
		/// </summary>
		/// <param name="view">The unregistered View.</param>
		public void Unregister(FrameworkElement view)
		{
			views.Remove(view);
		}                                  

		/// <summary>
		/// Shows a dialog.
		/// </summary>
		/// <remarks>
		/// The dialog used to represent the ViewModel is retrieved from the registered mappings.
		/// </remarks>
		/// <param name="ownerViewModel">
		/// A ViewModel that represents the owner window of the dialog.
		/// </param>
		/// <param name="viewModel">The ViewModel of the new dialog.</param>
		/// <returns>
		/// A nullable value of type bool that signifies how a window was closed by the user.
		/// </returns>
		public bool? ShowDialog(object ownerViewModel, object viewModel)
		{
			Type dialogType = windowViewModelMappings.GetWindowTypeFromViewModelType(viewModel.GetType());
			return ShowDialog(ownerViewModel, viewModel, dialogType);
		}

        public CustomWindow ShowTrayWindow(object ownerViewModel, object viewModel)
        {
            Type dialogType = windowViewModelMappings.GetWindowTypeFromViewModelType(viewModel.GetType());
            return ShowTrayWindow(ownerViewModel, viewModel, dialogType);
        }

		/// <summary>
		/// Shows a dialog.
		/// </summary>
		/// <param name="ownerViewModel">
		/// A ViewModel that represents the owner window of the dialog.
		/// </param>
		/// <param name="viewModel">The ViewModel of the new dialog.</param>
		/// <typeparam name="T">The type of the dialog to show.</typeparam>
		/// <returns>
		/// A nullable value of type bool that signifies how a window was closed by the user.
		/// </returns>
		public bool? ShowDialog<T>(object ownerViewModel, object viewModel) where T : Window
		{
			return ShowDialog(ownerViewModel, viewModel, typeof(T));
		}


		/// <summary>
		/// Shows a message box.
		/// </summary>
		/// <param name="ownerViewModel">
		/// A ViewModel that represents the owner window of the message box.
		/// </param>
		/// <param name="messageBoxText">A string that specifies the text to display.</param>
		/// <param name="caption">A string that specifies the title bar caption to display.</param>
		/// <param name="button">
		/// A MessageBoxButton value that specifies which button or buttons to display.
		/// </param>
		/// <param name="icon">A MessageBoxImage value that specifies the icon to display.</param>
		/// <returns>
		/// A MessageBoxResult value that specifies which message box button is clicked by the user.
		/// </returns>
		public MessageBoxResult ShowMessageBox(
			object ownerViewModel,
			string messageBoxText,
			string caption,
			MessageBoxButton button,
			MessageBoxImage icon)
		{
			return MessageBox.Show(FindOwnerWindow(ownerViewModel), messageBoxText, caption, button, icon);
		}


		/// <summary>
		/// Shows the OpenFileDialog.
		/// </summary>
		/// <param name="ownerViewModel">
		/// A ViewModel that represents the owner window of the dialog.
		/// </param>
		/// <param name="openFileDialog">The interface of a open file dialog.</param>
		/// <returns>DialogResult.OK if successful; otherwise DialogResult.Cancel.</returns>
		//public DialogResult ShowOpenFileDialog(object ownerViewModel, IOpenFileDialog openFileDialog)
		//{
			// Create OpenFileDialog with specified ViewModel
		//	OpenFileDialog dialog = new OpenFileDialog(openFileDialog);

			// Show dialog
		//	return dialog.ShowDialog(new WindowWrapper(FindOwnerWindow(ownerViewModel)));
		//}


		/// <summary>
		/// Shows the FolderBrowserDialog.
		/// </summary>
		/// <param name="ownerViewModel">
		/// A ViewModel that represents the owner window of the dialog.
		/// </param>
		/// <param name="folderBrowserDialog">The interface of a folder browser dialog.</param>
		/// <returns>The DialogResult.OK if successful; otherwise DialogResult.Cancel.</returns>
		//public DialogResult ShowFolderBrowserDialog(object ownerViewModel, IFolderBrowserDialog folderBrowserDialog)
		//{
			// Create FolderBrowserDialog with specified ViewModel
		//	FolderBrowserDialog dialog = new FolderBrowserDialog(folderBrowserDialog);

			// Show dialog
		//	return dialog.ShowDialog(new WindowWrapper(FindOwnerWindow(ownerViewModel)));
		//}

		#endregion


		#region Attached properties

		/// <summary>
		/// Attached property describing whether a FrameworkElement is acting as a View in MVVM.
		/// </summary>
		public static readonly DependencyProperty IsRegisteredViewProperty =
			DependencyProperty.RegisterAttached(
			"IsRegisteredView",
			typeof(bool),
			typeof(DialogService),
			new UIPropertyMetadata(IsRegisteredViewPropertyChanged));


		/// <summary>
		/// Gets value describing whether FrameworkElement is acting as View in MVVM.
		/// </summary>
		public static bool GetIsRegisteredView(FrameworkElement target)
		{
			return (bool)target.GetValue(IsRegisteredViewProperty);
		}


		/// <summary>
		/// Sets value describing whether FrameworkElement is acting as View in MVVM.
		/// </summary>
		public static void SetIsRegisteredView(FrameworkElement target, bool value)
		{
			target.SetValue(IsRegisteredViewProperty, value);
		}


		/// <summary>
		/// Is responsible for handling IsRegisteredViewProperty changes, i.e. whether
		/// FrameworkElement is acting as View in MVVM or not.
		/// </summary>
		private static void IsRegisteredViewPropertyChanged(DependencyObject target,
			DependencyPropertyChangedEventArgs e)
		{
			// The Visual Studio Designer or Blend will run this code when setting the attached
			// property, however at that point there is no IDialogService registered
			// in the ServiceLocator which will cause the Resolve method to throw a ArgumentException.
			if (DesignerProperties.GetIsInDesignMode(target)) return;

			FrameworkElement view = target as FrameworkElement;
			if (view != null)
			{
				// Cast values
				bool newValue = (bool)e.NewValue;
				bool oldValue = (bool)e.OldValue;

				if (newValue)
				{
					ServiceLocator.Resolve<IDialogService>().Register(view);
				}
				else
				{
					ServiceLocator.Resolve<IDialogService>().Unregister(view);
				}
			}
		}

		#endregion

	    readonly HashSet<Window> _dialogs = new HashSet<Window>();

        private void OnDialogClosed(object sender, EventArgs e)
        {
            var dialog = sender as Window;
            if (dialog != null)
            {
                dialog.Closed -= OnDialogClosed;
                _dialogs.Remove(dialog);
            }
        }

        /// <summary>
        /// Close all dialogs
        /// </summary>
	    public void CloseAllDialogs()
	    {
            foreach (var dialog in _dialogs)
	        {
                dialog.Closed -= OnDialogClosed; 
                dialog.Close();                
            }
            _dialogs.Clear();
	    }

		/// <summary>
		/// Shows a dialog.
		/// </summary>
		/// <param name="ownerViewModel">
		/// A ViewModel that represents the owner window of the dialog.
		/// </param>
		/// <param name="viewModel">The ViewModel of the new dialog.</param>
		/// <param name="dialogType">The type of the dialog.</param>
		/// <returns>
		/// A nullable value of type bool that signifies how a window was closed by the user.
		/// </returns>
		private bool? ShowDialog(object ownerViewModel, object viewModel, Type dialogType)
		{
			// Create dialog and set properties
			var dialog = (Window)Activator.CreateInstance(dialogType);
			dialog.Owner = FindOwnerWindow(ownerViewModel);
			dialog.DataContext = viewModel;
		    dialog.Closed += OnDialogClosed;
		    _dialogs.Add(dialog);

			// Show dialog
			return dialog.ShowDialog();
		}

        private CustomWindow ShowTrayWindow(object ownerViewModel, object viewModel, Type dialogType)
        {
            // Create dialog and set properties
            var dialog = (Window)Activator.CreateInstance(dialogType);
            //dialog.Owner = FindOwnerWindow(ownerViewModel);
            dialog.DataContext = viewModel;
            dialog.Topmost = true;

            dialog.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
            var bounds = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            dialog.Top = bounds.Height - dialog.Height;
            dialog.Left = bounds.Width - dialog.Width;

            dialog.Top = System.Windows.SystemParameters.WorkArea.Height - dialog.Height;
            dialog.Left = System.Windows.SystemParameters.WorkArea.Width - dialog.Width;

            // Show dialog
            dialog.Show();

            return new CustomWindow(dialog);
        }                

		/// <summary>
		/// Finds window corresponding to specified ViewModel.
		/// </summary>
		private Window FindOwnerWindow(object viewModel)
		{
			var view = views.SingleOrDefault(v => ReferenceEquals(v.DataContext, viewModel));
			if (view == null)
			{
				throw new ArgumentException("Viewmodel is not referenced by any registered View.");
			}

			// Get owner window
			Window owner = view as Window;
			if (owner == null)
			{
				owner = Window.GetWindow(view);
			}

			// Make sure owner window was found
			if (owner == null)
			{
				throw new InvalidOperationException("View is not contained within a Window.");
			}

			return owner;
		}


		/// <summary>
		/// Callback for late View register. It wasn't possible to do a instant register since the
		/// View wasn't at that point part of the logical nor visual tree.
		/// </summary>
		private void LateRegister(object sender, RoutedEventArgs e)
		{
			FrameworkElement view = sender as FrameworkElement;
			if (view != null)
			{
				// Unregister loaded event
				view.Loaded -= LateRegister;

				// Register the view
				Register(view);
			}
		}        

		/// <summary>
		/// Handles owner window closed, View service should then unregister all Views acting
		/// within the closed window.
		/// </summary>
		private void OwnerClosed(object sender, EventArgs e)
		{
			Window owner = sender as Window;
			if (owner != null)
			{
				// Find Views acting within closed window
				IEnumerable<FrameworkElement> windowViews =
					from view in views
					where Window.GetWindow(view) == owner
					select view;

				// Unregister Views in window
				foreach (FrameworkElement view in windowViews.ToArray())
				{
					Unregister(view);
				}
			}
		}        

		/// <summary>
		/// Gets the owning Window of a view.
		/// </summary>
		/// <param name="view">The view.</param>
		/// <returns>The owning Window if found; otherwise null.</returns>
		private Window GetOwner(FrameworkElement view)
		{
			return view as Window ?? Window.GetWindow(view);
		}           

        public IDialog ShowCustomCloseDialog(object ownerViewModel, object viewModel)
        {
            Type dialogType = windowViewModelMappings.GetWindowTypeFromViewModelType(viewModel.GetType());            

            Window dialog = (Window)Activator.CreateInstance(dialogType);

            if ( !(dialog is ICustomClosedWindow))
            {
                throw new Exception("Please, implement interface ICustomClosedWindow to Window before using this method");
            }

            dialog.Owner = FindOwnerWindow(ownerViewModel);
            dialog.DataContext = viewModel;            

            return new DialogWithResult(dialog);
        }           
    }

    public class DialogWithResult : IDialog
    {
        private readonly ICustomClosedWindow _wnd;

        public DialogWithResult(Window wnd)
        { 
            _wnd = (ICustomClosedWindow) wnd;
        }        

        public void Exit()
        {
            if (_wnd == null)
                return;

            var handle = new WindowInteropHelper((Window)_wnd).Handle;
            if (handle == IntPtr.Zero)
                return;

            _wnd.Exit();
        }

        public bool? ShowDialog()
        {
            if (_wnd == null)
                return null;

            if (((Window)_wnd).Visibility == Visibility.Visible)
            {
                ((Window)_wnd).Activate();
                return null;
            }

            return ((Window)_wnd).ShowDialog();
        }      
    }

    public class CustomWindow : IWindow
    {
        private readonly Window _wnd;

        public CustomWindow(Window wnd)
        {
            _wnd = wnd;
        }

        public void Exit()
        {
            if (_wnd == null)
                return;

            var handle = new WindowInteropHelper(_wnd).Handle;
            if (handle == IntPtr.Zero)
                return;

            _wnd.Close();
        }
    }
}
