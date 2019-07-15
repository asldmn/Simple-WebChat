using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Simple_WebChat.Models.Ctx;
using Simple_WebChat.Controllers;
namespace Simple_WebChat
{
    public class ChatHub : Hub
    {
        ChatDBContext DB = new ChatDBContext();
        HomeController home = new HomeController();
        public void Control(string name)
        {
            if (name != DB.User.Where(k => k.UserName == name).FirstOrDefault().UserName.ToString())
            {
                home.Login();
            }
        }

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            int userId = 0;

            //if (name != DB.User.Where(k => k.UserName == name).FirstOrDefault().UserName.ToString())
            //{
            //    home.Login();
            //}
            //else
            //{
                userId = DB.User.Where(k => k.UserName == name).FirstOrDefault().UserId;
                DB.Mesaj.Add(new Models.Mdl.Mesajlar
                {
                    Mesaj = message,
                    UserId = userId
                });

                DB.SaveChanges();
            //}



            Clients.All.addNewMessageToPage(name, message);
        }
    }
}