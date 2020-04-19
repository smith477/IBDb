using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Application.Persistence;
using Cassandra;
using Application.Domain.Chat;

namespace Application.DataProvider
{
    public class ChatDataProvider
    {
        public static List<Chat> GetAllChats() 
        {
            ISession session = SessionManager.GetSession();
            List<Chat> chats = new List<Chat>();

            if (session == null)
                return null;

            var chatsData = session.Execute("select * from chat");
            foreach(var chatData in chatsData)
            {
                Chat chat = new Chat();
                chat.Id = chatData["id"] != null ? chatData["id"].ToString() : string.Empty;
                chat.Name = chatData["name"] != null ? chatData["name"].ToString() : string.Empty;
                chat.Color = chatData["color"] != null ? chatData["color"].ToString() : string.Empty;
                chat.IdFirstUser = chatData["idfirstuser"] != null ? chatData["idfirstuser"].ToString() : null;
                chat.IdSecondUser = chatData["idseconduser"] != null ? chatData["idseconduser"].ToString() : string.Empty;
                chats.Add(chat);
            }

            return chats;
        }
        public static List<Chat> GetChatsByUserID(string userID)
        {
            ISession session = SessionManager.GetSession();
            List<Chat> chats = new List<Chat>();

            if (session == null)
                return null;

            var chatsData1 = session.Execute("select * from chat where idfirstuser=" + new Guid(userID) + " allow filtering;");
            foreach (var chatData in chatsData1)
            {
                Chat chat = new Chat();
                chat.Id = chatData["id"] != null ? chatData["id"].ToString() : string.Empty;
                chat.Name = chatData["name"] != null ? chatData["name"].ToString() : string.Empty;
                chat.IdFirstUser = chatData["idfirstuser"] != null ? chatData["idfirstuser"].ToString() : null;
                chat.IdSecondUser = chatData["idseconduser"] != null ? chatData["idseconduser"].ToString() : string.Empty;
                chats.Add(chat);
            }

            var chatsData2 = session.Execute("select * from chat where idSecondUser=" + new Guid(userID) + " allow filtering;");
            foreach (var chatData in chatsData2)
            {
                Chat chat = new Chat();
                chat.Id = chatData["id"] != null ? chatData["id"].ToString() : string.Empty;
                chat.Name = chatData["name"] != null ? chatData["name"].ToString() : string.Empty;
                chat.IdFirstUser = chatData["idfirstuser"] != null ? chatData["idfirstuser"].ToString() : null;
                chat.IdSecondUser = chatData["idseconduser"] != null ? chatData["idseconduser"].ToString() : string.Empty;
                chats.Add(chat);
            }

            return chats;
        }

        public static Chat GetChatByID(string chatID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return null;

            Chat chat = new Chat();

            Row chatData = session.Execute("select * from chat where id=" + new Guid(chatID) + " ;").FirstOrDefault();

            chat.Id = chatData["id"] != null ? chatData["id"].ToString() : string.Empty;
            chat.Name = chatData["name"] != null ? chatData["name"].ToString() : string.Empty;
            chat.IdFirstUser = chatData["idfirstuser"] != null ? chatData["idfirstuser"].ToString() : null;
            chat.IdSecondUser = chatData["idseconduser"] != null ? chatData["idseconduser"].ToString() : string.Empty;

            return chat;
        }

        public static void AddChat(Chat chat)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet chatData = session.Execute("insert into chat (id, name, color, idfirstuser , idseconduser) values (uuid(), '" + chat.Name + "', '#0000ff', " + new Guid(chat.IdFirstUser) + ", " + new Guid(chat.IdSecondUser) + ");");
        }

        public static void DeleteChatByID(string chatID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet chatData = session.Execute("delete from chat where id=" + new Guid(chatID) + " ;");
        }
    }
}
