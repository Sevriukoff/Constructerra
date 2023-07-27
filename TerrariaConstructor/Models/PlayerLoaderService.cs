namespace TerrariaConstructor.Models;

public class PlayerLoaderService
{
    private readonly PlayerModel _playerModel;

    public PlayerLoaderService(PlayerModel playerModel)
    {
        _playerModel = playerModel;
    }
}