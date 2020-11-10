using SiteWebAssembly.Model;
using System.Threading.Tasks;

namespace SiteWebAssembly.Api.Services
{
    public interface ISendEmailService
    {
        Task<bool> SendEmail(Contact contact);
    }
}
