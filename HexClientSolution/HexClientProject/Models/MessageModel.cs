using System;

namespace HexClientProject.Models;
public enum ChatScope
{
    Global,
    Party,
    Private,
    Guild
}

public class MessageModel
{
    public ChatScope Scope { get; set; }
    public string Sender { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }
}