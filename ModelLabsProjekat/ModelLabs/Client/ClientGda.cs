using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Threading;
using System.Diagnostics;
using FTN.Common;
using FTN.ServiceContracts;
using FTN.Services.NetworkModelService;
using FTN.Services.NetworkModelService.TestClient;
using Client;

namespace TelventDMS.Services.NetworkModelService.TestClient.Tests
{
	public class ClientGda : IDisposable
	{			

		private ModelResourcesDesc modelResourcesDesc = new ModelResourcesDesc();
		private NetworkModelGDAProxy gdaQueryProxy = null;

		public ClientGda()
		{
		}

		private NetworkModelGDAProxy GdaQueryProxy
		{
			get
			{
				if (gdaQueryProxy != null)
				{
					gdaQueryProxy.Abort();
                    gdaQueryProxy = null;
				}

				gdaQueryProxy = new NetworkModelGDAProxy("NetworkModelGDAEndpoint");
				gdaQueryProxy.Open();

				return gdaQueryProxy;
			}
		}



        public List<long> GetAllGids()
        {
            List<ModelCode> tipovi = new List<ModelCode>() { ModelCode.CONTROL, ModelCode.CURVEDATA, ModelCode.FREQUENCYCONVERTER, ModelCode.REACTIVECAPABILITYCURVE, ModelCode.REGULATINGCONTROL, ModelCode.SHUNTCOMPENSATOR, ModelCode.STATICVARCOMPENSATOR, ModelCode.SYNCHRONOUSMACHINE, ModelCode.TERMINAL };
            string message = "";
            ModelCode modelCode = ModelCode.CONTROL;

            int iteratorId = 0;
            List<long> gids = new List<long>();

            try
            {
                foreach (ModelCode t in tipovi)
                {
                    modelCode = t;
                    int numberOfResources = 2;
                    int resourcesLeft = 0;

                    List<ModelCode> properties = modelResourcesDesc.GetAllPropertyIds(t);

                    iteratorId = GdaQueryProxy.GetExtentValues(t, properties);
                    resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);


                    while (resourcesLeft > 0)
                    {
                        List<ResourceDescription> rds = GdaQueryProxy.IteratorNext(numberOfResources, iteratorId);

                        for (int i = 0; i < rds.Count; i++)
                        {
                            gids.Add(rds[i].Id);
                        }

                        resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);
                    }

                    GdaQueryProxy.IteratorClose(iteratorId);
                }

            }
            catch (Exception e)
            {
                message = string.Format("Getting extent values method failed for {0}.\n\t{1}", modelCode, e.Message);
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
            }

