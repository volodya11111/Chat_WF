using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_WF
{
    public class ChatWriter
    {
        private readonly string _filePath;
        private readonly string _nickName;


        public ChatWriter(string filePath, string nickName)
        {
            _filePath = filePath;
            _nickName = nickName;
        }

        public async Task SaveAsync(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                using (StreamWriter writer = new StreamWriter(_filePath, true))
                {
                    await writer.WriteLineAsync($"{_nickName} > {message}");
                }
            }
        }
    }
}
