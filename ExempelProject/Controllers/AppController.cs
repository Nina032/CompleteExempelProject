using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExempelProject.Data;
using ExempelProject.Services;
using ExempelProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExempelProject.Controllers
{
  public class AppController : Controller
  {
    private readonly IMailService _mailService;
    private readonly IExempelRepository _repository;

    public AppController(IMailService mailService, IExempelRepository repository)
    {
      _mailService = mailService;
      _repository = repository;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpGet("contact")]
    public IActionResult Contact()
    {
      return View();
    }

    [HttpPost("contact")]
    public IActionResult Contact(ContactViewModel model)
    {
      if (ModelState.IsValid)
      {
        // Send the Email
        _mailService.SendMessage("shawn@wildermuth.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
        ViewBag.UserMessage = "Mail Sent...";
        ModelState.Clear();
      }

      return View();
    }

    public IActionResult About()
    {
      return View();
    }

    public IActionResult Shop()
    {
      var results = _repository.GetAllProducts();

      return View(results);
    }
  }
}
