using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime CurrentDateTime { get; }
    }
}
