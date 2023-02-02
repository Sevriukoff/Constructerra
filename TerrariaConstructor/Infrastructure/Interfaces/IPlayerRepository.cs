using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Interfaces;

public interface IPlayerRepository
{
    PlayerModel GetById(int id);
    PlayerModel GetByName(string name);
    PlayerModel GetAll();

    void Save(PlayerModel player);
    bool Delete(PlayerModel player);
}