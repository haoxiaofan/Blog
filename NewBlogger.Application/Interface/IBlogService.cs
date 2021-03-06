﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewBlogger.Dto;

namespace NewBlogger.Application.Interface
{
    public interface IBlogService
    {
        IList<BlogDto> GetBlogs(Guid categoryId,Int32 pageIndex, Int32 pageSize, out Int32 totalCount);

        BlogDto GetBlog(Guid blogId);

        void AddViewCount(Guid blogId);

        void AddNewBlog(String title, String content, Guid categoryId, params Guid[] tagIds);

    }
}