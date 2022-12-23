using System;
using BepInEx.Configuration;

namespace ProliferatorMultiplier
{
    public static class Patch_Proliferator
    {
        public static ConfigEntry<int> ProliferatorMultForAdditionalProduction;
        public static ConfigEntry<int> ProliferatorMultForSpeedOfProduction;

        private static double[] CachedArray_AdditionalProduction = new double[0];
        private static double[] CachedArray_SpeedOfProduction = new double[0];


        public static void InitConfig(ConfigFile confFile)
        {
            ProliferatorMultForAdditionalProduction = confFile.Bind("1. Additional production",
                nameof(ProliferatorMultForAdditionalProduction),
                1,
                new ConfigDescription("Multiplies proliferator effect - Additional production",
                    new AcceptableValueRange<int>(1, 100000)));

            ProliferatorMultForSpeedOfProduction = confFile.Bind("1. Speed of production",
                nameof(ProliferatorMultForSpeedOfProduction),
                1,
                new ConfigDescription("Multiplies proliferator effect - Speed of production",
                    new AcceptableValueRange<int>(1, 100000)));
        }


        public static void ProliferatorStart(ConfigFile configFile)
        {
            InitConfig(configFile);

            // Additional production for:
            // Smelter, assembler, chemical plant, lab extra matrix and extra hashes
            ArrayHelperStart(ProliferatorMultForAdditionalProduction.Value, ref Cargo.incTableMilli, ref CachedArray_AdditionalProduction);

            // Increased production speed
            ArrayHelperStart(ProliferatorMultForSpeedOfProduction.Value, ref Cargo.accTableMilli, ref CachedArray_SpeedOfProduction);
        }


        public static void ProliferatorEnd()
        {
            ArrayHelperEnd(ref Cargo.incTableMilli, ref CachedArray_AdditionalProduction);
            ArrayHelperEnd(ref Cargo.accTableMilli, ref CachedArray_SpeedOfProduction);
        }


        private static void ArrayHelperStart(int value, ref double[] copyFrom, ref double[] copyTo)
        {
            int sourceArrayLength = copyFrom.Length;
            Array.Resize(ref copyTo, sourceArrayLength);
            Array.Copy(copyFrom, copyTo, sourceArrayLength);
            for (int i = 0; i < sourceArrayLength; i++)
            {
                copyFrom[i] *= value;
            }
        }


        private static void ArrayHelperEnd(ref double[] restoreTo, ref double[] restoreFrom)
        {
            Assert.True(restoreTo.Length == restoreFrom.Length);

            for (int i = 0; i < restoreFrom.Length; i++)
            {
                restoreTo[i] = restoreFrom[i];
            }
        }
    }
}