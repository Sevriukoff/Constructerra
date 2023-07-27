using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TerrariaConstructor.Common.Events;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.ViewModels;

public class BuffsViewModel : ReactiveObject
{
    [Reactive]
    public ObservableCollection<Buff> Buffs { get; set; }
    public ObservableCollection<Buff> AllBuffs { get; set; }
    [Reactive]
    public Buff SelectedEditBuff { get; set; }
    [Reactive]
    public Buff SelectedAddBuff { get; set; }

    private readonly BuffsModel _model;

    public BuffsViewModel(BuffsModel model)
    {
        _model = model;
        
        Buffs = new ObservableCollection<Buff>(_model.Buffs);
        AllBuffs = new ObservableCollection<Buff>(_model.GetAllBuffs());

        MessageBus.Current.Listen<PlayerUpdatedEvent>()
            .Subscribe(x => Update());

        this.WhenAnyValue(x => x.SelectedEditBuff)
            .Where(x => x is {Id: 0})
            .Subscribe(x =>
            {
                if (SelectedAddBuff != null)
                {
                    var index = Buffs.IndexOf(x);

                    var newBuff = new Buff()
                    {
                        Id = SelectedAddBuff.Id,
                        Name = SelectedAddBuff.Name,
                        ImageId = SelectedAddBuff.ImageId,
                        Image = SelectedAddBuff.Image,
                        IsNegative = SelectedAddBuff.IsNegative,
                        BaseDurationTime = SelectedAddBuff.BaseDurationTime,
                        CurrentDurationTime = SelectedAddBuff.CurrentDurationTime,
                        Sources = SelectedAddBuff.Sources,
                        ItemSources = SelectedAddBuff.ItemSources,
                        ToolTip = SelectedAddBuff.ToolTip,
                        EffectDescription = SelectedAddBuff.EffectDescription,
                        WikiUrl = SelectedAddBuff.WikiUrl
                    };

                    Buffs[index] = newBuff;
                    _model.Buffs[index] = newBuff;
                }
            });
    }

    private void Update()
    {
        Buffs = new ObservableCollection<Buff>(_model.Buffs);
    }
}