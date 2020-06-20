using System;

namespace Model
{
    public class VerificationCode
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Code { get; set; }
        public DateTimeOffset DueDate { get; set; }
    }
}
