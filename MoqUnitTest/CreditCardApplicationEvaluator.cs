using System;
using System.Collections.Generic;
using System.Text;

namespace MoqUnitTest
{
    public class CreditCardApplicationEvaluator
    {
        private readonly IFrequentFlyerNumberValidator _validtor;

        private const  int AutoReferralMaxAge = 20;
        private const int HighIncomeThreshold = 100_000;
        private const int LowIncomeThreshold = 20_000;

        public CreditCardApplicationEvaluator(IFrequentFlyerNumberValidator validator)
        {
            _validtor = validator ?? throw new System.ArgumentNullException(nameof(validator));
        }

        public CreditCardApplicationDecision Evaluate(CreditCardApplication application)
        {
            if(application.GrossAnnualIncome >= HighIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoAccepted;
            }

            if (_validtor.LicenseKey == "EXPIRED")
                return CreditCardApplicationDecision.ReferredToHuman;

            var isValidFrequentFlyerNumber = _validtor.IsValid(application.FrequentFlyerNumber);
            if(!isValidFrequentFlyerNumber)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }
            
            if(application.Age <= AutoReferralMaxAge)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if(application.GrossAnnualIncome < LowIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoDeclined;
            }

            return CreditCardApplicationDecision.ReferredToHuman;
        }


        public CreditCardApplicationDecision EvaluateUsingOut(CreditCardApplication application)
        {
            if (application.GrossAnnualIncome >= HighIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoAccepted;
            }

             _validtor.IsValid(application.FrequentFlyerNumber, out var isValidFrequentFlyerNumber);
            if (!isValidFrequentFlyerNumber)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (application.Age <= AutoReferralMaxAge)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (application.GrossAnnualIncome < LowIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoDeclined;
            }

            return CreditCardApplicationDecision.ReferredToHuman;
        }
    }
}
