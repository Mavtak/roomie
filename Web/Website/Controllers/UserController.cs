using System;
using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    public class UserController : RoomieBaseController
    {
        private static OpenIdRelyingParty openId = new OpenIdRelyingParty();

        public ActionResult SignOut()
        {
            UserUtilities.SignOff();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignIn(string returnUrl)
        {
            // Stage 1: display login form to user
            ViewBag.ReturnUrl = returnUrl;
            return View("SignIn");
        }

        [ValidateInput(false)]
        public ActionResult Authenticate(string returnUrl)
        {
            var response = openId.GetResponse();
            if (response == null)
            {
                // Stage 2: user submitting Identifier
                Identifier id;
                if (Identifier.TryParse(Request.Form["openid_identifier"], out id))
                {
                    try
                    {
                        return openId.CreateRequest(Request.Form["openid_identifier"]).RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException ex)
                    {
                        ViewData["Message"] = ex.Message;
                        return View("SignIn");
                    }
                }
                else
                {
                    ViewData["Message"] = "Invalid identifier";
                    return View("SignIn");
                }
            }
            else
            {
                // Stage 3: OpenID Provider sending assertion response
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:

                        UserUtilities.SignIn(Database, response.ClaimedIdentifier);

                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            return RedirectToAction("Index", "Home");
                        }
                    case AuthenticationStatus.Canceled:
                        ViewData["Message"] = "Canceled at provider";
                        return View("SignIn");
                    case AuthenticationStatus.Failed:
                        ViewData["Message"] = response.Exception.Message;
                        return View("SignIn");
                }
            }
            return new EmptyResult();
        }

        [WebsiteRestrictedAccess]
        public ActionResult Edit()
        {
            return View(User);
        }

        
        [HttpPost]
        [WebsiteRestrictedAccess]
        public ActionResult Edit(EntityFrameworkUserModel user)
        {
            this.User.Alias = user.Alias;
            Database.Users.Update(User);
            Database.SaveChanges();

            return View(User);
        }
    }
}
