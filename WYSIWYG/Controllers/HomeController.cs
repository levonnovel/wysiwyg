using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WYSIWYG.Models;

namespace WYSIWYG.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			UserModel u = new UserModel((int)Session["ID"]);
			//u.Update();
			string[] dirs = Directory.GetFiles(@"C:\Users\leomax\Desktop\web\myProjects\wysiwygReal\WYSIWYGDesigned\files\" + u.Login);
			for (int i = 0; i < dirs.Length; i++)
			{
				string fileName = Path.GetFileName(dirs[i]);
				dirs[i] = fileName.Substring(0, fileName.Length - 4);
			}

			//string[] proinfo = new string[] { "a", "b" };
			//ViewBag.ProInfo = dirs;

			return View("~/Views/Home/Index.cshtml", dirs);
		}
		public ActionResult Registration()
		{
			//TempData["alertMessage"] = "sss";

			return View();
		}
		public ActionResult CreateUser(UserModel u)
		{
			u.ModifiedDate = DateTime.Now;
			u.GUID = Guid.NewGuid().ToString();
			u.Insert();
			string path = (@"C:\Users\user\Dropbox\Zara(2)\ogma\project with jquery\WYSIWYG\files\" + u.Login);

            if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			TempData["alertMessage"] = u.ErrMessage;
			return View("Registration");
		}
		public void ActivateUser(string guid)
		{
			UserModel u = new UserModel(guid);
			u.IsActivated = true;
			u.Update();
			RedirectToAction("Index");
		}
		// GET: Home
		[HttpGet]
		public ActionResult CheckUser(string LogIn, string Password)
		{
			UserModel u = new UserModel();
			if (u.IsRealUser(LogIn, Password) && u.IsActivatedUser(LogIn, Password))
			{
				u.GetUserByLogIn(LogIn);
				Session["ID"] = u.ID;
				Session["LogIn"] = u.Login;
				return RedirectToAction("Index");
			}
			else
			{
				return RedirectToAction("LogIn");
			}
		}

		public ActionResult Home()
		{

			return View("~/Views/Home/Home.cshtml");
		}

		public JsonResult CreateFile(string name)
		{
			UserModel u = new UserModel((int)Session["ID"]);
			string[] dirs = Directory.GetFiles(@"C:\Users\leomax\Desktop\web\myProjects\wysiwygReal\WYSIWYGDesigned\files\" + u.Login);

			for (int i = 0; i < dirs.Length; i++)
			{
				dirs[i] = dirs[i].Substring(43, dirs[i].Length - 47);
			}

			bool isNameUsed = dirs.Contains(name);
			if (!isNameUsed && name != "")
			{
				string path = "C:\\Users\\leomax\\Desktop\\web\\myProjects\\wysiwygReal\\WYSIWYGDesigned\\files\\" + u.Login + "\\" + name + ".txt";
				StreamWriter File = new StreamWriter(path);
				File.Write("Something");

				File.Close();
			}

			return Json(data: isNameUsed ? "You always have a file with this name" : "Success");
		}
		public string ReadFile(string name)
		{
			UserModel u = new UserModel((int)Session["ID"]);
			string path = "C:\\Users\\leomax\\Desktop\\web\\myProjects\\wysiwygReal\\WYSIWYGDesigned\\files\\" + u.Login + "\\" + name + ".txt";

			using (StreamReader sr = new StreamReader(path))
			{
				String line = sr.ReadToEnd();
				return line;
			}
		}
		public void DeleteFile(string name)
		{
			UserModel u = new UserModel((int)Session["ID"]);
			string path = "C:\\Users\\leomax\\Desktop\\web\\myProjects\\wysiwygReal\\WYSIWYGDesigned\\files\\" + u.Login + "\\" + name + ".txt";

			System.IO.File.Delete(path);

		}
		public JsonResult SaveFile(string name, string text)
		{
			if (name != String.Empty)
			{
				UserModel u = new UserModel((int)Session["ID"]);
				string path = "C:\\Users\\leomax\\Desktop\\web\\myProjects\\wysiwygReal\\WYSIWYGDesigned\\files\\" + u.Login + "\\" + name + ".txt";

				StreamWriter File = new StreamWriter(path);
				File.Write(text);

				File.Close();
			}

			return Json(data: name == String.Empty ? "Failed" : "Saved");

		}
	}
}