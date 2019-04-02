using Emot.Common.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Emot.Stemming
{
    public class Stemmer
    {
        public async Task<Dictionary<string, int>> Stem(string text)
        {
            var stemmingProcess = CreateStemmingProcess();
            string stemmedText;
            using (var writer = new StreamWriter(stemmingProcess.StandardInput.BaseStream, Encoding.UTF8))
            {
                await writer.WriteLineAsync(text);
            }
            using (var reader = new StreamReader(stemmingProcess.StandardOutput.BaseStream, Encoding.UTF8))
            {
                stemmedText = reader.ReadToEnd();
            }
            var lems = Split(stemmedText);
            Dictionary<string, int> lemsDictionary = new Dictionary<string, int>();
            foreach (var lem in lems)
            {
                if(!lemsDictionary.ContainsKey(lem))
                {
                    lemsDictionary[lem] = 0;
                }
                lemsDictionary[lem]++;
            }
            return lemsDictionary();
        } 

        public async Task<Dictionary<string, int>> Stem(IEnumerable<string> texts)
        {
            // Should combine strings, stem it and then split back.
            throw new NotImplementedException();
        }

        private Process CreateStemmingProcess()
        {
            var info = new ProcessStartInfo("MyStem/mystem.exe", "-l -d")
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };
            var stemmingProcess = Process.Start(info);
            return stemmingProcess;
        }

        private IEnumerable<string> Split(string stemmedText)
        {
            return stemmedText.Split(new char[] { '}', '{' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
