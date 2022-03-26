using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MessageBoard.Service;
using MessageBoard.Models;
using MessageBoard.ViewModels;

namespace MessageBoard.Controllers
{
  public class MemberController : Controller
  {
    private readonly MembersDBService memberService = new MembersDBService();
    private readonly MailService mailService = new MailService();
    // GET: Member
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Regist()
    {
      if (User.Identity.IsAuthenticated)
      {
        return RedirectToAction("Index", "Guestbooks");
      }
      return View();
    }
    [HttpPost]
    public ActionResult Regist(MemberRegisterViewModel RegistMember)
    {
      string AuthCodeTemp;
      if (ModelState.IsValid)
      {
        AuthCodeTemp = mailService.ValidateCode();
        RegistMember.newMember.Password = RegistMember.Password;
        RegistMember.newMember.AuthCode = AuthCodeTemp;
        memberService.Register(RegistMember.newMember);
        UriBuilder ValidateUrl = new UriBuilder(Request.Url)
        {
          Path = Url.Action("EmailValidate", "Member", new { Account = RegistMember.newMember.Account, AuthCode = RegistMember.newMember.AuthCode })
        };
        mailService.SendRegisterMail(ValidateUrl.ToString().Replace("%3F","?"), RegistMember.newMember.Email);
        TempData["RegistStatus"] = "成功去收信";
        return RedirectToAction("RegistResult");
      }
      RegistMember.Password = null;
      RegistMember.CheckPassword = null;
      return View(RegistMember);
    }
    public ActionResult RegistResult()
    {
      return View();
    }
    public JsonResult CheckAccount(MemberRegisterViewModel RegistMember)
    {
      return Json(ViewData["EmailValidate"] = memberService.CheckAccount(RegistMember.newMember.Account), JsonRequestBehavior.AllowGet);
    }
    public ActionResult EmailValidate(string Account, string AuthCode)
    {
      ViewData["EmailValidate"] = memberService.EmailValidate(Account, AuthCode);
      return View();
    }

  }
}