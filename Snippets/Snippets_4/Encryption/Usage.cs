﻿namespace Snippets4.Encryption
{
    using NServiceBus;

    public class Usage
    {

        public Usage()
        {
            #region EncryptionServiceSimple

            Configure configure = Configure.With();
            configure.RijndaelEncryptionService();

            #endregion
        }
    }
}
