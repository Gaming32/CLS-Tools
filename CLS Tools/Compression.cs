using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace CLSTools
{
    class Compression
    {
        public enum CompressionTypes{Zip, OpenG32, G32};

        #region Compress
        public static Stream Compress(Stream file, CompressionTypes type = CompressionTypes.Zip)
        {
            switch (type)
            {
                case CompressionTypes.OpenG32:
                    byte[] OpenG32_buffer = new byte[file.Length];
                    file.Read(OpenG32_buffer, 0x00, (int)file.Length);
                    byte[] OpenG32_returnData = Compress_OpenG32(OpenG32_buffer);
                    byte[] OpenG32_temp = BitConverter.GetBytes(times);
                    Array.Resize(ref OpenG32_temp, 4);
                    OpenG32_returnData = (byte[])OpenG32_temp.Concat(OpenG32_returnData);
                    Stream OpenG32_data = null;
                    OpenG32_data.Write(OpenG32_returnData, 0x00, OpenG32_returnData.Length);
                    return OpenG32_data;
                case CompressionTypes.G32:
                    byte[] G32_buffer = new byte[file.Length];
                    file.Read(G32_buffer, 0x00, (int)file.Length);
                    byte[] G32_returnData = Compress_OpenG32(G32_buffer);
                    byte[] G32_temp = BitConverter.GetBytes(times);
                    Array.Resize(ref G32_temp, 4);
                    OpenG32_returnData = (byte[])G32_temp.Concat(G32_returnData);
                    Stream G32_data = null;
                    G32_data.Write(G32_returnData, 0x00, G32_returnData.Length);
                    GZipStream G32_archive = new GZipStream(G32_data, CompressionMode.Compress);
                    return G32_archive.BaseStream;
                default:
                    GZipStream Zip_archive = new GZipStream(file, CompressionMode.Compress);
                    return Zip_archive.BaseStream;

            }
        }

        public static void Compress(string inputFile, string outputFile, CompressionTypes type = CompressionTypes.Zip)
        {
            Stream fileStream = File.OpenWrite(outputFile);
            Compress(File.OpenRead(inputFile), type).CopyTo(fileStream);
            fileStream.Dispose();
        }
        #endregion

        #region Handlers
        #region Compressers
        static uint times = 0;
        private static byte[] Compress_OpenG32(byte[] input)
        {
            string bin = "";
            foreach(byte curr_byte in input)
            {
                string tmpBin = Convert.ToString(curr_byte, 2);
                bin += Convert.ToString(tmpBin.Length, 2).PadLeft(4, '0') + tmpBin;
            }
            string[] strBinBytes = BasicTools.StrSplit(bin, 8);
            strBinBytes[strBinBytes.Length - 1].PadRight(8, '0');
            List<byte> returnBytes = new List<byte>();
            foreach(string byte2 in strBinBytes)
                returnBytes.Add(Convert.ToByte(byte2, 2));
            if (returnBytes.ToArray().Length < input.Length)
                Compress_OpenG32(returnBytes.ToArray());
            times++;
            return returnBytes.ToArray();
        }
        #endregion
        #endregion
    }
}
