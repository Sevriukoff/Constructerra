namespace TerrariaConstructor.Models;

public class Item
{
    public int Id { get; set; }
    public byte Prefix { get; set; }
    public int Stack { get; set; }
    public bool IsFavorite { get; set; }
}