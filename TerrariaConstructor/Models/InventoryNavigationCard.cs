using System.Collections.Generic;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wpf.Ui.Common;

namespace TerrariaConstructor.Models;

public class InventoryNavigationCard : ReactiveObject
{
    public string Name { get; init; }
    public SymbolRegular Icon { get; init; }
    [Reactive]
    public List<Coin> Coins { get; set; }
    public string Description { get; init; }
    public string Link { get; init; }
}