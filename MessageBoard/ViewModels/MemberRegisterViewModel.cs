using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageBoard.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MessageBoard.ViewModels
{
  public class MemberRegisterViewModel
  {
    public Member newMember = new Member();
    [DisplayName("密碼")]
    [Required(ErrorMessage = "請輸入")]
    public string Password { get; set; }
    [DisplayName("密碼確認")]
    [Compare("Password", ErrorMessage = "密碼不一致")]
    [Required(ErrorMessage = "請輸入")]
    public string CheckPassword { get; set; }
  }
}