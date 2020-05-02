using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.WinUI.SnackMachines
{
    public class SnackPileViewModel
    {
        private readonly SnackPile _snackPile;

        public string Price => _snackPile.Price.ToString("C2");
        public int Amount => _snackPile.Quantity;
        public int ImageWidth => _snackPile.Snack.ImageWidth;
        public ImageSource Image => new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", _snackPile.Snack.Name + ".png")));

        public SnackPileViewModel(SnackPile snackPile)
        {
            _snackPile = snackPile;
        }
    }
}
