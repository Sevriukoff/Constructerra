namespace TerrariaConstructor.Models;

public class InventoriesModel
{
    public Item[] Inventory { get; set; } = new Item[50];
    public Item[] MiscEquip { get; set; } = new Item[5];
    public Item[] MiscDye { get; set; } = new Item[5];
    public Item[] Bank1 { get; set; } = new Item[40];
    public Item[] Bank2 { get; set; } = new Item[40];
    public Item[] Bank3 { get; set; } = new Item[40];
    public Item[] Bank4 { get; set; } = new Item[40];
}