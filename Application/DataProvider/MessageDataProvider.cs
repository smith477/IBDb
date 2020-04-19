using System;
using System.Collections.Generic;
using Application.Domain.Message;
using Application.Persistence;
using Cassandra;

namespace Application.DataProvider
{
    public class MessageDataProvider
    {
        public static List<Message> GetMessagesByChatID(string chatID)
        {
            ISession session = SessionManager.GetSession();
            List<Message> messages = new List<Message>();

            if (session == null)
                return null;

            var messagesData = session.Execute("select * from Message where chatID=" + new Guid(chatID) + ";");

            foreach (var messageData in messagesData)
            {
                Message message = new Message();
                message.Id = messageData["id"] != null ? messageData["id"].ToString() : string.Empty;
                message.SenderId = messageData["senderID"] != null ? messageData["senderID"].ToString() : string.Empty;
                message.DateOfMessage = messageData["dateOfMessage"] != null ? messageData["dateOfMessage"].ToString() : null;
                message.MessageText = messageData["messageText"] != null ? messageData["messageText"].ToString() : string.Empty;
                message.ChatId = messageData["chatID"] != null ? messageData["chatID"].ToString() : string.Empty;
                messages.Add(message);
            }

            return messages;
        }

        public static List<Message> GetMessagesByDateTimeAndChatID(Message msg)
        {
            ISession session = SessionManager.GetSession();
            List<Message> messages = new List<Message>();

            if (session == null)
                return null;

            var messagesData = session.Execute("select * from Message where dateOfMessage <= '" + msg.DateOfMessage + "' and chatID = '" + new Guid(msg.ChatId) + "' limit 10 allow filtering;");

            foreach (var messageData in messagesData)
            {
                Message message = new Message();
                message.Id = messageData["id"] != null ? messageData["id"].ToString() : string.Empty;
                message.SenderId = messageData["senderID"] != null ? messageData["senderID"].ToString() : string.Empty;
                message.DateOfMessage = messageData["dateOfMessage"] != null ? messageData["dateOfMessage"].ToString() : null;
                message.MessageText = messageData["messageText"] != null ? messageData["messageText"].ToString() : string.Empty;
                message.ChatId = messageData["chatID"] != null ? messageData["chatID"].ToString() : string.Empty;
                messages.Add(message);
            }

            return messages;
        }

        public static void AddMessage(Message msg)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet messageData = session.Execute("insert into Message (id, messageText, dateOfMessage, senderID, chatID) values (uuid(), '" + msg.MessageText + "', dateof(now()), " + new Guid(msg.SenderId) + ", " + new Guid(msg.ChatId) + ");");
        }

        public static void ChangeMessageText(Message msg)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet messageData = session.Execute("update Message set messageText='" + msg.MessageText + "' where id=" + new Guid(msg.Id) + " ;");
        }

        public static void DeleteMessageByID(string messageID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet messageDatas = session.Execute("delete from Message where id=" + new Guid(messageID) + " ;");
        }
    }
}
