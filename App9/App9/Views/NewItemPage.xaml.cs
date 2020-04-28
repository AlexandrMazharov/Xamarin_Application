using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App9;
using App9.Models;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Plugin.Media;


using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Diagnostics;
using System.IO;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;

namespace App9.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();
        FileData file;

        List<string> CONFDIRECTION = new List<string>
        {  "НАправление 1" , "НАправление 2", "НАправление 3", "НАправление 3"  };
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            picker.ItemsSource = CONFDIRECTION;

            //picker.BindingContext = CONFDIRECTION;
            if (this.p != null)
            {
                int index = 0;
                for (int i = 0; i < CONFDIRECTION.Count; i++)
                {
                    if (CONFDIRECTION[i] == p.Agreed)
                    { index = i; }

                }
                entryFio.Text = p.Fio;
                entryNameWork.Text = p.WorkName;
                entryOrg.Text = p.Organization;
                picker.SelectedIndex = index;

                //file = File.WriteAllBytes(p.LinkFile);// GET p.LinkFile;
                file_label.Text= p.LinkFile;

            }
        }
        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            String lnkFile = "str";
            Prompt pr = new Prompt();
            pr.Agreed = "draft";
            pr.Direction = picker.Items[picker.SelectedIndex];
            pr.Fio = entryFio.Text;
            pr.LinkFile = lnkFile;
            pr.WorkName = entryNameWork.Text;
            pr.Organization = entryOrg.Text;

            App.Database.SaveItem(pr);

            await DisplayAlert(Resx.Resource.text_success, Resx.Resource.text_prompt_save, Resx.Resource.text_ok);

            await Navigation.PushModalAsync(new MainPage());

        }

        /*
        private async void BtnAdd_Clicked(object sender, EventArgs e)        
        {
            String lnkFile = "str";
            var allPersons = await firebaseHelper.GetAllPersons();
            int newId = allPersons.Count;
            await firebaseHelper.AddPrompt(newId, entryFio.Text,
                entryOrg.Text, picker.Items[picker.SelectedIndex], entryNameWork.Text, "sent", lnkFile);            
            await DisplayAlert("Success", "Person Added Successfully", "OK");
           // var allPersons = await firebaseHelper.GetAllPersons();
            await Navigation.PushModalAsync(new MainPage());
            // lstPersons.ItemsSource = allPersons;
        }
        */


        private Prompt p;

        public Item Item
        {
            get; set;
        }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "New prompt",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        public NewItemPage(Prompt p)
        {
            InitializeComponent();
            this.p = p;
            Item = new Item { Text = "Your prompt" };
        }
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (picker.SelectedIndex == -1)
                {
                    await DisplayAlert(Resx.Resource.text_error, Resx.Resource.text_enter_naprav, Resx.Resource.text_ok);
                }
                else

                {
                    string p = Path.GetFileName(file.FilePath);

                    String lnkFile = file.FilePath;// file.GetStream();
                    //String lnkFile = Path.GetFullPath(file.FilePath);// file.GetStream();


                    //String lnkFile = "str";

                    Prompt pr = new Prompt();
                    pr.Agreed = "draft";
                    pr.Direction = picker.Items[picker.SelectedIndex];
                    pr.Fio = entryFio.Text;
                    pr.LinkFile = lnkFile;
                    pr.WorkName = entryNameWork.Text;
                    pr.Organization = entryOrg.Text;

                    App.Database.SaveItem(pr);
                    await DisplayAlert(Resx.Resource.text_success, Resx.Resource.text_prompt_save, Resx.Resource.text_ok);
                    //await DisplayAlert("Success", "Prompt Save Successfully", "OK");
                    // var allPersons = await firebaseHelper.GetAllPersons();
                    await Navigation.PushModalAsync(new MainPage());
                }
            }
            catch
            {

            }
        }

        public static async Task<Xamarin.Essentials.Location> GetLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            return location;

        }
        private async void BtnPick_Clicked(object sender, EventArgs e)
        {
            if (file != null)
            {
                file = null;
            }
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossFilePicker.Current.PickFile();// Plugin.Media.CrossMedia.Current.PPickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                String fileName = file.FileName;

                file_label.Text = file.FileName;

                if (file == null)
                    return;
                else
                {


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }



        async void Sent_Clicked(object sender, EventArgs e)
        {
            if (entryFio.Text == null)
            {
                await DisplayAlert(Resx.Resource.text_error, Resx.Resource.text_enter_authors, Resx.Resource.text_ok);
            }
            else if (entryNameWork.Text == null)
            {
                await DisplayAlert(Resx.Resource.text_error, Resx.Resource.text_enter_work_name, Resx.Resource.text_ok);
            }
            else if (entryOrg.Text == null)
            {
                await DisplayAlert(Resx.Resource.text_error, Resx.Resource.text_enter_org, Resx.Resource.text_ok);
            }
            else if (picker.SelectedIndex == -1)
            {
                await DisplayAlert(Resx.Resource.text_error, Resx.Resource.text_enter_naprav, Resx.Resource.text_ok);
            }
            else if (file == null)
            {
                await DisplayAlert(Resx.Resource.text_error, "File not selected", Resx.Resource.text_ok);

            }
            else
            {
                try
                {
                    double x = 1; double y = 1;

                    Xamarin.Essentials.Location location = await GetLocation();
                    if (location.Longitude != 0 && location.Latitude != 0) { x = location.Longitude; y = location.Latitude; }
                    String lnkFile = "str";
                    var allPersons = await firebaseHelper.GetAllPersons();
                    int newId = allPersons.Count;

                    string p = Path.GetFileName(file.FilePath);

                    lnkFile = await firebaseStorageHelper.UploadFile(file.GetStream(), p);

                    await firebaseHelper.AddPrompt(
                        newId, entryFio.Text,
                        entryOrg.Text, picker.Items[picker.SelectedIndex],
                        entryNameWork.Text, "sent", lnkFile,
                        x, y);


                    await firebaseHelper.AddPos(x, y, entryNameWork.Text);
                    // await firebaseHelper.AddLatLng(entryNameWork.Text, x, y);
                    await DisplayAlert(Resx.Resource.text_success, Resx.Resource.text_prompt_sent, Resx.Resource.text_ok);
                    //await DisplayAlert("Success", "Prompt Added Successfully", "OK");            
                    await Navigation.PushModalAsync(new MainPage());
                }
                catch
                {
                    await DisplayAlert(Resx.Resource.text_error, Resx.Resource.text_try_again, Resx.Resource.text_ok);
                }
            }


        }

    }
}
