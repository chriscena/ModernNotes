using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Views;

namespace ModernNotes.WpfClient.Services
{
	/// <summary>
	/// Example from Laurent Bugnions Flowers.Forms mvvm Examples
	/// </summary>
	public class DialogService : IDialogService
	{
		private readonly Window _window;

		public DialogService()
		{
			_window = Application.Current.MainWindow;
		}

		public Task ShowError(string message,
			string title,
			string buttonText,
			Action afterHideCallback)
		{
			MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);

			if (afterHideCallback != null)
			{
				afterHideCallback();
			}

			return Task.CompletedTask;
		}

		public Task ShowError(
			Exception error,
			string title,
			string buttonText,
			Action afterHideCallback)
		{
			MessageBox.Show(error.Message, title, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);

			if (afterHideCallback != null)
			{
				afterHideCallback();
			}

			return Task.CompletedTask;
		}

		public Task ShowMessage(
			string message,
			string title)
		{
			MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None);
			return Task.CompletedTask;
		}

		public Task ShowMessage(
			string message,
			string title,
			string buttonText,
			Action afterHideCallback)
		{
			MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None);

			if (afterHideCallback != null)
			{
				afterHideCallback();
			}
			return Task.CompletedTask;
		}

		public Task<bool> ShowMessage(
			string message,
			string title,
			string buttonConfirmText,
			string buttonCancelText,
			Action<bool> afterHideCallback)
		{

			var result = MessageBox.Show(message, title, MessageBoxButton.OKCancel, MessageBoxImage.None) == MessageBoxResult.OK;

			if (afterHideCallback != null)
			{
				afterHideCallback(result);
			}

			return Task.FromResult(result);
		}

		public Task ShowMessageBox(
			string message,
			string title)
		{
			MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None);
			return Task.CompletedTask;
		}
	}
}
