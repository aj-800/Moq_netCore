using System;
using System.Collections.Generic;
using System.Text;

namespace MoqUnitTest
{

    public interface ILicenseData
    {
        string LicenseKey { get; }
    }

    public interface IServiceInformation
    {
        ILicenseData License { get; set; }
    }


    public interface IFrequentFlyerNumberValidator
    {
        bool IsValid(string frequentFlyerNumber);
        void IsValid(string frequentFlyerNumber, out bool isValid);
        IServiceInformation ServiceInformation{ get; }
    }
}
