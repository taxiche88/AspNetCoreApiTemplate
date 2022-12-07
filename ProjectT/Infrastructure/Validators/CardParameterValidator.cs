using FluentValidation;
using ProjectT.Dtos.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectT.Infrastructure.Validators
{
    /// <summary>
    /// Card Parameter 的驗證器
    /// </summary>
    public class CardParameterValidator : AbstractValidator<CardParameter>
    {
        /// <summary>
        /// 驗證器的建構式: 在這裡註冊我們要驗證的規則
        /// </summary>
        public CardParameterValidator()
        {
            this.RuleFor(card => card.Attack)
            .GreaterThanOrEqualTo(0);

            this.RuleFor(card => card.Health)
                .GreaterThanOrEqualTo(0);

            this.RuleFor(card => card.Cost)
                .GreaterThanOrEqualTo(0);

            this.RuleFor(card => card.Description)
            .NotNull()
            .MaximumLength(50);

            this.RuleFor(card => card.Name)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
