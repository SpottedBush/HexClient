namespace HexClientProject.Models
{
    public enum ChatScope
    {
        Global,
        Party,
        Whisper,
        Guild,
        System
    }

    public static class ChatScopeExtensions
    {
        public static ChatScope IntToScopeConverter(int scope)
        {
            switch (scope)
            {
                case 0:
                    return ChatScope.Global;
                case 1:
                    return ChatScope.Party;
                case 2:
                    return ChatScope.Whisper;
                case 3:
                    return ChatScope.Guild;
                case 4:
                    return ChatScope.System;
                default:
                    return ChatScope.Global;
            }
        }
        public static int ScopeToIntConverter(ChatScope scope)
        {
            switch (scope)
            {
                case ChatScope.Global:
                    return 0;
                case ChatScope.Party:
                    return 1;
                case ChatScope.Whisper:
                    return 2;
                case ChatScope.Guild:
                    return 3;
                case ChatScope.System:
                    return 4;
                default:
                    return -1;
            }
        }
        public static ChatScope StringToScopeConverter(string scopeString)
        {
            switch (scopeString)
            {
                case "Global":
                    return ChatScope.Global;
                case "Party":
                    return ChatScope.Party;
                case "Whisper":
                    return ChatScope.Whisper;
                case "Guild":
                    return ChatScope.Guild;
                case "System":
                    return ChatScope.System;
                default:
                    return ChatScope.Global;
            }
        }
        public static string ScopeToStringConverter(ChatScope scope)
        {
            switch (scope)
            {
                case ChatScope.Global:
                    return "Global";
                case ChatScope.Party:
                    return "Party";
                case ChatScope.Whisper:
                    return "Whisper";
                case ChatScope.Guild:
                    return "Guild";
                case ChatScope.System:
                    return "System";
                default:
                    return "Global";
            }
        }
    }
}