using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BeyondVerbal.Cloud.ScarlettSDK
{
    public enum AudioEncodingFormat     { WAV, PCM }

    public interface IDataFormat
    {
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        AudioEncodingFormat Type { get;  }

        [JsonProperty("channels")]
        ushort Channels { get;  }

        [JsonProperty("sample_rate")]
        ulong SampleRate { get;  }

        [JsonProperty("bits_per_sample")]
        byte BitsPerSample { get;  }

        [JsonProperty("auto_detect")]
        bool AutoDetect {get;}
    }
}
