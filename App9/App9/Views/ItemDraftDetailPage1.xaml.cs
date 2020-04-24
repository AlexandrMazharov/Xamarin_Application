using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App9.ViewModels;
using Xamarin.Essentials;

namespace App9.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDraftDetailPage1 : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        ItemDetailViewModel viewModel;
        public ItemDraftDetailPage1(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
        public ItemDraftDetailPage1()
        {
            InitializeComponent();

            var item = new Prompt
            {
                WorkName = "Item 1",
                Id = 0

            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
        private void ToolbarUpload_Clicked(object sender, EventArgs e)
        {
            sqliteToFB();

        }
        private void ToolbarDell_Clicked(object sender, EventArgs e)
        {
            int id = this.viewModel.Prompt.Id;
            App.Database.DeleteItem(id);

            DisplayAlert("Success", "Prompt Deletted Successfully", "OK");
            Navigation.PushModalAsync(new MainPage());

        }

        //
        private void Toolbarhange_Clicked(object sender, EventArgs e)
        {
            Prompt p = this.viewModel.Prompt;
            int id = p.Id;
            Navigation.PushModalAsync(new NavigationPage( new NewItemPage(p)));
            //Navigation.PushModalAsync(new NewItemPage(p));
            //await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
            App.Database.DeleteItem(id);

        }
         private void sqliteToFB() {
            uploadPrompt();
            deleteFromSqlite();
            DisplayAlert("Success", "Prompt Added Successfully", "OK");            
            Navigation.PushModalAsync(new MainPage());


        }
        protected void TapDownloadFile(object sender, EventArgs e)
        {
            try
            {
                Device.OpenUri(new Uri(this.viewModel.Prompt.LinkFile));
            }
            catch
            {
                DisplayAlert(Resx.Resource.text_error, Resx.Resource.text_link_error, Resx.Resource.text_ok);
            }
        }

            private async void deleteFromSqlite()
        {
            int id = this.viewModel.Prompt.Id;
            App.Database.DeleteItem(id);

        }

            private async void uploadPrompt() {
            double x = 1; double y = 1;

            Location location = await NewItemPage.GetLocation();
            if (location.Longitude != 0 && location.Latitude != 0) { x = location.Longitude; y = location.Latitude; }

           

            Prompt p = this.viewModel.Prompt;
            
            String lnkFile = "str";

            var allPersons = await firebaseHelper.GetAllPersons();
            int newId = allPersons.Count;

           await firebaseHelper.AddPrompt(newId, p.Fio, p.Organization,p.Direction,p.WorkName, "sent", p.LinkFile, x,y);
            DisplayAlert("Success", "Prompt Added Successfully", "OK");
            // var allPersons = await firebaseHelper.GetAllPersons();
            Navigation.PushModalAsync(new MainPage());

        }
    }
}