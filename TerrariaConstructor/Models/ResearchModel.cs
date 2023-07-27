using System.Collections.Generic;

namespace TerrariaConstructor.Models;

public class ResearchModel
{
    public IList<Item> Items { get; set; } = new List<Item>();
}