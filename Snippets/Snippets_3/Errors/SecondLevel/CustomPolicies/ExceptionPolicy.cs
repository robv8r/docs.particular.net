﻿namespace Snippets3.Errors.SecondLevel.CustomPolicies
{
    using System;
    using NServiceBus.Unicast.Transport;

    public class ExceptionPolicy
    {

        #region SecondLevelRetriesCustomExceptionPolicyHandler
        TimeSpan MyCustomRetryPolicy(TransportMessage transportMessage)
        {
            if (transportMessage.ExceptionType() == typeof(MyBusinessException).FullName)
            {
                // Do not retry for MyBusinessException
                return TimeSpan.MinValue;
            }

            if (transportMessage.NumberOfRetries() >= 3)
            {
                return TimeSpan.MinValue;
            }

            return TimeSpan.FromSeconds(5);
        }


        #endregion

    }

    internal class MyBusinessException
    {
    }
}