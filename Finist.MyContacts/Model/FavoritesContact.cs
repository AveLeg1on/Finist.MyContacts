using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finist.MyContacts.ViewModel;

namespace Finist.MyContacts.Model
{
   public  class FavoritesContact:BaseViewModel
    {
        #region Fields

        private int _id;
        private int _contactid;

        #endregion

        #region Propirties

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
               Set(ref _id,value);
            }
        }
        public int ContactID
        {
            get
            {
                return _contactid;
            }
            set
            {
                Set(ref _contactid,value);  

            }
        }
        public virtual  Contact Contacts { get; set; }


        #endregion
        

    }
}