            return gids;
        }


       
        public string GetValues(long globalId, List<ModelCode> props)
        {
            ResourceDescription resDesc = null;
            string str = "";
            List<ModelCode> properties = new List<ModelCode>();
           // XmlTextWriter xmlWriter = null;
            //short type = ModelCodeHelper.ExtractTypeFromGlobalId(globalId);
            properties = props;

            resDesc = GdaQueryProxy.GetValues(globalId, properties); //pozove se server za gid,nazad resdesc

            ////xmlWriter = new XmlTextWriter(ConfigUI.Instance.ResultDirecotry + "\\GetValues_Results.xml", Encoding.Unicode);
            ////xmlWriter.Formatting = Formatting.Indented;
            //resDesc.ExportToXml(writer);
            // writer.Flush();
            //if (writer != null)
            //{
            //    writer.Close();
            //}

            str += String.Format("Item with gid: 0x{0:x16}:\n", globalId);
            foreach (Property p in resDesc.Properties)
            {
                str += String.Format("Property id:{0} = ", p.Id);
                string retVal = Export(p);
                str += retVal;
            }

            return str;
        }



        public string GetExtentValues(ModelCode modelCode, List<ModelCode> props)
        {
            int iteratorId = 0;
            List<long> ids = new List<long>();
            string str = "";
            bool proveriGid = true;
            
            int numberOfResources = 2;
            int resourcesLeft = 0;

            List<ModelCode> properties = props;
            if (!props.Contains(ModelCode.IDOBJ_GID))
            {
               properties.Add(ModelCode.IDOBJ_GID);
                proveriGid = false;
            }
            iteratorId = GdaQueryProxy.GetExtentValues(modelCode, properties);
            resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);
            str += String.Format("Items with ModelCode: {0}:\n", modelCode.ToString());
            while (resourcesLeft > 0)
            {
                List<ResourceDescription> rds = GdaQueryProxy.IteratorNext(numberOfResources, iteratorId);

                for (int i = 0; i < rds.Count; i++)
                {
                    str += String.Format("\tItem with gid: 0x{0:x16}\n", rds[i].Properties.Find(r => r.Id == ModelCode.IDOBJ_GID).AsLong());
                    foreach (Property p in rds[i].Properties)
                    {
                        if (!(p.Id == ModelCode.IDOBJ_GID && proveriGid == false))
                        {
                            str += String.Format("{0} = ", p.Id);
                            string retVal = Export(p);
                            str += retVal;
                        }
                    }
                }
                resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);
            }

            GdaQueryProxy.IteratorClose(iteratorId);

            return str;
        }

        public string GetRelatedValues(long sourceGlobalId, Association association, List<ModelCode> props)
        {
            string str = "";
            int numberOfResources = 2;
            bool gidBool = true;
            
            List<ModelCode> properties = props;
            if (!props.Contains(ModelCode.IDOBJ_GID))
            {
                properties.Add(ModelCode.IDOBJ_GID);
                gidBool = false;
            }
            int iteratorId = GdaQueryProxy.GetRelatedValues(sourceGlobalId, properties, association);
            int resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);

            while (resourcesLeft > 0)
            {
                List<ResourceDescription> rds = GdaQueryProxy.IteratorNext(numberOfResources, iteratorId);

                for (int i = 0; i < rds.Count; i++)
                {
                    str += String.Format("Item with gid: 0x{0:x16}\n", rds[i].Properties.Find(r => r.Id == ModelCode.IDOBJ_GID).AsLong());
                    foreach (Property p in rds[i].Properties)
                    {
                        if (!(p.Id == ModelCode.IDOBJ_GID && gidBool == false))
                        {
                            str += String.Format("{0} = ", p.Id);
                            string retVal = Export(p);
                            str += retVal;
                        }
                    }
                }
                resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);
            }

            GdaQueryProxy.IteratorClose(iteratorId);

            return str;
        }

        public string Export(Property p)
        {
            string str = "";
            switch (p.Type)
            {
                case PropertyType.Float:
                    str += String.Format(" {0}\n", p.AsFloat());
                    break;
                case PropertyType.Bool:
                    str += String.Format("{0}\n", (p.AsInt() == 1) ? true : false);
                    break;
                case PropertyType.Int32:
                case PropertyType.Int64:
                    if (p.Id == ModelCode.IDOBJ_GID)
                    {
                        str += (String.Format("0x{0:x16}\n", p.AsLong()));
                    }
                    else
                    {
                        str += String.Format("{0}\n", p.AsLong());
                    }
                    break;
                case PropertyType.Reference:
                    str += (String.Format("0x{0:x16}\n", p.AsReference()));
                    break;
                case PropertyType.String:
                    if (p.PropertyValue.StringValue == null)
                    {
                        p.PropertyValue.StringValue = String.Empty;
                    }
                    str += String.Format("{0}\n", p.AsString());
                    break;
                case PropertyType.ReferenceVector:
                    if (p.AsLongs().Count > 0)
                    {
                        string tempStr = "";
                        for (int j = 0; j < p.AsLongs().Count; j++)
                        {
                            tempStr += (String.Format("0x{0:x16},\n", p.AsLongs()[j]));

                        }
                        str += tempStr;
                    }
                    else
                    {
                        str += ("empty long/reference vector\n");
                    }
                    break;
                case PropertyType.Enum:

                    List<string> listEnums = new EnumDescs().GetEnumList(p.Id);
                    str += (String.Format(listEnums[Int32.Parse(p.ToString())]) + "\n");
                    break;

                default:
                    throw new Exception("Invalid property type");
            }
            return str;
        }

        public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
