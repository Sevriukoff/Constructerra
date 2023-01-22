using System.Windows;
using Autofac;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var player = App.Container.Resolve<PlayerModel>();
            player.LoadPlayer(@"C:\Users\Bellatrix\Documents\My Games\Terraria\Players\Пёплик.plr");

            var playerCharacteristics = App.Container.Resolve<CharacteristicsModel>();

            playerCharacteristics.Name += "V2";
        }
    }
}