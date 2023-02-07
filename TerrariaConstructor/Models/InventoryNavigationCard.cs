using System.Collections.Generic;
using Wpf.Ui.Common;

namespace TerrariaConstructor.Models;

public class InventoryNavigationCard
{
    public string Name { get; init; }
    public SymbolRegular Icon { get; init; }
    public List<Coin> Coins { get; set; }
    public string Description { get; init; }
    public string Link { get; init; }
}