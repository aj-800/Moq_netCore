﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MoqUnitTest
{
    public class FrequentFlyerNumberValidatorService : IFrequentFlyerNumberValidator
    {

        public IServiceInformation ServiceInformation => throw new NotImplementedException("For demo purpose only");

        public ValidationMode ValidationMode
        {
            get => throw new NotImplementedException("For demo purpose only");
            set => throw new NotImplementedException("For demo purpose only");
        }

        public bool IsValid(string frequentFlyerNumber)
        {
            throw new NotImplementedException("For demo purpose only");
        }

        public void IsValid(string frequentFlyerNumber, out bool isValid)
        {
            throw new NotImplementedException("For demo purpose only");
        }
    }
}
