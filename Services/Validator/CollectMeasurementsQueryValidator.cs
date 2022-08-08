using FluentValidation;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validator
{
    public class CollectMeasurementsQueryValidator : AbstractValidator<MeasurementsModel>
    {
        public static MeasurementsModel Instance = new MeasurementsModel();
        public CollectMeasurementsQueryValidator()
        {
            RuleFor(_ => _.Device).Cascade(CascadeMode.Stop).NotNull().WithMessage("Device is required!");
            RuleFor(_ => _.SensorType).Cascade(CascadeMode.Stop).NotNull().WithMessage("Sensor is required!");
        }
    }
}
