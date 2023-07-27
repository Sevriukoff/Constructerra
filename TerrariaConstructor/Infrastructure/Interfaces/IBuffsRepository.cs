using System.Collections.Generic;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Interfaces;

public interface IBuffsRepository
{
    Buff GetById(int id, bool loadImage = true);
    IEnumerable<Buff> GetAll();
}