﻿using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readdit.Domain.Interfaces
{
    public interface ISubReadditRepository
    {
        Task<IReadOnlyCollection<SubReaddit>> Search(string search);
        Task<IReadOnlyCollection<SubReaddit>> GetPopular();
        Task<SubReaddit> Add(SubReaddit subReaddit);
        Task<SubReaddit> GetByName(string name);
        Task<bool> Exists(string name);
    }
}
