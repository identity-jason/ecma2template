
using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Specialized;
using Microsoft.MetadirectoryServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FimSync_Ezma
{
    public partial class EzmaExtension
    {
#if SUPPORT_IMPORT
        public int ImportDefaultPageSize => throw new NotImplementedException();

        public int ImportMaxPageSize => throw new NotImplementedException();

        public CloseImportConnectionResults CloseImportConnection(CloseImportConnectionRunStep importRunStep)
        {
            throw new NotImplementedException();
        }

        public GetImportEntriesResults GetImportEntries(GetImportEntriesRunStep importRunStep)
        {
            throw new NotImplementedException();
        }

        public OpenImportConnectionResults OpenImportConnection(KeyedCollection<string, ConfigParameter> configParameters, Schema types, OpenImportConnectionRunStep importRunStep)
        {
            throw new NotImplementedException();
        }
#endif
    }
}
