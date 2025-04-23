using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Media;
using HexClientProject.Converters;

namespace HexClientProject.Models;
public class MessageModel
{
    public string Sender { get; init; } = null!;
    public string Content { get; init; } = null!;
    public DateTime Timestamp { get; init; }
    public ChatScope Scope { get; init; }
    public string SenderIcon { get; init; } = null!; // Rank Icon
    public string? WhisperingTo { get; init; }

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
            if (textBlock.Inlines == null) return textBlock;
            textBlock.Inlines.Add(new Run { Text = $"[{Timestamp:HH:mm}] ", Foreground = color });
            textBlock.Inlines.Add(new Run { Text = Sender, FontWeight = FontWeight.Bold, Foreground = color });
            if (Scope == ChatScope.Whisper)
            {
                textBlock.Inlines.Add(new Run { Text = " whispers to ", Foreground = color });
                textBlock.Inlines.Add(new Run
                    { Text = WhisperingTo, FontWeight = FontWeight.Bold, Foreground = color });
            }

            textBlock.Inlines.Add(new Run { Text = ": ", Foreground = color });
            textBlock.Inlines.Add(new Run { Text = Content, Foreground = color });

            return textBlock;
        }
    }
}