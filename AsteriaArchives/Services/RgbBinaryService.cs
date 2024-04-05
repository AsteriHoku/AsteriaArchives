using System.Drawing;
using System.Text;

namespace AsteriaArchives.Services;

public class RgbBinaryService {
    public async Task<byte[]> Create(string inputString)
    {
        byte[] binaryData = Encoding.ASCII.GetBytes(inputString);
        var sb = new StringBuilder();

        foreach (byte data in binaryData) {
            sb.Append(Convert.ToString(data, 2).PadLeft(8, '0'));
        }

        var binaryString = sb.ToString();

        // Output plain text binary string to console
        // Console.WriteLine(binaryString);

        int maxWidth = 1920; // Image width in pixels
        int lineHeight = 20; // Image height in pixels

        var image = new Bitmap(maxWidth, 1080);
        using var graphics = Graphics.FromImage(image);
        
        // Set the background color (optional)
        graphics.Clear(Color.Black);

        // Define styles for individual characters
        Font regularFont = new Font("Monospace", 16, FontStyle.Regular);
        Font boldFont = new Font("Monospace", 18, FontStyle.Bold);
        Font italicFont = new Font("Monospace", 22, FontStyle.Italic);

        int x = 0;
        int y = 0;
        int indexEnd = 108;
        int increment = indexEnd + 1;
        int rgb = 0;

        for (int i = 0; i < binaryString.Length; i++)
        {
            var colorMap = new Dictionary<int, (Brush, Font)>
            {
                { 0, (Brushes.DarkRed, boldFont) },
                { 1, (Brushes.OrangeRed, italicFont) },
                { 2, (Brushes.Yellow, regularFont) },
                { 3, (Brushes.Green, boldFont) },
                { 4, (Brushes.Blue, italicFont) },
                { 5, (Brushes.DarkBlue, regularFont) },
                { 6, (Brushes.BlueViolet, boldFont) },
                { 7, (Brushes.DarkMagenta, italicFont) }
            };

            if (colorMap.TryGetValue(rgb, out var brushFontPair)) {
                graphics.DrawString(binaryString[i].ToString(), brushFontPair.Item2, brushFontPair.Item1, x, y);
            } else {
                graphics.DrawString(binaryString[i].ToString(), regularFont, Brushes.Blue, x, y);
            }

            // switch (rgb)
            // {
            //     case 0:
            //         graphics.DrawString(binaryString[i].ToString(), regularFont, Brushes.DarkRed, x, y);
            //         break;
            //     case 1:
            //         graphics.DrawString(binaryString[i].ToString(), regularFont, Brushes.OrangeRed, x, y);
            //         break;
            //     case 2:
            //         graphics.DrawString(binaryString[i].ToString(), regularFont, Brushes.Yellow, x, y);
            //         break;
            //     case 3:
            //         graphics.DrawString(binaryString[i].ToString(), regularFont, Brushes.Green, x, y);
            //         break;
            //     case 4:
            //         graphics.DrawString(binaryString[i].ToString(), regularFont, Brushes.Blue, x, y);
            //         break;
            //     case 5:
            //         graphics.DrawString(binaryString[i].ToString(), regularFont, Brushes.DarkBlue, x, y);
            //         break;
            //     case 6:
            //         graphics.DrawString(binaryString[i].ToString(), regularFont, Brushes.BlueViolet, x, y);
            //         break;
            //     case 7:
            //         graphics.DrawString(binaryString[i].ToString(), regularFont, Brushes.DarkMagenta, x, y);
            //         break;
            // }

            // if (rgb == 0)
            //     graphics.DrawString(binaryString[i].ToString(), boldFont, Brushes.DarkRed, x, y);
            // else if (rgb == 1)
            //     graphics.DrawString(binaryString[i].ToString(), boldFont, Brushes.OrangeRed, x, y);
            // else if (rgb == 2)
            //     graphics.DrawString(binaryString[i].ToString(), boldFont, Brushes.Yellow, x, y);
            // else if (rgb == 3)
            //     graphics.DrawString(binaryString[i].ToString(), boldFont, Brushes.Green, x, y);
            // else if (rgb == 4)
            //     graphics.DrawString(binaryString[i].ToString(), boldFont, Brushes.Blue, x, y);
            // else if (rgb == 5)
            //     graphics.DrawString(binaryString[i].ToString(), boldFont, Brushes.DarkBlue, x, y);
            // else if (rgb == 6)
            //     graphics.DrawString(binaryString[i].ToString(), boldFont, Brushes.RoyalBlue, x, y);
            // else if (rgb == 7)
            //     graphics.DrawString(binaryString[i].ToString(), boldFont, Brushes.SlateBlue, x, y);
            // else if (rgb == 8)
            //     graphics.DrawString(binaryString[i].ToString(), boldFont, Brushes.MediumSlateBlue, x, y);
            // else if (rgb == 9)
            //     graphics.DrawString(binaryString[i].ToString(), boldFont, Brushes.BlueViolet, x, y);
            // else if (rgb == 10)
            //     graphics.DrawString(binaryString[i].ToString(), boldFont, Brushes.HotPink, x, y);
            // else if (rgb == 11)
            //     graphics.DrawString(binaryString[i].ToString(), boldFont, Brushes.Magenta, x, y);
            // else if (rgb == 12)
            //     graphics.DrawString(binaryString[i].ToString(), boldFont, Brushes.DeepPink, x, y);
            // else if (rgb == 13)
            //     graphics.DrawString(binaryString[i].ToString(), boldFont, Brushes.DarkMagenta, x, y);

            // if (rgb == 0)
            //     graphics.DrawString(binaryString[i].ToString(), regularFont, Brushes.MediumBlue, x, y);
            // else if (rgb == 1)
            //     graphics.DrawString(binaryString[i].ToString(), regularFont, Brushes.DodgerBlue, x, y);
            // else if (rgb == 2)
            //     graphics.DrawString(binaryString[i].ToString(), regularFont, Brushes.DarkCyan, x, y);
            // else if (rgb == 3)
            //     graphics.DrawString(binaryString[i].ToString(), regularFont, Brushes.Blue, x, y);
            // else if (rgb == 4)
            //     graphics.DrawString(binaryString[i].ToString(), regularFont, Brushes.DeepSkyBlue, x, y);
            // else if (rgb == 5)
            //     graphics.DrawString(binaryString[i].ToString(), regularFont, Brushes.Aqua, x, y);

            rgb++;

            if (rgb == 8) rgb = 0;

            // Update the x-coordinate for the next character
            x += (int)graphics.MeasureString(binaryString[i].ToString(), regularFont).Width;

            // Wrap to new line
            if (i == indexEnd)
            {
                indexEnd += increment;
                x = 0;
                y += lineHeight;
            }
        }

        byte[] imageBytes;
        using var ms = new MemoryStream();
        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        imageBytes = ms.ToArray();

        return imageBytes;
        // // Save image
        // string outputPath = ".png";
        // image.Save(outputPath, ImageFormat.Png);
        //
        // // Dispose of the image and graphics objects to release resources
        // image.Dispose();
    }
}