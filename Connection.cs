
using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Specialized;
using Microsoft.MetadirectoryServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace FimSync_Ezma
{
    public partial class EzmaExtension
    {

        /// <summary>
        /// Open a connection to the target system - can be called from multiple locations so is not specific to any one
        /// management agent action
        /// </summary>
        /// <returns></returns>
        public bool OpenConnection()
        {
            logger.Info("Opening Connection");
            /// Make use of the persisted Parameters property rather than try to pass around the source configParameters
            /// Individual elements can be recovered using the GetParameter method we've built into the ECMA2 code.
            /// 
            /// This should be done using the constant strings we've defined rather than "magic" strings
            /// 
            /// e.g.
            /// 
            var username = GetParameter(USERNAME).Value;
            var password = GetParameter(PASSWORD).SecureValue;
            return false;
        }

        /// <summary>
        /// Dispose of the connection once we're done with it
        /// also perform any additional cleanup we might require (e.g. save out watermarks where necessary)
        /// </summary>
        public void CloseConnection()
        {
            logger.Info("Closing Connection");
        }
    }
}
