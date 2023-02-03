using ReactiveUI;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.ViewModels;

public class MainViewModel : ReactiveObject
{
    public PlayerModel PlayerModel { get; }
    public CharacteristicsViewModel CharacteristicsViewModel { get; set; }
    public EquipsViewModel EquipsViewModel { get; set; }
    public InventoriesViewModel InventoriesViewModel { get; set; }

    public MainViewModel(PlayerModel playerModel,
        CharacteristicsViewModel characteristicsViewModel,
        EquipsViewModel equipsViewModel,
        InventoriesViewModel inventoriesViewModel)
    {
        PlayerModel = playerModel;
        CharacteristicsViewModel = characteristicsViewModel;
        EquipsViewModel = equipsViewModel;
        InventoriesViewModel = inventoriesViewModel;

        PlayerModel.PlayerUpdated += () =>
        {
            CharacteristicsViewModel.Update();
            EquipsViewModel.Update();
            InventoriesViewModel.Update();
        };

        //MessageBus.Current.SendMessage();
        
        //MessageBus.Current.RegisterMessageSource(this);
    }
}