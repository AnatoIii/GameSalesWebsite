namespace Models
{
    public class UserComment
    {
        public int UserCommentId { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }

        public Game Game { get; set; }
        public User User { get; set; }
    }
}
