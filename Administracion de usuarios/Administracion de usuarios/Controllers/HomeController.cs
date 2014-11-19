using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Administracion_de_usuarios.Models;

namespace Administracion_de_usuarios.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.day = DayStay();

            var user = new User();
            IEnumerable<User> listauser = user.GetUsers();
            return View(listauser);            
        }

        [HttpPost]
        public ActionResult Detalles()
        {
            return View();
        }

        public ActionResult Detalles(int id)
        {            
            var user = new User();
            user = user.GetUser(id);
            return View(user);
        }

        
        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(User user)
        {
           
            user.NewUser(user.UserName, user.UserLastName, user.UserAddr, user.UserEmail, user.UserPhone, user.UserGender);
            return View();
        }


        public String DayStay()
        {
            if (DateTime.Now.Hour < 12)
            {
                return "Buenos dias";
            }
            else
            {
                return "Buenas tardes";
            }
        }

        public ActionResult getUser() {
            var user = new User();
            IEnumerable<User> listauser = user.GetUsers();
            return View(listauser);        
        }

    }
}
