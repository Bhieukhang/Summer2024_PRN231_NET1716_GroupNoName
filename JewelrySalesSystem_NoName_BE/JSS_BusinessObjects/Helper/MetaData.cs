﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSS_BusinessObjects;

public class MetaData
{
    public MetaData(int pageIndex, int pageSize, int totalItems)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalItems = totalItems;
    }

    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalItems { get; set; }


}

