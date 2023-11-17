using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Core.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

      
        public async Task SendEmailAsync(string toEmail, string subject, string url)
        {
            string ResetPasswordBody()
            {
                return $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n\r\n    <title>Document</title>\r\n    <style>\r\n        /*Обнуление*/\r\n        * {{\r\n            padding: 0;\r\n            margin: 0;\r\n            border: 0;\r\n        }}\r\n\r\n        *,\r\n        *:before,\r\n        *:after {{\r\n            -moz-box-sizing: border-box;\r\n            -webkit-box-sizing: border-box;\r\n            box-sizing: border-box;\r\n        }}\r\n\r\n        :focus,\r\n        :active {{\r\n            outline: none;\r\n        }}\r\n\r\n        a:focus,\r\n        a:active {{\r\n            outline: none;\r\n        }}\r\n\r\n        nav,\r\n        footer,\r\n        header,\r\n        aside {{\r\n            display: block;\r\n        }}\r\n\r\n        html,\r\n        body {{\r\n            height: 100%;\r\n            width: 100%;\r\n            font-size: 100%;\r\n            line-height: 1;\r\n            font-size: 14px;\r\n            -ms-text-size-adjust: 100%;\r\n            -moz-text-size-adjust: 100%;\r\n            -webkit-text-size-adjust: 100%;\r\n        }}\r\n\r\n        input,\r\n        button,\r\n        textarea {{\r\n            font-family: inherit;\r\n        }}\r\n\r\n        input::-ms-clear {{\r\n            display: none;\r\n        }}\r\n\r\n        button {{\r\n            cursor: pointer;\r\n        }}\r\n\r\n        button::-moz-focus-inner {{\r\n            padding: 0;\r\n            border: 0;\r\n        }}\r\n\r\n        a,\r\n        a:visited {{\r\n            text-decoration: none;\r\n        }}\r\n\r\n        a:hover {{\r\n            text-decoration: none;\r\n        }}\r\n\r\n        ul li {{\r\n            list-style: none;\r\n        }}\r\n\r\n        img {{\r\n            vertical-align: top;\r\n        }}\r\n\r\n        h1,\r\n        h2,\r\n        h3,\r\n        h4,\r\n        h5,\r\n        h6 {{\r\n            font-size: inherit;\r\n            font-weight: 400;\r\n        }}\r\n\r\n        /*--------------------*/\r\n        .container {{\r\n            margin: 0 auto;\r\n            max-width: 1200px;\r\n        }}\r\n\r\n        .wraper {{\r\n            font-family: Arial, Helvetica, sans-serif;\r\n        }}\r\n\r\n        .header {{\r\n            background-color: #15112b;\r\n            text-align: center;\r\n        }}\r\n\r\n        .header__main {{\r\n            padding: 25px 0px;\r\n        }}\r\n\r\n        .header__main h2 {{\r\n            color: white;\r\n            font-size: 35px;\r\n            font-family: cursive;\r\n            font-style: italic;\r\n            font-weight: 700;\r\n        }}\r\n\r\n        .main {{\r\n            margin: auto;\r\n            text-align: center;\r\n            padding: 25px 0px;\r\n        }}\r\n\r\n        .main p {{\r\n            font-size: 26px;\r\n            padding: 40px 0px;\r\n            color: black;\r\n        }}\r\n\r\n        .main a {{\r\n            margin: 0 auto;\r\n            color: white;\r\n            font-size: 16px;\r\n            background-color: #15112b;\r\n            border-radius: 6px;\r\n            padding: 15px;\r\n        }}\r\n\r\n        .footer {{\r\n            background-color: #15112b;\r\n            padding: 20px 0px;\r\n            text-align: center;\r\n        }}\r\n    </style>\r\n</head>\r\n\r\n<body>\r\n    <div class=\"wraper\">\r\n        <header class=\"header\">\r\n            <div class=\"container\">\r\n                <header class=\"header\">\r\n                    <div class=\"header__main\">\r\n                        <h2>Sayron Pizza</h2>\r\n                    </div>\r\n            </div>\r\n        </header>\r\n        <div class=\"main\">\r\n            <p>If you dont remember your password click on the button</p>\r\n            <a href=\"{url}\">Reset Password</a>\r\n        </div>\r\n\r\n        <footer class=\"footer\">\r\n        </footer>\r\n    </div>\r\n</body>\r\n\r\n</html>";
            }
            string fromEmail = _config["EmailSettings:User"];
            string SMTP = _config["EmailSettings:SMTP"];
            string rgdd = $"ffs{url}";
            int port = Int32.Parse(_config["EmailSettings:PORT"]);
            string password = _config["EmailSettings:Password"];
            string body = ResetPasswordBody();
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(fromEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            email.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(SMTP, port, SecureSocketOptions.SslOnConnect);
                smtp.Authenticate(fromEmail, password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
           
        }
    }
}
