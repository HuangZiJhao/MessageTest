using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MessageBoard.Models
{
  public class Guestbooks
  {
    public int Id { get; set; }
    public string Acount { get; set; }
    [DisplayName("名字")]
    [Required(ErrorMessage ="請輸入名字")]
    [StringLength(10,ErrorMessage ="more 10")]
    public string Name { get; set; }
    [DisplayName("內容")]
    [Required(ErrorMessage = "請輸入內容")]
    [StringLength(100, ErrorMessage = "more 100")]
    public string Content { get; set; }
    [DisplayName("新增時間")]
    public DateTime CreateTime { get; set; }
    [DisplayName("回覆內容")]
    [Required(ErrorMessage = "請輸入內容")]
    [StringLength(100, ErrorMessage = "more 100")]
    public string Reply { get; set; }
    [DisplayName("回復時間")]
    public DateTime? ReplyTime { get; set; }
  }
}