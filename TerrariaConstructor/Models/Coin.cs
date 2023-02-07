using TerrariaConstructor.Common.Enums;

namespace TerrariaConstructor.Models;

public class Coin
{
    public byte[] Image { get; set; }
    public CoinType Type { get; set; }
    public int Amount { get; set; }
}