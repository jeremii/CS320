﻿using System;
using SMP.DAL.Repos.Base;
using SMP.Models.Entities;
using SMP.Models.ViewModels;
using System.Collections.Generic;

namespace SMP.DAL.Repos.Interfaces
{
    public interface IRssRepo : IRepo<Rss>
    {
        IEnumerable<Rss> GetRssOfUser(string userid);
    }
}
