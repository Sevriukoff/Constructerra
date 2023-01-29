using ReactiveUI;

namespace TerrariaConstructor.ViewModels;

public class MainViewModel : ReactiveObject
{
    public CharacteristicViewModel CharacteristicViewModel { get; set; }

    public MainViewModel(CharacteristicViewModel characteristicViewModel)
    {
        CharacteristicViewModel = characteristicViewModel;
    }
}