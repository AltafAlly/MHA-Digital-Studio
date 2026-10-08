using MHAWebDesign.Models;

namespace MHAWebDesign.Services;

public interface IEmailService
{
    Task SendProjectEnquiryAsync(ContactViewModel model);
}