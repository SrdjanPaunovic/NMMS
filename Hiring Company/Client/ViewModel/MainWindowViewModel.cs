using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModel
{
	public class MainWindowViewModel
	{
		#region Fields
		private ICommand loginCommand;
		#endregion Fields

		#region Properties
		public ICommand LoginCommand
		{
			get
			{
				return loginCommand ?? (loginCommand = new RelayCommand(param => this.LoginClick(param)));
			}
		}
		#endregion Properties

		#region Methods
		private void LoginClick(object param)
		{
			object[] parameters = param as object[];

			if(parameters == null)
			{
				throw new Exception("[LoginCommnad] Command parameters has NULL value");
			}
			//TODO
		}
		#endregion Methods

	}
}
