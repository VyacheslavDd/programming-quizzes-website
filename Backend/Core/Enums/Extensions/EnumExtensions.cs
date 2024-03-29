﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.Constants;

namespace Core.Enums.Extensions
{
    public static class EnumExtensions
    {
        public static string? GetDisplayNameProperty(this Enum target)
        {
            var field = target.GetType().GetField(target.ToString());
            if (field == null) return SpecialConstants.UnknownValue;
            var attributes = field.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attributes.Length == 0) return SpecialConstants.UnknownValue;
            return ((DisplayAttribute)attributes[0]).Name;
        }
    }
}
