using System;

namespace MessengerApp.Models.Message
{
    public class Message
    {
        public string User { get; set; }
        public string Text { get; set; }
        public string Time { get; set; }
        public Message() { }
        public Message(string Text)
        {
            this.Text = Text;
            Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
