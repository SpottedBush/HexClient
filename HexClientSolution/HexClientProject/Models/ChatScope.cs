namespace HexClientProject.Models
{
    public enum ChatScope
    {
        Global,
        Party,
        Whisper,
        Guild,
        Draft,
        System
    }

    public static class ChatScopeExtensions
    {
        public static ChatScope IntToScopeConverter(int scope)
        {
            return scope switch
            {
                0 => ChatScope.Global,
                1 => ChatScope.Party,
                2 => ChatScope.Whisper,
                3 => ChatScope.Guild,
                4 => ChatScope.Draft,
                5 => ChatScope.System,
                _ => ChatScope.Global
            };
        }
        public static int ScopeToIntConverter(ChatScope scope)
        {
            return scope switch
            {
                ChatScope.Global => 0,
                ChatScope.Party => 1,
                ChatScope.Whisper => 2,
                ChatScope.Guild => 3,
                ChatScope.Draft => 4,
                ChatScope.System => 5,
                _ => -1
            };
        }
        public static ChatScope StringToScopeConverter(string scopeString)
        {
            return scopeString switch
            {
                "Global" => ChatScope.Global,
                "Party" => ChatScope.Party,
                "Whisper" => ChatScope.Whisper,
                "Guild" => ChatScope.Guild,
                "Draft" => ChatScope.Draft,
                "System" => ChatScope.System,
                _ => ChatScope.Global
            };
        }
        public static string ScopeToStringConverter(ChatScope scope)
        {
            return scope switch
            {
                ChatScope.Global => "Global",
                ChatScope.Party => "Party",
                ChatScope.Whisper => "Whisper",
                ChatScope.Guild => "Guild",
                ChatScope.Draft => "Draft",
                ChatScope.System => "System",
                _ => "Global"
            };
        }
    }
}