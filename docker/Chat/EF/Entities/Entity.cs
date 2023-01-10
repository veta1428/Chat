namespace Chat.EF.Entities
{
    public class Entity<TId> where TId : struct, IEquatable<TId>
    {
        public TId Id { get; set; }
    }
}
