
namespace BeyondVerbal.Cloud.ScarlettSDK.DataFormat
{
    public class WAVFormat : IDataFormat
    {
        public WAVFormat()
        {
            this.AutoDetect = true;
        }

        public WAVFormat(ulong sampleRate, byte bitsPerSample, ushort channels)
        {
            this.Type = AudioEncodingFormat.WAV;
            this.SampleRate = sampleRate;
            this.Channels = channels;
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
