using System;
using System.Collections.Generic;
using System.Text;

namespace MoqUnitTest
{
    public interface IFrequentFlyerNumberValidator
    {
        bool IsValid(string frequentFlyerNumber);
        void IsValid(string frequentFlyerNumber, out bool isValid);
        string LicenseKey { get; }
    }
}
