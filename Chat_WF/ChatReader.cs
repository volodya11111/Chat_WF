using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat_WF
{
     public class ChatReader
     {
        private readonly string _filePath;
        private Form1 _form1;
        private int _lastMessageLineIndex;
        public ChatReader(string filePath, Form1 form1)
        {
            _filePath = filePath;
            _form1 = form1;
        }

        public void ReadNewMessages()
        {
            var lines = new List<string>();
            using (var fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        lines.Add(sr.ReadLine());
                    }
                }
            }

            //var fileText = File.ReadAllText(_filePath);
            //var lines = fileText.Split("\n");

            var newMessages = lines.Skip(_lastMessageLineIndex);

            _lastMessageLineIndex = lines.Count;

            PrintMessages(newMessages);
        }

        private void PrintMessages(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                _form1.CreateMessage(message);
            }
        }
     }
}
