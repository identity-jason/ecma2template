
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
        //------------------------------
        //-- Example Parameter Names
        //--
        private const string USERNAME = "User Name";
        private const string USERNAME_REGEX = @"/^[a-zA-Z][a-zA-Z0-9\-\.]{0,61}[a-zA-Z]\\\w[\w\.\- ]+$/";
        private const string USERNAME_DEFAULT = @"domain\username";
        private const string PASSWORD = "Password";
        private const string SERVER = "Server Name";

        //------------------------------
        //-- Default Page Sizes (for both import and export)
        //--
        private const int DEFAULT_PAGE_SIZE = 100;
        private const int MAXIMUM_PAGE_SIZE = 1000;
    }
}
