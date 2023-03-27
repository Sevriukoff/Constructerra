using LiteDB;
using TerrariaConstructor.Infrastructure.Interfaces;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Repositories;

public class BuffsRepository : IBuffsRepository
{
    private readonly ILiteDatabase _itemsDatabase;

    public BuffsRepository(ILiteDatabase itemsDatabase)
    {
        _itemsDatabase = itemsDatabase;
    }

    public Buff GetById(int id, bool loadImage = true)
    {
        var result = _itemsDatabase.GetCollection<Buff>("Buffs").FindOne(x => x.Id == id);

        if (result != null && loadImage)
        {
            var fileInfo = _itemsDatabase.FileStorage.OpenRead(result.ImageId.ToString());
            result.Image = new byte[fileInfo.Length];
            fileInfo.Read(result.Image, 0, (int) fileInfo.Length);
        }
        
        return result;
    }
}