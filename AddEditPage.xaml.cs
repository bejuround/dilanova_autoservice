using System;
using System.Collections.Generic;
using System.Linq;
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

namespace dilanova_autoservice
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Service _currentServices = new Service();

        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();

            if(SelectedService != null)
                _currentServices = SelectedService;

            DataContext = _currentServices;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentServices.Title))
                errors.AppendLine("Укажите название услуги");

            if (_currentServices.Cost == 0)
                errors.AppendLine("Укажите стоимость услуги");

            if (_currentServices.Discount == null)
                errors.AppendLine("Укажите скидку");
            if (_currentServices.Discount > 100)
                errors.AppendLine("Укажите скидку");
            if (_currentServices.Discount < 0)
                errors.AppendLine("Укажите скидку");

            if (string.IsNullOrWhiteSpace(_currentServices.DurationInSeconds))
                errors.AppendLine("Укажите длительность усоуги");
                
            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if(_currentServices.ID == 0)
                Dilanova_AutoservesEntities.GetContext().Service.Add(_currentServices);
            try
            {
                Dilanova_AutoservesEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Class1.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
