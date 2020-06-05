using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile
{
    public string Name;
    public DateTime AttemptDate;
    public int Wave;

    public SaveFile(string name, DateTime attemptDate, int wave)
    {
        Name = name;
        AttemptDate = attemptDate;
        Wave = wave;
    }
}
