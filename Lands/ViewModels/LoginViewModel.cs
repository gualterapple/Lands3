namespace Lands.ViewModels
{
	using System.Windows.Input;
	using Xamarin.Forms;
	using Services;
	using System.ComponentModel;
	using GalaSoft.MvvmLight.Command;
	using Views;


	public class LoginViewModel: BaseViewModel
    {
		#region Services
        private ApiService apiService;
        #endregion

		#region Attributes
		private string email;
		private string password;
		private bool isRunning;
		private bool isEnabled;
		#endregion

		#region Properties
		public string Email
		{
			get { return this.email; }
			set { SetValue(ref this.email, value); }
		}
		public string Password
        {
			get { return this.password; } 
			set { SetValue(ref this.password, value);}
		}
		public bool IsRunning
        {
			get { return this.isRunning; }
			set { SetValue(ref this.isRunning, value); }
        }
		public bool IsRemembered
        {
            get;
            set;
        }
		public bool IsEnabled
		{
			get { return this.isEnabled; }
			set { SetValue(ref this.isEnabled, value); }
		}
		#endregion

		#region Constructors
        public LoginViewModel()
		{
			this.apiService = new ApiService();

			this.IsRemembered = true;
			this.IsEnabled = true;

            this.Email = "gualteraura2017@hotmail.com";
            this.Password = "Gproducoes123_";
            
		}
		#endregion

		#region Commands
		public ICommand LoginCommand
        {
			get
			{
				return new RelayCommand(Login);

            }
        }
		private async void Login()
		{
			if(string.IsNullOrEmpty(this.Email))
			{
				await Application.Current.MainPage.DisplayAlert(
					"Erro",
					"Insira seu e-mail",
					"Aceitar");
				return;
            }
			if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erro",
                    "Insira sua senha",
                    "Aceitar");
                return;
            }

			this.IsRunning = true;
			this.IsEnabled = false;

			var connection = await this.apiService.CheckConnection();

			if(!connection.IsSuccess)
			{
				this.IsRunning = true;
                this.IsEnabled = false;
                
                await Application.Current.MainPage.DisplayAlert(
                    "Erro",
					connection.Message,
                    "Aceitar");
                return;
            }

			var token = await this.apiService.GetToken(
				"http://webapixamarin-001-site1.atempurl.com",
				this.Email,
				this.Password);
			
			if(token == null)
			{
				this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    "Erro",
                    "Something was wrong, please try later.",
                    "Aceitar");
                return;          
            }

			if(string.IsNullOrEmpty(token.AccessToken))
			{
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    "Erro",
					token.ErrorDescription,
                    "Aceitar");
				this.Password = string.Empty;
                return;
            }

			var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Lands = new LandsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());

			this.IsRunning = false;
            this.IsEnabled = true;

			this.Email = string.Empty;
			this.Password = string.Empty;


				
        }
		#endregion
		//Veficando se está ok
        
    }
}
