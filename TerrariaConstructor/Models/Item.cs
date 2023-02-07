namespace TerrariaConstructor.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte[] Image { get; set; }
    public string Description { get; set; }
    public long Buy { get; set; }
    public long Sell { get; set; }
    public byte Prefix { get; set; }
    public int Stack { get; set; }
    public bool IsFavorite { get; set; }
}