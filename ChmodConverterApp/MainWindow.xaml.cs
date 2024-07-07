using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChmodConverterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-7]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void SymbolicValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string newText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
                Regex regex = new Regex("^[dlbcsprwx-]+$");
                e.Handled = !regex.IsMatch(newText);
            }
        }

        private void permissionNumeric_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (permissionNumeric == null || permissionSymbolic == null)
                return;
            if (permissionNumeric.Text.Length==3)
            {
                permissionSymbolic.Text = ChmodConverter.ChmodConverter.ToSymbolic(short.Parse(permissionNumeric.Text));
            }
        }
        private void permissionSymbolic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (permissionNumeric == null || permissionSymbolic == null)
                return;
            Regex regex = new Regex(@"^[dlbcsp-][r-][w-][x-][r-][w-][x-][r-][w-][x-]$");
            
            if (permissionSymbolic.Text.Length == 10 && regex.IsMatch(permissionSymbolic.Text))
            {
                permissionNumeric.Text = ChmodConverter.ChmodConverter.ToNumeric(permissionSymbolic.Text).ToString();
            }
        }

        private void ChbChange(object sender, RoutedEventArgs e)
        {

        }
    }
}