using System;
using System.Threading.Tasks;

namespace BookOrca.Core.Dispatch;

public interface IDispatcher
{
    private static IDispatcher? instance;

    public static IDispatcher Instance
    {
        get => instance ??= new UiDispatcher();
        set => instance = value;
    }

    public void Invoke(Action action);

    public Task BeginInvoke(Action action);
}