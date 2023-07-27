using System.Collections.Generic;
using System.Threading.Tasks;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Interfaces;

public interface IItemsRepository
{
    Item GetById(int id, bool loadImage = true);
    Item GetByInternalName(string internalName);
    IEnumerable<Item> GetAll();
}