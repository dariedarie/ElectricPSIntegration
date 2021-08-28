using System;

namespace FTN.Common
{

    public enum CurveStyle : short
    {

        constantYValue = 0,
        formula = 1,
        rampYValue = 2,
        straightLineYValues = 3,
    }

    public enum PhaseCode : short
	{
        A,

        /// Phases A and B.
        AB,

        /// Phases A, B, and C.
        ABC,

        /// Phases A, B, C, and N.
        ABCN,

        /// Phases A, B, and neutral.
        ABN,

        /// Phases A and C.
        AC,

        /// Phases A, C and neutral.
        ACN,

        /// Phases A and neutral.
        AN,

        /// Phase B.
        B,

        /// Phases B and C.
        BC,

        /// Phases B, C, and neutral.
        BCN,

        /// Phases B and neutral.
        BN,

        /// Phase C.
        C,

        /// Phases C and neutral.
        CN,

        /// Neutral phase.
        N,

        /// Secondary phase 1.
        s1,

        /// Secondary phase 1 and 2.
        s12,

        /// Secondary phases 1, 2, and neutral.
        s12N,

        /// Secondary phase 1 and neutral.
        s1N,

        /// Secondary phase 2.
        s2,

        /// Secondary phase 2 and neutral.
        s2N,
    }


    public enum RegulatingControlModeKind : short
    {

        activePower = 0,     
        admittance,
        currentFlow,
        @fixed,
        powerFactor,
        reactivePower,
        temperature,
        timeScheduled,
        voltage,
    }

    public enum UnitMultiplier : short
    {

        c = 0,
        d,
        G,
        k,
        m,
        M,
        micro,
        n,
        none,
        p,
        T,
    }

    public enum UnitSymbol : short
    {

        A = 0,
        deg,
        degC,
        F,
        g,
        h,
        H,
        Hz,
        J,
        m,
        m2,
        m3,
        min,
        N,
        none,
        ohm,
        Pa,
        rad,
        s,
        S,
        V,
        VA,
        VAh,
        VAr,
        VArh,
        W,
        Wh,
    }

   
}
