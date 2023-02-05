using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Splat;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels;

namespace TerrariaConstructor.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var player = App.Container.Resolve<PlayerModel>();
            player.LoadPlayer(@"C:\Users\Bellatrix\Documents\My Games\Terraria\Players\CreativePlayerV2.plr");
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            var player = App.Container.Resolve<PlayerModel>();
            player.SavePlayer(@"C:\Users\Bellatrix\Documents\My Games\Terraria\Players\CreativePlayerV2.plr");
            //player.SavePlayerIntoDat(@"C:\Users\Bellatrix\Documents\My Games\Terraria\Players\CreativePlayerV3.plr.dat");
        }
    }
}