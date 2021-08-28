using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class SynchronousMachine:RotatingMachine
    {
        private long reactiveCapabilityCruve = 0;
        public SynchronousMachine(long globalId) : base(globalId)
        {
        }

        public long ReactiveCapabilityCruve { get => reactiveCapabilityCruve; set => reactiveCapabilityCruve = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                SynchronousMachine x = (SynchronousMachine)obj;
                return (x.reactiveCapabilityCruve == this.reactiveCapabilityCruve);
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
                case ModelCode.SYNCHRONOUSMACHINE_REACTIVECAPABILITYCURVE:
                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {

                case ModelCode.SYNCHRONOUSMACHINE_REACTIVECAPABILITYCURVE:
                    property.SetValue(reactiveCapabilityCruve);
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
                case ModelCode.SYNCHRONOUSMACHINE_REACTIVECAPABILITYCURVE:
                    reactiveCapabilityCruve = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }
    }
}
