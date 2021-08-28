using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class RegulatingCondEq:ConductingEquipment
    {
        private long regulatingControl = 0;

        private List<long> controls = new List<long>();
        public RegulatingCondEq(long globalId)
            : base(globalId)
        {
        }

        public long RegulatingControl { get => regulatingControl; set => regulatingControl = value; }
        public List<long> Controls { get => controls; set => controls = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegulatingCondEq x = (RegulatingCondEq)obj;
                return (x.regulatingControl == this.regulatingControl &&
                        CompareHelper.CompareLists(x.controls, this.controls));
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

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                
                case ModelCode.REGULATINGCONDEQ_REGULATINGCONTROL:
                case ModelCode.REGULATINGCONDEQ_CONTROLS:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
               

                case ModelCode.REGULATINGCONDEQ_REGULATINGCONTROL:
                    property.SetValue(regulatingControl);
                    break;

                case ModelCode.REGULATINGCONDEQ_CONTROLS:
                    property.SetValue(controls);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGULATINGCONDEQ_REGULATINGCONTROL:
                    regulatingControl = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override bool IsReferenced
        {
            get
            {
                return controls.Count != 0 || base.IsReferenced;
            }
        }

      

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.CONTROL_REGULATINGCONDEQ:
                    controls.Add(globalId);
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
                case ModelCode.CONTROL_REGULATINGCONDEQ:

                    if (controls.Contains(globalId))
                    {
                        controls.Remove(globalId);
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
