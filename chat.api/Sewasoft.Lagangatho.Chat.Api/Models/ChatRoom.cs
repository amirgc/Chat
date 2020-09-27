using System;
using System.ComponentModel.DataAnnotations;

namespace Sewasoft.Lagangatho.Chat.Api.Models
{
    public class ChatRoom
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}