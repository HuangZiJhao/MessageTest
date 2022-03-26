using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MessageBoard.Service;
using MessageBoard.ViewModels;
using MessageBoard.Models;

namespace MessageBoard.Controllers
{
  public class GuestbooksController : Controller
  {
    private readonly GuestbooksDBService GuestbooksService = new GuestbooksDBService();
    // GET: Guestbooks
    public ActionResult Index()
    {
      GuestbooksViewModel Data = new GuestbooksViewModel();
      Data.DataList = GuestbooksService.GetDataList();
      return View(Data);
    }
    [HttpPost]
    public ActionResult Create([Bind(Include = "Name,Content")] Guestbooks Data)
    {
      GuestbooksService.InserGuestbook(Data);
      return RedirectToAction("Index");
    }
    public ActionResult Create()
    {
      return PartialView();
    }
    public ActionResult Edit(int id)
    {
      Guestbooks Data = GuestbooksService.GetDataById(id);
      return View(Data);
    }
    [HttpPost]
    public ActionResult Edit(int Id, [Bind(Include = "Name,Content")] Guestbooks Data)
    {
      if (GuestbooksService.IsUpdate(Data.Id))
      {
        GuestbooksService.UpdateGuestbooks(Data);
      }
      return RedirectToAction("Index");
    }
    public ActionResult Reply(int Id)
    {
      Guestbooks data = GuestbooksService.GetDataById(Id);
      return View(data);
    }
    [HttpPost]
    public ActionResult Reply(int Id, [Bind(Include = "Reply,ReplyTime")] Guestbooks Data)
    {
      if (GuestbooksService.IsUpdate(Id))
      {
        Data.Id = Id;
        GuestbooksService.UpdateGuestbooks(Data);
      }
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int Id)
    {
      GuestbooksService.DeleteGuestbook(Id);
      return RedirectToAction("Index");
    }
  }
}