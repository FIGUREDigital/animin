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
		secretCode = Account.Instance.UniqueID;
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
		mail.Body = @"
New Purchase by user: " + secretCode + @"

Address: " + address + @"

Lots of love, Ad xx
";
		client.Send(mail);
	}
}
