using System;
using System.Collections.Generic;

using Xamarin.Auth;


using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using App9.Views.Maps;
using System.IO;

namespace App9.Views
{
    class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://app9-271512.firebaseio.com/");
        
     //   Account account;

        String clientId = Application.Current.Properties["Id"].ToString();
        public async Task AddPos(double x,double y, string t)
        {          
            await firebase
              .Child("pos")
              .PostAsync(new LatLng() {
                  Latitude = x, Longitude = y,Title=t}).ConfigureAwait(false);
        }
        public async Task AddPrompt(int id, string fio, string organization, string direction, string workName, string agreed, string linkFike, double x, double y)
        {
            await firebase
              .Child("Prompt").Child(clientId)
              .PostAsync(new Prompt()
              {
                  Id = id,
                  Fio = fio,
                  Organization = organization,
                  Direction = direction,
                  WorkName = workName,
                  Agreed = agreed,
                  LinkFile = linkFike,
                  X = x,
                  Y = y
              }).ConfigureAwait(false);
            //   await AddPos(x, y,workName);


        }

        public async Task<List<LatLng>> GetAllLatLnd()
        {
            return (await firebase
              .Child("pos")
              .OnceAsync<LatLng>()).Select(elem => new LatLng
              {
                  Latitude= elem.Object.Latitude,
                  Longitude = elem.Object.Longitude,
                  Title= elem.Object.Title
              }).ToList();
        }
        public async Task<List<Prompt>> GetAllPersons()
        {            
            return (await firebase
              .Child("Prompt").Child(clientId)
              .OnceAsync<Prompt>().ConfigureAwait(false)).Select(item => new Prompt
              {
                  Id = item.Object.Id,
                  Fio = item.Object.Fio,
                    Organization = item.Object.Organization,
                    Direction = item.Object.Direction,
                    WorkName= item.Object.WorkName,
                    Agreed = item.Object.Agreed,
                    LinkFile = item.Object.LinkFile      ,
                    X=item.Object.X,
                    Y=item.Object.Y
              }).ToList();
        }


        

        public async Task<Prompt> GetPerson(int personId)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Prompt").Child(clientId)
              .OnceAsync<Prompt>();
            return allPersons.Where(a => a.Id == personId).FirstOrDefault();
        }

        //public async Task UpdatePerson(int personId, string name)
        //{
        //    var toUpdatePerson = (await firebase
        //      .Child("Persons")
        //      .OnceAsync<Person>()).Where(a => a.Object.PersonId == personId).FirstOrDefault();

        //    await firebase
        //      .Child("Persons")
        //      .Child(toUpdatePerson.Key)
        //      .PutAsync(new Person() { PersonId = personId, Name = name });
        //}

        //public async Task DeletePerson(int personId)
        //{
        //    var toDeletePerson = (await firebase
        //      .Child("Persons")
        //      .OnceAsync<Person>()).Where(a => a.Object.PersonId == personId).FirstOrDefault();
        //    await firebase.Child("Persons").Child(toDeletePerson.Key).DeleteAsync();

        //}
    }
}
