using System;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public enum MeasureUnit
    {
        Millimeters,
        Inches
    }

    public static class MeasureUnitsConvertions
    {
        private const float Inches2MillimitersF = 25.4f;
        private const decimal Inches2Millimiters = 25.4M;

        public static float InchesToMillimiters(float f) => f * Inches2MillimitersF;

        public static decimal InchesToMillimiters(decimal f) => f * Inches2Millimiters;

        public static float MillimitersToInches(float f) => f / Inches2MillimitersF;

        public static decimal MillimitersToInches(decimal f) => f / Inches2Millimiters;
    }

    public static class MeasureUnitExtentions
    {
        public static TReturn Combine<TReturn>(this (MeasureUnit, MeasureUnit) mus, 
            float left, float right, 
            Func<float, float, MeasureUnit, TReturn> combineFunction)
        {
            var (lmu, rmu) = mus;
            if (lmu == rmu)
            {
                return combineFunction(left, right, lmu);
            }
            else
            {
                var right2 = lmu.GetValueOrConvert(() => (right, rmu));
                return combineFunction(left, right2, lmu);
            }
        }

        public static TReturn Combine<TReturn>(this (MeasureUnit, MeasureUnit) mus, 
            decimal left, decimal right, 
            Func<decimal, decimal, MeasureUnit, TReturn> combineFunction)
        {
            var (lmu, rmu) = mus;
            if (lmu == rmu)
            {
                return combineFunction(left, right, lmu);
            }
            else
            {
                var right2 = lmu.GetValueOrConvert(() => (right, rmu));
                return combineFunction(left, right2, lmu);
            }
        }

        /// <summary>
        /// Apply the correct function depending on the measure unit. 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="mu"></param>
        /// <param name="value">the value</param>
        /// <param name="inFunction">the function applied to inches values</param>
        /// <param name="mmFunction">the function applied to millimeters values</param>
        /// <returns></returns>
        public static TReturn Apply<TValue, TReturn>(this MeasureUnit mu, 
            TValue value, 
            Func<TValue, TReturn> inFunction, 
            Func<TValue, TReturn> mmFunction)
        {
            return mu switch
            {
                MeasureUnit.Millimeters => mmFunction.Invoke(value),
                MeasureUnit.Inches => inFunction.Invoke(value),
                _ => throw new ArgumentOutOfRangeException(mu.ToString())
            };
        }

        /// <summary>
        /// It returns the exctracted value when the measure unit is the same as the current one, 
        /// otherwise it will run the convertion functions.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="mu"></param>
        /// <param name="value"></param>
        /// <param name="extractFunction"></param>
        /// <returns></returns>
        public static float GetValueOrConvert(this MeasureUnit mu, Func<(float, MeasureUnit)> extractFunction)
        {
            var (value, currentMU) = extractFunction.Invoke();

            return mu switch
            {
                MeasureUnit.Millimeters => (mu == currentMU) ? value : MeasureUnitsConvertions.InchesToMillimiters(value),
                MeasureUnit.Inches => (mu == currentMU) ? value : MeasureUnitsConvertions.MillimitersToInches(value),
                _ => throw new ArgumentOutOfRangeException(mu.ToString())
            };
        }

        /// <summary>
        /// It returns the exctracted value when the measure unit is the same as the current one, 
        /// otherwise it will run the convertion functions.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="mu"></param>
        /// <param name="value"></param>
        /// <param name="extractFunction"></param>
        /// <returns></returns>
        public static decimal GetValueOrConvert(this MeasureUnit mu, Func<(decimal, MeasureUnit)> extractFunction)
        {
            var (value, currentMU) = extractFunction.Invoke();

            return mu switch
            {
                MeasureUnit.Millimeters => (mu == currentMU) ? value : MeasureUnitsConvertions.InchesToMillimiters(value),
                MeasureUnit.Inches => (mu == currentMU) ? value : MeasureUnitsConvertions.MillimitersToInches(value),
                _ => throw new ArgumentOutOfRangeException(mu.ToString())
            };
        }
    }
}
