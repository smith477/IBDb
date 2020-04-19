using System;
using System.Collections.Generic;
using System.Linq;
using Application.Persistence;
using Cassandra;
using Application.Domain.Chat;
using Application.Domain.User;

namespace Application.DataProvider
{
    public class GroupChatDataProvider
    {
        public static List<GroupChat> GetGroupChats()
        {
            ISession session = SessionManager.GetSession();
            List<GroupChat> groupChats = new List<GroupChat>();

            if (session == null)
                return null;

            var groupChatsData = session.Execute("select * from groupchat");

            foreach (var groupChatData in groupChatsData)
            {
                GroupChat groupChat = new GroupChat();
                groupChat.Id = groupChatData["id"] != null ? groupChatData["id"].ToString() : string.Empty;
                groupChat.Name = groupChatData["name"] != null ? groupChatData["name"].ToString() : string.Empty;
                groupChat.Color = groupChatData["color"] != null ? groupChatData["color"].ToString() : string.Empty;
                groupChat.AdminId = groupChatData["adminid"] != null ? groupChatData["adminid"].ToString() : string.Empty;
                groupChat.UserList.Add(groupChatData["userlist"] != null ? groupChatData["userlist"].ToString() : string.Empty);
                if (!groupChat.UserList.Any())
                    foreach (var item in groupChat.UserList)
                    {
                        groupChat.UserList.Add(item.ToString());
                    }
                else
                    groupChat.UserList.Add(string.Empty);
                groupChats.Add(groupChat);
            }

            return groupChats;
        }
        public static List<GroupChat> GetGroupChatsByUserID(string userID)
        {
            ISession session = SessionManager.GetSession();
            List<GroupChat> groupChats = new List<GroupChat>();

            if (session == null)
                return null;

            //U CQL POSLE CREATE TABLE GROUPCHAT DODATI : " CREATE INDEX ON groupChats (userList); " 
            //NECE RADITI BEZ SEKUNDARNOG INDEXA KOJI SE KREIRA OVAKO IZNAD OVE LINIJE
            //ISPOD OVE LINIJE U UPITU PROBATI SA I BEZ APOSTROFA OKO GUID-ID USER-a I PROBATI SA I BEZ ALLOW FILTERING
            var groupChatsData = session.Execute("select * from groupChat where userList contains " + new Guid(userID) + " allow filtering;");

            foreach (var groupChatData in groupChatsData)
            {
                GroupChat groupChat = new GroupChat();
                groupChat.Id = groupChatData["id"] != null ? groupChatData["id"].ToString() : string.Empty;
                groupChat.Name = groupChatData["name"] != null ? groupChatData["name"].ToString() : string.Empty;
                groupChat.Color = groupChatData["color"] != null ? groupChatData["color"].ToString() : string.Empty;
                groupChat.AdminId = groupChatData["adminid"] != null ? groupChatData["adminid"].ToString() : string.Empty;
                groupChat.UserList.Add(groupChatData["userlist"] != null ? groupChatData["userlist"].ToString() : string.Empty);
                if (!groupChat.UserList.Any())
                    foreach (var item in groupChat.UserList)
                    {
                        groupChat.UserList.Add(item.ToString());
                    }
                else
                    groupChat.UserList.Add(string.Empty);
                groupChats.Add(groupChat);
            }

            return groupChats;  
        }

        public static GroupChat GetGroupChatByID(string groupChatID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return null;

            GroupChat groupChat = new GroupChat();

            Row groupChatData = session.Execute("select * from groupchat where id=" + new Guid(groupChatID) + " ;").FirstOrDefault();

            groupChat.Id = groupChatData["id"] != null ? groupChatData["id"].ToString() : string.Empty;
            groupChat.Name = groupChatData["name"] != null ? groupChatData["name"].ToString() : string.Empty;
            groupChat.Color = groupChatData["color"] != null ? groupChatData["color"].ToString() : string.Empty;
            groupChat.AdminId = groupChatData["adminid"] != null ? groupChatData["adminid"].ToString() : string.Empty;
            groupChat.UserList.Add(groupChatData["userlist"] != null ? groupChatData["userlist"].ToString() : string.Empty);
            if (!groupChat.UserList.Any())
                foreach (var item in groupChat.UserList)
                {
                    groupChat.UserList.Add(item.ToString());
                }
            else
                groupChat.UserList.Add(string.Empty);

            return groupChat;
        }   

        public static void AddGroupChat(GroupChat groupChat, User user)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet groupChatData = session.Execute("insert into groupchat (id, name, color, userList, adminID) values " +
                "(uuid(), '" + groupChat.Name + "', '" + groupChat.Color + "', [" + new Guid(user.Id) + "], " + new Guid(user.Id) + ");");
        }

        public static void AddGroupChatUser(GroupChat groupChat, User user)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet groupChatData = session.Execute("update groupchat set userList  = userList  + [" + new Guid(user.Id) + "] where id=" + new Guid(groupChat.Id) + " ;");
        }

        public static void RemoveGroupChatUserID(GroupChat groupChat, User user)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet groupChatData = session.Execute("update groupChat set userList  = userList  - [" + user.Id + "] where id=" + new Guid(groupChat.Id) + " ;");
        }

        public static void DeleteGroupChat(string groupChatID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet groupChatData = session.Execute("delete from groupChat where id=" + new Guid(groupChatID) + " ;");
        }
    }
}
