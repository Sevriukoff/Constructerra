using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.ViewModels;

public class BuffsViewModel
{
    public ObservableCollection<Buff> Buffs { get; set; }

    private readonly BuffsModel _model;

    public BuffsViewModel(BuffsModel model)
    {
        _model = model;
        Buffs = new ObservableCollection<Buff>(_model.Buffs);
    }
}