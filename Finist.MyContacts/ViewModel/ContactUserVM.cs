using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Finist.MyContacts.Model;
using Finist.MyContacts.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using PhoneNumbers;


namespace Finist.MyContacts.ViewModel
{
    class ContactUserVM : Contact
    {
        #region Fields
        string _name;
        string? _Secondname;
        string? _Surename;
        string _Phone;
        string? _Homephone;
        string? _Email;
        string? _Description;
        DateOnly? _DateBirthday;
        private byte[]? _photo;
        private DateTime? _selectedDate;
        private readonly MyINavigationService _myNavigationServie;
        PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
        private string _placeholderMessage = "Телефон";
        private ImageBrush _imageFill;
        private const string fileBytes = "/9j/4AAQSkZJRgABAQEBLAEsAAD/4QBhRXhpZgAASUkqAAgAAAABAA4BAgA/AAAAGgAAAAAAAABJbmNvZ25pdG8gbW9kZSAtIHByaXZhdGUgYW5kIHNhZmUgYnJvd3NpbmcgaW4gaW50ZXJuZXQgd2Vic2l0ZXP/4QVnaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/Pgo8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIj4KCTxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+CgkJPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIiB4bWxuczpJcHRjNHhtcENvcmU9Imh0dHA6Ly9pcHRjLm9yZy9zdGQvSXB0YzR4bXBDb3JlLzEuMC94bWxucy8iICAgeG1sbnM6R2V0dHlJbWFnZXNHSUZUPSJodHRwOi8veG1wLmdldHR5aW1hZ2VzLmNvbS9naWZ0LzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6cGx1cz0iaHR0cDovL25zLnVzZXBsdXMub3JnL2xkZi94bXAvMS4wLyIgIHhtbG5zOmlwdGNFeHQ9Imh0dHA6Ly9pcHRjLm9yZy9zdGQvSXB0YzR4bXBFeHQvMjAwOC0wMi0yOS8iIHhtbG5zOnhtcFJpZ2h0cz0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL3JpZ2h0cy8iIHBob3Rvc2hvcDpDcmVkaXQ9IkdldHR5IEltYWdlcy9pU3RvY2twaG90byIgR2V0dHlJbWFnZXNHSUZUOkFzc2V0SUQ9IjEzNTI4NTcwNTEiIHhtcFJpZ2h0czpXZWJTdGF0ZW1lbnQ9Imh0dHBzOi8vd3d3LmlzdG9ja3Bob3RvLmNvbS9sZWdhbC9saWNlbnNlLWFncmVlbWVudD91dG1fbWVkaXVtPW9yZ2FuaWMmYW1wO3V0bV9zb3VyY2U9Z29vZ2xlJmFtcDt1dG1fY2FtcGFpZ249aXB0Y3VybCIgPgo8ZGM6Y3JlYXRvcj48cmRmOlNlcT48cmRmOmxpPkRtaXRyeSBLb3ZhbGNodWs8L3JkZjpsaT48L3JkZjpTZXE+PC9kYzpjcmVhdG9yPjxkYzpkZXNjcmlwdGlvbj48cmRmOkFsdD48cmRmOmxpIHhtbDpsYW5nPSJ4LWRlZmF1bHQiPkluY29nbml0byBtb2RlIC0gcHJpdmF0ZSBhbmQgc2FmZSBicm93c2luZyBpbiBpbnRlcm5ldCB3ZWJzaXRlczwvcmRmOmxpPjwvcmRmOkFsdD48L2RjOmRlc2NyaXB0aW9uPgo8cGx1czpMaWNlbnNvcj48cmRmOlNlcT48cmRmOmxpIHJkZjpwYXJzZVR5cGU9J1Jlc291cmNlJz48cGx1czpMaWNlbnNvclVSTD5odHRwczovL3d3dy5pc3RvY2twaG90by5jb20vcGhvdG8vbGljZW5zZS1nbTEzNTI4NTcwNTEtP3V0bV9tZWRpdW09b3JnYW5pYyZhbXA7dXRtX3NvdXJjZT1nb29nbGUmYW1wO3V0bV9jYW1wYWlnbj1pcHRjdXJsPC9wbHVzOkxpY2Vuc29yVVJMPjwvcmRmOmxpPjwvcmRmOlNlcT48L3BsdXM6TGljZW5zb3I+CgkJPC9yZGY6RGVzY3JpcHRpb24+Cgk8L3JkZjpSREY+CjwveDp4bXBtZXRhPgo8P3hwYWNrZXQgZW5kPSJ3Ij8+Cv/tAJJQaG90b3Nob3AgMy4wADhCSU0EBAAAAAAAdhwCUAAQRG1pdHJ5IEtvdmFsY2h1axwCeAA/SW5jb2duaXRvIG1vZGUgLSBwcml2YXRlIGFuZCBzYWZlIGJyb3dzaW5nIGluIGludGVybmV0IHdlYnNpdGVzHAJuABhHZXR0eSBJbWFnZXMvaVN0b2NrcGhvdG//2wBDAAoHBwgHBgoICAgLCgoLDhgQDg0NDh0VFhEYIx8lJCIfIiEmKzcvJik0KSEiMEExNDk7Pj4+JS5ESUM8SDc9Pjv/2wBDAQoLCw4NDhwQEBw7KCIoOzs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozv/wgARCAHFAmQD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";

