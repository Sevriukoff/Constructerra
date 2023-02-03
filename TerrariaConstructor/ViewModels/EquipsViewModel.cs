using ReactiveUI;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.ViewModels;

public class EquipsViewModel : ReactiveObject
{
    public EquipsModel Model { get; set; }
    
    public EquipsViewModel(EquipsModel model)
    {
        Model = model;
    }

    public void Update()
    {
        
    }
}