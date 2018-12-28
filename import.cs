
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
        public int ImportDefaultPageSize => DEFAULT_PAGE_SIZE;

        public int ImportMaxPageSize => MAXIMUM_PAGE_SIZE;
        private string Watermark { get; set; } = "0";

        public CloseImportConnectionResults CloseImportConnection(CloseImportConnectionRunStep importRunStep)
        {
            CloseConnection();

            return new CloseImportConnectionResults();
        }

        public OpenImportConnectionResults OpenImportConnection(KeyedCollection<string, ConfigParameter> configParameters, Schema types, OpenImportConnectionRunStep importRunStep)
        {
            StoreParameters(configParameters, types, importRunStep);
            var rv = new OpenImportConnectionResults();

            //-- create a connection out to the target system
            //-- and persist it for later use as required
            PersistedConnector = OpenConnection();
            if (PersistedConnector!=null)
            {

            }

            //-- acquire the starting watermark
            Watermark = importRunStep.CustomData;

            return rv;
        }

        public GetImportEntriesResults GetImportEntries(GetImportEntriesRunStep importRunStep)
        {
#if SUPPORT_DELTA
            if (IRS.ImportType == OperationType.Full)
                return FetchImport(importRunStep);
            else
                return FetchDeltaImport(importRunStep);
#else
            return FetchImport(importRunStep);
#endif
        }
#endif


#if SUPPORT_DELTA
        private GetImportEntriesResults FetchDeltaImport(GetImportEntriesRunStep importRunStep)
        {
            return new GetImportEntriesResults(Watermark, false, new List<CSEntryChange>());
        }
#endif
        private GetImportEntriesResults FetchImport(GetImportEntriesRunStep importRunStep)
        {
            return new GetImportEntriesResults(Watermark, false, new List<CSEntryChange>());
        }
    }

}
