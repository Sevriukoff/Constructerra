using LiteDB;

namespace TerrariaConstructor.Infrastructure.Interfaces;

public interface IEntityConfiguration<T>
{
    BsonValue Serialize(T obj);

    T Deserialize(BsonValue bsonValue);
}