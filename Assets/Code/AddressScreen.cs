﻿using UnityEngine;
using System.Collections;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class AddressScreen : MonoBehaviour 
{
	private string userName;
	private string realName;
	private string secretCode;
	private string address;


	public void Send()
	{
		Debug.Log("Reading Address");
		ReadAddress();
		Debug.Log("Preparing email");
		OpenExternalEmail();
		Debug.Log("Email sent!");
	}

	void ReadAddress()
	{ 
		userName = Account.Instance.UserName;
		realName = Account.Instance.FirstName + " " + Account.Instance.LastName;
		secretCode = Account.Instance.UniqueID;
		address = "";
		UIInput[] text = GetComponentsInChildren<UIInput>();
		foreach(UIInput line in text)
		{
			address += NGUIText.StripSymbols(line.value) + "\n";
		}
	}


	private void OpenExternalEmail()
	{
			
		string email = "sendcard@animin.me";
			
		string subject = MyEscapeURL("Purchase by user " + secretCode);
			
		string body = MyEscapeURL(Body());
			
			
		Application.OpenURL ("mailto:" + email + "?subject=" + subject + "&body=" + body);
			
	}  
		
	string MyEscapeURL (string url)
	{
			
		return WWW.EscapeURL(url).Replace("+","%20");
	}

	private void PrepareEmail() 
	{
		MailMessage mail = new MailMessage();
		SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
		mail.From = new MailAddress("animindev@gmail.com");
		mail.To.Add("sendcard@animin.me");
		mail.Subject ="Purchase by user " + secretCode;
		mail.Body = Body();
		
		SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
		SmtpServer.Port = 587;
		Debug.Log("DO THE THING");
		SmtpServer.Credentials = new NetworkCredential("animindev@gmail.com","Code1red") as ICredentialsByHost;
		Debug.Log("THE THING WORKS");
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
		MailMessage mail = new MailMessage("animinDev@animin.me", "sendcard@animin.me");
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
Profile Id: {1}

Name: {2}

Address: {3}

With thanks,
Team Animin
", userName, secretCode, realName, address);
	}
}
