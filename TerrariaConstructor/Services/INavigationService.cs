using System;
using ReactiveUI;

namespace TerrariaConstructor.Services;

public interface INavigationService : Wpf.Ui.Contracts.INavigationService
{
    bool NavigateWithDataContext(Type pageType, ReactiveObject dataContext);
}