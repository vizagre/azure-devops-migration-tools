﻿using System;
using Newtonsoft.Json;

namespace MigrationTools.Options
{
    public interface IOldOptions
    {
        /// <summary>
        /// If you set a `RefName` then this configration will be added to a Catalog of configurations that can be refernced using tha `RefName` so tha tyou dont have to keep adding the ame items with the same configuration.
        /// </summary>
        public string RefName { get; set; }

        //[JsonIgnore]
        //Type ToConfigure { get; }

        //void SetDefaults();
    }

    public interface IOptions
    {
        [JsonIgnore]
        public string ConfigurationOptionFor { get; }
        [JsonIgnore]
        public string ConfigurationSectionPath { get; }
        [JsonIgnore]
        public string ConfigurationCollectionPath { get; }
        [JsonIgnore]
        public string ConfigurationCollectionObjectName { get; }

        /// <summary>
        /// Will be used if enabled
        /// </summary>
        [JsonProperty(Order = -2)]
        bool Enabled { get; set; }

    }
}