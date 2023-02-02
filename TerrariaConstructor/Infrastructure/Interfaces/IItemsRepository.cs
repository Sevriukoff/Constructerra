using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Interfaces;

public interface IItemsRepository
{
    Item GetById(int id);
    Item GetByName(string name);
}