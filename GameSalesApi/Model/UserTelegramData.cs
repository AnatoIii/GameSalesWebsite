using System;

namespace Model
{
    public class UserTelegramData
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string TelegramId { get; set; }
        public string TelegramName { get; set; }
    }
}
