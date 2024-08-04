﻿using MailKit.Net.Pop3;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Сервис умеет отправлять электронную почту и считывать входящие письма
/// </summary>
public sealed class MailRuService2
{
    public string EmailName { get; set; }
    public string EmailAddress { get; set; }
    public string AppPassword { get; set; }
    public string SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public string PopHost { get; set; }
    public int PopPort { get; set; }

    public MailRuService2(  )
    {
        this.EmailName = "Администрация сайта";
        this.EmailAddress = "kba-2018@mail.ru";
        this.AppPassword = "2LDtctEezQuQyxtcaasv";
        this.SmtpHost = "smtp.mail.ru";
        this.SmtpPort = 587;
        this.PopHost = "pop.mail.ru";
        this.PopPort = 995;
    }


    /// <summary>
    /// Отправка сообщения по электронной почте
    /// </summary> 
    public void Send(string email, string subject, string message, IEnumerable<string> files=null)
    {
        try
        {
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.Connect(this.SmtpHost, this.SmtpPort, SecureSocketOptions.StartTls);
                //smtp.Connect(smtpHost, smtpPort);

                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(this.EmailName, EmailAddress));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;

                var builder = new BodyBuilder();
                builder.TextBody = message;
                if (files != null)
                    foreach (var resource in files)
                        builder.Attachments.Add(resource);
                emailMessage.Body = builder.ToMessageBody();

                smtp.Authenticate(this.EmailAddress, this.AppPassword);
                smtp.Send(emailMessage);
                smtp.Disconnect(true);
            }
        }
        catch(Exception ex)
        {
            throw new Exception("Не удалось отправить сообщение по электронной почте",ex);
        }
    }


    public void Clear()
    {
        using (var client = new Pop3Client())
        {
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.Connect(PopHost, PopPort);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(EmailAddress, this.AppPassword);
            client.Disconnect(true);

        }
    }
          

    
    public IEnumerable<MessageModel> Recieve()
    {
        var messages = new List<MessageModel>();
        using (var client = new Pop3Client())
        {
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.Connect(PopHost, PopPort);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(EmailAddress, this.AppPassword);
            for (int i = 0; i < client.Count; i++)
            {
                MimeMessage message = client.GetMessage(i);
                
                var sender = message.Sender;
                var subject = message.Subject;
                var date = message.Date;
                var text = message.GetTextBody(MimeKit.Text.TextFormat.Text);
                var files = new Dictionary<string, FileModel>();
                foreach(var attachment in message.Attachments)
                {
                    var contentType = attachment.ContentType;
                    var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
                    byte[] binData = null;
                    using (var stream = new MemoryStream())
                    {
                        if (attachment is MessagePart)
                        {
                            var rfc822 = (MessagePart)attachment;
                            rfc822.Message.WriteTo(stream);
                        }
                        else
                        {
                            var part = (MimePart)attachment;
                            part.Content.DecodeTo(stream);
                        }
                        stream.Seek(0, SeekOrigin.Begin);
                        binData = new byte[stream.Length];
                        int readed = stream.Read(binData, 0, (int)stream.Length);
                        files[fileName] = new FileModel()
                        {
                            ContentType = contentType.ToString(),
                            FileName = fileName,
                            Data = binData 
                        };
                        Console.WriteLine($"{fileName} {binData.Length}");
                    }
                }
                messages.Add(new MessageModel()
                {
                    Subject = subject,
                    Date = $"{date.Day}.{date.Month}.{date.Year}",
                    Sender = sender?.Address.ToString(),
                    Text = text,
                    Files = files
                });
                Console.WriteLine($"{message.Date} {message.Sender} {message.Subject}");
            }
            client.Disconnect(true);
        }
        return messages;
    }

    public class MessageModel
    {
        public string Sender { get; set; }
        public string Date { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public IDictionary<string, FileModel> Files { get; set; }

        public override string ToString()
            => $"{GetHashCode()} {Date.ToString()} {Sender} {Subject}";
    }

    public class FileModel
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }

    public async Task SendEmailAsync(string email, string subject, string html)
    {
        this.Send( email,subject, html);
        await Task.CompletedTask;
    }
}
