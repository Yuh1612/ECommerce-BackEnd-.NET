using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.ViewModels
{
    public class ErrorViewModel
    {
        public string? Message { get; set; }
        public string? Detail { get; set; }

        public ErrorViewModel()
        {
        }

        public ErrorViewModel(string message)
        {
            Message = message;
        }

        public ErrorViewModel(string message, string detail) : this(message)
        {
            Detail = detail;
        }
    }
}