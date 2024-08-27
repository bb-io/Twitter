﻿using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Twitter.DataSources.Static;

public class LanguageDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            { "am", "Amharic" },
            { "ar", "Arabic" },
            { "hy", "Armenian" },
            { "eu", "Basque" },
            { "bn", "Bengali" },
            { "bs", "Bosnian" },
            { "bg", "Bulgarian" },
            { "my", "Burmese" },
            { "ca", "Catalan" },
            { "hr", "Croatian" },
            { "cs", "Czech" },
            { "da", "Danish" },
            { "nl", "Dutch" },
            { "en", "English" },
            { "et", "Estonian" },
            { "fi", "Finnish" },
            { "fr", "French" },
            { "ka", "Georgian" },
            { "de", "German" },
            { "el", "Greek" },
            { "gu", "Gujarati" },
            { "ht", "Haitian Creole" },
            { "he", "Hebrew" },
            { "hi", "Hindi" },
            { "hu", "Hungarian" },
            { "is", "Icelandic" },
            { "in", "Indonesian" },
            { "it", "Italian" },
            { "ja", "Japanese" },
            { "kn", "Kannada" },
            { "km", "Khmer" },
            { "ko", "Korean" },
            { "lo", "Lao" },
            { "lv", "Latvian" },
            { "lt", "Lithuanian" },
            { "ml", "Malayalam" },
            { "dv", "Maldivian" },
            { "mr", "Marathi" },
            { "ne", "Nepali" },
            { "no", "Norwegian" },
            { "or", "Oriya" },
            { "ps", "Pashto" },
            { "fa", "Persian" },
            { "pl", "Polish" },
            { "pt", "Portuguese" },
            { "pa", "Panjabi" },
            { "ro", "Romanian" },
            { "ru", "Russian" },
            { "sr", "Serbian" },
            { "sd", "Sindhi" },
            { "si", "Sinhala" },
            { "sk", "Slovak" },
            { "sl", "Slovenian" },
            { "ckb", "Sorani Kurdish" },
            { "es", "Spanish" },
            { "sv", "Swedish" },
            { "tl", "Tagalog" },
            { "ta", "Tamil" },
            { "te", "Telugu" },
            { "th", "Thai" },
            { "bo", "Tibetan" },
            { "tr", "Turkish" },
            { "uk", "Ukrainian" },
            { "ug", "Uyghur" },
            { "ur", "Urdu" },
            { "vi", "Vietnamese" },
            { "cy", "Welsh" },
            { "zh-TW", "Traditional Chinese" },
            { "zh-CN", "Simplified Chinese" },
            
        };
    }
}