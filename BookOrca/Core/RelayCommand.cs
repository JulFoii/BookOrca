using System;
using System.Windows.Input;

namespace BookOrca.Core;

public class RelayCommand : ICommand
{
    private readonly Func<object?, bool> canExecute = _ => true;
    private readonly Action<object?> execute;

    public bool CanExecute(object? parameter = null)
    {
        return canExecute(parameter);
    }

    public void Execute(object? parameter = null)
    {
        if (CanExecute()) execute(parameter);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    #region Ctors

    public RelayCommand(Action<object?> execute)
    {
        this.execute = execute;
    }

    public RelayCommand(Action<object?> execute, Func<object?, bool> canExecute)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public RelayCommand(Action<object?> execute, Func<bool> canExecute)
    {
        this.execute = execute;
        this.canExecute = _ => canExecute();
    }

    public RelayCommand(Action execute)
    {
        this.execute = _ => execute();
    }

    public RelayCommand(Action execute, Func<bool> canExecute)
    {
        this.execute = _ => execute();
        this.canExecute = _ => canExecute();
    }

    public RelayCommand(Action execute, Func<object?, bool> canExecute)
    {
        this.execute = _ => execute();
        this.canExecute = canExecute;
    }

    #endregion
}