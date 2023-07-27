using System.Reactive;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Controls;

public class BuffEditorPresenter : Control
{
    public static readonly DependencyProperty SelectedBuffProperty = DependencyProperty.Register(
        nameof(SelectedBuff), typeof(Buff), typeof(BuffEditorPresenter), new PropertyMetadata(default(Buff)));

    public Buff SelectedBuff
    {
        get { return (Buff) GetValue(SelectedBuffProperty); }
        set { SetValue(SelectedBuffProperty, value); }
    }

    public static readonly DependencyProperty DeleteSelectedBuffCommandProperty = DependencyProperty.Register(
        nameof(DeleteSelectedBuffCommand), typeof(ReactiveCommand<Unit, Unit>), typeof(BuffEditorPresenter), new PropertyMetadata(default(ReactiveCommand<Unit, Unit>)));

    public ReactiveCommand<Unit, Unit> DeleteSelectedBuffCommand => (ReactiveCommand<Unit, Unit>) GetValue(DeleteSelectedBuffCommandProperty);

    public BuffEditorPresenter()
    {
        SetValue(DeleteSelectedBuffCommandProperty,
            ReactiveCommand.Create(() =>
            {
                SelectedBuff.Id = 0;
                SelectedBuff.Name = null;
                SelectedBuff.ImageId = 0;
                SelectedBuff.Image = null;
                SelectedBuff.IsNegative = false;
                SelectedBuff.BaseDurationTime = null;
                SelectedBuff.CurrentDurationTime = null;
                SelectedBuff.Sources = null;
                SelectedBuff.ItemSources = null;
                SelectedBuff.ToolTip = null;
                SelectedBuff.EffectDescription = null;
                SelectedBuff.WikiUrl = null;
            }));
    }
}