using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Interfaces;

public interface IModifierRepository
{
    Modifier GetById(int id);
}