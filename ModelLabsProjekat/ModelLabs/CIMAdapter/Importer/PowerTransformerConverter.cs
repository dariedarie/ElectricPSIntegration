namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)  // ako sam kroz cim dobio vrednost
                {
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
				}
			}
		}

        public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
        {
            if ((cimPowerSystemResource != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd); //pozivam od parenta
  
            }
        }

        public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd)
        {
            if ((cimEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd);

                if (cimEquipment.AggregateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
                }
                if (cimEquipment.NormallyInServiceHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_NINSERVICE, cimEquipment.NormallyInService));
                }
            }
        }

        public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd)
        {
            if ((cimConductingEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd);
            }
        }


        public static void PopulateRegulatingCondEqProperties(FTN.RegulatingCondEq cimRegulatingCondEq, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRegulatingCondEq != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductingEquipmentProperties(cimRegulatingCondEq, rd);

                if (cimRegulatingCondEq.RegulatingControlHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimRegulatingCondEq.RegulatingControl.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimRegulatingCondEq.GetType().ToString()).Append(" rdfID = \"").Append(cimRegulatingCondEq.ID);
                        report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimRegulatingCondEq.RegulatingControl.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REGULATINGCONDEQ_REGULATINGCONTROL, gid));
                }
            }
        }

        public static void PopulateStaticVarCompensatorProperties(FTN.StaticVarCompensator cimStaticVarCompensator, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimStaticVarCompensator != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateRegulatingCondEqProperties(cimStaticVarCompensator, rd, importHelper, report);
            }
        }

        public static void PopulateShuntCompensatorProperties(FTN.ShuntCompensator cimShuntCompensator, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimShuntCompensator != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateRegulatingCondEqProperties(cimShuntCompensator, rd, importHelper, report);
            }
        }

        public static void PopulateFrequencyConverterProperties(FTN.FrequencyConverter cimFrequencyConverter, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimFrequencyConverter != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateRegulatingCondEqProperties(cimFrequencyConverter, rd, importHelper, report);
            }
        }

        public static void PopulateRotatingMachineProperties(FTN.RotatingMachine cimRotatingMachine, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRotatingMachine != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateRegulatingCondEqProperties(cimRotatingMachine, rd, importHelper, report);
            }
        }

        public static void PopulateSynchronousMachineProperties(FTN.SynchronousMachine cimSynchronousMachine, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSynchronousMachine != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateRotatingMachineProperties(cimSynchronousMachine, rd, importHelper, report);

                if (cimSynchronousMachine.ReactiveCapabilityCurvesHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimSynchronousMachine.ReactiveCapabilityCurves.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSynchronousMachine.GetType().ToString()).Append(" rdfID = \"").Append(cimSynchronousMachine.ID);
                        report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimSynchronousMachine.ReactiveCapabilityCurves.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.SYNCHRONOUSMACHINE_REACTIVECAPABILITYCURVE, gid));
                }

            }
        }

        public static void PopulateControlProperties(FTN.Control cimControl, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimControl != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimControl, rd);

                if (cimControl.RegulatingCondEqHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimControl.RegulatingCondEq.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimControl.GetType().ToString()).Append(" rdfID = \"").Append(cimControl.ID);
                        report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimControl.RegulatingCondEq.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.CONTROL_REGULATINGCONDEQ, gid));
                }

            }
        }

        public static void PopulateTerminalProperties(FTN.Terminal cimTerminal, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTerminal != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimTerminal, rd);

                if (cimTerminal.ConductingEquipmentHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTerminal.ConductingEquipment.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
                        report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimTerminal.ConductingEquipment.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TERMINAL_CONDUCTINGEQUIPMENT, gid));
                }

            }
        }

        public static void PopulateRegulatingControlProperties(FTN.RegulatingControl cimRegulatingControl, ResourceDescription rd)
        {
            if ((cimRegulatingControl != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimRegulatingControl, rd);

                if (cimRegulatingControl.DiscreteHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_DISCRETE, cimRegulatingControl.Discrete));
                }
                if (cimRegulatingControl.ModeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_MODE, (short)cimRegulatingControl.Mode));
                }
                if (cimRegulatingControl.MonitoredPhaseHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_MPHASE, (short)cimRegulatingControl.MonitoredPhase));
                }
                if (cimRegulatingControl.TargetRangeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_TRANGE, cimRegulatingControl.TargetRange));
                }
                if (cimRegulatingControl.TargetValueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_TVALUE, cimRegulatingControl.TargetValue));
                }
               
            }
        }

        public static void PopulateCurveDataProperties(FTN.CurveData cimCurveData, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimCurveData != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimCurveData, rd);

                if (cimCurveData.XvalueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_XVALUE, cimCurveData.Xvalue));
                }
                if (cimCurveData.Y1valueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_Y1VALUE, cimCurveData.Y1value));
                }
                if (cimCurveData.Y2valueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_Y2VALUE, cimCurveData.Y2value));
                }
                if (cimCurveData.Y3valueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_Y3VALUE, cimCurveData.Y3value));
                }

                if (cimCurveData.CurveHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimCurveData.Curve.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimCurveData.GetType().ToString()).Append(" rdfID = \"").Append(cimCurveData.ID);
                        report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimCurveData.Curve.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_CURVE, gid));
                }

            }
        }


        public static void PopulateCurveProperties(FTN.Curve cimCurve, ResourceDescription rd)
        {
            if ((cimCurve != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimCurve, rd);

                if (cimCurve.CurveStyleHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_CSTYLE, (short)cimCurve.CurveStyle));
                }
                if (cimCurve.XMultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_XMULTIPLIER, (short)cimCurve.XMultiplier));
                }
                if (cimCurve.XUnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_XUNIT, (short)cimCurve.XUnit));
                }
                if (cimCurve.Y1MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y1MULTIPLIER, (short)cimCurve.Y1Multiplier));
                }
                if (cimCurve.Y1UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y1UNIT,(short)cimCurve.Y1Unit));
                }
                if (cimCurve.Y2MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y2MULTIPLIER, (short)cimCurve.Y2Multiplier));
                }
                if (cimCurve.Y2UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y2UNIT, (short)cimCurve.Y2Unit));
                }
                if (cimCurve.Y3MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y3MULTIPLIER, (short)cimCurve.Y3Multiplier));
                }
                if (cimCurve.Y3UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y3UNIT, (short)cimCurve.Y3Unit));
                }

            }
        }

        public static void PopulateReactiveCapabilityCurveProperties(FTN.ReactiveCapabilityCurve cimReactiveCapabilityCurve, ResourceDescription rd)
        {
            if ((cimReactiveCapabilityCurve != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateCurveProperties(cimReactiveCapabilityCurve, rd);
            }
        }


        /*
                public static void PopulateLocationProperties(FTN.Location cimLocation, ResourceDescription rd)
                {
                    if ((cimLocation != null) && (rd != null))
                    {
                        PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimLocation, rd);

                        if (cimLocation.CategoryHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.LOCATION_CATEGORY, cimLocation.Category));
                        }
                        if (cimLocation.CorporateCodeHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.LOCATION_CORPORATECODE, cimLocation.CorporateCode));
                        }
                    }
                }

                public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
                {
                    if ((cimPowerSystemResource != null) && (rd != null))
                    {
                        PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);

                        if (cimPowerSystemResource.CustomTypeHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.PSR_CUSTOMTYPE, cimPowerSystemResource.CustomType));
                        }
                        if (cimPowerSystemResource.LocationHasValue)
                        {
                            long gid = importHelper.GetMappedGID(cimPowerSystemResource.Location.ID);
                            if (gid < 0)
                            {
                                report.Report.Append("WARNING: Convert ").Append(cimPowerSystemResource.GetType().ToString()).Append(" rdfID = \"").Append(cimPowerSystemResource.ID);
                                report.Report.Append("\" - Failed to set reference to Location: rdfID \"").Append(cimPowerSystemResource.Location.ID).AppendLine(" \" is not mapped to GID!");
                            }
                            rd.AddProperty(new Property(ModelCode.PSR_LOCATION, gid));
                        }
                    }
                }

                public static void PopulateBaseVoltageProperties(FTN.BaseVoltage cimBaseVoltage, ResourceDescription rd)
                {
                    if ((cimBaseVoltage != null) && (rd != null))
                    {
                        PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBaseVoltage, rd);

                        if (cimBaseVoltage.NominalVoltageHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.BASEVOLTAGE_NOMINALVOLTAGE, cimBaseVoltage.NominalVoltage));
                        }
                    }
                }

                public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
                {
                    if ((cimEquipment != null) && (rd != null))
                    {
                        PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);

                        if (cimEquipment.PrivateHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.EQUIPMENT_ISPRIVATE, cimEquipment.Private));
                        }
                        if (cimEquipment.IsUndergroundHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.EQUIPMENT_ISUNDERGROUND, cimEquipment.IsUnderground));
                        }
                    }
                }

                public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
                {
                    if ((cimConductingEquipment != null) && (rd != null))
                    {
                        PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd, importHelper, report);

                        if (cimConductingEquipment.PhasesHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.CONDEQ_PHASES, (short)GetDMSPhaseCode(cimConductingEquipment.Phases)));
                        }
                        if (cimConductingEquipment.RatedVoltageHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.CONDEQ_RATEDVOLTAGE, cimConductingEquipment.RatedVoltage));
                        }
                        if (cimConductingEquipment.BaseVoltageHasValue)
                        {
                            long gid = importHelper.GetMappedGID(cimConductingEquipment.BaseVoltage.ID);
                            if (gid < 0)
                            {
                                report.Report.Append("WARNING: Convert ").Append(cimConductingEquipment.GetType().ToString()).Append(" rdfID = \"").Append(cimConductingEquipment.ID);
                                report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimConductingEquipment.BaseVoltage.ID).AppendLine(" \" is not mapped to GID!");
                            }
                            rd.AddProperty(new Property(ModelCode.CONDEQ_BASVOLTAGE, gid));
                        }
                    }
                }

                public static void PopulatePowerTransformerProperties(FTN.PowerTransformer cimPowerTransformer, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
                {
                    if ((cimPowerTransformer != null) && (rd != null))
                    {
                        PowerTransformerConverter.PopulateEquipmentProperties(cimPowerTransformer, rd, importHelper, report);

                        if (cimPowerTransformer.FunctionHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.POWERTR_FUNC, (short)GetDMSTransformerFunctionKind(cimPowerTransformer.Function)));
                        }
                        if (cimPowerTransformer.AutotransformerHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.POWERTR_AUTO, cimPowerTransformer.Autotransformer));
                        }
                    }
                }

                public static void PopulateTransformerWindingProperties(FTN.TransformerWinding cimTransformerWinding, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
                {
                    if ((cimTransformerWinding != null) && (rd != null))
                    {
                        PowerTransformerConverter.PopulateConductingEquipmentProperties(cimTransformerWinding, rd, importHelper, report);

                        if (cimTransformerWinding.ConnectionTypeHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.POWERTRWINDING_CONNTYPE, (short)GetDMSWindingConnection(cimTransformerWinding.ConnectionType)));
                        }
                        if (cimTransformerWinding.GroundedHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.POWERTRWINDING_GROUNDED, cimTransformerWinding.Grounded));
                        }
                        if (cimTransformerWinding.RatedSHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.POWERTRWINDING_RATEDS, cimTransformerWinding.RatedS));
                        }
                        if (cimTransformerWinding.RatedUHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.POWERTRWINDING_RATEDU, cimTransformerWinding.RatedU));
                        }
                        if (cimTransformerWinding.WindingTypeHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.POWERTRWINDING_WINDTYPE, (short)GetDMSWindingType(cimTransformerWinding.WindingType)));
                        }
                        if (cimTransformerWinding.PhaseToGroundVoltageHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.POWERTRWINDING_PHASETOGRNDVOLTAGE, cimTransformerWinding.PhaseToGroundVoltage));
                        }
                        if (cimTransformerWinding.PhaseToPhaseVoltageHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.POWERTRWINDING_PHASETOPHASEVOLTAGE, cimTransformerWinding.PhaseToPhaseVoltage));
                        }
                        if (cimTransformerWinding.PowerTransformerHasValue)
                        {
                            long gid = importHelper.GetMappedGID(cimTransformerWinding.PowerTransformer.ID);
                            if (gid < 0)
                            {
                                report.Report.Append("WARNING: Convert ").Append(cimTransformerWinding.GetType().ToString()).Append(" rdfID = \"").Append(cimTransformerWinding.ID);
                                report.Report.Append("\" - Failed to set reference to PowerTransformer: rdfID \"").Append(cimTransformerWinding.PowerTransformer.ID).AppendLine(" \" is not mapped to GID!");
                            }
                            rd.AddProperty(new Property(ModelCode.POWERTRWINDING_POWERTRW, gid));
                        }
                    }
                }

                public static void PopulateWindingTestProperties(FTN.WindingTest cimWindingTest, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
                {
                    if ((cimWindingTest != null) && (rd != null))
                    {
                        PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimWindingTest, rd);

                        if (cimWindingTest.LeakageImpedanceHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDN, cimWindingTest.LeakageImpedance));
                        }
                        if (cimWindingTest.LoadLossHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.WINDINGTEST_LOADLOSS, cimWindingTest.LoadLoss));
                        }
                        if (cimWindingTest.NoLoadLossHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.WINDINGTEST_NOLOADLOSS, cimWindingTest.NoLoadLoss));
                        }
                        if (cimWindingTest.PhaseShiftHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.WINDINGTEST_PHASESHIFT, cimWindingTest.PhaseShift));
                        }
                        if (cimWindingTest.LeakageImpedance0PercentHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDN0PERCENT, cimWindingTest.LeakageImpedance0Percent));
                        }
                        if (cimWindingTest.LeakageImpedanceMaxPercentHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDNMAXPERCENT, cimWindingTest.LeakageImpedanceMaxPercent));
                        }
                        if (cimWindingTest.LeakageImpedanceMinPercentHasValue)
                        {
                            rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDNMINPERCENT, cimWindingTest.LeakageImpedanceMinPercent));
                        }

                        if (cimWindingTest.From_TransformerWindingHasValue)
                        {
                            long gid = importHelper.GetMappedGID(cimWindingTest.From_TransformerWinding.ID);
                            if (gid < 0)
                            {
                                report.Report.Append("WARNING: Convert ").Append(cimWindingTest.GetType().ToString()).Append(" rdfID = \"").Append(cimWindingTest.ID);
                                report.Report.Append("\" - Failed to set reference to TransformerWinding: rdfID \"").Append(cimWindingTest.From_TransformerWinding.ID).AppendLine(" \" is not mapped to GID!");
                            }
                            rd.AddProperty(new Property(ModelCode.WINDINGTEST_POWERTRWINDING, gid));
                        }
                    }
                }*/
        #endregion Populate ResourceDescription

        #region Enums convert
        /*	public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
                {
                    switch (phases)
                    {
                        case FTN.PhaseCode.A:
                            return PhaseCode.A;
                        case FTN.PhaseCode.AB:
                            return PhaseCode.AB;
                        case FTN.PhaseCode.ABC:
                            return PhaseCode.ABC;
                        case FTN.PhaseCode.ABCN:
                            return PhaseCode.ABCN;
                        case FTN.PhaseCode.ABN:
                            return PhaseCode.ABN;
                        case FTN.PhaseCode.AC:
                            return PhaseCode.AC;
                        case FTN.PhaseCode.ACN:
                            return PhaseCode.ACN;
                        case FTN.PhaseCode.AN:
                            return PhaseCode.AN;
                        case FTN.PhaseCode.B:
                            return PhaseCode.B;
                        case FTN.PhaseCode.BC:
                            return PhaseCode.BC;
                        case FTN.PhaseCode.BCN:
                            return PhaseCode.BCN;
                        case FTN.PhaseCode.BN:
                            return PhaseCode.BN;
                        case FTN.PhaseCode.C:
                            return PhaseCode.C;
                        case FTN.PhaseCode.CN:
                            return PhaseCode.CN;
                        case FTN.PhaseCode.N:
                            return PhaseCode.N;
                        case FTN.PhaseCode.s12N:
                            return PhaseCode.ABN;
                        case FTN.PhaseCode.s1N:
                            return PhaseCode.AN;
                        case FTN.PhaseCode.s2N:
                            return PhaseCode.BN;
                        default: return PhaseCode.Unknown;
                    }
                }*/

        /*
                public static TransformerFunction GetDMSTransformerFunctionKind(FTN.TransformerFunctionKind transformerFunction)
                {
                    switch (transformerFunction)
                    {
                        case FTN.TransformerFunctionKind.voltageRegulator:
                            return TransformerFunction.Voltreg;
                        default:
                            return TransformerFunction.Consumer;
                    }
                }

                public static WindingType GetDMSWindingType(FTN.WindingType windingType)
                {
                    switch (windingType)
                    {
                        case FTN.WindingType.primary:
                            return WindingType.Primary;
                        case FTN.WindingType.secondary:
                            return WindingType.Secondary;
                        case FTN.WindingType.tertiary:
                            return WindingType.Tertiary;
                        default:
                            return WindingType.None;
                    }
                }

                public static WindingConnection GetDMSWindingConnection(FTN.WindingConnection windingConnection)
                {
                    switch (windingConnection)
                    {
                        case FTN.WindingConnection.D:
                            return WindingConnection.D;
                        case FTN.WindingConnection.I:
                            return WindingConnection.I;
                        case FTN.WindingConnection.Z:
                            return WindingConnection.Z;
                        case FTN.WindingConnection.Y:
                            return WindingConnection.Y;
                        default:
                            return WindingConnection.Y;
                    }
                }*/
        #endregion Enums convert
    }
}
