
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
#if SUPPORT_EXPORT
        public int ExportDefaultPageSize => throw new NotImplementedException();

        public int ExportMaxPageSize => throw new NotImplementedException();

        public void CloseExportConnection(CloseExportConnectionRunStep exportRunStep)
        {
            throw new NotImplementedException();
        }

        public void OpenExportConnection(KeyedCollection<string, ConfigParameter> configParameters, Schema types, OpenExportConnectionRunStep exportRunStep)
        {
            throw new NotImplementedException();
        }

        public PutExportEntriesResults PutExportEntries(IList<CSEntryChange> csentries)
        {
            throw new NotImplementedException();
        }
#endif
    }
}
