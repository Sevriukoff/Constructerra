using Autofac;

namespace TerrariaConstructor.ViewModels.Base;

public class ViewModelLocator
{
    public MainViewModel MainViewModel => App.Container.Resolve<MainViewModel>();
}