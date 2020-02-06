using System;
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
        int _label;
        public ImageDataBlock(byte[] image, DataBlockCommonInfo commonInfo, int label)
        {
            _commonInfo = commonInfo;
            _image = image;
            _label = label;
        }

        public byte[] Image { get => _image;  }
        public int Label { get => _label; }

        public byte GetPixelAt(int x,int y)
        {
            return _image[x + y * _commonInfo.Width];
        }
    }
}
