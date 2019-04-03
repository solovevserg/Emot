using Emot.Common.Collections;
using Emot.Common.Models;
using Emot.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emot.Stemming
{
    public class Stemmer
    {
        public async Task<TokenCollection> StemAsync(IEnumerable<Opinion> opinions)
        {
            var classesOpinionsDictionary = new Dictionary<OpinionClass, List<Opinion>>();
            foreach (var opinion in opinions)
            {
                if (!classesOpinionsDictionary.ContainsKey(opinion.OpinionClass))
                {
                    classesOpinionsDictionary[opinion.OpinionClass] = new List<Opinion>();
                }
                classesOpinionsDictionary[opinion.OpinionClass].Add(opinion);
            }

            var classesLems = new Dictionary<OpinionClass, IEnumerable<string>>();
            foreach (var @class in classesOpinionsDictionary.Keys)
            {
                var oneClassOpinions = classesOpinionsDictionary[@class];
                classesLems[@class] = await StemOneClassOpinions(oneClassOpinions);
            }

            var tokens = new TokenCollection();
            foreach (var @class in classesLems.Keys)
            {
                foreach (var occurence in classesLems[@class])
                {
                    tokens.AddOccurence(@class, occurence);
                }
            }
            return tokens;
        }

        private async Task<IEnumerable<string>> StemOneClassOpinions(IEnumerable<Opinion> oneClassOpinions)
        {
            var texts = oneClassOpinions.Select(o => o.Text);
            var text = string.Join(' ', texts);
            var lems = await StemAsync(text);
            return lems;
        }

        public async Task<IEnumerable<string>> StemAsync(string text)
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
            if (!stemmingProcess.HasExited)
            {
                stemmingProcess.Kill();
            }
            var lems = Split(stemmedText);
            return lems;
        }

        public async Task<Dictionary<string, int>> StemAsync(IEnumerable<string> texts)
        {
            // Should combine strings, stem it and then split back.
            throw new NotImplementedException();
        }

        private Process CreateStemmingProcess()
        {
            // TODO: return it back
            //var info = new ProcessStartInfo("MyStem/mystem.exe", "-l -d")
            var info = new ProcessStartInfo(@"D:\Emot\Emot.Stemming\MyStem\mystem.exe", "-l -d")
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
