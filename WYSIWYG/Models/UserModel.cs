using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WYSIWYG.Models
{
	public class UserModel
	{
		#region Fields
		string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

		public int ID { get; set; }
		public string GUID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EMail { get; set; }
		//public DateTime BirthDate { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public DateTime ModifiedDate { get; set; }
		public bool IsActivated { get; set; } = false;
		public string ErrMessage { get; set; }
		#endregion Fields

		#region Methods
		public UserModel() { }
		public UserModel(int ID)
		{
			GetUserByID(ID);
		}
		public UserModel(string GUID)
		{
			GetUserByGUID(GUID);
		}
		private void GetUserByID(int ID)
		{
			using (SqlConnection conn = new SqlConnection(cs))
			{
				SqlCommand cmd = new SqlCommand("SELECT * FROM UserAccounts WHERE ID = @userID", conn);
				SqlParameter paramID = new SqlParameter("@UserID", ID);
				cmd.Parameters.Add(paramID);

				conn.Open();
				SqlDataReader dr = cmd.ExecuteReader();

				while (dr.Read())
				{
					this.ID = Convert.ToInt32(dr["ID"]);
					//this.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
					this.EMail = dr["EMail"].ToString();
					this.LastName = dr["LastName"].ToString();
					this.FirstName = dr["FirstName"].ToString();
					this.Login = dr["LogIn"].ToString();
					this.Password = dr["Password"].ToString();
					this.GUID = dr["GUID"].ToString();
				}
			}
		}
		private void GetUserByGUID(string GUID)
		{
			using (SqlConnection conn = new SqlConnection(cs))
			{
				SqlCommand cmd = new SqlCommand("SELECT * FROM UserAccounts WHERE GUID = @userGUID", conn);
				SqlParameter paramGUID = new SqlParameter("@UserGUID", GUID);
				cmd.Parameters.Add(paramGUID);

				conn.Open();
				SqlDataReader dr = cmd.ExecuteReader();

				while (dr.Read())
				{
					this.ID = Convert.ToInt32(dr["ID"]);
					//this.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
					this.EMail = dr["EMail"].ToString();
					this.LastName = dr["LastName"].ToString();
					this.FirstName = dr["FirstName"].ToString();
					this.Login = dr["LogIn"].ToString();
					this.Password = dr["Password"].ToString();
					this.GUID = dr["GUID"].ToString();
					this.IsActivated = Convert.ToBoolean(dr["isActivated"]);
				}
			}
		}
		public void GetUserByLogIn(string LogIn)
		{
			using (SqlConnection conn = new SqlConnection(cs))
			{
				SqlCommand cmd = new SqlCommand("SELECT * FROM UserAccounts WHERE LogIn = @UserLogIn", conn);
				SqlParameter paramLogIn = new SqlParameter("@UserLogIn", LogIn);
				cmd.Parameters.Add(paramLogIn);

				conn.Open();
				SqlDataReader dr = cmd.ExecuteReader();

				while (dr.Read())
				{
					this.ID = Convert.ToInt32(dr["ID"]);
					//this.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
					this.EMail = dr["EMail"].ToString();
					this.LastName = dr["LastName"].ToString();
					this.FirstName = dr["FirstName"].ToString();
					this.Login = dr["LogIn"].ToString();
					this.Password = dr["Password"].ToString();
					this.GUID = dr["GUID"].ToString();
					this.IsActivated = Convert.ToBoolean(dr["isActivated"]);
				}
			}
		}
		public void SendActivationEmal()
		{
			string g = Convert.ToString(this.GUID);
			string senderEMail = "levonpetrosyan9@gmail.com";
			string senderPassword = "googlecomm123";
			StringBuilder _messageBody = new StringBuilder("Your Confirmation link is \n ");
			_messageBody.Append("http://localhost:64709/Home/ActivateUser?guid=");
			SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Timeout = 100000;
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.UseDefaultCredentials = false;
			client.Credentials = new NetworkCredential(senderEMail, senderPassword);

			_messageBody.Append(g);
			//_messageBody.Append("\" />");
			string messageBody = _messageBody.ToString();

			MailMessage message = new MailMessage(senderEMail, this.EMail, "confirm your email", messageBody);
			message.IsBodyHtml = true;
			message.BodyEncoding = UTF8Encoding.UTF8;

			client.Send(message);
		}
		public bool IsRealUser(string LogIn, string Password)
		{
			using (SqlConnection conn = new SqlConnection(cs))
			{
				SqlCommand cmd = new SqlCommand("SELECT * FROM UserAccounts WHERE LogIn = @userLogIn AND Password = @userPassword", conn);
				SqlParameter paramLogIn = new SqlParameter("@userLogIn", LogIn);
				SqlParameter paramPassword = new SqlParameter("@userPassword", Password);
				cmd.Parameters.Add(paramLogIn);
				cmd.Parameters.Add(paramPassword);

				conn.Open();
				SqlDataReader dr = cmd.ExecuteReader();

				return dr.Read();
			}
		}
		public bool IsActivatedUser(string LogIn, string Password)
		{
			using (SqlConnection conn = new SqlConnection(cs))
			{
				SqlCommand cmd = new SqlCommand("SELECT IsActivated FROM UserAccounts WHERE LogIn = @userLogIn AND Password = @userPassword", conn);
				SqlParameter paramLogIn = new SqlParameter("@userLogIn", LogIn);
				SqlParameter paramPassword = new SqlParameter("@userPassword", Password);
				cmd.Parameters.Add(paramLogIn);
				cmd.Parameters.Add(paramPassword);

				conn.Open();
				SqlDataReader dr = cmd.ExecuteReader();
				bool b = false;
				while (dr.Read())
				{
					b = Convert.ToBoolean(dr["IsActivated"]);
				}
				return b;
			}
		}
		public void Insert()
		{
			
			using (SqlConnection conn = new SqlConnection(cs))
			{
				SqlCommand cmd = new SqlCommand(
					"INSERT INTO UserAccounts " +
					"(FirstName, LastName, EMail, BirthDate, LogIn, Password, ModifiedDate, GUID, IsActivated)" +
					" VALUES(@FirstName, @LastName, @EMail, @BirthDate, @LogIn, @Password, @ModifiedDate, @GUID, @IsActivated)"
					, conn);

				cmd.Parameters.AddRange(new[]
				{
					new SqlParameter("@FirstName", this.FirstName),
					new SqlParameter("@LastName", this.LastName),
					//new SqlParameter("@BirthDate", this.BirthDate),
					new SqlParameter("@LogIn", this.Login),
					new SqlParameter("@EMail", this.EMail),
					new SqlParameter("@Password", this.Password),
					new SqlParameter("@ModifiedDate", DateTime.Now),
					new SqlParameter("@GUID", this.GUID),
					new SqlParameter("@IsActivated", this.IsActivated),
				});
				conn.Open();
                //cmd.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                SendActivationEmal();

                try
                {
				//	cmd.ExecuteNonQuery();
					//SendActivationEmal();
					//this.Json(new BaseJsonData { HasError = true, Message = ex.Message })
				}
				catch (System.Data.SqlClient.SqlException ex)
				{
					string pat = @"The duplicate key value is \([a-z,A-Z,0-9,@,.]*\)";
					Regex r = new Regex(pat, RegexOptions.IgnoreCase);

					// Match the regular expression pattern against a text string.
					Match m = r.Match(ex.Message);
					ErrorMessage.Message = m.ToString();
					//this.ErrMessage = m.ToString();
				}

			}
		}
		public void Update()
		{
			UserModel origin = new UserModel(this.ID);
			StringBuilder queryString = new StringBuilder("UPDATE UserAccounts SET ");

			foreach(var elem in origin.GetType().GetProperties())
			{
				if (elem.GetValue(this)?.ToString() != elem.GetValue(origin)?.ToString())
				{
					queryString.Append(elem.Name);
					queryString.Append(" = ");
					queryString.Append("@"); //elem.GetValue(this)
					queryString.Append(elem.Name);
					queryString.Append(", ");
				}
			}
			queryString.Replace(",", "");
			queryString.Append("WHERE ID = ");
			queryString.Append(this.ID);

			using (SqlConnection conn = new SqlConnection(cs))
			{

				SqlCommand cmd = new SqlCommand(Convert.ToString(queryString), conn);

				cmd.Parameters.AddRange(new[]
				{
					new SqlParameter("@FirstName", this.FirstName),
					new SqlParameter("@LastName", this.LastName),
					//new SqlParameter("@BirthDate", this.BirthDate),
					new SqlParameter("@LogIn", this.Login),
					new SqlParameter("@EMail", this.EMail),
					new SqlParameter("@Password", this.Password),
					new SqlParameter("@ModifiedDate", DateTime.Now),
					new SqlParameter("@GUID", this.GUID),
					new SqlParameter("@IsActivated", this.IsActivated),
				});
				conn.Open();
				cmd.ExecuteNonQuery();
				

			}
			
		}
		#endregion Methods
	}
}