using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ProjectRestaurant.Service.Service
{
    public class ContactService
    {
        /// <summary>
        /// send e-mail to restaurant's mail
        /// </summary>
        /// <param name="body"></param>
        public void MailSender(string body)
        {
            var fromAddress = new MailAddress("info.testrestaurant@gmail.com");
            var toAddress = new MailAddress("info.testrestaurant@gmail.com");
            const string subject = "Masadan Sipariş | Masadan Sipariş İletişim Formu";
            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "Abc12345.")
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}
