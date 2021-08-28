using FTN.Common;
using FTN.Services.NetworkModelService.TestClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using TelventDMS.Services.NetworkModelService.TestClient.Tests;
using FTN.Services.NetworkModelService.TestClient;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for GetValuesView.xaml
    /// </summary>
    public partial class GetValuesView : Window, INotifyPropertyChanged
    {
        private List<long> gidComboBox = new List<long>();
        private long selectedGidFromComboBox;

        private List<ModelCode> propsForSelectedGid = new List<ModelCode>();

        public event PropertyChangedEventHandler PropertyChanged;

        public GetValuesView()
        {
            InitializeComponent();

            ClientGda gdaProxy = new ClientGda();
            gidComboBox = gdaProxy.GetAllGids();
            DataContext = this;
        }

        public List<long> GidComboBox
        {
            get { return gidComboBox; }
            set
            {
                gidComboBox = value;
                OnPropertyChanged("ComboBoxGetValuesPath");
            }
        }

        public long SelectedGidFromComboBox
        {
            get { return selectedGidFromComboBox; }
            set
            {
                selectedGidFromComboBox = value;
                OnPropertyChanged("SelectedGidFromComboBox");
                OnPropertyChanged("PropsForSelectedGid");
            }
        }

        public List<ModelCode> PropsForSelectedGid
        {
            get
            {
                if (selectedGidFromComboBox != 0)
                {
                    ModelResourcesDesc modelResDesc = new ModelResourcesDesc();
                    List<ModelCode> propsList = modelResDesc.GetAllPropertyIdsForEntityId(selectedGidFromComboBox);

                    return propsList;
                }
                else
                    return null;
            }
            set
            {
                propsForSelectedGid = value;
                OnPropertyChanged("PropsForSelectedGid");
                OnPropertyChanged("SelectedGidFromComboBox");
            }
        }

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void GetValuesViewResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (propsListBox.SelectedItems == null || SelectedGidFromComboBox == 0)
            {
                MessageBox.Show("Choose attribute!");
                return;
            }

            List<ModelCode> selectedPropsList = new List<ModelCode>();
            foreach (var item in propsListBox.SelectedItems)
            {
                selectedPropsList.Add((ModelCode)item);
            }

           //ClientGda g = new ClientGda();
           //Program.t.GetValues(SelectedGidFromComboBox, selectedPropsList);
           // g.GetValues(SelectedGidFromComboBox, selectedPropsList);

            ResultTextBox.Text = new ClientGda().GetValues(SelectedGidFromComboBox, selectedPropsList);
            // ResultTextBox.Text = File.ReadAllText(Config.Instance.ResultDirecotry + "\\GetValues_Results.xml");
        }

        private void GetExtentValuesButton_Click(object sender, RoutedEventArgs e)
        {
            GetExtentValuesView getExtentValuesWindow = new GetExtentValuesView();
            getExtentValuesWindow.Show();
            this.Close();
        }

        private void GetRelatedValuesButton_Click(object sender, RoutedEventArgs e)
        {
            GetRelatedValuesView getRelatedValuesWindow = new GetRelatedValuesView();
            getRelatedValuesWindow.Show();
            this.Close();

        }
    }
}
