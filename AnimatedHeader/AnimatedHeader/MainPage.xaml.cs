using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AnimatedHeader
{
    public partial class MainPage : ContentPage
    {
        private List<string> itemList;

        public MainPage()
        {
            InitializeComponent();
            itemList = new List<string>();
            for (int i = 0; i < 500; i++)
                itemList.Add($"Item {i}");

            cvItems.ItemsSource = itemList;
        }

        private async void cvItems_Scrolled(Object sender, ItemsViewScrolledEventArgs e)
        {
            if (gridContent.Width <= 0)
                return;

            System.Diagnostics.Debug.WriteLine($"Scrolled {e.VerticalOffset}");

            double maxOffset = 100;
            double offset = 0;
            if (e.VerticalOffset > maxOffset)
                offset = maxOffset;
            else if (e.VerticalOffset < 0)
                offset = 0;
            else
                offset = e.VerticalOffset;

            double imageXTranslation = -offset *  (gridContent.Width - 140) / (2 * maxOffset);
            double imageScale = 1 - (offset * 0.5 / maxOffset);
            double imageYTranslation = ((frImage.HeightRequest * imageScale) - frImage.HeightRequest) / 2;
            uint animationLenght = 50;

            await Task.WhenAll(frImage.TranslateTo(imageXTranslation, imageYTranslation, animationLenght),
                               frImage.ScaleTo(imageScale, animationLenght),
                               lbName.TranslateTo(offset / 2, -offset, animationLenght),
                               lbJob.TranslateTo(offset / 2, -offset, animationLenght),
                               bvHeaderBackground.TranslateTo(0, -offset, animationLenght));
        }
    }
}
