﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Management.InSkillProduct
{
    public class PrivacyAndCompliance
    {
        [JsonProperty("locales")]
        public Dictionary<string, LocalePrivacyAndCompliance> Locales { get; set; } = new Dictionary<string, LocalePrivacyAndCompliance>();
    }
}