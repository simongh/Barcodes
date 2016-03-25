using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace BarcodeWriterTests.Helpers
{
    public static class ImageHelper
    {
        public static bool AreEquals(this Image self, Image target)
        {
            var img1 = self as Bitmap;
            var img2 = target as Bitmap;

            //高さが違えばfalse
            if (img1.Width != img2.Width || img1.Height != img2.Height) return false;
            //BitmapData取得
            var bd1 = img1.LockBits(new Rectangle(0, 0, img1.Width, img1.Height), ImageLockMode.ReadOnly, img1.PixelFormat);
            var bd2 = img2.LockBits(new Rectangle(0, 0, img2.Width, img2.Height), ImageLockMode.ReadOnly, img2.PixelFormat);
            try
            {
                //スキャン幅が違う場合はfalse
                if (bd1.Stride != bd2.Stride)
                    return false;

                int bsize = bd1.Stride * img1.Height;
                var byte1 = new byte[bsize];
                var byte2 = new byte[bsize];
                //バイト配列にコピー
                Marshal.Copy(bd1.Scan0, byte1, 0, bsize);
                Marshal.Copy(bd2.Scan0, byte2, 0, bsize);

                //MD5ハッシュを取る
                var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                var hash1 = md5.ComputeHash(byte1);
                var hash2 = md5.ComputeHash(byte2);

                //ハッシュを比較
                return hash1.SequenceEqual(hash2);
            }
            finally
            {
                //ロックを解除
                img1.UnlockBits(bd1);
                img2.UnlockBits(bd2);
            }
        }


        public static byte[] ComputeHash(this Image self)
        {
            var img1 = self as Bitmap;

            //BitmapData取得
            var bd1 = img1.LockBits(new Rectangle(0, 0, img1.Width, img1.Height), ImageLockMode.ReadOnly, img1.PixelFormat);
            try
            {

                int bsize = bd1.Stride * img1.Height;
                var byte1 = new byte[bsize];

                //バイト配列にコピー
                Marshal.Copy(bd1.Scan0, byte1, 0, bsize);

                //MD5ハッシュを取る
                //var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                using (var sum = new System.Security.Cryptography.SHA256CryptoServiceProvider())
                    return sum.ComputeHash(byte1);

            }
            finally
            {
                //ロックを解除
                img1.UnlockBits(bd1);
            }
        }
    }
}
