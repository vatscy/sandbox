using KenshinLoginSkipDemo.DependencyServices;
using Prism.Commands;
using Prism.Navigation;

namespace KenshinLoginSkipDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _idToken;
        private readonly IShareDataService _dataService;

        public DelegateCommand GetTokenCommand { get; private set; }
        public DelegateCommand ClearTokenCommand { get; private set; }
        public DelegateCommand UpdateTokenCommand { get; private set; }

        public const string ShareAuthorizationToken = "KenshinShareAuthorizationToken";

        public string IdToken
        {
            get { return _idToken; }
            set { SetProperty(ref _idToken, value); }
        }

        public MainPageViewModel(INavigationService navigationService, IShareDataService dataService)
            : base(navigationService)
        {
            Title = "Main Page";
            GetTokenCommand = new DelegateCommand(OnGetToken, CanExecuteMethod);
            ClearTokenCommand = new DelegateCommand(OnClearToken, CanExecuteMethod);
            UpdateTokenCommand = new DelegateCommand(OnUpdateToken, CanExecuteMethod);
            _dataService = dataService;
        }

        private void OnGetToken()
        {
            IdToken = _dataService.GetSharingSecureData(ShareAuthorizationToken);
        }

        private void OnClearToken()
        {
            _dataService.DeleteSharingSecureData(ShareAuthorizationToken);
            if (string.IsNullOrEmpty(_dataService.GetSharingSecureData(ShareAuthorizationToken)))
            {
                IdToken = string.Empty;
            }
        }

        private void OnUpdateToken()
        {
            var token = "eyJraWQiOiIyMDE2MDkxNTE3NDE1NiIsImFsZyI6IlJTMjU2In0.eyJjYXJhZGFfaWQiOiJ6MDAwMDEwXzAwNDQiLCJ1cGRhdGVkX2F0IjoxNTU4NDk4NDIwLCJpc3MiOiJodHRwczovL3BmLmF1dGguY2FyYWRhLmpwIiwic3ViIjoiQzkrd2UvTm9yQzdVcTk2L0FGNnlyQy9ndHhjVFdPZXdrdGhmcjJqUm1qVEV6Y0pEMWU3SXZMR1A4Y1R6aTR0cyIsImF1ZCI6WyIxODQ1OTMyNzIwMTQxIl0sImV4cCI6NDcxNTA0NDA3OSwiaWF0IjoxNTYxNDQ0MDc5LCJhdXRoX3RpbWUiOjE1NjE0NDQwNzksIm5vbmNlIjoiMDY1NTFhNWY1OThhNGU4YmEwNmZjNTcyYjE3MDk2YjUiLCJjX2hhc2giOiJqQ2NYZV9TbldaRzJ5cmxGUU02c0JnIiwic19oYXNoIjoiZmE2QzN1anpPSV9HdDZqenVyc1BmZyJ9.OVFdsMMOA53JKzRwDjvWvZnV7fWUkwUvf4kl5ZqowzaGbuiLGGF2SfGQPIGn7lD2LqOlB900wu2I-zjGrCYBwBNSKCA_oSPinMaN_hoPIbmQvfjcI1RcHZ1zFvfmYlVy4QwRDCZkGgWXrWNbJC36IgubeKh9zBRBSgdmlCJuYRkjV8kwpMdqJAsHREhQ7sIHWc8uRjP1yK2XXN233iSwX2koLYpZX0nzHkVHBYTdM_T-WzoRpK2ufJjChA2OWOp-lzNudgVshLLlG-PkRNSbohjATsD-JJbFW11WeL1F1HAUoI14fSlhSRtSpBpte8Wto45XTKIEKFjA80u5rX4EFA";
            _dataService.DeleteSharingSecureData(ShareAuthorizationToken);
            _dataService.SaveSharingSecureData(ShareAuthorizationToken, token);
            if (!string.IsNullOrEmpty(_dataService.GetSharingSecureData(ShareAuthorizationToken)))
            {
                IdToken = _dataService.GetSharingSecureData(ShareAuthorizationToken);
            }
        }

        private bool CanExecuteMethod()
        {
            return true;
        }
    }
}
