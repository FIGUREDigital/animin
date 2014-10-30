using UnityEngine;
using System.Collections;
using System.Net.Mail;

public class AddressScreen : MonoBehaviour 
{
	private string secretCode;
	private string address;


	public void Send()
	{
		ReadAddress();
		SendEmail();
	}

	void ReadAddress()
	{ 
		address = "";
		UIInput[] text = GetComponentsInChildren<UIInput>();
		foreach(UIInput line in text)
		{
			address += NGUIText.StripSymbols(line.value);
		}
	}



	void SendEmail()
	{
		MailMessage mail = new MailMessage("animinDev@animin.me", "hello@animin.me");
		SmtpClient client = new SmtpClient();            
		client.Port = 25;
		client.DeliveryMethod = SmtpDeliveryMethod.Network;
		client.UseDefaultCredentials = false;
		client.Host = "smtp.gmail.com";
		mail.Subject = "Purchase by user " + secretCode;
		mail.Body = @"
New Purchase by user: " + secretCode + @"

Address: " + address + @"

Lots of love, Ad xx
";
		client.Send(mail);
	}
}
