using System;
using System.IO;
using NAudio.Wave;
using UnityEngine;

namespace Audio
{
    public static class Mp3Loader
    {
        private static MemoryStream AudioMemStream(WaveStream waveStream)
        {
            var outputStream = new MemoryStream();
            using var waveFileWriter = new WaveFileWriter(outputStream, waveStream.WaveFormat);
            var bytes = new byte[waveStream.Length];
            waveStream.Position = 0;
            waveStream.Read(bytes, 0, Convert.ToInt32(waveStream.Length));
            waveFileWriter.Write(bytes, 0, bytes.Length);
            waveFileWriter.Flush();
            return outputStream;
        }
        
        public static AudioClip LoadMp3(string name, byte[] bytes)
        {
            var reader = new Mp3FileReader(new MemoryStream(bytes));
            var waveStream = WaveFormatConversionStream.CreatePcmStream(reader);
            var ac = OpenWavParser.ByteArrayToAudioClip(AudioMemStream(waveStream).ToArray(), name);
            return ac;
        }
    }
}