using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App9.Models;
using App9.ViewModels;

namespace App9.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public  ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
        
        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Prompt
            {
                WorkName = "Item 1",
                Id= 0

            };
            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
        
            protected void TapDownloadFile(object sender, EventArgs e)
        {
            try
            {
                Device.OpenUri(new Uri(this.viewModel.Prompt.LinkFile));
            }
            catch {
                 DisplayAlert(Resx.Resource.text_error, Resx.Resource.text_link_error, Resx.Resource.text_ok);
            }

        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}