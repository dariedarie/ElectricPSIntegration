using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Curve:IdentifiedObject
    {
        private CurveStyle curveStyle;
        private UnitMultiplier xMultiplier;
        private UnitSymbol xUnit;
        private UnitMultiplier y1Multiplier;
        private UnitMultiplier y2Multiplier;
        private UnitMultiplier y3Multiplier;
        private UnitSymbol y1Unit;
        private UnitSymbol y2Unit;
        private UnitSymbol y3Unit;
        private List<long> curveDatas = new List<long>();

        public Curve(long globalId) : base(globalId)
        {
        }

        public CurveStyle CurveStyle { get => curveStyle; set => curveStyle = value; }
        public UnitMultiplier XMultiplier { get => xMultiplier; set => xMultiplier = value; }
        public UnitSymbol XUnit { get => xUnit; set => xUnit = value; }
        public UnitMultiplier Y1Multiplier { get => y1Multiplier; set => y1Multiplier = value; }
        public UnitMultiplier Y2Multiplier { get => y2Multiplier; set => y2Multiplier = value; }
        public UnitMultiplier Y3Multiplier { get => y3Multiplier; set => y3Multiplier = value; }
        public UnitSymbol Y1Unit { get => y1Unit; set => y1Unit = value; }
        public UnitSymbol Y2Unit { get => y2Unit; set => y2Unit = value; }
        public UnitSymbol Y3Unit { get => y3Unit; set => y3Unit = value; }
        public List<long> CurveDatas { get => curveDatas; set => curveDatas = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Curve x = (Curve)obj;
                return (x.curveStyle == this.curveStyle && x.xMultiplier == this.xMultiplier && x.xUnit == this.xUnit && x.y1Multiplier == this.y1Multiplier &&
                        x.y2Multiplier == this.y2Multiplier &&
                        x.y3Multiplier == this.y3Multiplier && x.y1Unit == this.y1Unit && x.y2Unit == this.y2Unit && x.y3Unit == this.y3Unit &&
                        CompareHelper.CompareLists(x.curveDatas, this.curveDatas));
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
        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.CURVE_CSTYLE:
                case ModelCode.CURVE_XMULTIPLIER:
                case ModelCode.CURVE_XUNIT:
                case ModelCode.CURVE_Y1MULTIPLIER:
                case ModelCode.CURVE_Y2MULTIPLIER:
                case ModelCode.CURVE_Y3MULTIPLIER:
                case ModelCode.CURVE_Y1UNIT:
                case ModelCode.CURVE_Y2UNIT:
                case ModelCode.CURVE_Y3UNIT:
                case ModelCode.CURVE_CURVEDATAS:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.CURVE_CSTYLE:
                    prop.SetValue((short)curveStyle);
                    break;
                case ModelCode.CURVE_XMULTIPLIER:
                    prop.SetValue((short)xMultiplier);
                    break;
                case ModelCode.CURVE_XUNIT:
                    prop.SetValue((short)xUnit);
                    break;
                case ModelCode.CURVE_Y1MULTIPLIER:
                    prop.SetValue((short)y1Multiplier);
                    break;
                case ModelCode.CURVE_Y2MULTIPLIER:
                    prop.SetValue((short)y2Multiplier);
                    break;
                case ModelCode.CURVE_Y3MULTIPLIER:
                    prop.SetValue((short)y3Multiplier);
                    break;
                case ModelCode.CURVE_Y1UNIT:
                    prop.SetValue((short)y1Unit);
                    break;
                case ModelCode.CURVE_Y2UNIT:
                    prop.SetValue((short)y2Unit);
                    break;
                case ModelCode.CURVE_Y3UNIT:
                    prop.SetValue((short)y3Unit);
                    break;
                case ModelCode.CURVE_CURVEDATAS:
                    prop.SetValue(curveDatas);
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
                
                case ModelCode.CURVE_CSTYLE:
                    curveStyle = (CurveStyle)property.AsEnum();
                    break;
                case ModelCode.CURVE_XMULTIPLIER:
                    xMultiplier = (UnitMultiplier)property.AsEnum();
                    break;
                case ModelCode.CURVE_XUNIT:
                    xUnit = (UnitSymbol)property.AsEnum();
                    break;
                case ModelCode.CURVE_Y1MULTIPLIER:
                    y1Multiplier = (UnitMultiplier)property.AsEnum();
                    break;
                case ModelCode.CURVE_Y2MULTIPLIER:
                    y2Multiplier = (UnitMultiplier)property.AsEnum();
                    break;
                case ModelCode.CURVE_Y3MULTIPLIER:
                    y3Multiplier = (UnitMultiplier)property.AsEnum();
                    break;
                case ModelCode.CURVE_Y1UNIT:
                    y1Unit = (UnitSymbol)property.AsEnum();
                    break;
                case ModelCode.CURVE_Y2UNIT:
                    y2Unit = (UnitSymbol)property.AsEnum();
                    break;
                case ModelCode.CURVE_Y3UNIT:
                    y3Unit = (UnitSymbol)property.AsEnum();
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
                return curveDatas.Count != 0 || base.IsReferenced;
            }
        }



        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.CURVEDATA_CURVE:
                    curveDatas.Add(globalId);
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
                case ModelCode.CURVEDATA_CURVE:

                    if (curveDatas.Contains(globalId))
                    {
                        curveDatas.Remove(globalId);
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
