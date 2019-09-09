using EventsNearMe.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsNearMe.Security
{
    public class AppUserManager : UserManager<EventOrganizer>
    {
        public AppUserManager(IUserStore<EventOrganizer> store) : base(store)
        {
        }

        public static AppUserManager Create(
            IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var manager = new AppUserManager(
                new UserStore<EventOrganizer>(context.Get<EventContext>()));
            return manager;
        }
    }
}