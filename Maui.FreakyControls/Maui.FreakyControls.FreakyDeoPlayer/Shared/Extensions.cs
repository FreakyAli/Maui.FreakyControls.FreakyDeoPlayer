using System;
using System.Windows.Input;

namespace Maui.FreakyControls;

public static class Extensions
{
    public static void ExecuteCommandIfAvailable(this ICommand command, object parameter = null)
    {
        if (command?.CanExecute(parameter) == true)
        {
            command.Execute(parameter);
        }
    }

    public static void AddFreakyDeoHandlers(this IMauiHandlersCollection handlers)
    {
        handlers.AddHandler(typeof(FreakyDeoPlayer),typeof(FreakyDeoPlayerHandler));
    }
}

