using LiteDB;
using TerrariaConstructor.Infrastructure.Interfaces;
using TerrariaConstructor.Infrastructure.Mappers.EntityConfigurations;

namespace TerrariaConstructor.Infrastructure.Mappers;

public class ItemsDatabaseMapper : BsonMapper
{
    public ItemsDatabaseMapper()
    {
        ApplyConfiguration(new BuffEntityConfiguration());
        ApplyConfiguration(new ModifierEntityConfiguration());
        ApplyConfiguration(new ItemEntityConfiguration());
    }

    private void ApplyConfiguration<T>(IEntityConfiguration<T> configuration)
    {
        RegisterType(configuration.Serialize, configuration.Deserialize);
    }
    
    /*
     
     private BsonMapper _mapper = Global;
     
    public void Register()
    {
        _mapper.Entity<Item>()
            .Field(x => x.Name, "RuName");
    }
    */
}