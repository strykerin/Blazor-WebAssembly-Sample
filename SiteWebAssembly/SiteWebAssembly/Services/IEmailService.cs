using SiteWebAssembly.Model;
using System.Threading.Tasks;

namespace SiteWebAssembly
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Contact contactForm);
    }
}