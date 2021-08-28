using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class RegulatingControl:PowerSystemResource
    {
        private bool discrete;
        private float targetRange;
        private float targetValue;
        private RegulatingControlModeKind mode;
        private PhaseCode monitoredPhase;
        private List<long> regulatingCondEqs = new List<long>();
        public RegulatingControl(long globalId) : base(globalId)
        {
        }

        public List<long> RegulatingCondEqs { get => regulatingCondEqs; set => regulatingCondEqs = value; }
        public bool Discrete { get => discrete; set => discrete = value; }
        public float TargetRange { get => targetRange; set => targetRange = value; }
        public float TargetValue { get => targetValue; set => targetValue = value; }
        public RegulatingControlModeKind Mode { get => mode; set => mode = value; }
        public PhaseCode MonitoredPhase { get => monitoredPhase; set => monitoredPhase = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj)) //da li su mi svi atributi u baznoj klasi jednaki
            {
                RegulatingControl x = (RegulatingControl)obj;
                return (x.discrete == this.discrete && x.mode == this.mode && x.monitoredPhase == this.monitoredPhase && x.targetRange == this.targetRange &&
                        x.targetValue == this.targetValue &&
                        CompareHelper.CompareLists(x.regulatingCondEqs, this.regulatingCondEqs));
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

        #region IAccess
        public override bool HasProperty(ModelCode property) // da li properti pripada ovoj klasi
        {
            switch (property)
            {
                case ModelCode.REGULATINGCONTROL_DISCRETE:
                case ModelCode.REGULATINGCONTROL_MODE:
                case ModelCode.REGULATINGCONTROL_MPHASE:
                case ModelCode.REGULATINGCONTROL_TRANGE:
                case ModelCode.REGULATINGCONTROL_TVALUE:
                case ModelCode.REGULATINGCONTROL_REGULATINGCONDEQS:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop) //dodje properti prazan za value, kada se pravi resourceDesc
        {
            switch (prop.Id)
            {
                case ModelCode.REGULATINGCONTROL_DISCRETE:
                    prop.SetValue(discrete);
                    break;
                case ModelCode.REGULATINGCONTROL_MODE:
                    prop.SetValue((short)mode);
                    break;
                case ModelCode.REGULATINGCONTROL_MPHASE:
                    prop.SetValue((short)monitoredPhase);
                    break;
                case ModelCode.REGULATINGCONTROL_TRANGE:
                    prop.SetValue(targetRange);
                    break;
                case ModelCode.REGULATINGCONTROL_TVALUE:
                    prop.SetValue(TargetValue);
                    break;
                case ModelCode.REGULATINGCONTROL_REGULATINGCONDEQS:
                    prop.SetValue(regulatingCondEqs);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)//zapisi mi u atribut klase
        {
            switch (property.Id)
            {
                case ModelCode.REGULATINGCONTROL_DISCRETE:
                    discrete = property.AsBool();
                    break;
                case ModelCode.REGULATINGCONTROL_MODE:
                    mode = (RegulatingControlModeKind)property.AsEnum();
                    break;
                case ModelCode.REGULATINGCONTROL_MPHASE:
                    monitoredPhase = (PhaseCode)property.AsEnum();
                    break;
                case ModelCode.REGULATINGCONTROL_TRANGE:
                    targetRange = property.AsFloat();
                    break;
                case ModelCode.REGULATINGCONTROL_TVALUE:
                    targetValue = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }
        #endregion IAccess


        public override bool IsReferenced
        {
            get
            {
                return regulatingCondEqs.Count != 0 || base.IsReferenced;
            }
        }



        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULATINGCONDEQ_REGULATINGCONTROL:
                    regulatingCondEqs.Add(globalId);
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
                case ModelCode.REGULATINGCONDEQ_REGULATINGCONTROL:

                    if (regulatingCondEqs.Contains(globalId))
                    {
                        regulatingCondEqs.Remove(globalId);
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
