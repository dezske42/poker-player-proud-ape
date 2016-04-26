namespace Nancy.Simple
{
    public class FullCard : ICards
    {
        public Cards Rank { get; set; }
        public Suits Suit { get; set; }
    }
}