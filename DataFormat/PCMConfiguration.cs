
namespace BeyondVerbal.Cloud.ScarlettSDK.DataFormat
{
    public class PCMFormat : IDataFormat
    {
        public PCMFormat(ulong sampleRate, byte bitsPerSample, ushort channels)
        {
            this.Type = AudioEncodingFormat.PCM;
            this.BitsPerSample = bitsPerSample;
            this.SampleRate = sampleRate;
            this.Channels = channels;
            this.AutoDetect = false;
        }

        public AudioEncodingFormat Type
        { get; set; }

        public ushort Channels
        { get; set; }

        public ulong SampleRate
        { get; set; }

        public byte BitsPerSample
        { get; set; }

        public bool AutoDetect
        { get; set; }        
    }
}
