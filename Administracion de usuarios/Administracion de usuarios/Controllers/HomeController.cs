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

            //Muestra una lista de usuarios
            var myUsers = new User();
            IEnumerable<User> userList = myUsers.GetUsers();
            return View(userList);            
        }

        //Muestra los detalles de un usuario
        [HttpPost]
        public ActionResult Usuario()
        {
            return View();
        }

        public ActionResult Usuario(int id)
        {
            var myUser = new User();
            myUser = myUser.GetUser(id);
            return View(myUser);
        }       
        
        public ActionResult Nuevo()
        {
            return View();
        }

        //Crea un nuevo usuario
        [HttpPost]
        public ActionResult Nuevo(User user)
        {
            user.NewUser(user.UserName, user.UserLastName, user.UserAddr, user.UserEmail, user.UserPhone, user.UserGender);
            return Redirect("~/Home");
        }

        
        public ActionResult Eliminar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            var myUser = new User();
            bool coco = myUser.DeleteUser(id);
            return Redirect("~/Home");
        }

        public ActionResult Actualizar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Actualizar(User user)
        {
            var myUser = new User();
            myUser.UpdateUser(user.UserId, user.UserName, user.UserLastName, user.UserAddr, user.UserEmail, user.UserPhone, user.UserGender);
            return Redirect("~/Home");
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
    }
}
