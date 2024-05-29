namespace UniversityCommunity.Business.Interfaces
{
	public interface IEmailService
	{
		void SendMail(string mailTo, string subject, string body, string from = null);
	}
}
