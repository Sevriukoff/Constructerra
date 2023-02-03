using ReactiveUI;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.ViewModels;

public class InventoriesViewModel : ReactiveObject
{
    public InventoriesModel Model { get; set; }
    
    public InventoriesViewModel(InventoriesModel model)
    {
        Model = model;
    }

    public void Update()
    {
        
    }
}