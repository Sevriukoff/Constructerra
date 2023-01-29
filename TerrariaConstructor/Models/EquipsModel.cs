namespace TerrariaConstructor.Models;

public class EquipsModel
{
    public Item[] Purse { get; set; } = new Item[4];
    public Item[] Ammo { get; set; } = new Item[4];
    public Item[] Armor { get; set; } = new Item[20];
    public Item[] Dye { get; set; } = new Item[10];

    public Loadout[] Loadouts { get; set; } = new Loadout[3];
    public int CurrentLoadoutIndex { get; set; }

    public class Loadout
    {
        public Item[] Armor { get; set; } = new Item[20];
        public Item[] Dye { get; set; } = new Item[10];
        public bool[] IsHide { get; set; } = new bool[10];
    }
}