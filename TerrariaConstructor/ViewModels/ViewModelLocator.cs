using Autofac;

namespace TerrariaConstructor.ViewModels;

public class ViewModelLocator
{
    public MainViewModel MainViewModel => App.Container.Resolve<MainViewModel>();
}