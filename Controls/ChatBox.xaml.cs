using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YetAnotherMessenger.Controls
{
	/// <summary>
	/// Логика взаимодействия для ChatBox.xaml
	/// </summary>
	public partial class ChatBox : UserControl
	{
		public ChatBox()
		{
			InitializeComponent();
		}

		private void ChatBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			TextBox textBox = (TextBox)sender;

			if (e.Key == Key.Enter && Keyboard.IsKeyDown(Key.LeftShift))
			{
				textBox.Text += "\r\n";
				textBox.CaretIndex = textBox.Text.Length;

				e.Handled = true;
			}
		}
    }
}
