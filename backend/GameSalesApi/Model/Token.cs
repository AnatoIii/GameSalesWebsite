using System;

namespace Model
{
    public class Token
    {
        public Guid Id { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public Guid UserId { get; set; }
    }
}
