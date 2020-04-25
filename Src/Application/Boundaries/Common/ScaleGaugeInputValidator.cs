﻿using FluentValidation;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Common
{
    public sealed class ScaleGaugeInputValidator : AbstractValidator<ScaleGaugeInput>
    {
        public ScaleGaugeInputValidator()
        {
            RuleFor(x => x.TrackGauge)
                .IsEnumName(typeof(TrackGauge), caseSensitive: false);

            RuleFor(x => x.Millimeters)
                .GreaterThan(0M);

            RuleFor(x => x.Inches)
                .GreaterThan(0M);
        }
    }
}
