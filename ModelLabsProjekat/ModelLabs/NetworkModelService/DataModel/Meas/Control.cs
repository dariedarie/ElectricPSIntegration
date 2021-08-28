using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Meas
{
    public class Control:IdentifiedObject
    {
        private long regulatingCondEq = 0;
        

        public Control(long globalId)
            : base(globalId)
        {
        }

        public long RegulatingCondEq { get => regulatingCondEq; set => regulatingCondEq = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Control x = (Control)obj;
                return (x.regulatingCondEq == this.regulatingCondEq);
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

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.CONTROL_REGULATINGCONDEQ:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.CONTROL_REGULATINGCONDEQ:
                    property.SetValue(regulatingCondEq);
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

                case ModelCode.CONTROL_REGULATINGCONDEQ:
                    regulatingCondEq = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

    }
}
