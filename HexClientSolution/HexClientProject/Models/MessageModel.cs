using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Media;
using HexClientProject.Converters;

namespace HexClientProject.Models;
public class MessageModel
{
    public string Sender { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public ChatScope Scope { get; set; }
    public string SenderIcon { get; set; } // Rank Icon
    public string? WhisperingTo { get; set; }

    public TextBlock DisplayTextBlock
    {
        get
        {
            ScopeToColorConverter scopeToColorConverter = new ScopeToColorConverter();
            var color = (IBrush)scopeToColorConverter.Convert(Scope, typeof(IBrush), null, CultureInfo.CurrentCulture);
            var textBlock = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                MaxWidth = 500
            };
            textBlock.Inlines.Add(new Run { Text = $"[{Timestamp:HH:mm}] ", Foreground = color });
            textBlock.Inlines.Add(new Run { Text = Sender, FontWeight = FontWeight.Bold, Foreground = color });
            if (Scope == ChatScope.Whisper)
            {
                textBlock.Inlines.Add(new Run { Text = " whispers to ", Foreground = color });
                textBlock.Inlines.Add(new Run { Text = WhisperingTo, FontWeight = FontWeight.Bold, Foreground = color });
            }
            textBlock.Inlines.Add(new Run { Text = ": ", Foreground = color });
            textBlock.Inlines.Add(new Run { Text = Content, Foreground = color });
            return textBlock;
        }
    }
}