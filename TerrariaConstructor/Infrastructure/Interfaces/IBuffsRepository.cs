using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Interfaces;

public interface IBuffsRepository
{
    Buff GetById(int id);
}