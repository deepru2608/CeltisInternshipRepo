﻿using System.Collections.Generic;
using System.Threading.Tasks;
using IntershipTaskFront.Models;

namespace IntershipTaskFront.Services
{
    public interface ICenterService
    {
        Task<IEnumerable<Item>> GetItems();
    }
}