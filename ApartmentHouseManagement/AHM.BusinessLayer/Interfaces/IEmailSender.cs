namespace AHM.BusinessLayer.Interfaces
{
    public interface IEmailSender
    {
        void Send(string toEmail, string subject, string message, string filePath = "");
    }
}