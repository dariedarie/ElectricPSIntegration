using FTN.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using TelventDMS.Services.NetworkModelService.TestClient.Tests;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for GetRelatedValuesView.xaml
    /// </summary>
    public partial class GetRelatedValuesView : Window, INotifyPropertyChanged
    {
        private List<long> gidComboBox = new List<long>();
        private long selectedGidFromComboBox;

        private List<ModelCode> propIdComboBox = new List<ModelCode>();
        private ModelCode selectedPropIdFromComboBox;

        private List<ModelCode> typeComboBox = new List<ModelCode>();
        private ModelCode selectedTypeFromComboBox;

        private List<ModelCode> propsForSelectedAssociation = new List<ModelCode>();

        public event PropertyChangedEventHandler PropertyChanged;

        public GetRelatedValuesView()
        {
            InitializeComponent();
            ClientGda gdaProxy = new ClientGda();
            GidComboBox = gdaProxy.GetAllGids();
            DataContext = this;
        }

        public List<long> GidComboBox
        {
            get { return gidComboBox; }
            set
            {
                gidComboBox = value;
                OnPropertyChanged("GidComboBox");
            }
        }

        public long SelectedGidFromComboBox
        {
            get { return selectedGidFromComboBox; }
            set
            {
                selectedGidFromComboBox = value;
                OnPropertyChanged("SelectedGidFromComboBox");
                OnPropertyChanged("PropIdComboBox");
                OnPropertyChanged("TypeComboBox");
                OnPropertyChanged("PropsForSelectedAssociation");
            }
        }

        public List<ModelCode> PropIdComboBox
        {
            get
            {
                if (selectedGidFromComboBox != 0)
                {
                    ModelResourcesDesc modelRes = new ModelResourcesDesc();
                    List<ModelCode> list = modelRes.GetAllPropertyIdsForEntityId(selectedGidFromComboBox);
                    List<ModelCode> ret = new List<ModelCode>();

                    foreach (ModelCode m in list)
                    {
                        if (Property.GetPropertyType(m) == PropertyType.Reference ||
                            Property.GetPropertyType(m) == PropertyType.ReferenceVector)
                        {
                            ret.Add(m);
                        }
                    }

                    return ret;
                }
                return null;
            }
            set
            {
                propIdComboBox = value;
                OnPropertyChanged("PropIdComboBox");
                OnPropertyChanged("TypeComboBox");
            }
        }

        public ModelCode SelectedPropIdFromComboBox
        {
            get { return selectedPropIdFromComboBox; }
            set
            {
                selectedPropIdFromComboBox = value;
                OnPropertyChanged("SelectedPropIdFromComboBox");
                GetTypes(selectedPropIdFromComboBox);
                //typeComboBox.Add((long)0x000);
                OnPropertyChanged("TypeComboBox");
            }
        }

        public List<ModelCode> TypeComboBox
        {
            get { return typeComboBox; }
            set
            {
                typeComboBox = value;
                OnPropertyChanged("TypeComboBox");
                OnPropertyChanged("PropsForSelectedAssociation");
            }
        }
        public ModelCode SelectedTypeFromComboBox
        {
            get { return selectedTypeFromComboBox; }
            set
            {
                selectedTypeFromComboBox = value;
                OnPropertyChanged("SelectedTypeFromComboBox");
                OnPropertyChanged("PropsForSelectedAssociation");
            }
        }

        public List<ModelCode> PropsForSelectedAssociation
        {
            get
            {
                if (selectedTypeFromComboBox != 0)
                {
                    return SetPropList(selectedTypeFromComboBox, false);
                }
                else
                {
                    return SetPropList(selectedTypeFromComboBox, true);
                }
            }
            set
            {
                propsForSelectedAssociation = value;
                OnPropertyChanged("PropsForSelectedAssociation");
            }
        }

       
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        


        private List<ModelCode> GetTypes(ModelCode selectedPropId)
        {
            ModelResourcesDesc modResDes = new ModelResourcesDesc();

            string[] props = (selectedPropId.ToString()).Split('_');
            props[1] = props[1].TrimEnd('S');

            

            foreach (ModelCode modelCode in Enum.GetValues(typeof(ModelCode))) 
            {
                if (String.Compare(props[1], modelCode.ToString()) == 0)
                {
                    DMSType type = ModelCodeHelper.GetTypeFromModelCode(modelCode); 
                    if (type == 0)
                    {
                        typeComboBox = new List<ModelCode>();
                        List<DMSType> leafs = modResDes.GetLeaves(modelCode);
                        foreach (DMSType cc in leafs)
                        {
                            typeComboBox.Add(modResDes.GetModelCodeFromType(cc));
                        }
                    }
                    else
                    {
                        typeComboBox = new List<ModelCode>();
                        typeComboBox.Add(modelCode);
                    }
                }
            }

            return new List<ModelCode>();
        }

        public List<ModelCode> SetPropList(ModelCode mc, bool isTypeZero)
        {
            ModelResourcesDesc mr = new ModelResourcesDesc();
            List<ModelCode> list = new List<ModelCode>();
            if (!isTypeZero)
            {
                list = mr.GetAllPropertyIds(mc);
            }
            //else
            //{
            //    List<ModelCode> DmsTypes = new List<ModelCode>();
            //    DmsTypes = TypeComboBox;
            //    foreach (var type in DmsTypes)
            //    {
            //        if (type.ToString() == "0")
            //        {
            //            continue;
            //        }
            //        List<ModelCode> helperList = new List<ModelCode>();
            //        helperList = mr.GetAllPropertyIds(type);
            //        foreach (var prop in helperList)
            //        {
            //            if (list.Contains(prop))
            //            {
            //                continue;
            //            }
            //            list.Add(prop);
            //        }
            //    }
            //}

            return list;
        }

        private void GetRelatedValuesViewResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (propsListBox.SelectedItems == null || SelectedPropIdFromComboBox == 0 || SelectedGidFromComboBox == 0)
            {
                MessageBox.Show("Choose inputs!");
                return;
            }

            List<ModelCode> selectedProps = new List<ModelCode>();
            foreach (var prop in propsListBox.SelectedItems)
            {
                selectedProps.Add((ModelCode)prop);
            }

            Association association = new Association();
            association.PropertyId = SelectedPropIdFromComboBox;
            association.Type = SelectedTypeFromComboBox;

            string str = "";

            if (SelectedTypeFromComboBox.ToString() != "0")
            {
                ResultTextBox.Text = new ClientGda().GetRelatedValues(SelectedGidFromComboBox, association, selectedProps);
            }
            else
            {
                //foreach (var type in TypeComboBox)
                //{
                //    if (type.ToString() == "0")
                //    {
                //        continue;
                //    }
                //    association.Type = type;
                //    List<ModelCode> retVal = new List<ModelCode>();
                //    retVal = GetPropsOfAllTypes(selectedProps, SetPropList(type, false));
                //    str += new ClientGda().GetRelatedValues(SelectedGidFromComboBox, association, retVal);
                //}
                //ResultTextBox.Text = str;
            }
        }

        private List<ModelCode> GetPropsOfAllTypes(List<ModelCode> selectedProps, List<ModelCode> allProps)
        {
            List<ModelCode> retVal = new List<ModelCode>();

            foreach (var prop in allProps)
            {
                if (selectedProps.Contains(prop))
                {
                    retVal.Add(prop);
                }
            }

            return retVal;
        }

        private void GetExtentValuesButton_Click(object sender, RoutedEventArgs e)
        {
            GetExtentValuesView getExtentValuesView = new GetExtentValuesView();
            getExtentValuesView.Show();
            this.Close();
        }

        private void GetValuesButton_Click(object sender, RoutedEventArgs e)
        {
            GetValuesView getValuesView = new GetValuesView();
            getValuesView.Show();

            this.Close();
        }
    }
}
