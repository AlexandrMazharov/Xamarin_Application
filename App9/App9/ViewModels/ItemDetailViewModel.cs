using System;

using App9.Models;
using App9.Views;

namespace App9.ViewModels
{
    //partial class ItemDetailViewModel : BaseViewModel
    public class ItemDetailViewModel : BaseViewModel
    {
        public Prompt Prompt { get; set; }
        //public Item Item { get; set; }
        public ItemDetailViewModel(Prompt item = null)
        {

            //Title = item?.WorkName;
            //Title = item?.Organization;
            //Title = item.Id.ToString();
            //Title = item?.LinkFile;
            Title = item?.Agreed;
           // Title = item?.Fio;                                    
            
            Prompt = item;
           
        }
    }
}
