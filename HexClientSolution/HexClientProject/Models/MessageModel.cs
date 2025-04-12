using System;

namespace HexClientProject.Models;
public class MessageModel
{
    public string Sender { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public ChatScope Scope { get; set; }
}