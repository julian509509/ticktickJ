﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Camera
{
    public Vector3 Position { get; set; }

    public Camera(Vector3 position)
    {
        this.Position = position;
    }

}
