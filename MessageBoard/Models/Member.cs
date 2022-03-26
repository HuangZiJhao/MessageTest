using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MessageBoard.Models
{
  public class Member
  {
    [DisplayName("帳號")]
    [Required(ErrorMessage = "input")]
    [StringLength(30, MinimumLength = 6, ErrorMessage = "6~30")]
    [Remote("CheckAccount", "Member", ErrorMessage = "有人用過")]
    public string Account { get; set; }
    public string Password { get; set; }
    [DisplayName("姓名")]
    public string Name { get; set; }
    [DisplayName("信箱")]
    [Required(ErrorMessage = "input")]
    [StringLength(200, ErrorMessage = "6~30")]
    [EmailAddress(ErrorMessage = "錯誤格式")]
    public string Email { get; set; }
    public string AuthCode { get; set; }
    public bool IsAdmin { get; set; }

  }
}