using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class ReactiveCapabilityCurve:Curve
    {
        private List<long> synMachines = new List<long>();

        public List<long> SynMachines { get => synMachines; set => synMachines = value; }

        public ReactiveCapabilityCurve(long globalId) : base(globalId)
        {
        }


        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ReactiveCapabilityCurve x = (ReactiveCapabilityCurve)obj;
                return (CompareHelper.CompareLists(x.synMachines, this.synMachines, true));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.REACTIVECAPABILITYCURVE_SYNCHRONOUSMACHINES:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {

                case ModelCode.REACTIVECAPABILITYCURVE_SYNCHRONOUSMACHINES:
                    prop.SetValue(synMachines);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        public override bool IsReferenced
        {
            get
            {
                return synMachines.Count != 0 || base.IsReferenced;
            }
        }



        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SYNCHRONOUSMACHINE_REACTIVECAPABILITYCURVE:
                    synMachines.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SYNCHRONOUSMACHINE_REACTIVECAPABILITYCURVE:

                    if (synMachines.Contains(globalId))
                    {
                        synMachines.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }


    }
}
