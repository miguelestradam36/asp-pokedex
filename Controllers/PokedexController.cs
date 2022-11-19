using ASP_Pokemon.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;
using System.Diagnostics;
using MimeKit.Text;
using MimeKit;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace ASP_Pokemon.Controllers
{
    public class PokedexController : Controller
    {

        private readonly ILogger<PokedexController> _logger;
        public PokedexController(ILogger<PokedexController> logger)
        {
            _logger = logger;
        }
        [Route("")]
        [Route("home")]
        [RequireHttps]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        public IActionResult Index()
        {
            return View();
        }
        [Route("pokemon/")]
        [HttpGet]
        [RequireHttps]
        public IActionResult Pokemon(int id)
        {
            List<PokemonModel> pokemons = new List<PokemonModel>();
            JSONReadWrite readWrite = new JSONReadWrite();
            pokemons = JsonConvert.DeserializeObject<List<PokemonModel>>(readWrite.Read("pokeapi.json", "data"));
            if (pokemons != null)
            {
                PokemonModel Pokemon_ = pokemons.FirstOrDefault(x => x.id == id);
                if (Pokemon_ == null)
                {
                    ViewBag.Error = "Pokemon not in Pokedex";
                    return View(Pokemon_);
                }
                else
                {
                    if (ModelState.IsValid) 
                    {
                        return View(Pokemon_);
                    }
                    else
                    {
                        ViewBag.Error = "Information not loaded correctly";
                        return View(Pokemon_);
                    }
                }
            }
            else
            {
                PokemonModel Pokemon_ = new PokemonModel();
                ViewBag.Error = "Pokedex not loaded to server";
                return View(Pokemon_);
            }
        }
        [Route("privacy/")]
        [RequireHttps]
        public IActionResult Privacy()
        {
            return View();
        }
        [Route("legendary-pokemons")]
        [RequireHttps]
        public IActionResult Legendaries()
        {
            return View();
        }

        [Route("pokedex/")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        [RequireHttps]
        public IActionResult Pokedex()
        {
            try 
            {
                List<PokemonModel> pokemons = new List<PokemonModel>();
                JSONReadWrite readWrite = new JSONReadWrite();
                pokemons = JsonConvert.DeserializeObject<List<PokemonModel>>(readWrite.Read("pokeapi.json", "data"));
                return View(pokemons);
            } 
            catch (Exception error)
            {
                ViewBag.Error = $" Pokedex could not be loaded:  {error.Message}";
                return View();
            }
        }
        [Route("pokedex/")]
        [HttpPost]
        [RequireHttps]
        [ValidateAntiForgeryToken]
        public IActionResult Pokedex(string searchString)
        {
            List<PokemonModel> pokemons = new List<PokemonModel>();
            List<PokemonModel> filtered = new List<PokemonModel>();
            JSONReadWrite readWrite = new JSONReadWrite();
            pokemons = JsonConvert.DeserializeObject<List<PokemonModel>>(readWrite.Read("pokeapi.json", "data"));
            if (!String.IsNullOrEmpty(searchString))
            {
                foreach (PokemonModel pokemon in pokemons)
                {
                    if (pokemon.name.Contains(searchString))
                    {
                        filtered.Add(pokemon);
                    }
                }
                if (filtered.Count > 0)
                {
                    ViewBag.Filter = "Search results";
                    return View(filtered);
                }
                else
                {
                    ViewBag.Filter = $"No pokemons containing: {searchString}";
                    return View(pokemons);
                }
            }
            else
            {
                return View(pokemons);
            }
        }
        [Route("about")]
        [RequireHttps]
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
        [HttpPost]
        [Route("about")]
        [RequireHttps]
        [ValidateAntiForgeryToken]
        public IActionResult About(SuscribeModel SuscribeModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string emailBody = string.Empty;
                    //instantiate a new MimeMessage
                    var message = new MimeMessage();
                    //Setting the To e-mail address
                    message.To.Add(MailboxAddress.Parse(SuscribeModel.Email));
                    //Setting the From e-mail address
                    message.From.Add(MailboxAddress.Parse(""));
                    //E-mail subject 
                    message.Subject = "Subscription Confirmation";
                    //E-mail message body
                    emailBody = "<h2>Thanks for subscribing! </h2>" 
                                + "<h4>Sincerily,<br>Miguel Estrada</h4>";
                    message.Body = new TextPart(TextFormat.Html)
                    {
                        Text = emailBody
                    };
                    //Configure the e-mail
                    using (var emailClient = new SmtpClient())
                    {
                        emailClient.Connect("", 465, true);
                        emailClient.Authenticate("", "");
                        emailClient.Send(message);
                        emailClient.Disconnect(true);
                        ViewBag.Exception = "Email was sent succesfully.";
                    }
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Exception = $" Oops! Message could not be sent. Error:  {ex.Message}";
                }

            }
            else
            {
                ModelState.Clear();
                ViewBag.Exception = "Model is not valid";
            }
            return View();
        }
        [Route("contact")]
        [RequireHttps]
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [Route("contact")]
        [RequireHttps]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(EmailModel EmailModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string emailBody = string.Empty;
                    //instantiate a new MimeMessage
                    var message = new MimeMessage();
                    //Setting the To e-mail address
                    message.To.Add(MailboxAddress.Parse(EmailModel.Email));
                    //Setting the From e-mail address
                    message.From.Add(MailboxAddress.Parse(""));
                    //E-mail subject 
                    message.Subject = EmailModel.Subject;
                    //E-mail message body
                    emailBody = "<h2>Thanks for contacting us,  " + EmailModel.Name + " ! </h2>" +
                        "<h4>We will answer as soon as possible. The message you send contained the following content:</h4><br/>"
                        + "<h5>" + EmailModel.Subject + "</h5><p>" + EmailModel.Message + "</p>" + "<br/>" + "<h4>Sincerily,<br>Miguel Estrada</h4>";
                    message.Body = new TextPart(TextFormat.Html)
                    {
                        Text = emailBody
                    };
                    //Configure the e-mail
                    using (var emailClient = new SmtpClient())
                    {
                        emailClient.Connect("", 465, true);
                        emailClient.Authenticate("", "");
                        emailClient.Send(message);
                        emailClient.Disconnect(true);
                        ViewBag.Exception = "Email was sent succesfully.";
                    }
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Exception = $" Oops! Message could not be sent. Error:  {ex.Message}";
                }

            }
            else
            {
                ModelState.Clear();
                ViewBag.Exception = "Model is not valid";
            }
            return View();
        }
        [Route("wiki/")]
        [RequireHttps]
        public IActionResult Wiki()
        {
            return View();
        }
        [Route("error/")]
        [RequireHttps]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public class JSONReadWrite
        {
            public JSONReadWrite() { }

            public string Read(string fileName, string location)
            {
                string root = "wwwroot";
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    root,
                    location,
                    fileName);

                string jsonResult;

                using (StreamReader streamReader = new StreamReader(path))
                {
                    jsonResult = streamReader.ReadToEnd();
                }
                return jsonResult;
            }
        }
    }
}