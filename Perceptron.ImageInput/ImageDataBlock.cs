﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.ImageInput
{
    public struct DataBlockCommonInfo
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
    public struct ImageDataBlock
    {
        byte[] _image;
        DataBlockCommonInfo _commonInfo;
        byte _label;
        public ImageDataBlock(byte[] image, DataBlockCommonInfo commonInfo, byte label)
        {
            _commonInfo = commonInfo;
            _image = image;
            _label = label;
        }

        public byte[] Image { get => _image;  }
        public byte Label { get => _label; }
        public DataBlockCommonInfo CommonInfo { get => _commonInfo;  }

        public byte GetPixelAt(int x,int y)
        {
            return _image[x + y * _commonInfo.Width];
        }
    }
}
