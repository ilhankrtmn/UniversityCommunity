﻿using UniversityCommunity.Data.EntityFramework.Entities;

namespace UniversityCommunity.Data.Models.PageModel
{
    public class AnnouncementforListPage
    {
        public List<Announcement> Announcements { get; set; }
    }

    public class AnnouncementforPage
    {
        public Announcement Announcement { get; set; }
    }
}
