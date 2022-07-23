using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Threading.Tasks;

namespace MeCommand
{
    internal partial class MainViewModel : ObservableObject
    {
        private bool nameSwitch = false;


        [ObservableProperty]
        [AlsoNotifyCanExecuteFor(nameof(Button2Command))]
        [AlsoNotifyCanExecuteFor(nameof(Button3Command))]
        [AlsoNotifyCanExecuteFor(nameof(AsyncButton4Command))]
        private string? _name1;

        [ObservableProperty]
        private string? _name2;

        #region Commands

        // Button1Command Just execute a function...
        [ICommand]
        private void Button1()
        {
            Name1 = nameSwitch ? "Uriah Heep" : "Deep Purple";
            nameSwitch = !nameSwitch;
        }

        // Button2Command
        private bool CanButton2Execute() =>
            !string.IsNullOrWhiteSpace(Name1);
        

        [ICommand(CanExecute = nameof(CanButton2Execute))]
        private void Button2()
        {
            Name1 = nameSwitch ? "Uriah Heep" : "Deep Purple";
            nameSwitch = !nameSwitch;
        }

        // Button3Command
        private bool CanButton3Execute(string? obj) =>
            !string.IsNullOrWhiteSpace(Name1);


        [ICommand(CanExecute = nameof(CanButton3Execute))]
        private void Button3(string? name)
        {
            Name2 = name;
        }

        #endregion Commands

        #region Async Commands

        // AsyncButton4Command
        private bool CanAsyncButton4Execute(string? obj) =>
            !string.IsNullOrWhiteSpace(Name1);


        [ICommand(CanExecute = nameof(CanAsyncButton4Execute))]
        private async Task<string> AsyncButton4(string? name)
        {
            Task<string> task;
            if (string.IsNullOrEmpty(name))
                return await Task<string>.FromResult(string.Empty);
            await Task.Delay(5000);
            task = Task.Run(() => Name2 = name);
            await task;
            return task.Result;
        }

        #endregion Async Commands
    }
}