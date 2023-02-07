using System;
using System.Linq;
using System.Reflection;

namespace TerrariaConstructor.Services;

public class NameToViewModelTypeService
{
    private static readonly Type[] PageTypes = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.Namespace?.StartsWith("TerrariaConstructor.ViewModels") ?? false)
        .ToArray();

    public static Type Convert(string pageName)
    {
        pageName = pageName.Trim().ToLower() + "viewmodel";

        return PageTypes.FirstOrDefault(singlePageType => singlePageType.Name.ToLower() == pageName);
    }
}