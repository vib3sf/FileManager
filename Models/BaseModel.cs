﻿using System;

namespace FileManager.Models;

[Serializable]
public abstract class BaseModel
{
    
    public string Name { get; set; }
    public string FullPath { get; set; }

    protected BaseModel(string name, string fullPath)
    {
        Name = name;
        FullPath = fullPath;
    }

    protected BaseModel()
    {
        
    }

    public override string ToString()
    {
        return Name;
    }
}