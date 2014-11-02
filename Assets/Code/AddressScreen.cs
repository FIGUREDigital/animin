using UnityEngine;
using System.Collections;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class AddressScreen : MonoBehaviour 
{
	private string secretCode;
	private string address;


	public void Send()
	{
		ReadAddress();
		PrepareEmail();
		Debug.Log("Email sent!");
	}

	void ReadAddress()
	{ 
		secretCode = Account.Instance.UniqueID;
		address = "";
		UIInput[] text = GetComponentsInChildren<UIInput>();
		foreach(UIInput line in text)
		{
			address += NGUIText.StripSymbols(line.value);
		}
	}

	private void PrepareEmail() {
		MailMessage mail = new MailMessage();
		SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
		mail.From = new MailAddress("animindev@gmail.com");
		mail.To.Add("hello@animin.me");
		mail.Subject ="Purchase by user " + secretCode;
		mail.Body = Body();
		
		SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
		SmtpServer.Port = 587;
		SmtpServer.Credentials = (ICredentialsByHost) new NetworkCredential("animindev@gmail.com","Code1red");
		SmtpServer.EnableSsl = true;
		SmtpServer.Timeout = 20000;
		SmtpServer.UseDefaultCredentials = false;
		ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors){
			return true;
		};
		try {
			SmtpServer.Send(mail);
		} catch (SmtpException myEx) {
			Debug.Log ("Expection: \n" + myEx.ToString());
		}
		
		
		
	}

	void SendEmail()
	{
		MailMessage mail = new MailMessage("animinDev@animin.me", "hello@animin.me");
		SmtpClient client = new SmtpClient
		{
			Host = "smtp.gmail.com",
			Port = 587,
			EnableSsl = true,
			UseDefaultCredentials = false,
			Credentials = (System.Net.ICredentialsByHost)new System.Net.NetworkCredential("animindev@gmail.com", "Code1red")            
		};
		client.DeliveryMethod = SmtpDeliveryMethod.Network;
		mail.Subject = "Purchase by user " + secretCode;
		mail.Body = Body();
		client.Send(mail);
	}

	private string Body()
	{
		return string.Format(
@"
New Purchase by user: {0}

Address: {1}

Lots of love, Ad xx
", secretCode, address);
	}
}
