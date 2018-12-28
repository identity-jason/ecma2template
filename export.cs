
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
        public int ExportDefaultPageSize => DEFAULT_PAGE_SIZE;

        public int ExportMaxPageSize => MAXIMUM_PAGE_SIZE;

        public void CloseExportConnection(CloseExportConnectionRunStep exportRunStep)
        {
            CloseConnection();
        }

        public void OpenExportConnection(KeyedCollection<string, ConfigParameter> configParameters, Schema types, OpenExportConnectionRunStep exportRunStep)
        {
            StoreParameters(configParameters, types, exportRunStep);
        }

        public PutExportEntriesResults PutExportEntries(IList<CSEntryChange> csentries)
        {
            throw new NotImplementedException();
        }
#endif
    }
}
