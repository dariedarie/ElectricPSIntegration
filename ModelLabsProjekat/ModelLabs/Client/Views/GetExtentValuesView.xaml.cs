using FTN.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using TelventDMS.Services.NetworkModelService.TestClient.Tests;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for GetExtentValuesView.xaml
    /// </summary>
    public partial class GetExtentValuesView : Window, INotifyPropertyChanged
    {

        private List<ModelCode> concreteClassComboBox2 = new List<ModelCode>();
        
        private ModelCode selectedConcreteClassFromComboBox2;

        private List<ModelCode> propsForSelectedConcreteClass = new List<ModelCode>();

        public event PropertyChangedEventHandler PropertyChanged;

        public GetExtentValuesView()
        {
            InitializeComponent();
            ClientGda gdaProxy = new ClientGda();
            concreteClassComboBox2 = new List<ModelCode>() { ModelCode.CONTROL, ModelCode.CURVEDATA, ModelCode.FREQUENCYCONVERTER, ModelCode.REACTIVECAPABILITYCURVE, ModelCode.REGULATINGCONTROL, ModelCode.SHUNTCOMPENSATOR, ModelCode.STATICVARCOMPENSATOR, ModelCode.SYNCHRONOUSMACHINE, ModelCode.TERMINAL };
            
            DataContext = this;
        }

        

        public List<ModelCode> ConcreteClassComboBox2
        {
            get { return concreteClassComboBox2; }
            set { concreteClassComboBox2 = value; OnPropertyChanged("ConcreteClassComboBox2"); }
        }

      

        public ModelCode SelectedConcreteClassFromComboBox2
        {
            get { return selectedConcreteClassFromComboBox2; }
            set { selectedConcreteClassFromComboBox2 = value; OnPropertyChanged("SelectedConcreteClassFromComboBox2"); OnPropertyChanged("PropsForSelectedConcreteClass"); }
        }

        public List<ModelCode> PropsForSelectedConcreteClass
        {
            get
            {
                if (selectedConcreteClassFromComboBox2 != 0)
                {
                    ModelResourcesDesc modelResDesc = new ModelResourcesDesc();
                    List<ModelCode> list = modelResDesc.GetAllPropertyIds(selectedConcreteClassFromComboBox2);

                    return list;
                }
                return null;
            }

            set
            {
                propsForSelectedConcreteClass = value;
                OnPropertyChanged("PropsForSelectedConcreteClass");
                OnPropertyChanged("SelectedConcreteClassFromComboBox2");
            }
        }

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void GetValuesButton_Click(object sender, RoutedEventArgs e)
        {
            GetValuesView getValuesView = new GetValuesView();
            getValuesView.Show();
            this.Close();
        }

        private void GetRelatedValuesButton_Click(object sender, RoutedEventArgs e)
        {
            GetRelatedValuesView getRelatedValuesView = new GetRelatedValuesView();
            getRelatedValuesView.Show();
            this.Close();
        }

        private void GetExtentValuesViewResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxGetExtentValues.SelectedItems == null || SelectedConcreteClassFromComboBox2 == 0)
            {
                MessageBox.Show("Choose attribute!");
            }
            List<ModelCode> retVal = new List<ModelCode>();
            foreach (var item in listBoxGetExtentValues.SelectedItems)
            {
                retVal.Add((ModelCode)item);
            }
             
             ResultTextBox.Text = new ClientGda().GetExtentValues(SelectedConcreteClassFromComboBox2, retVal);
        }
    }
}
