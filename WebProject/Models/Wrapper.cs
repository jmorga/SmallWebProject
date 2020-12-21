using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Models
{
    public class Wrapper
    {
        public bool result { get; }
        public string data { get; }

        public Wrapper(bool result, string data)
        {
            this.result = result;
            this.data = data;
        }
    }
}