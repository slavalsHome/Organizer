using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.MvvmBase.Dialogs;
using Common.ViewModel;
using Organizer.ViewModel;

namespace Organizer.View
{
    public class WindowViewModelMappings : IWindowViewModelMappings
    {
        private readonly IDictionary<Type, Type> _mappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowViewModelMappings"/> class.
        /// </summary>
        public WindowViewModelMappings()
        {
            _mappings = new Dictionary<Type, Type>
			{
                { typeof(MessageDialogViewModel), typeof(MessageDialog) },
            };
        }

        /// <summary>
        /// Gets the window type based on registered ViewModel type.
        /// </summary>
        /// <param name="viewModelType">The type of the ViewModel.</param>
        /// <returns>The window type based on registered ViewModel type.</returns>
        public Type GetWindowTypeFromViewModelType(Type viewModelType)
        {
            return _mappings[viewModelType];
        }
    }
}
