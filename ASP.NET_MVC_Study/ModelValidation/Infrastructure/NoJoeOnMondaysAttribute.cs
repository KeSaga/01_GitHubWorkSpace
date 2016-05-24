using System;
using System.ComponentModel.DataAnnotations;
using ModelValidation.Models;

namespace ModelValidation.Infrastructure
{
    public class NoJoeOnMondaysAttribute : ValidationAttribute
    {
        public NoJoeOnMondaysAttribute()
        {
            ErrorMessage = "Joe cannot book appointment on Mondays";
        }

        public override bool IsValid(object value)
        {
            Appointment app = value as Appointment;
            if (app == null
                || string.IsNullOrEmpty(app.ClinetName)
                || app.Date == null)
            {
                // 还没有正确的类型的模型要验证，或者还没有所需要的 ClientName 和 Date 属性的值
                return true;
            }
            else
            {
                return !(app.ClinetName == "Joe" && app.Date.DayOfWeek == DayOfWeek.Monday);
            }
        }

    }
}