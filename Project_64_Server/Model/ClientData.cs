﻿using Project_64_Server.Object;
using System.Collections.ObjectModel;

namespace Project_64_Server.Model
{
    internal class ClientData
    {
        public string? FirstName { get; set; }
        public string? Password { get; set; }
        public bool IsLogin { get; set; }
        public bool Login { get; set; }
        public bool IsRegister { get; set; }
        public bool IsMessage { get; set; }
        public ChatMessage? Message { get; set; }
        public bool IsFriendChat { get; set; }
        public Chat? FriendChat { get; set; }
        public ClientData() { }
    }
}
