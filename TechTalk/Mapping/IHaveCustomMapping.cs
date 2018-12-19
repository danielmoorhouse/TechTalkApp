using TechTalk.Models.ApplicationUser;

namespace TechTalk.Mapping
{
    public interface IHaveCustomMapping
    {
         void ConfigureMapping(ProfileModel profile);
    }
}