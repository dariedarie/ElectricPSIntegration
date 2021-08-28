using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{
	
	public enum DMSType : short
	{		
		MASK_TYPE							= unchecked((short)0xFFFF),

        /*BASEVOLTAGE							= 0x0001,
		LOCATION							= 0x0002,
		POWERTR								= 0x0003,
		POWERTRWINDING						= 0x0004,
		WINDINGTEST							= 0x0005,*/

        CONTROL = 0x0001,
        CURVEDATA = 0x0002,
        FREQUENCYCONVERTER = 0x0003,
        REACTIVECAPABILITYCURVE = 0x0004,
        REGULATINGCONTROL = 0x0005,
        SHUNTCOMPENSATOR = 0x0006,
        STATICVARCOMPENSATOR = 0x0007,
        SYNCHRONOUSMACHINE = 0x0008,
        TERMINAL = 0x0009,
    }

    [Flags]
	public enum ModelCode : long
	{
        /*IDOBJ								= 0x1000000000000000,
		IDOBJ_GID							= 0x1000000000000104,
		IDOBJ_DESCRIPTION					= 0x1000000000000207,
		IDOBJ_MRID							= 0x1000000000000307,
		IDOBJ_NAME							= 0x1000000000000407,	

		PSR									= 0x1100000000000000,
		PSR_CUSTOMTYPE						= 0x1100000000000107,
		PSR_LOCATION						= 0x1100000000000209,

		BASEVOLTAGE							= 0x1200000000010000,
		BASEVOLTAGE_NOMINALVOLTAGE			= 0x1200000000010105,
		BASEVOLTAGE_CONDEQS					= 0x1200000000010219,

		LOCATION							= 0x1300000000020000,
		LOCATION_CORPORATECODE				= 0x1300000000020107,
		LOCATION_CATEGORY					= 0x1300000000020207,
		LOCATION_PSRS						= 0x1300000000020319,

		WINDINGTEST							= 0x1400000000050000,
		WINDINGTEST_LEAKIMPDN				= 0x1400000000050105,
		WINDINGTEST_LOADLOSS				= 0x1400000000050205,
		WINDINGTEST_NOLOADLOSS				= 0x1400000000050305,
		WINDINGTEST_PHASESHIFT				= 0x1400000000050405,
		WINDINGTEST_LEAKIMPDN0PERCENT		= 0x1400000000050505,
		WINDINGTEST_LEAKIMPDNMINPERCENT		= 0x1400000000050605,
		WINDINGTEST_LEAKIMPDNMAXPERCENT		= 0x1400000000050705,
		WINDINGTEST_POWERTRWINDING			= 0x1400000000050809,

		EQUIPMENT							= 0x1110000000000000,
		EQUIPMENT_ISUNDERGROUND				= 0x1110000000000101,
		EQUIPMENT_ISPRIVATE					= 0x1110000000000201,		

		CONDEQ								= 0x1111000000000000,
		CONDEQ_PHASES						= 0x111100000000010a,
		CONDEQ_RATEDVOLTAGE					= 0x1111000000000205,
		CONDEQ_BASVOLTAGE					= 0x1111000000000309,

		POWERTR								= 0x1112000000030000,
		POWERTR_FUNC						= 0x111200000003010a,
		POWERTR_AUTO						= 0x1112000000030201,
		POWERTR_WINDINGS					= 0x1112000000030319,

		POWERTRWINDING						= 0x1111100000040000,
		POWERTRWINDING_CONNTYPE				= 0x111110000004010a,
		POWERTRWINDING_GROUNDED				= 0x1111100000040201,
		POWERTRWINDING_RATEDS				= 0x1111100000040305,
		POWERTRWINDING_RATEDU				= 0x1111100000040405,
		POWERTRWINDING_WINDTYPE				= 0x111110000004050a,
		POWERTRWINDING_PHASETOGRNDVOLTAGE	= 0x1111100000040605,
		POWERTRWINDING_PHASETOPHASEVOLTAGE	= 0x1111100000040705,
		POWERTRWINDING_POWERTRW				= 0x1111100000040809,
		POWERTRWINDING_TESTS				= 0x1111100000040919,*/

        IDOBJ = 0x1000000000000000,
        IDOBJ_GID = 0x1000000000000104,
        IDOBJ_ALIASNAME = 0x1000000000000207,
        IDOBJ_MRID = 0x1000000000000307,
        IDOBJ_NAME = 0x1000000000000407,


        CONTROL = 0x1100000000010000,
        //CONTROL_RCONDEQ = 0x1100000000010109,
        CONTROL_REGULATINGCONDEQ = 0x1100000000010109,

        TERMINAL = 0x1200000000090000,
        //  TERMINAL_RCONTROLS = 0x1200000000090119,
        TERMINAL_CONDUCTINGEQUIPMENT = 0x1200000000090109,

        CURVEDATA = 0x1300000000020000,
        CURVEDATA_XVALUE = 0x1300000000020105,
        CURVEDATA_Y1VALUE = 0x1300000000020205,
        CURVEDATA_Y2VALUE = 0x1300000000020305,
        CURVEDATA_Y3VALUE = 0x1300000000020405,
        CURVEDATA_CURVE = 0x1300000000020509,

        CURVE = 0x1400000000000000,
        CURVE_CSTYLE = 0x140000000000010a,
        CURVE_XMULTIPLIER = 0x140000000000020a,
        CURVE_XUNIT = 0x140000000000030a,
        CURVE_Y1MULTIPLIER = 0x140000000000040a,
        CURVE_Y1UNIT = 0x140000000000050a,
        CURVE_Y2MULTIPLIER = 0x140000000000060a,
        CURVE_Y2UNIT = 0x140000000000070a,
        CURVE_Y3MULTIPLIER = 0x140000000000080a,
        CURVE_Y3UNIT = 0x140000000000090a,
        CURVE_CURVEDATAS = 0x1400000000000a19,
        //CURVE_CDATAS = 0x1400000000000a19,

        REACTIVECAPABILITYCURVE = 0x1410000000040000,
        //REACTIVECAPABILITYCURVE_SMACHINES = 0x1410000000040119,
        REACTIVECAPABILITYCURVE_SYNCHRONOUSMACHINES = 0x1410000000040119,

        POWERSYSTEMRESOURCE = 0x1500000000000000,

        REGULATINGCONTROL = 0x1510000000050000,
        REGULATINGCONTROL_DISCRETE = 0x1510000000050101,
        REGULATINGCONTROL_MODE = 0x151000000005020a,
        REGULATINGCONTROL_MPHASE = 0x151000000005030a,
        REGULATINGCONTROL_TRANGE = 0x1510000000050405,
        REGULATINGCONTROL_TVALUE = 0x1510000000050505,
        // REGULATINGCONTROL_RCONDEQS = 0x1510000000050619,
        REGULATINGCONTROL_REGULATINGCONDEQS = 0x1510000000050619,

        EQUIPMENT = 0x1520000000000000,
        EQUIPMENT_AGGREGATE = 0x1520000000000101,
        EQUIPMENT_NINSERVICE = 0x1520000000000201,

        CONDUCTINGEQUIPMENT = 0x1521000000000000,
        CONDUCTINGEQUIPMENT_TERMINALS = 0x1521000000000119,

        REGULATINGCONDEQ = 0x1521100000000000,
        REGULATINGCONDEQ_CONTROLS = 0x1521100000000119,
        //REGULATINGCONDEQ_REGULATINGC = 0x1521100000000209,
        REGULATINGCONDEQ_REGULATINGCONTROL = 0x1521100000000209,

        STATICVARCOMPENSATOR = 0x1521110000070000,

        SHUNTCOMPENSATOR = 0x1521120000060000,

        FREQUENCYCONVERTER = 0x1521130000030000,

        ROTATINGMACHINE = 0x1521140000000000,

        SYNCHRONOUSMACHINE = 0x1521141000080000,
        //SYNCHRONOUSMACHINE_RCC = 0x1521141000080109,
        SYNCHRONOUSMACHINE_REACTIVECAPABILITYCURVE = 0x1521141000080109,


    }

    [Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			 = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	 = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		  = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	  = unchecked((long)0xfffffff000000000),		
	}																		
}


