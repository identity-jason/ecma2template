
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
        /// <summary>
        /// Retrieve the schema for use in the connector
        /// </summary>
        /// <param name="configParameters"></param>
        /// <returns></returns>
        public Schema GetSchema(KeyedCollection<string, ConfigParameter> configParameters)
        {
            Schema rv = null;
            StoreParameters(configParameters);
            if (OpenConnection())
            {
                rv = new Schema();

                //-- for each object
                //-- create a new SchemaType Object
                //--    var st = SchemaType.Create(Name_Of_ObjectClass, lock_anchor_definition)
                //--    in most cases, set the anchor to be locked unless we want the end user to be able to change this 
                //--    at some point via the Sync Engine UI
                var st = SchemaType.Create("Person", true);

                //-- add the anchor attributes for the object
                st.Attributes.Add(SchemaAttribute.CreateAnchorAttribute("EmployeeID", AttributeType.String));

                //-- add the additional single and multi-valued attributes and their types
                st.Attributes.Add(SchemaAttribute.CreateSingleValuedAttribute("givenName", AttributeType.String));
                st.Attributes.Add(SchemaAttribute.CreateMultiValuedAttribute("roles", AttributeType.Reference));

                //-- and make sure that we've added this into the schema itself!
                rv.Types.Add(st);

                CloseConnection();
            }

            return rv;
        }
    }
}