        #endregion
        #region Properties

        public EditMode Mode { get; private set; }

        public ImageBrush ImageFill
        {
            get
            {
                return _imageFill;
            }
            set
            {
                Set(ref _imageFill, value);
            }
        }
        public byte[] Photo
        {
            get
            {
                return _photo;
            }
            set
            {
                Set(ref _photo, value);
            }
        }
        public Contact EditContact { get; set; }

        public string PlaceholderMessage
        {
            get { return _placeholderMessage; }
            set
            {
                Set(ref _placeholderMessage, value);
            }
        }
        public MyINavigationService MyNavigationServiceP => _myNavigationServie;
        public string Name
        {
            get
            {
                return _name;
            }
            set { Set(ref _name, value); }
        }
        public string? Secondname
        {
            get
            {
                return _Secondname;
            }
            set
            {
                Set(ref _Secondname, value);
            }
        }
        public string? Surename
        {
            get
            {
                return _Surename;
            }
            set
            {
                Set(ref _Surename, value);
            }
        }
        public string Phone
        {
            get { return _Phone; }
            set
            {
                try
                {
                    if (value.Any(char.IsDigit))
                    {



                        var number =
                            phoneUtil.Parse(value,
                                "RU"); // "RU" — это пример страны по умолчанию. Можешь заменить на нужное значение.

                        if (phoneUtil.IsValidNumber(number))
                        {
                            Set(ref _Phone, value);

                        }
                        else
                        {
                            throw new NumberParseException(ErrorType.NOT_A_NUMBER, "test");




                            return;

                        }
                    }
                    else
                    {
                        throw new NumberParseException(ErrorType.NOT_A_NUMBER, "test");




                        return;

                    }

                }

                catch (NumberParseException)
                {
                    PlaceholderMessage = "Номер выглядит не правдоподобно";



                }
            }
        }
        public string? Email
        {
            get
            {
                return _Email;
            }
            set
            {
                Set(ref _Email, value);
            }
        }
        public string? Description
        {
            get
            {
                return _Description;
            }
            set
            {
                Set(ref _Description, value);
            }
        }
        public string? HomePhone
        {
            get
            {
                return _Homephone;
            }
            set
            {
                _Homephone = value;

            }
        }
        public DateTime? SeleteDateTime
        {
            get => _selectedDate;
            set
            {
                Set(ref _selectedDate, value);
                DateBirthday = DateOnly.FromDateTime(SeleteDateTime.Value);

            }
        }

        public DateOnly? DateBirthday
        {
            get
            {
                return _DateBirthday;

            }
            set
            {
                Set(ref _DateBirthday, value);
            }
        }
        #endregion
        private void InitializeForAdd()
        {
            Mode = EditMode.Add;

        }

