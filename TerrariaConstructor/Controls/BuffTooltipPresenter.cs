using System.Windows;
using System.Windows.Controls;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Controls;

public class BuffTooltipPresenter : Control
{
    public static readonly DependencyProperty BuffProperty = DependencyProperty.Register(
        nameof(Buff), typeof(Buff), typeof(BuffTooltipPresenter), new PropertyMetadata(default(Buff)));

    public Buff Buff
    {
        get => (Buff) GetValue(BuffProperty);
        set => SetValue(BuffProperty, value);
    }
}