using ReactiveUI;

namespace TerrariaConstructor.Models;

public class Appearance : ReactiveObject
{
    private bool _isSelected;
    public int Id { get; set; }
    public byte[] Image { get; set; }

    public bool IsSelected
    {
        get => _isSelected;
        set => this.RaiseAndSetIfChanged(ref _isSelected, value);
    }
}