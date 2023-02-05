using System.Collections.Generic;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Interfaces;

public interface IAppearanceRepository
{
    Appearance GetHairById(int id);
    IEnumerable<Appearance> GetAllHairs();

    Appearance GetSkinById(int id);

    IEnumerable<Appearance> GetAllSkins();
}