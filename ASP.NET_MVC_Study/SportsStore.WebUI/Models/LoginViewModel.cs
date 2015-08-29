using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models
{
    // 该类也将使用视图模型，这样可以使将数据从控制器传递给视图与将数据从模型绑定器传递给动作方法的输入方式是一致的。
    // 而且，这么做也能让我们更方便的使用模板视图辅助器。
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}