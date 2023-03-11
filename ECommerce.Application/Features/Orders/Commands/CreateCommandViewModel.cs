using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Orders.Commands
{
    public class CreateCommandViewModel
    {
        public int Id { get; set; }
        public string? OrderNo { get; set; }
        public string? UserId { get; set; }
    }
}
