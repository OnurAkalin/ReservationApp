namespace Core.Utilities.Mail;

public class EmailConfiguration : IEmailConfiguration
{
    public string SmtpServer { get; set; } = null!;

    public int SmtpPort { get; set; }

    public string SmtpUserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}