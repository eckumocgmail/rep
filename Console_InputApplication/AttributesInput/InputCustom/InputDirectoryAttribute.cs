﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Label("Файл")]
public class InputDirectoryAttribute : BaseInputAttribute
{
    public InputDirectoryAttribute() : base(InputTypes.File) { }
    public InputDirectoryAttribute(string exts) : base(InputTypes.File)
    {

    }
    public override bool IsValidValue(object value)
    {
        throw new System.NotImplementedException();
    }

    public override string OnValidate(object model, string property, object value)
    {
        return "";
    }

    public override string OnGetMessage(object model, string property, object value)
    {
        throw new NotImplementedException();
    }
}
