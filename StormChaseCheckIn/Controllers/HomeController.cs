using Microsoft.AspNet.Identity;
using StormChaseCheckIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace StormChaseCheckIn.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult CheckMeIn()
        {
            ViewBag.BodyClass = "checkin";

            return View();
        }

        public ActionResult MyCheckIns()
        {
            using (StormChaseCheckInEntities db = new StormChaseCheckInEntities())
            {
                List<CheckInListViewModel> model = new List<CheckInListViewModel>();
                model = (from x in db.CheckIns
                         where !x.IsDeleted
                         orderby x.CreatedDateTime descending
                         select new CheckInListViewModel
                         {
                             CheckInID = x.CheckInID,
                             Text = x.Text,
                             Latitude = x.Latitude,
                             Longitude = x.Longitude,
                             Timezone = x.Timezone,
                             CreatedDate = x.CreatedDateTime
                         }).ToList();

                return View(model);
            }
        }

        public ActionResult Settings()
        {
            using (StormChaseCheckInEntities db = new StormChaseCheckInEntities())
            {
                SettingsViewModel model = new SettingsViewModel();
                List<SelectListItem> timezoneList = new List<SelectListItem>();

                for (int hour = -12; hour < 12; hour++)
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = hour.ToString() + " hours";
                    item.Value = hour.ToString();

                    timezoneList.Add(item);
                }

                model.Timezone = timezoneList;
                //model.SelectedTimezone = db.Settings.Where(x => x.SettingsTypeID == (int)Enums.SettingsType.Timezone).Select(s => s.SettingsValue.ToString()).FirstOrDefault();

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckMeIn(CheckInViewModel form)
        {
            if (ModelState.IsValid)
            {
                CheckIn checkIn = new CheckIn();
                checkIn.Text = form.Text;
                checkIn.Latitude = form.Latitude;
                checkIn.Longitude = form.Longitude;
                checkIn.Timezone = (decimal)Session[Functions.Settings.Session.TIMEZONE];
                checkIn.CreatedDateTime = DateTime.Now;
                checkIn.IsDeleted = false;
                checkIn.UserID = User.Identity.GetUserId();

                using (StormChaseCheckInEntities db = new StormChaseCheckInEntities())
                {
                    db.Database.Connection.Open();

                    db.CheckIns.Add(checkIn);
                    db.SaveChanges();
                }

                return RedirectToAction("MyCheckIns", "Home");
            }

            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(SettingsViewModel form)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Settings", "Home");
            }

            using (StormChaseCheckInEntities db = new StormChaseCheckInEntities())
            {
                Setting setting = new Setting();
                setting = db.Settings.Where(x => x.SettingsTypeID == (int)Enums.SettingsType.Timezone).FirstOrDefault();

                if (setting != null)
                {
                    setting.SettingsValue = decimal.Parse(form.SelectedTimezone);

                    db.SaveChanges();
                    Session[Functions.Settings.Session.TIMEZONE] = setting.SettingsValue;

                    return RedirectToAction("CheckMeIn", "Home");
                }

                return RedirectToAction("Settings", "Home");
            }
        }
    }
}