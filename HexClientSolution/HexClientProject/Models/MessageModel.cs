using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using HexClientProject.Converters;
using HexClientProject.Services.Providers;
using HexClientProject.StateManagers;
using HexClientProject.Utils;
using ReactiveUI;

namespace HexClientProject.Models;
public class MessageModel
{
    private readonly SocialStateManager _socialStateManager = SocialStateManager.Instance;
    public required string Sender { get; init; }
    public required string Content { get; init; }
    public DateTime Timestamp { get; init; }
    public ChatScope Scope { get; init; }
    public string? SenderIcon { get; init; } // Rank Icon
    public string? WhisperingTo { get; init; } // GameNameTag

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
            textBlock.Inlines!.Add(new Run { Text = $"[{Timestamp:HH:mm}] ", Foreground = color });
            var clickableText = new TextBlock
            {
                Text = Sender,
                FontWeight = FontWeight.Bold,
                Foreground = color,
                Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Hand)
            };
            clickableText.PointerPressed += (_, e) =>
            {
                if (e.GetCurrentPoint(clickableText).Properties.IsRightButtonPressed)
                {
                    if (Sender == GlobalStateManager.Instance.SummonerInfo.GameName || Sender == "System") return;
                    var flyout = new Flyout
                    {
                        Placement = PlacementMode.Bottom,
                        Content = new StackPanel
                        {
                            Children =
                            {
                                new Button { Content = "View Profile", Command = ReactiveCommand.Create(() => SocialUtils.ViewProfile(Sender)) },
                                new Button { Content = "Invite to Party", Command = ReactiveCommand.Create(() =>
                                    ApiProvider.SocialService.PostInviteToLobby(ApiProvider.SocialService.GetFriendModel(Sender)!)) },
                                new Button { Content = "Add Friend", Command = ReactiveCommand.Create(() => 
                                    _socialStateManager.AddFriend(Sender))},
                                new Button { Content = "Mute", Command = ReactiveCommand.Create(()=>
                                    _socialStateManager.MuteUser(Sender)) },
                                new Button { Content = "Block", Command = ReactiveCommand.Create(()=>
                                    _socialStateManager.BlockFriend(Sender)) }
                            }
                        }
                    };

                    FlyoutBase.SetAttachedFlyout(clickableText, flyout);
                    flyout.ShowAt(clickableText);
                }
                else if (e.GetCurrentPoint(clickableText).Properties.IsLeftButtonPressed)
                {
                    if (Sender != GlobalStateManager.Instance.SummonerInfo.GameName && Sender != "System")
                        _socialStateManager.ChatBoxViewModel.WhisperTo(Sender, changeFilteringScope:false);
                }
            };

            var inlineUi = new InlineUIContainer
            {
                Child = clickableText
            };
            textBlock.Inlines.Add(inlineUi);
            
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