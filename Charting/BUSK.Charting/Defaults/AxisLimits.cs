using System;

namespace BUSK.Charting.Defaults
{
    internal static class AxisLimits
    {
        internal static double StretchMax(AxisCore axis)
        {
            return axis.TopLimit; //Math.Ceiling(axis.TopLimit/axis.Magnitude)*axis.Magnitude;
        }

        internal static double StretchMin(AxisCore axis)
        {
            return axis.BotLimit; //Math.Floor(axis.BotLimit/axis.Magnitude)*axis.Magnitude;
        }

        internal static double UnitRight(AxisCore axis)
        {
            return Math.Ceiling(axis.TopLimit/axis.Magnitude)*axis.Magnitude + 1;
        }

        internal static double UnitLeft(AxisCore axis)
        {
            return Math.Floor(axis.BotLimit/axis.Magnitude)*axis.Magnitude - 1;
        }

        internal static double SeparatorMax(AxisCore axis)
        {
            return (Math.Floor(axis.TopLimit/axis.S) + 1.0)*axis.S;
        }

        internal static double SeparatorMaxRounded(AxisCore axis)
        {
            return Math.Round((axis.TopLimit/axis.S) + 1.0, 0)*axis.S;
        }

        internal static double SeparatorMin(AxisCore axis)
        {
            return ((Math.Floor(axis.BotLimit/axis.S)) - 1.0)*axis.S;
        }
    }
}