        private void InitializeForEdit(Contact item)
        {
            Mode = EditMode.Edit;
            EditContact = item;
            Name = item.Name;
            DateBirthday = item.DateBirthday;
            Surename = item.Surename;
            Secondname = item.Secondname;
            Phone = item.PhoneMobile;
            Description = item.Description;
            Email = item.Email;
            HomePhone = item.PhoneHome;
            Id = item.Id;
            Photo = item.Photo;

            /// Assets / StockContactPhoto.jpeg
        }
        public ContactUserVM(MyINavigationService mynNavigationService)
        {
            _myNavigationServie = mynNavigationService;
            Photo = Convert.FromBase64String(fileBytes);
            SetImageFromBase64String(fileBytes);

            if (MyNavigationService.Parameters.ContainsKey("ContactUsesPage"))
            {
                var contactItem = MyNavigationService.Parameters["ContactUsesPage"] as Contact;
                if (contactItem != null)
                {
                    InitializeForEdit(contactItem);
                    // Режим редактирования

                }
                else
                {
                    InitializeForAdd();
                    // Режим добавления
                }
            }
        }




        #region Command
        public RelayCommand SaveContactCommand => new RelayCommand(Saver);
        public RelayCommand BackToContacts => new RelayCommand(LetsBack);
        public RelayCommand AddedPhotoCommand => new RelayCommand(ReadToBytesAndAdd);


        #endregion
        #region Methods 
        private void ReadToBytesAndAdd()
        {
            var openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage();
                try
                {


                    Photo = File.ReadAllBytes(openFile.FileName);

                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(openFile.FileName);
                    bitmap.EndInit();



                }
                catch
                {
                    MessageBox.Show("Пожалуйста выберите фотографию");
                    return;
                }

                ImageBrush _imageBrush = new ImageBrush(bitmap);
                
                ImageFill = _imageBrush;
            }

        }
        private void SetImageFromBase64String(string fileBytes)
        {
            byte[] bytes = Convert.FromBase64String(fileBytes);
            BitmapImage image = new BitmapImage();
            using (var mem = new MemoryStream(bytes))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            ImageFill=new ImageBrush(image);
        }


        private void Saver()
        {
            using (MyDBContext context = new MyDBContext())
            {
                if (!(Mode == EditMode.Edit))
                {
                    var _newContact = new Contact();
                    _newContact.Name = Name;
                    _newContact.Secondname = Secondname;
                    _newContact.PhoneMobile = Phone;
                    _newContact.Description = Description;
                    _newContact.Surename = Surename;
                    _newContact.Email = Email;
                    _newContact.PhoneHome = HomePhone;
                    _newContact.DateBirthday = DateBirthday;
                    _newContact.Photo = Photo;
                    _newContact.DateCreated=DateTime.Now;
                    context.Contacts.Add(_newContact);

                    context.SaveChanges();
                }
                else
                {
                    //var _newContact = new Contact();
                    EditContact.Name = Name;
                    EditContact.Secondname = Secondname;
                    EditContact.PhoneMobile = Phone;
                    EditContact.Description = Description;
                    EditContact.Surename = Surename;
                    EditContact.Email = Email;
                    EditContact.PhoneHome = HomePhone;
                    EditContact.DateBirthday = DateBirthday;
                    EditContact.Photo= Photo;   
                    context.Contacts.Update(EditContact);

                    context.SaveChanges();

                }

            }

            LetsBack();

        }

        private void LetsBack()
        {

            MyNavigationServiceP.NavigateTo("ContactsPage");
        }

        #endregion

    }
}
/*var openFile = new OpenFileDialog();
   if (openFile.ShowDialog() == true)
   {
   
   byte[] fileBytes = File.ReadAllBytes(openFile.FileName);
   
  
   string fileContentAsBase64 = Convert.ToBase64String(fileBytes);
   
   
   string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
   string outputPath = Path.Combine(desktopPath, "ImageAsBase64.txt");
   
   File.WriteAllText(outputPath, fileContentAsBase64);
   }
   */
