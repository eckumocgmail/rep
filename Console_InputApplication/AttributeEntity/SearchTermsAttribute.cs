﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

 
public class SearchTermsAttribute: ModelCreatingAttribute
{

    public SearchTermsAttribute(string terms) : base()
    {
            
    }
}
 