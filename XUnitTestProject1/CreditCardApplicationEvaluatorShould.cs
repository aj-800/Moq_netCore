using Moq;
using MoqUnitTest;
using System;
using Xunit;


namespace XUnitTestProject1
{
    public class CreditCardApplicationEvaluatorShould
    {
        [Fact]
        public void AcceptHighIncomeApplications()
        {

            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication() { GrossAnnualIncome = 100_000 };

            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
        }

        [Fact]
        public void ReferYoungApplications()
        {

            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(m => m.IsValid(It.IsAny<string>())).Returns(true);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication() { Age = 19 };

            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }


        [Fact]
        public void DeclineLowIncomeApplication()
        {

            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            //mockValidator.Setup(m => m.IsValid("x")).Returns(true);
            //mockValidator.Setup(m => m.IsValid(It.IsAny<string>())).Returns(true);
            //mockValidator.Setup(m => m.IsValid(It.Is<string>(number => number.StartsWith("x")))).Returns(true);
            //mockValidator.Setup(m => m.IsValid(It.IsIn("x", "y", "z"))).Returns(true);
            //mockValidator.Setup(m => m.IsValid(It.IsInRange("a", "z", Range.Inclusive))).Returns(true);
            mockValidator.Setup(m => m.IsValid(It.IsRegex("[a-z]", System.Text.RegularExpressions.RegexOptions.None))).Returns(true);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication()
            {
                GrossAnnualIncome = 19_000,
                Age = 43,
                FrequentFlyerNumber = "x"
            };

            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);

        }

        [Fact]
        public void DeclineLowIncomeApplicationOutDemo()
        {

            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            bool isValid = true;
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>(), out isValid));

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication()
            {
                GrossAnnualIncome = 19_000,
                Age = 43,
                FrequentFlyerNumber = "x"
            };

            var decision = sut.EvaluateUsingOut(application);

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);

        }

        [Fact]
        public void ReferInvalidFrequentFlyerApplications()
        {
            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Strict);
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(m => m.IsValid(It.IsAny<string>())).Returns(false);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication();

            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);

        }


        [Fact]
        public void ReferWhenLicenceKeyExpired()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            mockValidator.Setup(x => x.LicenseKey).Returns(GetLicenseKeyExpireString());

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication { Age = 42 };

            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        private static string GetLicenseKeyExpireString()
        {
            return "EXPIRED";
        }
    }
}
