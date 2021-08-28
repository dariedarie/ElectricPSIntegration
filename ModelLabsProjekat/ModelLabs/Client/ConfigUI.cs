using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ConfigUI
    {

        private string resultDirecotry = string.Empty;

        public string ResultDirecotry
        {
            get { return resultDirecotry; }
        }

        private ConfigUI()
        {
            resultDirecotry = ConfigurationManager.AppSettings["ResultDirecotry"];

            if (!Directory.Exists(resultDirecotry))
            {
                Directory.CreateDirectory(resultDirecotry);
            }
        }

       

        private static ConfigUI instance = null;

        public static ConfigUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigUI();
                }

                return instance;
            }
        }
    }
}
