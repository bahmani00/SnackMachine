using FluentValidation;
using System;
using System.Collections.Generic;
using static SnackMachineApp.Domain.SharedKernel.Money;

namespace SnackMachineApp.Domain.SharedKernel
{
    public class MoneyValidator : AbstractValidator<Money>
    {
        private static readonly List<Money> valids = 
            new List<Money> { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
        
        public MoneyValidator()
        {
            //return valids.Contains(money);
            //RuleFor(m => m.LastName).NotNull().Length(1, 50);
            //RuleFor(m => m.FirstMidName).NotNull().Length(1, 50);
            //RuleFor(m => m.EnrollmentDate).NotNull();
        }
    }
}