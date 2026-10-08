using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MHAWebDesign.Data;
using MHAWebDesign.Models;
using MHAWebDesign.Services;

namespace MHAWebDesign.Controllers;

public class HomeController : Controller
{
    private readonly IEmailService _emailService;
    private readonly MhaDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        IEmailService emailService,
        MhaDbContext context,
        ILogger<HomeController> logger)
    {
        _emailService = emailService;
        _context = context;
        _logger = logger;
    }


    public IActionResult Index()
    {
        return View();
    }


    public IActionResult Services()
    {
        return View();
    }


    public IActionResult Portfolio()
    {
        return View();
    }

    public IActionResult Vela()
{
    return View();
}

public IActionResult Inventory()
{
    return View();
}


    public IActionResult About()
    {
        return View();
    }


    // =========================================================
    // CONTACT
    // =========================================================

    [HttpGet]
    public IActionResult Contact()
    {
        return View(new ContactViewModel());
    }


 [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Contact(
    ContactViewModel model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    var lead = new Lead
    {
        Name = model.Name,
        Email = model.Email,
        Business = model.Business,
        Phone = model.Phone,
        ProjectType = model.ProjectType,
        Budget = model.Budget,
        Timeline = model.Timeline,
        Message = model.Message,
        SubmittedAt = DateTime.UtcNow,
        Status = "New",
        EmailSent = false
    };


    // =========================================================
    // SAVE LEAD
    // =========================================================

    try
    {
        _context.Leads.Add(lead);

        await _context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
        _logger.LogError(
            ex,
            "An error occurred while saving a project enquiry."
        );

        ModelState.AddModelError(
            string.Empty,
            "We couldn't process your enquiry right now. Please try again, or contact us directly by WhatsApp, phone or email."
        );

        return View(model);
    }


    // =========================================================
    // SEND EMAIL
    // =========================================================

    try
    {
        await _emailService.SendProjectEnquiryAsync(model);

        lead.EmailSent = true;
        lead.EmailSentAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
        _logger.LogError(
            ex,
            "Lead {LeadId} was saved, but the email notification failed.",
            lead.Id
        );

        TempData["ContactSuccess"] =
            "Your enquiry has been received successfully. We'll be in touch shortly.";

        return RedirectToAction(nameof(Contact));
    }


    TempData["ContactSuccess"] =
        "Thanks for getting in touch. Your project enquiry has been received successfully. We'll be in touch shortly.";

    return RedirectToAction(nameof(Contact));
}


    public IActionResult Privacy()
    {
        return View();
    }


    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId =
                Activity.Current?.Id
                ?? HttpContext.TraceIdentifier
        });
    }
}