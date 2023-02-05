using Wpf.Ui.Common;

namespace TerrariaConstructor.Models;

public class NavigationCard
{
    public string Name { get; init; }

    public SymbolRegular Icon { get; init; }

    public string Description { get; init; }

    public string Link { get; init; }
}