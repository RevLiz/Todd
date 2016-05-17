using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;
using Newtonsoft.Json;

namespace Todd
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            

            if (message.Text.ToLower().Contains("hello") || message.Text.ToLower().Contains("hi"))
            {
                return message.CreateReplyMessage("Hello to you too, I'm Todd. What is your name?");
            }
            else if (message.Text.ToLower().Contains("what time is it") || message.Text.ToLower().Contains("what time it is"))
            {
                return message.CreateReplyMessage("The time is " + string.Format("{0:HH:mm:ss tt}", DateTime.Now));
            }
            else if (message.Text.ToLower().Contains("what date is it") || message.Text.ToLower().Contains("what date it is"))
            {
                return message.CreateReplyMessage("Today is " + DateTime.Now.ToString("yyyy-MM-dd"));
            }
            else if (message.Text.ToLower().Contains("what is my timezone"))
            {
                return message.CreateReplyMessage("Your timezone is " + TimeZone.CurrentTimeZone);
            }
            else
            {
                return HandleSystemMessage(message);
            }
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
                return message.CreateReplyMessage($"Oh no, a rival");
            } 
            else if (message.Type == "BotRemovedFromConversation")
            {
                return message.CreateReplyMessage($"YOU KILLED HIM!!! You monster");
            }
            else if (message.Type == "UserAddedToConversation")
            {
                return message.CreateReplyMessage("Welcome to the chatroom");
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
                return message.CreateReplyMessage($"Talk to you later, see you");
            }

            return null;
        }
    }
}