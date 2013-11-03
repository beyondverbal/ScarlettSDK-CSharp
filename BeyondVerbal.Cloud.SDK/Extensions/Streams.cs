using System;
using System.IO;
using System.Threading.Tasks;

namespace BeyondVerbal.Cloud.ScarlettSDK
{
    public static class Streams
    {
        public static async Task CopyStreamWithAutoFlush(this Stream input, Stream output)
        {
            try
            {
                byte[] buffer = new byte[32768];
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) >= 0)
                {
                    await output.WriteAsync(buffer, 0, read);
                    output.Flush();
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                output.Close();
            }
        }
    }
}
