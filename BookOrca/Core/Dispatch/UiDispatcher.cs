using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BookOrca.Core.Dispatch;

public class UiDispatcher : IDispatcher
{
    private static Dispatcher Dispatcher => Application.Current.Dispatcher;

    public void Invoke(Action action)
    {
        Dispatcher.Invoke(action);
    }

    public async Task BeginInvoke(Action action)
    {
        await Dispatcher.BeginInvoke(action);
    }
}