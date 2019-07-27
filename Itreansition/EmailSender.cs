using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition //Отправка Email при регистрации
{
    public class EmailSender
    { 
   public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("Администрация сайта", "6dafboy6@mail.ru"));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.mail.ru", 465);
                
             await client.AuthenticateAsync("6dafboy6@mail.ru", "6daf2531495boy6");
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}
}
