using System.Net;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MHAWebDesign.Models;

namespace MHAWebDesign.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendProjectEnquiryAsync(ContactViewModel model)
    {
        var message = new MimeMessage();

        message.From.Add(
            new MailboxAddress(
                _settings.SenderName,
                _settings.SenderEmail
            )
        );

        message.To.Add(
            MailboxAddress.Parse(
                _settings.RecipientEmail
            )
        );

        message.ReplyTo.Add(
            new MailboxAddress(
                model.Name,
                model.Email
            )
        );

        message.Subject =
            $"New M.H.A Project Enquiry — {model.ProjectType}";


        string name =
            WebUtility.HtmlEncode(model.Name);

        string email =
            WebUtility.HtmlEncode(model.Email);

        string business =
            WebUtility.HtmlEncode(
                string.IsNullOrWhiteSpace(model.Business)
                    ? "Not provided"
                    : model.Business
            );

        string phone =
            WebUtility.HtmlEncode(
                string.IsNullOrWhiteSpace(model.Phone)
                    ? "Not provided"
                    : model.Phone
            );

        string projectType =
            WebUtility.HtmlEncode(model.ProjectType);

        string budget =
            WebUtility.HtmlEncode(
                string.IsNullOrWhiteSpace(model.Budget)
                    ? "Not specified"
                    : model.Budget
            );

        string timeline =
            WebUtility.HtmlEncode(
                string.IsNullOrWhiteSpace(model.Timeline)
                    ? "Not specified"
                    : model.Timeline
            );

        string projectMessage =
            WebUtility.HtmlEncode(model.Message)
                .Replace("\r\n", "<br>")
                .Replace("\n", "<br>");


        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $"""
                <!DOCTYPE html>
                <html>

                <body style="
                    margin:0;
                    padding:0;
                    background:#0a0a0a;
                    color:#e5e5e5;
                    font-family:Arial,Helvetica,sans-serif;
                ">

                    <div style="
                        max-width:680px;
                        margin:0 auto;
                        padding:40px 25px;
                    ">

                        <div style="
                            margin-bottom:35px;
                            padding-bottom:20px;
                            border-bottom:1px solid #292929;
                        ">

                            <div style="
                                font-size:24px;
                                font-weight:700;
                                letter-spacing:2px;
                            ">
                                M.H.A
                            </div>

                            <div style="
                                margin-top:8px;
                                color:#777;
                                font-size:12px;
                                letter-spacing:2px;
                            ">
                                NEW PROJECT ENQUIRY
                            </div>

                        </div>


                        <h1 style="
                            margin:0 0 30px;
                            font-size:28px;
                        ">
                            {projectType}
                        </h1>


                        <table style="
                            width:100%;
                            border-collapse:collapse;
                        ">

                            <tr>
                                <td style="
                                    padding:15px 0;
                                    border-bottom:1px solid #242424;
                                    color:#777;
                                    width:160px;
                                ">
                                    Name
                                </td>

                                <td style="
                                    padding:15px 0;
                                    border-bottom:1px solid #242424;
                                ">
                                    {name}
                                </td>
                            </tr>


                            <tr>
                                <td style="
                                    padding:15px 0;
                                    border-bottom:1px solid #242424;
                                    color:#777;
                                ">
                                    Email
                                </td>

                                <td style="
                                    padding:15px 0;
                                    border-bottom:1px solid #242424;
                                ">
                                    {email}
                                </td>
                            </tr>


                            <tr>
                                <td style="
                                    padding:15px 0;
                                    border-bottom:1px solid #242424;
                                    color:#777;
                                ">
                                    Phone
                                </td>

                                <td style="
                                    padding:15px 0;
                                    border-bottom:1px solid #242424;
                                ">
                                    {phone}
                                </td>
                            </tr>


                            <tr>
                                <td style="
                                    padding:15px 0;
                                    border-bottom:1px solid #242424;
                                    color:#777;
                                ">
                                    Business
                                </td>

                                <td style="
                                    padding:15px 0;
                                    border-bottom:1px solid #242424;
                                ">
                                    {business}
                                </td>
                            </tr>


                            <tr>
                                <td style="
                                    padding:15px 0;
                                    border-bottom:1px solid #242424;
                                    color:#777;
                                ">
                                    Budget
                                </td>

                                <td style="
                                    padding:15px 0;
                                    border-bottom:1px solid #242424;
                                ">
                                    {budget}
                                </td>
                            </tr>


                            <tr>
                                <td style="
                                    padding:15px 0;
                                    border-bottom:1px solid #242424;
                                    color:#777;
                                ">
                                    Timeline
                                </td>

                                <td style="
                                    padding:15px 0;
                                    border-bottom:1px solid #242424;
                                ">
                                    {timeline}
                                </td>
                            </tr>

                        </table>


                        <div style="
                            margin-top:35px;
                        ">

                            <div style="
                                margin-bottom:12px;
                                color:#777;
                                font-size:12px;
                                letter-spacing:1px;
                            ">
                                PROJECT DETAILS
                            </div>

                            <div style="
                                padding:22px;
                                background:#111;
                                border:1px solid #292929;
                                line-height:1.8;
                            ">
                                {projectMessage}
                            </div>

                        </div>


                        <div style="
                            margin-top:35px;
                            padding-top:20px;
                            border-top:1px solid #292929;
                            color:#666;
                            font-size:12px;
                        ">

                            Submitted through the M.H.A website.

                        </div>

                    </div>

                </body>

                </html>
                """
        };

        message.Body = bodyBuilder.ToMessageBody();


        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(
            _settings.SmtpServer,
            _settings.Port,
            SecureSocketOptions.StartTls
        );

        await smtpClient.AuthenticateAsync(
            _settings.SenderEmail,
            _settings.Password
        );

        await smtpClient.SendAsync(message);

        await smtpClient.DisconnectAsync(true);
    }
}