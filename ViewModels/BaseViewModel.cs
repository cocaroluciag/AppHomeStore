using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AppHomeStore.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string? title;


        protected async Task ExecuteBusyTask(Func<Task> task)
        {
            IsBusy = true;
            try
            {
                await task();
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Método para establecer el título
        public void SetTitle(string title)
        {
            Title = title;
        }
    }
}
