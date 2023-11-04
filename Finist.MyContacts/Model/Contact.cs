using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finist.MyContacts.ViewModel;

namespace Finist.MyContacts.Model
{
   public class Contact : BaseViewModel
    {
        #region Fields

          private int _id;    
        private string _name;
        private string? _surename;
        private string? _secondname;
        private string _phoneMobile;
        private string? _email;
        private string? _phoneHome;
        private byte[]? _photo;
        private string? _description;
        private DateOnly? _dateBirthday;
        private DateTime _dateCreated;

        #endregion

        #region Properties
        public virtual int Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        public virtual string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Set(ref _name,value);

            }
        }

        public virtual string? Surename
        {
            get
            {
                return _surename;

            }
            set
            {
                Set(ref _surename,value);

            }

        }

        public virtual string? Secondname
        {
            get
            {
                return _secondname; 

            }

            set
            {
                Set(ref _secondname, value); 

            }
        }

        public virtual string PhoneMobile
        {
            get { return _phoneMobile; }
            set { Set(ref _phoneMobile, value); }
        }

        public virtual string? Email
        {
            get { return _email; }
            set { Set(ref _email, value); }
        }

        public virtual string? PhoneHome
        {
            get
            {
                return _phoneHome;

            }
            set
            {
                Set(ref _phoneHome, value);

            }
        }

        public virtual byte[]? Photo { 
            get
        {
            return _photo;
           
        } 
            set
            {
                Set(ref _photo, value);
            }
        }

        public virtual string? Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }

        public virtual DateOnly? DateBirthday
        {
            get { return _dateBirthday; }
            set { Set(ref _dateBirthday, value); }
        }

        public DateTime DateCreated
        {
            get
            {
                return _dateCreated;
            }
            set
            {
                Set(ref _dateCreated,value);
            }
        }
        public virtual ICollection<FavoritesContact> FavoritesContacts { get; set; }
        #endregion

    }

}
