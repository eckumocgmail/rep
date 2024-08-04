﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataCommon.Metadata
{
    public class KeyMetadata
    {
        public string SourceTable { get; set; }
        public string SourceColumn { get; set; }
        public string TargetTable { get; set; }
        public string TargetColumn { get; set; }
    }
}
