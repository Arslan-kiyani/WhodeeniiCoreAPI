using MailKit.Net.Imap;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MailKit.Search;
using MailKit;
using WhoDeenii.Domain.Services.Services;
using Spire.Pdf;
using System.Drawing;
using System.Drawing.Imaging;

public class EmailReaderService
{
    private readonly IConfiguration _configuration;
    private readonly string StaticSubjectLine = "Fw: Daily Pickup - From PB";
    private readonly string emailFolder;
    private readonly string attachmentsFolder;

    public EmailReaderService(IConfiguration configuration)
    {
        _configuration = configuration;
        emailFolder = @"C:\Users\laptop wala\Documents\Emails";
        attachmentsFolder = @"C:\Users\laptop wala\Documents\Attachments";

        Directory.CreateDirectory(emailFolder);
        Directory.CreateDirectory(attachmentsFolder);
    }

    public async Task<List<MimeMessage>> ReadEmailsAsync(int startIndex = 0)
    {
        var unreadEmails = new List<MimeMessage>();

        try
        {
            using (var client = await ConnectToServerAsync())
            {
                var inbox = await OpenInboxAsync(client);
                var uids = await inbox.SearchAsync(SearchQuery.NotSeen);

                foreach (var uid in uids)
                {
                    var message = await inbox.GetMessageAsync(uid);

                    if (message.Subject.Equals(StaticSubjectLine, StringComparison.OrdinalIgnoreCase))
                    {
                        await SaveEmailAsync(message);
                        await SaveAttachmentsAsync(message);
                        await MarkAsReadAsync(inbox, uid);

                        unreadEmails.Add(message);
                    }
                }

                await client.DisconnectAsync(true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return unreadEmails;
    }

    private async Task<ImapClient> ConnectToServerAsync()
    {
        var imapSettings = _configuration.GetSection("SmtpSettings");
        var client = new ImapClient();
        client.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

        await client.ConnectAsync(imapSettings["Host"], int.Parse(imapSettings["Port"]), SecureSocketOptions.SslOnConnect);
        await client.AuthenticateAsync(imapSettings["Email"], imapSettings["Password"]);

        return client;
    }

    private async Task<IMailFolder> OpenInboxAsync(ImapClient client)
    {
        var Prmary = client.GetFolder(client.PersonalNamespaces[0]);
        var inbox = await Prmary.GetSubfolderAsync("INBOX");
        await inbox.OpenAsync(FolderAccess.ReadWrite);


        return inbox;
    }

    private async Task SaveEmailAsync(MimeMessage message)
    {
        var emailFilePath = Path.Combine(emailFolder, $"{message.MessageId}.eml");
        await File.WriteAllTextAsync(emailFilePath, message.ToString());
    }

    private async Task SaveAttachmentsAsync(MimeMessage message)
    {
        foreach (var attachment in message.Attachments)
        {
            if (attachment is MimePart mimePart)
            {
                var fileName = mimePart.ContentDisposition?.FileName ?? mimePart.ContentType.Name;
                var uniqueFileName = GetUniqueFileName(fileName);
                var attachmentFilePath = Path.Combine(attachmentsFolder, fileName);

                using (var stream = File.Create(attachmentFilePath))
                {
                    await mimePart.Content.DecodeToAsync(stream);
                }

                if (fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    var outputFolder = Path.Combine(attachmentsFolder, Path.GetFileNameWithoutExtension(fileName));
                    var pdfImageExtractor = new PdfImageExtractor(attachmentFilePath, outputFolder);
                    pdfImageExtractor.ExtractImages();
                }
            }
        }
    }

    private string GetUniqueFileName(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        var nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var uniqueName = $"{nameWithoutExtension}_{DateTime.Now}{extension}";

        return uniqueName;
    }

    private async Task MarkAsReadAsync(IMailFolder inbox, UniqueId uid)
    {
        await inbox.AddFlagsAsync(uid, MessageFlags.Seen, true);
    }

    public class PdfImageExtractor
    {
        private readonly string _pdfFilePath;
        private readonly string _outputFolder;

        public PdfImageExtractor(string pdfFilePath, string outputFolder)
        {
            _pdfFilePath = pdfFilePath;
            _outputFolder = outputFolder;

            Directory.CreateDirectory(_outputFolder);
        }

        public void ExtractImages()
        {
            try
            {
                using (PdfDocument pdf = new PdfDocument())
                {
                    pdf.LoadFromFile(_pdfFilePath);

                    int imageCounter = 1;

                    for (int i = 0; i < pdf.Pages.Count; i++)
                    {
                        string pageImageFilePath = Path.Combine(_outputFolder, $"Page_{imageCounter}.png");

                        using (Bitmap bitmap = (Bitmap)pdf.SaveAsImage(i, 300, 300))
                        {
                            bitmap.Save(pageImageFilePath, ImageFormat.Png);
                        }

                        imageCounter++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while extracting images: {ex.Message}");
            }
        }

    }
}
