using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PartyInvites.Models
{
    /// <summary>
    /// 域类，负责存储、验证并确认RSVP
    /// </summary>
    public class GusetResponse
    {
        [Required(ErrorMessage = "Plaease enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your Phone number")]
        public string Phone { get; set; }
        
        // 说明：bool? 表明属性 WillAttend 是一个 nullable 的（可空的）bool型，其值可以是 true、false 或 null。
        // 这样设置的目的是为了可以运用 Required 验证特性。如果是一个常规的 bool 型，就无法判断用户是否已经选择了一个值。
        [Required(ErrorMessage = "Please specify whether you'll attend")]
        public bool? WillAttend { get; set; }
    }
}