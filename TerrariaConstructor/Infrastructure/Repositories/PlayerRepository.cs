using LiteDB;
using TerrariaConstructor.Infrastructure.Interfaces;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly ILiteDatabase _playersDatabase;

    public PlayerRepository(ILiteDatabase playerDatabase)
    {
        _playersDatabase = playerDatabase;
    }

    public PlayerModel GetById(int id)
    {
        throw new System.NotImplementedException();
    }

    public PlayerModel GetByName(string name)
    {
        throw new System.NotImplementedException();
    }

    public PlayerModel GetAll()
    {
        throw new System.NotImplementedException();
    }

    public void Save(PlayerModel player)
    {
        throw new System.NotImplementedException();
    }

    public bool Delete(PlayerModel player)
    {
        throw new System.NotImplementedException();
    }
}