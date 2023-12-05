using SportsDataApp.Models;

namespace SportsDataApp.ViewModels
{
    public class SampleViewModel
    {
        public string username { get; set; }
        public string password { get; set; }

        public SampleViewModel() {
            username = string.Empty;
            password = string.Empty;
        }

        public SampleViewModel(LoginUser user)
        {
            username = user.Name;
            password = user.Password;
        }
    }
}
