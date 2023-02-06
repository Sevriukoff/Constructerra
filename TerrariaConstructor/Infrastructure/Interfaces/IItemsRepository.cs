using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Interfaces;

public interface IItemsRepository
{
    Item GetById(int id, bool loadImage = true);
    Item GetByName(string name);
}