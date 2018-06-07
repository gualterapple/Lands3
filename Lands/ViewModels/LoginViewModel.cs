namespace Lands.ViewModels
{
	using System.Windows.Input;
	using Xamarin.Forms;
	using System.ComponentModel;
	using GalaSoft.MvvmLight.Command;
	using Views;


	public class LoginViewModel: BaseViewModel
    {
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
			this.IsRemembered = true;
			this.IsEnabled = true;

			this.Email = "gualter@hotmail.com";
            this.Password = "1234";
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


			if (this.Email != "gualter@hotmail.com" || this.Password != "1234")
            {
				this.IsRunning = false;
				this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Erro",
                    "Email ou senha incorrecta.",
                    "Aceitar");
				this.Password = string.Empty;
                return;
            }

			this.IsRunning = false;
            this.IsEnabled = true;

			this.Email = string.Empty;
			this.Password = string.Empty;

			MainViewModel.GetInstance().Lands = new LandsViewModel();
			await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
				
        }
		#endregion
        
    }
}
