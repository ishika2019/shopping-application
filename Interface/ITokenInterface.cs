using project.Entities.identity;

namespace project.Interface
{
    public interface ITokenInterface
    {
        public string CreateToken(AppUser user);
    }
}
