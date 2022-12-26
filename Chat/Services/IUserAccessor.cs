using Chat.EF.Entities;

namespace Chat.Services
{
    public interface IUserAccessor
    {
        public User? CurrentUser { get; protected set; }

        void SetUser(User? user);
    }
}
