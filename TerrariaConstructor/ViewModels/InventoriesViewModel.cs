using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;
using TerrariaConstructor.Models;
using Wpf.Ui.Common;

namespace TerrariaConstructor.ViewModels;

public class InventoriesViewModel : ReactiveObject
{
    public InventoriesModel Model { get; set; }

    public ICollection<NavigationCard> NavigationCards => new ObservableCollection<NavigationCard>
    {
        new()
        {
            Name = "Основной инветарь",
            Icon = SymbolRegular.Navigation24,
            Description = "Основной инвентарь игрока, где храниться всякая шелуха.",
            Link = "MainInventory"
        },
        new()
        {
            Name = "Копилка",
            Icon = SymbolRegular.Navigation24,
            Description = "Свиное пездное рыло",
            Link = "Characteristics"
        },
        new()
        {
            Name = "Сейф",
            Icon = SymbolRegular.Navigation24,
            Description = "Сейф",
            Link = "MainInventory"
        },
        new()
        {
            Name = "Кузница",
            Icon = SymbolRegular.Navigation24,
            Description = "Кузница",
            Link = "MainInventory"
        },
        new()
        {
            Name = "Безднонный мешок",
            Icon = SymbolRegular.Navigation24,
            Description = "Безднонный мешок",
            Link = "MainInventory"
        }
    };
    
    public InventoriesViewModel(InventoriesModel model)
    {
        Model = model;
    }

    public void Update()
    {
        
    }
}