
using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Specialized;
using Microsoft.MetadirectoryServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security;

namespace FimSync_Ezma
{
    public partial class EzmaExtension
    {
#if SUPPORT_PASSWORDS
        public void ChangePassword(CSEntry csentry, SecureString oldPassword, SecureString newPassword)
        {
            throw new NotImplementedException();
        }

        public void ClosePasswordConnection()
        {
            throw new NotImplementedException();
        }

        public ConnectionSecurityLevel GetConnectionSecurityLevel()
        {
            throw new NotImplementedException();
        }

        public void OpenPasswordConnection(KeyedCollection<string, ConfigParameter> configParameters, Partition partition)
        {
            throw new NotImplementedException();
        }

        public void SetPassword(CSEntry csentry, SecureString newPassword, PasswordOptions options)
        {
            throw new NotImplementedException();
        }
#endif
    }
}
